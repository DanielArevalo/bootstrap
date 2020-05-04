using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class AsesoresData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AsesoresData(){
            dbConnectionFactory = new ConnectionDataBase();
        }

        #region Cliente
        public Cliente CrearCliente(Cliente pAseEntidadCliente,Usuario pUsuario) {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "IDCODIGO";
                        pIdPrograma.Value = pAseEntidadCliente.IdCliente;

                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pPrimerNombre.Direction = ParameterDirection.Input;
                        pPrimerNombre.ParameterName = "SNOMBRE1";
                        pPrimerNombre.Value = pAseEntidadCliente.PrimerNombre;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pSegundoNombre.Direction = ParameterDirection.Input;
                        pSegundoNombre.ParameterName = "SNOMBRE2";
                        pSegundoNombre.Value = pAseEntidadCliente.SegundoNombre;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntidadCliente.PrimerApellido;
                        
                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.Direction = ParameterDirection.Input;
                        pSegundoApellido.ParameterName = "SAPELLIDO2";
                        pSegundoApellido.Value = pAseEntidadCliente.SegundoApellido;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "CODTIPOIDENTIFICACION";
                        pTipoDocumento.Value = pAseEntidadCliente.TipoDocumento;

                        DbParameter pNumeroDocumento = cmdTransaccionFactory.CreateParameter();
                        pNumeroDocumento.Direction = ParameterDirection.Input;
                        pNumeroDocumento.ParameterName = "SIDENTIFICACION";
                        pNumeroDocumento.Value = pAseEntidadCliente.NumeroDocumento;

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.Direction = ParameterDirection.Input;
                        pDireccion.ParameterName = "SDIRECCION";
                        pDireccion.Value = pAseEntidadCliente.Direccion;

                        DbParameter pZona = cmdTransaccionFactory.CreateParameter();
                        pZona.Direction = ParameterDirection.Input;
                        pZona.ParameterName = "ICODZONA";
                        pZona.Value = pAseEntidadCliente.CodigoZona;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.Direction = ParameterDirection.Input;
                        pTelefono.ParameterName = "STELEFONO";
                        pTelefono.Value = pAseEntidadCliente.Telefono;

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.Direction = ParameterDirection.Input;
                        pEmail.ParameterName = "SMAIL";
                        pEmail.Value = pAseEntidadCliente.Email;

                        DbParameter pFechaRegistro = cmdTransaccionFactory.CreateParameter();
                        pFechaRegistro.ParameterName = "FREGISTRO";
                        pFechaRegistro.Direction = ParameterDirection.Input;
                        pFechaRegistro.DbType = DbType.DateTime;
                        pFechaRegistro.Value = pAseEntidadCliente.FechaRegistro.ToString("dd-mm-yyyy");

                        DbParameter pRazonSocial = cmdTransaccionFactory.CreateParameter();
                        pRazonSocial.Direction = ParameterDirection.Input;
                        pRazonSocial.ParameterName = "SRAZONSOC";
                        pRazonSocial.Value = pAseEntidadCliente.RazonSocial;

                        DbParameter pSiglaNegocio = cmdTransaccionFactory.CreateParameter();
                        pSiglaNegocio.Direction = ParameterDirection.Input;
                        pSiglaNegocio.ParameterName = "SSIGLA";
                        pSiglaNegocio.Value = pAseEntidadCliente.SiglaNegocio;

                        DbParameter P_codusuario = cmdTransaccionFactory.CreateParameter();
                        P_codusuario.Direction = ParameterDirection.Input;
                        P_codusuario.ParameterName = "P_codusuario";
                        P_codusuario.Value = pAseEntidadCliente.cod_asesor;

                        DbParameter pCodigoActividad = cmdTransaccionFactory.CreateParameter();
                        pCodigoActividad.Direction = ParameterDirection.Input;
                        pCodigoActividad.ParameterName = "CODACTIVIDAD";
                        pCodigoActividad.Value = pAseEntidadCliente.CodigoActividad;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);
                        cmdTransaccionFactory.Parameters.Add(pPrimerNombre);
                        cmdTransaccionFactory.Parameters.Add(pSegundoNombre);
                        cmdTransaccionFactory.Parameters.Add(pPrimerApellido);
                        cmdTransaccionFactory.Parameters.Add(pSegundoApellido);
                        cmdTransaccionFactory.Parameters.Add(pTipoDocumento);
                        cmdTransaccionFactory.Parameters.Add(pNumeroDocumento);
                        cmdTransaccionFactory.Parameters.Add(pDireccion);
                        cmdTransaccionFactory.Parameters.Add(pZona);
                        cmdTransaccionFactory.Parameters.Add(pTelefono);
                        cmdTransaccionFactory.Parameters.Add(pEmail);
                        cmdTransaccionFactory.Parameters.Add(pFechaRegistro);
                        cmdTransaccionFactory.Parameters.Add(pRazonSocial);
                        cmdTransaccionFactory.Parameters.Add(pSiglaNegocio);
                        cmdTransaccionFactory.Parameters.Add(P_codusuario);
                        cmdTransaccionFactory.Parameters.Add(pCodigoActividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAseEntidadCliente, "CLIENTE", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAseEntidadCliente.IdCliente = Convert.ToInt64(pIdPrograma.Value);
                        return pAseEntidadCliente;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "CrearCliente", ex);
                        return null;
                    }
                }
            }
            
        }//end crear
        
        public void EliminarCliente(Int64 pIdCliente, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Cliente aseEntCliente = new Cliente();

                        if (pUsuario.programaGeneraLog)
                            aseEntCliente = ConsultarCliente(pIdCliente, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.ParameterName = "ICODIGO";
                        pIdPrograma.Value = pIdCliente;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(aseEntCliente, "CLIENTE", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "EliminarCliente", ex);
                    }
                }
            }
        }
        
        public Cliente ConsultarCliente(Int64 pIdAseEntiCliente, Usuario pUsuario) {
            DbDataReader reader = default(DbDataReader);
            Cliente aseEntiCliente = new Cliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand()) {
                    try { 
                        DbParameter pIdAseCliente = cmdTransaccionFactory.CreateParameter();
                        pIdAseCliente.ParameterName = "P_IDCLIENTE";
                        pIdAseCliente.Direction = ParameterDirection.Input;
                        pIdAseCliente.Value = pIdAseEntiCliente;
                        cmdTransaccionFactory.Parameters.Add(pIdAseCliente);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = "SELECT * FROM  ASCLIENTES WHERE ICODIGO =" + pIdAseEntiCliente;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        /*cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_CONSULTAR";*/
                        reader = cmdTransaccionFactory.ExecuteReader();
                        
                        if(reader.Read()){
                            if(reader["ICODIGO"]    != DBNull.Value) aseEntiCliente.IdCliente = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if(reader["SNOMBRE1"]   != DBNull.Value) aseEntiCliente.PrimerNombre = reader["SNOMBRE1"].ToString();
                            if(reader["SNOMBRE2"]   != DBNull.Value) aseEntiCliente.SegundoNombre = reader["SNOMBRE2"].ToString();
                            if(reader["SAPELLIDO1"] != DBNull.Value) aseEntiCliente.PrimerApellido = reader["SAPELLIDO1"].ToString();
                            if(reader["SAPELLIDO2"] != DBNull.Value) aseEntiCliente.SegundoApellido = reader["SAPELLIDO2"].ToString();
                            if (reader["ITIPIDEN"] != DBNull.Value) aseEntiCliente.TipoDocumento = Convert.ToString(reader["ITIPIDEN"].ToString());
                            if (reader["SIDENTIFICACION"] != DBNull.Value) aseEntiCliente.NumeroDocumento = Convert.ToString(reader["SIDENTIFICACION"].ToString());
                            if(reader["SDIRECCION"] != DBNull.Value) aseEntiCliente.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) aseEntiCliente.Telefono = Convert.ToString(reader["STELEFONO"]);
                            if(reader["SEMAIL"]     != DBNull.Value) aseEntiCliente.Email = reader["SEMAIL"].ToString();
                            if(reader["FREGISTRO"]  != DBNull.Value) aseEntiCliente.FechaRegistro = Convert.ToDateTime(reader["FREGISTRO"].ToString());
                            if(reader["SRAZONSOC"]  != DBNull.Value) aseEntiCliente.RazonSocial = reader["SRAZONSOC"].ToString();
                            if(reader["SSIGLA"]     != DBNull.Value) aseEntiCliente.SiglaNegocio = reader["SSIGLA"].ToString();
                            if(reader["IACTIVIDAD"] != DBNull.Value) aseEntiCliente.CodigoActividad = Convert.ToInt64(reader["IACTIVIDAD"].ToString());
                        }
                        else{
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return aseEntiCliente;
                    }catch(Exception ex){
                        BOExcepcion.Throw("ProgramaData", "ConsultarPrograma", ex);
                        return null;
                    }
                }
            }

        }
        
        public List<Cliente> ListarCliente(Cliente pAseEntidadCliente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cliente> lstPrograma = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM ASCLIENTES " + ObtenerFiltro(pAseEntidadCliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["idPrograma"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["idPrograma"]);
                            if (resultado["codigoPrograma"] != DBNull.Value) entidad.CodigoPrograma = Convert.ToString(resultado["codigoPrograma"]);
                            if (resultado["SNOMBRE1"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["nombrePrograma"]);
                            if (resultado["SNOMBRE2"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["SNOMBRE2"]);
                            if (resultado["SAPELLIDO1"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["SAPELLIDO1"]);
                            if (resultado["SAPELLIDO2"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["SAPELLIDO2"]);
                            if (resultado["ITIPIDEN"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["ITIPIDEN"].ToString());
                            if (resultado["SIDENTIFICACION"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["SIDENTIFICACION"].ToString());
                            if (resultado["SDIRECCION"] != DBNull.Value) entidad.Direccion = (resultado["SDIRECCION"]).ToString();
                            if (resultado["ICODZONA"] != DBNull.Value) entidad.CodigoZona = resultado["ICODZONA"].ToString();
                            if (resultado["STELEFONO"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["STELEFONO"]);
                            if (resultado["SMAIL"] != DBNull.Value) entidad.Email = resultado["SMAIL"].ToString();
                            if (resultado["FREGISTRO"] != DBNull.Value) entidad.FechaRegistro = Convert.ToDateTime(resultado["FREGISTRO"]);
                            if (resultado["SRAZONSOC"] != DBNull.Value) entidad.RazonSocial = resultado["SRAZONSOC"].ToString();
                            if (resultado["SSIGLA"] != DBNull.Value) entidad.SiglaNegocio = resultado["SSIGLA"].ToString();
                            if (resultado["IACTIVIDAD"] != DBNull.Value) entidad.CodigoActividad = Convert.ToInt64(resultado["IACTIVIDAD"].ToString());
                            if (resultado["usuarioCrea"] != DBNull.Value) entidad.UsuarioCrea = Convert.ToString(resultado["usuarioCrea"]);
                            if (resultado["fechaEdita"] != DBNull.Value) entidad.FechaEdita = Convert.ToDateTime(resultado["fechaEdita"]);
                            if (resultado["usuarioEdita"] != DBNull.Value) entidad.UsuarioEdita = Convert.ToString(resultado["usuarioEdita"]);

                            lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarPrograma", ex);
                        return null;
                    }
                }
            }
        }
        #endregion Cliente

        #region Ejecutivo
        public Ejecutivo CrearEjecutivo(Ejecutivo pAseEntiEjecutivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "ICODIGO";
                        pIdPrograma.Value = pAseEntiEjecutivo.IdEjecutivo;

                        DbParameter pIdUsuario = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "ICODIGO";
                        pIdPrograma.Value = pAseEntiEjecutivo.IdUsuario;

                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pPrimerNombre.ParameterName = "SNOMBRE1";
                        pPrimerNombre.Value = pAseEntiEjecutivo.PrimerNombre;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pSegundoNombre.ParameterName = "SNOMBRE2";
                        pSegundoNombre.Value = pAseEntiEjecutivo.SegundoNombre;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntiEjecutivo.PrimerApellido;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.ParameterName = param + "SAPELLIDO2";
                        pSegundoApellido.ParameterName = "SAPELLIDO2";
                        pSegundoApellido.Value = pAseEntiEjecutivo.SegundoApellido;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "ITIPIDEN";
                        pTipoDocumento.Value = pAseEntiEjecutivo.TipoDocumento;

                        DbParameter pNumeroDocumento = cmdTransaccionFactory.CreateParameter();
                        pNumeroDocumento.Direction = ParameterDirection.Input;
                        pNumeroDocumento.ParameterName = param + "SIDENTIFICACION";
                        pNumeroDocumento.Value = pAseEntiEjecutivo.NumeroDocumento;

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.Direction = ParameterDirection.Input;
                        pDireccion.ParameterName = "SDIRECCION";
                        pDireccion.Value = pAseEntiEjecutivo.Direccion;

                        DbParameter pBarrio = cmdTransaccionFactory.CreateParameter();
                        pBarrio.Direction = ParameterDirection.Input;
                        pBarrio.ParameterName = "SBARRIO";
                        pBarrio.Value = pAseEntiEjecutivo.Barrio;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.Direction = ParameterDirection.Input;
                        pTelefono.ParameterName = "STELEFONO";
                        pTelefono.Value = pAseEntiEjecutivo.Telefono;

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.ParameterName = param + "SMAIL";
                        pEmail.ParameterName = "SMAIL";
                        pEmail.Value = pAseEntiEjecutivo.Email;

                        DbParameter pFechaIngreso = cmdTransaccionFactory.CreateParameter();
                        pFechaIngreso.ParameterName = "FINGRESO";
                        pFechaIngreso.Direction = ParameterDirection.Input;
                        pFechaIngreso.DbType = DbType.Date;
                        pFechaIngreso.Value = pAseEntiEjecutivo.FechaIngreso.ToString("dd-mm-yyyy");

                        DbParameter pOficina = cmdTransaccionFactory.CreateParameter();
                        pOficina.Direction = ParameterDirection.Input;
                        pOficina.ParameterName = "IOFICINA";
                        pOficina.Value = pAseEntiEjecutivo.IdOficina;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);
                        cmdTransaccionFactory.Parameters.Add(pPrimerNombre);
                        cmdTransaccionFactory.Parameters.Add(pSegundoNombre);
                        cmdTransaccionFactory.Parameters.Add(pPrimerApellido);
                        cmdTransaccionFactory.Parameters.Add(pSegundoApellido);
                        cmdTransaccionFactory.Parameters.Add(pTipoDocumento);
                        cmdTransaccionFactory.Parameters.Add(pNumeroDocumento);
                        cmdTransaccionFactory.Parameters.Add(pDireccion);
                        cmdTransaccionFactory.Parameters.Add(pBarrio);
                        cmdTransaccionFactory.Parameters.Add(pTelefono);
                        cmdTransaccionFactory.Parameters.Add(pEmail);
                        cmdTransaccionFactory.Parameters.Add(pFechaIngreso);
                        cmdTransaccionFactory.Parameters.Add(pOficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAseEntiEjecutivo, "EJECUTIVO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAseEntiEjecutivo.IdEjecutivo = Convert.ToInt64(pIdPrograma.Value);
                        return pAseEntiEjecutivo;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "CrearCliente", ex);
                        return null;
                    }
                }
            }

        }//end crear
        
        public void EliminarEjecutivo(Int64 pIdEjecutivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Ejecutivo aseEntEjecutivo = new Ejecutivo();

                        if (pUsuario.programaGeneraLog)
                            aseEntEjecutivo = ConsultarEjecutivo(pIdEjecutivo, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.ParameterName = "ICODIGO";
                        pIdPrograma.Value = pIdEjecutivo;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(aseEntEjecutivo, "EJECUTIVO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "EliminarEjecutivo", ex);
                    }
                }
            }
        }

        public Ejecutivo ConsultarEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            Ejecutivo aseEntEjecutivo = new Ejecutivo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdAseEjecutivo = cmdTransaccionFactory.CreateParameter();
                        pIdAseEjecutivo.ParameterName = "P_ICODIGO";
                        pIdAseEjecutivo.Value = pIdAseEntiEjecutivo;
                        cmdTransaccionFactory.Parameters.Add(pIdAseEjecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_CONSULTAR";
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["ICODIGO"]       != DBNull.Value) aseEntEjecutivo.IdEjecutivo    = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["IUSUARIO"]      != DBNull.Value) aseEntEjecutivo.IdUsuario      = Convert.ToInt64(reader["IUSUARIO"].ToString());
                            if (reader["SNOMBRE1"]      != DBNull.Value) aseEntEjecutivo.PrimerNombre   = reader["SNOMBRE1"].ToString();
                            if (reader["SNOMBRE2"]      != DBNull.Value) aseEntEjecutivo.SegundoNombre  = reader["SNOMBRE2"].ToString();
                            if (reader["SAPELLIDO1"]    != DBNull.Value) aseEntEjecutivo.PrimerApellido = reader["SAPELLIDO1"].ToString();
                            if (reader["SAPELLIDO2"]    != DBNull.Value) aseEntEjecutivo.SegundoApellido= reader["SAPELLIDO2"].ToString();
                            if (reader["ITIPOIDEN"]     != DBNull.Value) aseEntEjecutivo.TipoDocumento  = Convert.ToInt64(reader["ITIPOIDEN"].ToString());
                            if (reader["SIDENTIFICACION"] != DBNull.Value) aseEntEjecutivo.NumeroDocumento = reader["SIDENTIFICACION"].ToString();
                            if (reader["SDIRECCION"]    != DBNull.Value) aseEntEjecutivo.Direccion      = reader["SDIRECCION"].ToString();
                            if (reader["SBARRIO"]       != DBNull.Value) aseEntEjecutivo.Barrio         = reader["SBARRIO"].ToString();
                            if (reader["STELEFONO"]     != DBNull.Value) aseEntEjecutivo.Telefono       = Convert.ToInt64(reader["STELEFONO"].ToString());
                            if (reader["SEMAIL"]        != DBNull.Value) aseEntEjecutivo.Email          = reader["SEMAIL"].ToString();
                            if (reader["FINGRESO"]      != DBNull.Value) aseEntEjecutivo.FechaIngreso   = Convert.ToDateTime(reader["FINGRESO"].ToString());
                            if (reader["IOFICINA"]      != DBNull.Value) aseEntEjecutivo.IdOficina        = Convert.ToInt64(reader["IOFICINA"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return aseEntEjecutivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ConsultarPrograma", ex);
                        return null;
                    }
                }
            }

        }

        public List<Ejecutivo> ListarEjecutivo(Ejecutivo pAseEntiEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand()){
                    
                    try{
                        string sql = "SELECT * FROM ASEJECUTIVOS " + ObtenerFiltro(pAseEntiEjecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["ICODIGO"]  != DBNull.Value) aseEntEjecutivo.IdEjecutivo = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["IUSUARIO"] != DBNull.Value) aseEntEjecutivo.IdUsuario = Convert.ToInt64(reader["IUSUARIO"].ToString());
                            if (reader["SNOMBRE1"] != DBNull.Value) aseEntEjecutivo.PrimerNombre = reader["SNOMBRE1"].ToString();
                            if (reader["SNOMBRE2"] != DBNull.Value) aseEntEjecutivo.SegundoNombre = reader["SNOMBRE2"].ToString();
                            if (reader["SAPELLIDO1"] != DBNull.Value) aseEntEjecutivo.PrimerApellido = reader["SAPELLIDO1"].ToString();
                            if (reader["SAPELLIDO2"] != DBNull.Value) aseEntEjecutivo.SegundoApellido = reader["SAPELLIDO2"].ToString();
                            if (reader["ITIPOIDEN"] != DBNull.Value) aseEntEjecutivo.TipoDocumento = Convert.ToInt64(reader["ITIPOIDEN"].ToString());
                            if (reader["SIDENTIFICACION"] != DBNull.Value) aseEntEjecutivo.NumeroDocumento = reader["SIDENTIFICACION"].ToString();
                            if (reader["SDIRECCION"] != DBNull.Value) aseEntEjecutivo.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["SBARRIO"] != DBNull.Value) aseEntEjecutivo.Barrio = reader["SBARRIO"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) aseEntEjecutivo.Telefono = Convert.ToInt64(reader["STELEFONO"].ToString());
                            if (reader["SEMAIL"] != DBNull.Value) aseEntEjecutivo.Email = reader["SEMAIL"].ToString();
                            if (reader["FINGRESO"] != DBNull.Value) aseEntEjecutivo.FechaIngreso = Convert.ToDateTime(reader["FINGRESO"].ToString());
                            if (reader["IOFICINA"] != DBNull.Value) aseEntEjecutivo.IdOficina = Convert.ToInt64(reader["IOFICINA"].ToString());

                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "ListarEjecutivo", ex);
                        return null;
                    }
                }
            }
        }

        #endregion Ejecutivo

    }//end class
}// end NameSpace
