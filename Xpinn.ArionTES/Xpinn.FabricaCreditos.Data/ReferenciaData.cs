using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Referencias
    /// </summary>
    public class ReferenciaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Referencias
        /// </summary>
        public ReferenciaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Referencias de la base de datos
        /// </summary>
        /// <param name="pReferencia">Entidad Referencia</param>
        /// <returns>Entidad Referencia creada</returns>
        public Referencia CrearReferencia(Referencia pReferencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = param + "p_codreferencia";
                        pNUMERO_RADICACION.Value = 0;
                        pNUMERO_RADICACION.Direction = ParameterDirection.Output;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = param + "p_identificacion";
                        pIDENTIFICACION.Value = pReferencia.identificacion;

                        DbParameter pTIPO_REFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_REFERENCIA.ParameterName = param + "p_tipo_referencia";
                        pTIPO_REFERENCIA.Value = pReferencia.primer_apellido;

                        DbParameter pNOMBRES = cmdTransaccionFactory.CreateParameter();
                        pNOMBRES.ParameterName = param + "p_nombres";
                        pNOMBRES.Value = pReferencia.nombres;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = param + "p_direccion";
                        pDIRECCION.Value = pReferencia.nombres;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = param + "p_telefono";
                        pTELEFONO.Value = pReferencia.nombres;

                        DbParameter pCODUSUVERIFICA = cmdTransaccionFactory.CreateParameter();
                        pCODUSUVERIFICA.ParameterName = param + "p_codusuverifica";
                        pCODUSUVERIFICA.Value = pReferencia.referenciado;

                        DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCALIFICACION.ParameterName = param + "p_calificacion";
                        pCALIFICACION.Value = pReferencia.oficina;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = param + "p_observaciones";
                        pOBSERVACIONES.Value = pReferencia.tipo_referencia;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_REFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRES);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUVERIFICA);
                        cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_REF_VERIF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pReferencia, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pReferencia.numero_radicacion = Convert.ToInt64(pNUMERO_RADICACION.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "CrearReferencia", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla Referencias de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Referencia modificada</returns>
        public Referencia ModificarReferencia(Referencia pReferencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODREFERENCIA.ParameterName = "p_codreferencia";
                        pCODREFERENCIA.Value = pReferencia.cod_referencia;

                        DbParameter pNUMERORADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERORADICACION.ParameterName = "p_numero_radicacion";
                        pNUMERORADICACION.Value = pReferencia.numero_radicacion;

                        DbParameter pCODUSUVERIFICA = cmdTransaccionFactory.CreateParameter();
                        pCODUSUVERIFICA.ParameterName = "p_codusuverifica";
                        pCODUSUVERIFICA.Value = pReferencia.codigo_verificador;

                        DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCALIFICACION.ParameterName = "p_calificacion";
                        pCALIFICACION.Value = pReferencia.resultado;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_observaciones";
                        pOBSERVACIONES.Value = pReferencia.detalle;

                        DbParameter pCHECKNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pCHECKNOMBRE.ParameterName = "p_checknombre";
                        pCHECKNOMBRE.Value = pReferencia.check_nombre;

                        DbParameter pCHECKCEDULA = cmdTransaccionFactory.CreateParameter();
                        pCHECKCEDULA.ParameterName = "p_checkcedula";
                        pCHECKCEDULA.Value = pReferencia.check_cedula;

                        DbParameter pCHECKDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pCHECKDIRECCION.ParameterName = "p_checkdireccion";
                        pCHECKDIRECCION.Value = pReferencia.check_direccion;

                        DbParameter pCHECKPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCHECKPARENTESCO.ParameterName = "p_checkparentesco";
                        pCHECKPARENTESCO.Value = pReferencia.check_parentesco;

                        cmdTransaccionFactory.Parameters.Add(pCODREFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERORADICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUVERIFICA);
                        cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pCHECKNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pCHECKCEDULA);
                        cmdTransaccionFactory.Parameters.Add(pCHECKDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pCHECKPARENTESCO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_REF_VERIF_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pReferencia, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ModificarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Referencias de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Referencias</param>
        public void EliminarReferencia(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Referencia pReferencia = new Referencia();

                        if (pUsuario.programaGeneraLog)
                            pReferencia = ConsultarReferencia(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = param + "NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_FabricaCreditos_Referencias_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pReferencia, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "InsertarReferencia", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Referencias de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Referencias</param>
        /// <returns>Entidad Referencia consultado</returns>
        public Referencia ConsultarReferencia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Referencia entidad = new Referencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_REFERENCIAS WHERE CODREFERENCIA=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODREFERENCIA"] != DBNull.Value) entidad.cod_referencia = Convert.ToInt64(resultado["CODREFERENCIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["REFERENCIADO"] != DBNull.Value) entidad.referenciado = Convert.ToString(resultado["REFERENCIADO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.tipo_referencia = Convert.ToString(resultado["TIPOREFERENCIA"]);
                            if (resultado["VINCULO"] != DBNull.Value) entidad.cod_vinculo = Convert.ToInt64(resultado["VINCULO"]);
                            if (resultado["DESCVINCULO"] != DBNull.Value) entidad.vinculo = Convert.ToString(resultado["DESCVINCULO"]);
                            if (resultado["CEDULA"] != DBNull.Value) entidad.cedula_referenciado = Convert.ToInt64(resultado["CEDULA"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.nombre_referenciado = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion_referenciado = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.resultado = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["VERIFICADOR"] != DBNull.Value) entidad.nombre_verificador = Convert.ToString(resultado["VERIFICADOR"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["CHECKNOMBRE"] != DBNull.Value) entidad.check_nombre = Convert.ToInt16(resultado["CHECKNOMBRE"]);
                            if (resultado["CHECKCEDULA"] != DBNull.Value) entidad.check_cedula = Convert.ToInt16(resultado["CHECKCEDULA"]);
                            if (resultado["CHECKDIRECCION"] != DBNull.Value) entidad.check_direccion = Convert.ToInt16(resultado["CHECKDIRECCION"]);
                            if (resultado["CHECKPARENTESCO"] != DBNull.Value) entidad.check_parentesco = Convert.ToInt16(resultado["CHECKPARENTESCO"]);
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
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public void guardar(long CODREFERENCIA, int TIPO_REFERENCIACION, int NUM_PREGUNTA, int RESPPUESTA, long REFERENCIADOR, string NUMERO_RADICACION, long COD_PERSONA, string OBSERVACIONES, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"INSERT INTO VERIFICACION_REFERENCIAS(CODREFERENCIA, TIPO_REFERENCIACION, NUM_PREGUNTA, RESPPUESTA, NUMERO_RADICACION, REFERENCIADOR, COD_PERSONA, OBSERVACIONES)
                                        VALUES(" + CODREFERENCIA + "," + TIPO_REFERENCIACION + "," + NUM_PREGUNTA + "," + RESPPUESTA + "," + NUMERO_RADICACION + "," + REFERENCIADOR + "," + COD_PERSONA + ",'" + OBSERVACIONES + "')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        connection.Close();

                        DbParameter pReferencia = cmdTransaccionFactory.CreateParameter();
                        pReferencia.ParameterName = "pReferencia";
                        pReferencia.Value = CODREFERENCIA;

                        DbParameter pRadicacion = cmdTransaccionFactory.CreateParameter();
                        pRadicacion.ParameterName = "pRadicacion";
                        pRadicacion.Value = NUMERO_RADICACION;

                        cmdTransaccionFactory.Parameters.Add(pRadicacion);
                        cmdTransaccionFactory.Parameters.Add(pReferencia);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_VERIFICACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch
                    {
                    }
                }
            }
        }

        public void guardarpregunta(long CODREFERENCIA, int TIPO_REFERENCIACION, int NUM_PREGUNTA, int RESPPUESTA, long REFERENCIADOR, string NUMERO_RADICACION, long COD_PERSONA, string OBSERVACIONES, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "insert into respuestastiemporeferencias (CODREFERENCIA,RESPUESTA,NUMERO_RADICACION,COD_PERSONA)VALUES(" + CODREFERENCIA + ","  + RESPPUESTA + "," + NUMERO_RADICACION + "," + COD_PERSONA + "')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {
                    }
                }
            }
        }


        public void modificar( int NUM_PREGUNTA, int RESPPUESTA, string OBSERVACIONES,string NUMERO_RADICACION, int TIPO_REFERENCIACION, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "DELETE FROM verificacion_referencias WHERE Numero_Radicacion = " + NUMERO_RADICACION.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public Referencia ConsultarReferenciacionPersonas(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Referencia entidad = new Referencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        string sql = "select Decode(tiporeferencia, 1, 'Familiar', 2, 'Personal', 3, 'Comercial') as tiporeferencia,telefono,direccion,CELULAR,TELOFICINA from referencias where estado=0 and codreferencia = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.tipo_referencia = Convert.ToString(resultado["TIPOREFERENCIA"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono_referenciado = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion_referenciado = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.Celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["TELOFICINA"] != DBNull.Value) entidad.TELOFICINA = Convert.ToString(resultado["TELOFICINA"]);
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
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<Referencia> Listarrefereciados(string pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Referencia> lstReferencia = new List<Referencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "select nombres,codreferencia from referencias where estado=0 and cod_persona = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            Referencia entidad = new Referencia();

                            //Asociar todos los valores a la entidad
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["codreferencia"] != DBNull.Value) entidad.cod_referencia = Convert.ToInt64(resultado["codreferencia"]);
                            lstReferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencia;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<Referencia> Listardatosrefereciacion(string pId, Usuario pUsuario, int referencia)
        {
            
            DbDataReader resultado = default(DbDataReader);
            List<Referencia> lstpreguntas = new List<Referencia>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "select num_pregunta, resppuesta, observaciones from  VERIFICACION_REFERENCIAS where tipo_referenciacion = " + referencia + " and numero_radicacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            Referencia entidad = new Referencia();
                            if (resultado["NUM_PREGUNTA"] != DBNull.Value) entidad.numero_pregunta = Convert.ToInt64(resultado["NUM_PREGUNTA"]);
                            if (resultado["RESPPUESTA"] != DBNull.Value) entidad.numero_respuesta = Convert.ToInt64(resultado["RESPPUESTA"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            lstpreguntas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstpreguntas;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }

        }

        public Referencia ConsultarReferenciacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Referencia entidad = new Referencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_referenciacion WHERE NUMERO_RADICACION=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]) + " " + Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_Persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_Clasificacion = Convert.ToInt16(resultado["COD_CLASIFICA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToString(resultado["FECHA_SOLICITUD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono_referenciado = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion_referenciado = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]); else entidad.monto = 0;
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

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
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }


        public Referencia ConsultardatosReferenciacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Referencia entidad = new Referencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_datosconsultareferenciacion WHERE codpersona =" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TELEFONOPERSONA"] != DBNull.Value) entidad.telefonos = Convert.ToString(resultado["TELEFONOPERSONA"]);
                            if (resultado["DIRECCIONPERSONA"] != DBNull.Value) entidad.DIRECCION = Convert.ToString(resultado["DIRECCIONPERSONA"]); 
                            if (resultado["BARRIOPERSONA"] != DBNull.Value) entidad.BARRIO = Convert.ToString(resultado["BARRIOPERSONA"]); 
                            if (resultado["FECHANACIMIENTOPERSONA"] != DBNull.Value) entidad.FECHANACIMIENTO = Convert.ToDateTime(resultado["FECHANACIMIENTOPERSONA"]);
                            if (resultado["ESTADOCIVILPERSONA"] != DBNull.Value) entidad.ESTADO_CIVIL = Convert.ToString(resultado["ESTADOCIVILPERSONA"]); 
                            if (resultado["ACTIVIDADPERSONA"] != DBNull.Value) entidad.ACTIVIDAD_PERSONA = Convert.ToString(resultado["ACTIVIDADPERSONA"]); 
                            if (resultado["TIPOVIVIENDAPERSONA"] != DBNull.Value) entidad.TIPO_VIVIENDA = Convert.ToString(resultado["TIPOVIVIENDAPERSONA"]); 
                            if (resultado["NOMBREARRENDATARIOPERSONA"] != DBNull.Value) entidad.NOMBRE_ARRENDATARIO = Convert.ToString(resultado["NOMBREARRENDATARIOPERSONA"]); 
                            if (resultado["TELEFONOARRENDATARIO"] != DBNull.Value) entidad.TELEFONO_ARRENDATARIO = Convert.ToInt64(resultado["TELEFONOARRENDATARIO"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.VALOR_ARRIENDO = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["PERSONASACARGO"] != DBNull.Value) entidad.PERSONAS_A_CARGO = Convert.ToInt64(resultado["PERSONASACARGO"]);
                            if (resultado["DIRECCIONNEGOCIO"] != DBNull.Value) entidad.DIRECCION_NEGOCIO = Convert.ToString(resultado["DIRECCIONNEGOCIO"]); 
                            if (resultado["ANTIGUEDADNEGOCIOPERSONA"] != DBNull.Value) entidad.ANTIGUEDAD_NEGOCIO = Convert.ToString(resultado["ANTIGUEDADNEGOCIOPERSONA"]);
                            if (resultado["ACTIVIDADEMPRESAPERSONA"] != DBNull.Value) entidad.ACTIVIDAD_EMPRESA = Convert.ToString(resultado["ACTIVIDADEMPRESAPERSONA"]); 
                            if (resultado["NOMBREEMPRESAPERSONA"] != DBNull.Value) entidad.NOMBRE_EMPRESA = Convert.ToString(resultado["NOMBREEMPRESAPERSONA"]);
                            if (resultado["NITEMPRESAPERSONA"] != DBNull.Value) entidad.NIT_EMPRESA = Convert.ToString(resultado["NITEMPRESAPERSONA"]);
                            if (resultado["CARGOPERSONA"] != DBNull.Value) entidad.CARGO_PERONSA = Convert.ToString(resultado["CARGOPERSONA"]); 
                            if (resultado["SALARIOPERSONA"] != DBNull.Value) entidad.SALARIO_PERSONA = Convert.ToInt64(resultado["SALARIOPERSONA"]);
                            if (resultado["ANTIGUEDADEMPRESAPERSONA"] != DBNull.Value) entidad.ANTIGUEDAD_EMPRESA = Convert.ToInt64(resultado["ANTIGUEDADEMPRESAPERSONA"]);
                            if (resultado["DIRECCIONEMPRESAPERSONA"] != DBNull.Value) entidad.DIRECCION_EMPRESA_PERSONA = Convert.ToString(resultado["DIRECCIONEMPRESAPERSONA"]); 
                            if (resultado["IDENTIFICACIONCONYUGE"] != DBNull.Value) entidad.identificacion_conyge = Convert.ToInt64(resultado["IDENTIFICACIONCONYUGE"]);
                            if (resultado["NOMBRECONYUGE"] != DBNull.Value) resultado["NOMBRECONYUGE"].ToString();
                            entidad.nombres_conyuge = Convert.ToString(resultado["NOMBRECONYUGE"]);
                            if (resultado["TELEFONOCONYUGE"] != DBNull.Value) entidad.telefono_CONYUGE = Convert.ToString(resultado["TELEFONOCONYUGE"]);
                            if (resultado["DIRECCIONCONYUGE"] != DBNull.Value) entidad.DIRECCION_CONYUGE = Convert.ToString(resultado["DIRECCIONCONYUGE"]);
                            if (resultado["ACTIVIDADCONYUGE"] != DBNull.Value) entidad.ACTIVIDAD_CONYUGE = Convert.ToString(resultado["ACTIVIDADCONYUGE"]);
                            if (resultado["NOMBREEMPRESACONYUGE"] != DBNull.Value) entidad.NOMBRE_EMPRESA_CONYUGE = Convert.ToString(resultado["NOMBREEMPRESACONYUGE"]);
                            if (resultado["NITEMPRESACONYUGE"] != DBNull.Value) entidad.NIT_EMPRESA_CONYUGE = Convert.ToString(resultado["NITEMPRESACONYUGE"]);
                            if (resultado["ACTIVIDADEMPRESACONYUGE"] != DBNull.Value) entidad.ACTIVIDAD_EMPRESA_CONYUGE = Convert.ToString(resultado["ACTIVIDADEMPRESACONYUGE"]);
                            if (resultado["CARGOCONYUGE"] != DBNull.Value) entidad.CARGO_CONYUGE = Convert.ToString(resultado["CARGOCONYUGE"]);
                            if (resultado["SALARIOCONYUGE"] != DBNull.Value) entidad.SALARIO_CONYUGE = Convert.ToInt64(resultado["SALARIOCONYUGE"]);
                            if (resultado["ANTIGUEDADEMPRESACONYUGE"] != DBNull.Value) entidad.ACTIVIDAD_EMPRESA_CONYUGE = Convert.ToString(resultado["ANTIGUEDADEMPRESACONYUGE"]);

                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public Referencia ConsultarCorreo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Referencia entidad = new Referencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_email_asesores WHERE numero_radicacion =" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SEMAIL"] != DBNull.Value) entidad.correo = Convert.ToString(resultado["SEMAIL"]);                           
                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Referencias dados unos filtros
        /// </summary>
        /// <param name="pReferencias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referencia obtenidos</returns>
        public List<Referencia> ListarReferencia(Referencia pReferencia, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Referencia> lstReferencia = new List<Referencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "Select  r.codreferencia, c.numero_radicacion, p.identificacion, p.nombres, 'Deudor' As referenciado, (Select o.nombre From oficina o Where o.cod_oficina = c.cod_oficina) As Oficina, Decode(r.tiporeferencia, 1, 'Familiar', 2, 'Persona', 3, 'Comercial') as tiporeferencia, r.nombres As Referencia, Decode(r.estado, 0, 'N', 1, 'S', r.estado) As estado, p.primer_apellido, p.segundo_apellido, c.cod_linea_credito From v_persona p inner join credito c On p.cod_persona = c.cod_deudor inner join referencias r On p.cod_persona = r.cod_persona Where c.estado = 'S'" + ObtenerFiltro(pReferencia) + " Union All Select  r.codreferencia, c.numero_radicacion, p.identificacion, p.nombres, 'Codeudor' As referenciado, (Select o.nombre From oficina o Where o.cod_oficina = c.cod_oficina) As Oficina, Decode(r.tiporeferencia, 1, 'Familiar', 2, 'Persona', 3, 'Comercial') as tiporeferencia, r.nombres As Referencia, Decode(r.estado, 0, 'N', 1, 'S', r.estado) As estado, p.primer_apellido, p.segundo_apellido, c.cod_linea_credito From v_persona p inner join credito c On p.cod_persona = c.cod_deudor inner join codeudores co On co.numero_radicacion = c.numero_radicacion inner join referencias r On p.cod_persona = co.codpersona Where c.estado = 'S'" + ObtenerFiltro(pReferencia);
                        string sql = "Select * from v_referencias " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Referencia entidad = new Referencia();
                            if (resultado["CODREFERENCIA"] != DBNull.Value) entidad.cod_referencia = Convert.ToInt64(resultado["CODREFERENCIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["REFERENCIADO"] != DBNull.Value) entidad.referenciado = Convert.ToString(resultado["REFERENCIADO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.tipo_referencia = Convert.ToString(resultado["TIPOREFERENCIA"]);
                            if (resultado["VINCULO"] != DBNull.Value) entidad.cod_vinculo = Convert.ToInt64(resultado["VINCULO"]);
                            if (resultado["DESCVINCULO"] != DBNull.Value) entidad.vinculo = Convert.ToString(resultado["DESCVINCULO"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.nombre_referenciado = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.resultado = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["VERIFICADOR"] != DBNull.Value) entidad.nombre_verificador = Convert.ToString(resultado["VERIFICADOR"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["OBSERVACIONES"]);

                            lstReferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ListarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<Referencia> ListarReferenciacion(Referencia pReferencia, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Referencia> lstReferencia = new List<Referencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_referenciacion " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        Referencia entidad;

                        while (resultado.Read())
                        {
                            entidad = new Referencia();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]) + Convert.ToString(resultado["PRIMER_APELLIDO"]) + Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToString(resultado["FECHA_SOLICITUD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value)
                                entidad.monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            else entidad.monto = 0;
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);

                            lstReferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ListarReferencia", ex);
                        return null;
                    }
                }
            }
        }



        public List<Referencia> ObservacionReferenciacion(Referencia vReferencia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Referencia> lstReferencia = new List<Referencia>();
            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select tipo_referenciacion, nombre, num_pregunta, pregunta, alerta, observaciones from v_verificacion_referencias where  nombre is not null and  numero_radicacion =  " + vReferencia.numero_radicacion + " Order by 1, 2, 3";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Referencia entidad = new Referencia();
                            if (resultado["tipo_referenciacion"] != DBNull.Value) entidad.referenciado = Convert.ToString(resultado["tipo_referenciacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_referenciado = Convert.ToString(resultado["nombre"]);
                            if (resultado["pregunta"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["pregunta"]);
                            if (resultado["alerta"] != DBNull.Value) entidad.resultado = Convert.ToString(resultado["alerta"]);
                            if (resultado["observaciones"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["observaciones"]);
                            lstReferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ObservacionReferenciacion", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Método para consultar las referencias ingresadas a un crédito
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Referencia> ConsultarReferenciacionCredito(Int64 pnumero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Referencia> lstReferencias = new List<Referencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select r.codreferencia, Decode(r.tiporeferencia, 1, 'Familiar', 2, 'Personal', 3, 'Comercial') as tiporeferencia, r.nombres, p.descripcion As parentesco, r.direccion, r.telefono, r.teloficina, r.celular " +
                                     "From referencias r Left Join parentescos p On r.codparentesco = p.codparentesco where r.numero_radicacion = " + pnumero_radicacion.ToString() + " Order by r.codreferencia";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Referencia entidad = new Referencia();

                            if (resultado["CODREFERENCIA"] != DBNull.Value) entidad.cod_referencia = Convert.ToInt64(resultado["CODREFERENCIA"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.tipo_referencia = Convert.ToString(resultado["TIPOREFERENCIA"]);
                            if (resultado["PARENTESCO"] != DBNull.Value) entidad.vinculo = Convert.ToString(resultado["PARENTESCO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.DIRECCION = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefonos = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["TELOFICINA"] != DBNull.Value) entidad.TELOFICINA = Convert.ToString(resultado["TELOFICINA"]); 
                            if (resultado["CELULAR"] != DBNull.Value) entidad.Celular = Convert.ToString(resultado["CELULAR"]);

                            lstReferencias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferenciacionCredito", ex);
                        return null;
                    }
                }
            }
        }



    }
}