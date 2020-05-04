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
    /// Objeto de acceso a datos para la tabla Oficina
    /// </summary>
    public class OficinaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public OficinaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Oficina en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Oficina</param>
        /// <returns>Entidad creada</returns>
        public Oficina InsertarOficina(Oficina pEntidad, Usuario pUsuario)
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
                        pcod_oficina.Size = 8;
                        pcod_oficina.Direction = ParameterDirection.InputOutput;

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcodigoempresa";
                        pcod_empresa.Value = pEntidad.cod_empresa;
                        pcod_empresa.DbType = DbType.Int64;
                        pcod_empresa.Size = 8;
                        pcod_empresa.Direction = ParameterDirection.Input;

                        DbParameter pnom_oficina = cmdTransaccionFactory.CreateParameter();
                        pnom_oficina.ParameterName = "pnomoficina";
                        pnom_oficina.Value = pEntidad.nombre;
                        pnom_oficina.DbType = DbType.AnsiString;
                        pnom_oficina.Direction = ParameterDirection.Input;
                        pnom_oficina.Size = 50;

                        DbParameter pcod_ciudad = cmdTransaccionFactory.CreateParameter();
                        pcod_ciudad.ParameterName = "pcodigociudad";
                        pcod_ciudad.Value = pEntidad.cod_ciudad;
                        pcod_ciudad.DbType = DbType.Int64;
                        pcod_ciudad.Size = 8;
                        pcod_ciudad.Direction = ParameterDirection.Input;

                        DbParameter p_direccion = cmdTransaccionFactory.CreateParameter();
                        p_direccion.ParameterName = "pdireccion";
                        p_direccion.Value = pEntidad.direccion;
                        p_direccion.Size = 200;
                        p_direccion.DbType = DbType.AnsiString;
                        p_direccion.Direction = ParameterDirection.Input;

                        DbParameter p_telefono = cmdTransaccionFactory.CreateParameter();
                        p_telefono.ParameterName = "ptelefono";
                        p_telefono.Value = pEntidad.telefono;
                        p_telefono.DbType = DbType.AnsiString;
                        p_telefono.Size = 50;
                        p_telefono.Direction = ParameterDirection.Input;


                        DbParameter pcod_encargado = cmdTransaccionFactory.CreateParameter();
                        pcod_encargado.ParameterName = "pcodigoencargado";
                        pcod_encargado.Value = pEntidad.cod_persona;
                        pcod_encargado.DbType = DbType.Int64;
                        pcod_encargado.Size = 8;
                        pcod_encargado.Direction = ParameterDirection.Input;


                        DbParameter pcod_centrocosto = cmdTransaccionFactory.CreateParameter();
                        pcod_centrocosto.ParameterName = "pcodigocentrocosto";
                        pcod_centrocosto.Value = pEntidad.centro_costo;
                        pcod_centrocosto.DbType = DbType.Int64;
                        pcod_centrocosto.Size = 8;
                        pcod_centrocosto.Direction = ParameterDirection.Input;


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

                        DbParameter p_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta.ParameterName = "pcod_cuenta";
                        p_cod_cuenta.Value = pEntidad.cod_cuenta;
                        p_cod_cuenta.DbType = DbType.String;
                        p_estado.Direction = ParameterDirection.Input;

                        DbParameter p_sede_propia = cmdTransaccionFactory.CreateParameter();
                        p_sede_propia.ParameterName = "psede_propia";
                        p_sede_propia.Value = pEntidad.sede_propia;
                        p_sede_propia.DbType = DbType.Int32;
                        p_sede_propia.Direction = ParameterDirection.Input;

                        DbParameter p_indicador = cmdTransaccionFactory.CreateParameter();
                        p_indicador.ParameterName = "pindicador";
                        p_indicador.Value = pEntidad.indicador_corresponsal;
                        p_indicador.DbType = DbType.Int32;
                        p_indicador.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_negocio = cmdTransaccionFactory.CreateParameter();
                        p_tipo_negocio.ParameterName = "ptipo_negocio";
                        if (pEntidad.tipo_negocio != null) p_tipo_negocio.Value = pEntidad.tipo_negocio; else p_tipo_negocio.Value = DBNull.Value;
                        p_tipo_negocio.DbType = DbType.Int32;
                        p_tipo_negocio.Direction = ParameterDirection.Input;


                        DbParameter p_codsuper = cmdTransaccionFactory.CreateParameter();
                        p_codsuper.ParameterName = "pcod_super";
                        p_codsuper.Value = pEntidad.cod_super;
                        p_codsuper.DbType = DbType.Int32;
                        p_codsuper.Direction = ParameterDirection.Input;


                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);
                        cmdTransaccionFactory.Parameters.Add(pnom_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_ciudad);
                        cmdTransaccionFactory.Parameters.Add(p_direccion);
                        cmdTransaccionFactory.Parameters.Add(p_telefono);
                        cmdTransaccionFactory.Parameters.Add(pcod_encargado);
                        cmdTransaccionFactory.Parameters.Add(pcod_centrocosto);
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(p_sede_propia);
                        cmdTransaccionFactory.Parameters.Add(p_indicador);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_negocio);
                        cmdTransaccionFactory.Parameters.Add(p_codsuper);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OFICINAINSERTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OFICINA", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Crear Oficina");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_oficina), "OFICINA", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        // se coloca nuevo codigo que permite crear el procesoofocina incativo al menos debe existir como inactivo la primera vez
                        // esto es con el fin de que siempre alla un registro de apetura o de cierre
                        cmdTransaccionFactory.Parameters.Clear();

                        DbParameter pcod_oficinax = cmdTransaccionFactory.CreateParameter();
                        pcod_oficinax.ParameterName = "pcodigooficina";
                        pcod_oficinax.Value = pEntidad.cod_oficina;
                        pcod_oficinax.DbType = DbType.Int16;
                        pcod_oficinax.Size = 8;
                        pcod_oficinax.Direction = ParameterDirection.Input;

                        DbParameter pcod_encargadox = cmdTransaccionFactory.CreateParameter();
                        pcod_encargadox.ParameterName = "pcodigoencargado";
                        pcod_encargadox.Value = pUsuario.codusuario;
                        pcod_encargadox.Size = 8;
                        pcod_encargadox.DbType = DbType.Int16;
                        pcod_encargadox.Direction = ParameterDirection.Input;

                        DbParameter pfecha_procesox = cmdTransaccionFactory.CreateParameter();
                        pfecha_procesox.ParameterName = "pfechaproceso";
                        pfecha_procesox.Value = DateTime.Now;
                        pfecha_procesox.DbType = DbType.DateTime;
                        pfecha_procesox.Direction = ParameterDirection.Input;
                        pfecha_procesox.Size = 7;

                        DbParameter ptipo_proceso = cmdTransaccionFactory.CreateParameter();
                        ptipo_proceso.ParameterName = "ptipoproceso";
                        ptipo_proceso.Value = 1;
                        ptipo_proceso.DbType = DbType.Int16;
                        ptipo_proceso.Direction = ParameterDirection.Input;
                        ptipo_proceso.Size = 8;

                        DbParameter ptipo_horario = cmdTransaccionFactory.CreateParameter();
                        ptipo_horario.ParameterName = "ptipohorario";
                        ptipo_horario.Value = 1;
                        ptipo_horario.DbType = DbType.Int16;
                        ptipo_horario.Direction = ParameterDirection.Input;
                        ptipo_horario.Size = 8;

                        cmdTransaccionFactory.Parameters.Add(pcod_oficinax);
                        cmdTransaccionFactory.Parameters.Add(pcod_encargadox);
                        cmdTransaccionFactory.Parameters.Add(pfecha_procesox);
                        cmdTransaccionFactory.Parameters.Add(ptipo_proceso);
                        cmdTransaccionFactory.Parameters.Add(ptipo_horario);

                       // connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_PROCOFIINSERTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                       

                        pEntidad.cod_oficina = pcod_oficina.Value.ToString();

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "InsertarOficina", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad Oficina en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Oficina</param>
        /// <returns>Entidad modificada</returns>
        public Oficina ModificarOficina(Oficina pEntidad, Usuario pUsuario)
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
                        pcod_oficina.Size = 8;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcodigoempresa";
                        pcod_empresa.Value = pEntidad.cod_empresa;
                        pcod_empresa.DbType = DbType.Int64;
                        pcod_empresa.Size = 8;
                        pcod_empresa.Direction = ParameterDirection.Input;

                        DbParameter pnom_oficina = cmdTransaccionFactory.CreateParameter();
                        pnom_oficina.ParameterName = "pnomoficina";
                        pnom_oficina.Value = pEntidad.nombre;
                        pnom_oficina.DbType = DbType.AnsiString;
                        pnom_oficina.Direction = ParameterDirection.Input;
                        pnom_oficina.Size = 50;

                        DbParameter pcod_ciudad = cmdTransaccionFactory.CreateParameter();
                        pcod_ciudad.ParameterName = "pcodigociudad";
                        pcod_ciudad.Value = pEntidad.cod_ciudad;
                        pcod_ciudad.DbType = DbType.Int64;
                        pcod_ciudad.Size = 8;
                        pcod_ciudad.Direction = ParameterDirection.Input;

                        DbParameter p_direccion = cmdTransaccionFactory.CreateParameter();
                        p_direccion.ParameterName = "pdireccion";
                        p_direccion.Value = pEntidad.direccion;
                        p_direccion.Size = 200;
                        p_direccion.DbType = DbType.AnsiString;
                        p_direccion.Direction = ParameterDirection.Input;

                        DbParameter p_telefono = cmdTransaccionFactory.CreateParameter();
                        p_telefono.ParameterName = "ptelefono";
                        p_telefono.Value = pEntidad.telefono;
                        p_telefono.DbType = DbType.AnsiString;
                        p_telefono.Size = 50;
                        p_telefono.Direction = ParameterDirection.Input;

                        DbParameter pcod_encargado = cmdTransaccionFactory.CreateParameter();
                        pcod_encargado.ParameterName = "pcodigoencargado";
                        pcod_encargado.Value = pEntidad.cod_persona;
                        pcod_encargado.DbType = DbType.Int64;
                        pcod_encargado.Size = 8;
                        pcod_encargado.Direction = ParameterDirection.Input;

                        DbParameter pcod_centrocosto = cmdTransaccionFactory.CreateParameter();
                        pcod_centrocosto.ParameterName = "pcodigocentrocosto";
                        pcod_centrocosto.Value = pEntidad.centro_costo;
                        pcod_centrocosto.DbType = DbType.Int64;
                        pcod_centrocosto.Size = 8;
                        pcod_centrocosto.Direction = ParameterDirection.Input;

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

                        DbParameter p_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta.ParameterName = "pcod_cuenta";
                        p_cod_cuenta.Value = pEntidad.cod_cuenta;
                        p_cod_cuenta.DbType = DbType.String;
                        p_estado.Direction = ParameterDirection.Input;

                        DbParameter p_sede_propia = cmdTransaccionFactory.CreateParameter();
                        p_sede_propia.ParameterName = "psede_propia";
                        p_sede_propia.Value = pEntidad.sede_propia;
                        p_sede_propia.DbType = DbType.Int32;
                        p_sede_propia.Direction = ParameterDirection.Input;

                        DbParameter p_indicador = cmdTransaccionFactory.CreateParameter();
                        p_indicador.ParameterName = "pindicador";
                        p_indicador.Value = pEntidad.indicador_corresponsal;
                        p_indicador.DbType = DbType.Int32;
                        p_indicador.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_negocio = cmdTransaccionFactory.CreateParameter();
                        p_tipo_negocio.ParameterName = "ptipo_negocio";
                        if (pEntidad.tipo_negocio != null) p_tipo_negocio.Value = pEntidad.tipo_negocio; else p_tipo_negocio.Value = DBNull.Value;
                        p_tipo_negocio.DbType = DbType.Int32;
                        p_tipo_negocio.Direction = ParameterDirection.Input;


                        DbParameter p_codsuper = cmdTransaccionFactory.CreateParameter();
                        p_codsuper.ParameterName = "pcod_super";
                        p_codsuper.Value = pEntidad.cod_super;
                        p_codsuper.DbType = DbType.Int32;
                        p_codsuper.Direction = ParameterDirection.Input;


                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);
                        cmdTransaccionFactory.Parameters.Add(pnom_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_ciudad);
                        cmdTransaccionFactory.Parameters.Add(p_direccion);
                        cmdTransaccionFactory.Parameters.Add(p_telefono);
                        cmdTransaccionFactory.Parameters.Add(pcod_encargado);
                        cmdTransaccionFactory.Parameters.Add(pcod_centrocosto);
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(p_sede_propia);
                        cmdTransaccionFactory.Parameters.Add(p_indicador);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_negocio);
                        cmdTransaccionFactory.Parameters.Add(p_codsuper);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OFICINMODIFICAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OFICINA", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Modificar Oficina");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_oficina), "OFICINA", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ModificarOficina", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Elimina una Oficina en la base de datos
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        public void EliminarOficina(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Oficina pEntidad = new Oficina();


                        if (pUsuario.programaGeneraLog)
                            pEntidad = ConsultarOficina(pId, pUsuario); //REGISTRO DE AUDITORIA


                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pId;
                        pcod_oficina.DbType = DbType.Int16;
                        pcod_oficina.Size = 8;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OFICINAELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OFICINA", pUsuario, Accion.Eliminar.ToString(), TipoAuditoria.CajaFinanciera, "Eliminar Oficina");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_oficina), "OFICINA", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "EliminarOficina", ex);
                    }

                }
            }
        }

        public Oficina ConsultarDireccionYCiudadDeUnaOficina(long codigoOficina, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Oficina entidad = new Oficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_ciudad, direccion FROM OFICINA where cod_oficina =" + codigoOficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_ciudad"] != DBNull.Value) entidad.cod_ciudad = Convert.ToInt64(resultado["cod_ciudad"].ToString());
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ConsultarDireccionYCiudadDeUnaOficina", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Oficina de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Oficina consultada</returns>
        public Oficina ConsultarOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Oficina entidad = new Oficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM OFICINA where cod_oficina=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["cod_empresa"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["cod_empresa"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["cod_ciudad"] != DBNull.Value) entidad.cod_ciudad = Convert.ToInt64(resultado["cod_ciudad"].ToString());
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            if (resultado["responsable"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["responsable"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["SEDE_PROPIA"] != DBNull.Value) entidad.sede_propia = Convert.ToInt32(resultado["SEDE_PROPIA"]);
                            if (resultado["INDICADOR_CORRESPONSAL"] != DBNull.Value) entidad.indicador_corresponsal = Convert.ToInt32(resultado["INDICADOR_CORRESPONSAL"]);
                            if (resultado["TIPO_NEGOCIO"] != DBNull.Value) entidad.tipo_negocio = Convert.ToInt32(resultado["TIPO_NEGOCIO"]);
                            if (resultado["COD_SUPER"] != DBNull.Value) entidad.cod_super = Convert.ToInt64(resultado["COD_SUPER"].ToString());

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ConsultarOficina", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Oficina de la base de datos relacionado
        /// ciudad, responsable
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Oficina consultada</returns>
        public Oficina ConsultarXOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Oficina entidad = new Oficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select a.cod_oficina codoficina, (PRIMER_NOMBRE ||' '|| SEGUNDO_NOMBRE ||' '|| PRIMER_APELLIDO ||' '|| SEGUNDO_APELLIDO) nom_persona, a.estado_caja estadoOfi, a.responsable responsable, 
                                        (select decode(max(x.fecha_proceso), null, sysdate, max(x.fecha_proceso)) from procesooficina x where x.cod_oficina=a.cod_oficina) fechaproceso, 
                                        (select Max(tipo_proceso) from procesooficina x where fecha_proceso = (select max(x.fecha_proceso) from procesooficina x where x.cod_oficina = a.cod_oficina) And x.cod_oficina = a.cod_oficina) tipoproceso, sysdate fechanuevoproceso 
                                        From oficina a                                          
                                        Left Join centro_costo c On a.centro_costo = c.centro_costo
                                        Left Join persona d On a.responsable = d.cod_persona
                                        Left Join ciudades b On a.cod_ciudad = b.codciudad
                                        Where a.cod_oficina = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["codoficina"]);
                            if (resultado["responsable"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["responsable"]);
                            if (resultado["estadoOfi"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estadoOfi"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["nom_persona"]);
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fechaproceso = Convert.ToDateTime(resultado["fechaproceso"]);
                            if (resultado["fechanuevoproceso"] != DBNull.Value) entidad.fecha_nuevo_proceso = Convert.ToDateTime(resultado["fechanuevoproceso"]);
                            if (resultado["tipoproceso"] != DBNull.Value) entidad.tipo_proceso = Convert.ToInt64(resultado["tipoproceso"]);
                            if (entidad.tipo_proceso == 0 || entidad.tipo_proceso == null)
                                entidad.estado = 0;
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ConsultarXOficina", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidas</returns>
        public List<Oficina> ListarOficina(Oficina pEntidad, Usuario pUsuario)
        {
            return ListarOficina(pEntidad, pUsuario, "");
        }

        public List<Oficina> ListarOficina(Oficina pEntidad, Usuario pUsuario, string filtro)
        {
           
            DbDataReader resultado = default(DbDataReader);
            List<Oficina> lstOficina = new List<Oficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        if (filtro.Trim() != "")
                            filtro = " Where " + filtro;

                        if (connection.GetType().Name.ToUpper().Equals("SQLCONNECTION"))
                       

                        {
                            sql = " SELECT a.cod_oficina codoficina,a.cod_super, a.nombre nomoficina, cod_ciudad, a.cod_empresa codempresa, fecha_creacion, a.direccion dir, a.telefono tel, (select b.nomciudad from Ciudades b where a.cod_ciudad=b.codciudad) nomciudad, (select c.centro_costo from CENTRO_COSTO c where a.centro_costo=c.centro_costo) codcentrocosto, (select c.nom_centro from CENTRO_COSTO c where a.centro_costo=c.centro_costo) nom_centro, (select concat(PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO, SEGUNDO_APELLIDO) from Persona d where a.responsable=d.cod_persona) nom_persona FROM OFICINA a  " + filtro + " Order By a.cod_oficina asc";
                        }
                        else
                            {
                            sql = " SELECT a.cod_oficina codoficina,a.cod_super, a.nombre nomoficina, cod_ciudad, a.cod_empresa codempresa, fecha_creacion, a.direccion dir, a.telefono tel, (select b.nomciudad from Ciudades b where a.cod_ciudad=b.codciudad) nomciudad, (select c.centro_costo from CENTRO_COSTO c where a.centro_costo=c.centro_costo) codcentrocosto, (select c.nom_centro from CENTRO_COSTO c where a.centro_costo=c.centro_costo) nom_centro, (select (PRIMER_NOMBRE ||' '|| SEGUNDO_NOMBRE ||' '|| PRIMER_APELLIDO ||' '|| SEGUNDO_APELLIDO) from Persona d where a.responsable=d.cod_persona) nom_persona FROM OFICINA a  " + filtro + " Order By a.cod_oficina asc";

                        }


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Oficina entidad = new Oficina();
                            //Asociar todos los valores a la entidad
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["codoficina"]);
                            if (resultado["codempresa"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["codempresa"]);
                            if (resultado["nomoficina"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nomoficina"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["cod_ciudad"] != DBNull.Value) entidad.cod_ciudad = Convert.ToInt64(resultado["cod_ciudad"]);
                            if (resultado["dir"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["dir"]);
                            if (resultado["tel"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["tel"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["nomciudad"]);
                            if (resultado["codcentrocosto"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["codcentrocosto"]);
                            if (resultado["nom_centro"] != DBNull.Value) entidad.nom_centro = Convert.ToString(resultado["nom_centro"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["nom_persona"]);
                            if (resultado["cod_super"] != DBNull.Value) entidad.cod_super = Convert.ToInt64(resultado["cod_super"]);
                            lstOficina.Add(entidad);
                        }

                        return lstOficina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ListarOficina", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista de oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidas</returns>
        public List<Oficina> ListarOficinaUsuario(Oficina pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Oficina> lstOficina = new List<Oficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT a.cod_oficina codoficina,a.nombre nomoficina ,a.cod_empresa codempresa,fecha_creacion,codciudad ,a.direccion dir,a.telefono tel,nomciudad,c.centro_costo codcentrocosto,nom_centro,(PRIMER_NOMBRE ||' '|| SEGUNDO_NOMBRE ||' '|| PRIMER_APELLIDO ||' '|| SEGUNDO_APELLIDO) nom_persona FROM OFICINA a , CIUDADES b, CENTRO_COSTO c, PERSONA d WHERE a.cod_ciudad=b.codciudad and a.centro_costo=c.centro_costo and a.responsable=d.cod_persona and a.cod_oficina=" + pUsuario.cod_oficina.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Oficina entidad = new Oficina();
                            //Asociar todos los valores a la entidad
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["codoficina"]);
                            if (resultado["codempresa"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["codempresa"]);
                            if (resultado["nomoficina"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nomoficina"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["codciudad"] != DBNull.Value) entidad.cod_ciudad = Convert.ToInt64(resultado["codciudad"].ToString());
                            if (resultado["dir"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["dir"]);
                            if (resultado["tel"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["dir"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["nomciudad"]);
                            if (resultado["codcentrocosto"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["codcentrocosto"]);
                            if (resultado["nom_centro"] != DBNull.Value) entidad.nom_centro = Convert.ToString(resultado["nom_centro"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["nom_persona"].ToString());
                            lstOficina.Add(entidad);
                        }

                        return lstOficina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ListarOficinaUsuario", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene el conteo de usuarios asociados a la oficina especifica 
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Oficina consultada</returns>
        public Oficina ConsultarUsersXOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Oficina entidad = new Oficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT count(*) count_users FROM USUARIOS where cod_oficina=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["count_users"] != DBNull.Value) entidad.conteo = Convert.ToInt64(resultado["count_users"]);
                        }
                        else
                        {
                            entidad.conteo = 0;
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ConsultarUsersXOficina", ex);
                        return null;
                    }
                }
            }
        }


    }
}
