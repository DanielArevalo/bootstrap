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
    public class ClienteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ClienteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Cliente CrearCliente(Cliente pAseEntidadCliente, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /*DbParameter pCodigoPrograma = cmdTransaccionFactory.CreateParameter();
                        pCodigoPrograma.ParameterName = param + "CodigoPrograma";
                        pCodigoPrograma.Value = pAseEntidadCliente.CodigoPrograma;

                        DbParameter pGenerarLog = cmdTransaccionFactory.CreateParameter();
                        pGenerarLog.ParameterName = param + "GenerarLog";
                        pGenerarLog.Value = pAseEntidadCliente.GeneraLog;*/

                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pPrimerNombre.Direction = ParameterDirection.Input;
                        pPrimerNombre.ParameterName = "P_SNOMBRE1";
                        pPrimerNombre.Value = pAseEntidadCliente.PrimerNombre;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pSegundoNombre.Direction = ParameterDirection.Input;
                        pSegundoNombre.ParameterName = "P_SNOMBRE2";
                        pSegundoNombre.Value = pAseEntidadCliente.SegundoNombre;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "P_SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntidadCliente.PrimerApellido;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.Direction = ParameterDirection.Input;
                        pSegundoApellido.ParameterName = "P_SAPELLIDO2";
                        pSegundoApellido.Value = pAseEntidadCliente.SegundoApellido;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "P_CODTIPOIDENTIFICACION";
                        pTipoDocumento.Value = pAseEntidadCliente.TipoDocumento;

                        DbParameter pNumeroDocumento = cmdTransaccionFactory.CreateParameter();
                        pNumeroDocumento.Direction = ParameterDirection.Input;
                        pNumeroDocumento.ParameterName = "P_SIDENTIFICACION";
                        pNumeroDocumento.Value = pAseEntidadCliente.NumeroDocumento;

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.Direction = ParameterDirection.Input;
                        pDireccion.ParameterName = "P_SDIRECCION";
                        pDireccion.Value = pAseEntidadCliente.Direccion;

                        DbParameter pZona = cmdTransaccionFactory.CreateParameter();
                        pZona.Direction = ParameterDirection.Input;
                        pZona.ParameterName = "P_ICODZONA";
                        pZona.Value = pAseEntidadCliente.CodigoZona;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.Direction = ParameterDirection.Input;
                        pTelefono.ParameterName = "P_STELEFONO";
                        pTelefono.Value = pAseEntidadCliente.Telefono;

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.Direction = ParameterDirection.Input;
                        pEmail.ParameterName = "P_SMAIL";
                        pEmail.Value = pAseEntidadCliente.Email;

                        DbParameter pFechaRegistro = cmdTransaccionFactory.CreateParameter();
                        pFechaRegistro.ParameterName = "P_FREGISTRO";
                        pFechaRegistro.Direction = ParameterDirection.Input;
                        pFechaRegistro.Value = pAseEntidadCliente.FechaRegistro;

                        DbParameter pRazonSocial = cmdTransaccionFactory.CreateParameter();
                        pRazonSocial.Direction = ParameterDirection.Input;
                        pRazonSocial.ParameterName = "P_SRAZONSOC";
                        pRazonSocial.Value = pAseEntidadCliente.RazonSocial;

                        DbParameter pSiglaNegocio = cmdTransaccionFactory.CreateParameter();
                        pSiglaNegocio.Direction = ParameterDirection.Input;
                        pSiglaNegocio.ParameterName = "P_SSIGLA";
                        pSiglaNegocio.Value = pAseEntidadCliente.SiglaNegocio;

                        DbParameter pCodigoActividad = cmdTransaccionFactory.CreateParameter();
                        pCodigoActividad.Direction = ParameterDirection.Input;
                        pCodigoActividad.ParameterName = "P_CODACTIVIDAD";
                        pCodigoActividad.Value = pAseEntidadCliente.CodigoActividad;

                        DbParameter P_codusuario = cmdTransaccionFactory.CreateParameter();
                        P_codusuario.Direction = ParameterDirection.Input;
                        P_codusuario.ParameterName = "P_codusuario";
                        P_codusuario.Value = pAseEntidadCliente.cod_asesor;

                        DbParameter pIdCliente = cmdTransaccionFactory.CreateParameter();
                        pIdCliente.Direction = ParameterDirection.Output;
                        pIdCliente.ParameterName = param + "P_IDCODIGO";
                        pIdCliente.DbType = DbType.String;
                        pIdCliente.Size = 50;
                        pIdCliente.Value = pAseEntidadCliente.IdCliente.ToString();

                        /*cmdTransaccionFactory.Parameters.Add(pCodigoPrograma);
                        cmdTransaccionFactory.Parameters.Add(pGenerarLog);*/
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
                        cmdTransaccionFactory.Parameters.Add(pCodigoActividad);
                        cmdTransaccionFactory.Parameters.Add(P_codusuario);
                        cmdTransaccionFactory.Parameters.Add(pIdCliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAseEntidadCliente, "ASECLIENTE", pUsuario, Accion.Crear.ToString());//REGISTRO DE AUDITORIA
                        DAauditoria.InsertarLog(pAseEntidadCliente, "CLIENTE", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        pAseEntidadCliente.IdCliente = Convert.ToInt64(pIdCliente.Value);

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
                        Cliente pAseEntidadCliente = new Cliente();

                        //if (pUsuario.programaGeneraLog)
                        //pAseEntidadCliente = ConsultarCliente(pIdCliente, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = param + "P_IDCODIGO";
                        pIdPrograma.Value = pIdCliente;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(pAseEntidadCliente, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "EliminarCliente", ex);
                    }
                }
            }
        }

        public Cliente ConsultarCliente(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            Cliente aseEntiCliente = new Cliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pIdAseCliente = cmdTransaccionFactory.CreateParameter();
                        pIdAseCliente.ParameterName = param + "P_IDCLIENTE";
                        pIdAseCliente.Direction = ParameterDirection.Input;
                        pIdAseCliente.Value = pIdAseEntiCliente;

                        cmdTransaccionFactory.Parameters.Add(pIdAseCliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_CONSULTAR";
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["ICODIGO"] != DBNull.Value) aseEntiCliente.IdCliente = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["SNOMBRE1"] != DBNull.Value) aseEntiCliente.PrimerNombre = reader["SNOMBRE1"].ToString();
                            if (reader["SNOMBRE2"] != DBNull.Value) aseEntiCliente.SegundoNombre = reader["SNOMBRE2"].ToString();
                            if (reader["SAPELLIDO1"] != DBNull.Value) aseEntiCliente.PrimerApellido = reader["SAPELLIDO1"].ToString();
                            if (reader["SAPELLIDO2"] != DBNull.Value) aseEntiCliente.SegundoApellido = reader["SAPELLIDO2"].ToString();
                            if (reader["codtipoidentificacion"] != DBNull.Value) aseEntiCliente.TipoDocumento = reader["codtipoidentificacion"].ToString();
                            if (reader["nombIdentificacion"] != DBNull.Value) aseEntiCliente.NombreTipoIdentificacion = reader["nombIdentificacion"].ToString();
                            if (reader["SIDENTIFICACION"] != DBNull.Value) aseEntiCliente.NumeroDocumento = Convert.ToString(reader["SIDENTIFICACION"].ToString());
                            if (reader["SDIRECCION"] != DBNull.Value) aseEntiCliente.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["icodzona"] != DBNull.Value) aseEntiCliente.CodigoZona = reader["icodzona"].ToString();
                            if (reader["nombZona"] != DBNull.Value) aseEntiCliente.NombreZona = reader["nombZona"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) aseEntiCliente.Telefono = Convert.ToString(reader["STELEFONO"]);
                            //if (reader["STELEFONO"] != DBNull.Value) aseEntiCliente.Telefono = Convert.ToInt64(reader["STELEFONO"]);

                            if (reader["SEMAIL"] != DBNull.Value) aseEntiCliente.Email = reader["SEMAIL"].ToString();
                            if (reader["FREGISTRO"] != DBNull.Value) aseEntiCliente.FechaRegistro = Convert.ToDateTime(reader["FREGISTRO"].ToString());
                            if (reader["SRAZONSOC"] != DBNull.Value) aseEntiCliente.RazonSocial = reader["SRAZONSOC"].ToString();
                            if (reader["SSIGLA"] != DBNull.Value) aseEntiCliente.SiglaNegocio = reader["SSIGLA"].ToString();
                            if (reader["codactividad"] != DBNull.Value) aseEntiCliente.CodigoActividad = Convert.ToInt64(reader["codactividad"].ToString());
                            if (reader["nombActividad"] != DBNull.Value) aseEntiCliente.NombreActividad = reader["nombActividad"].ToString();
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return aseEntiCliente;
                    }
                    catch (Exception ex)
                    {
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
                        string sql = "SELECT * FROM VAsesoresCliente " + ObtenerFiltro(pAseEntidadCliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["icodigo"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["icodigo"]);
                            if (resultado["SNOMBRE1"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["SNOMBRE1"]);
                            if (resultado["SNOMBRE2"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["SNOMBRE2"]);
                            if (resultado["SAPELLIDO1"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["SAPELLIDO1"]);
                            if (resultado["SAPELLIDO2"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["SAPELLIDO2"]);
                            if (resultado["codtipoidentificacion"] != DBNull.Value) entidad.TipoDocumento = resultado["codtipoidentificacion"].ToString();
                            if (resultado["nombIdentificacion"] != DBNull.Value) entidad.NombreTipoIdentificacion = resultado["nombIdentificacion"].ToString();
                            if (resultado["SIDENTIFICACION"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["SIDENTIFICACION"].ToString());
                            if (resultado["SDIRECCION"] != DBNull.Value) entidad.Direccion = (resultado["SDIRECCION"]).ToString();
                            if (resultado["ICODZONA"] != DBNull.Value) entidad.CodigoZona = resultado["ICODZONA"].ToString();
                            if (resultado["STELEFONO"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["STELEFONO"]);
                            //if (resultado["STELEFONO"] != DBNull.Value) entidad.Telefono = Convert.ToInt64(resultado["STELEFONO"]);
                            if (resultado["SEMAIL"] != DBNull.Value) entidad.Email = resultado["SEMAIL"].ToString();
                            if (resultado["FREGISTRO"] != DBNull.Value) entidad.FechaRegistro = Convert.ToDateTime(resultado["FREGISTRO"]);
                            if (resultado["SRAZONSOC"] != DBNull.Value) entidad.RazonSocial = resultado["SRAZONSOC"].ToString();
                            if (resultado["SSIGLA"] != DBNull.Value) entidad.SiglaNegocio = resultado["SSIGLA"].ToString();
                            if (resultado["codactividad"] != DBNull.Value) entidad.CodigoActividad = Convert.ToInt64(resultado["codactividad"].ToString());

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

        public Cliente ConsultarClienteEjecutivo(Int64 pIdCliente, Usuario pUsuario)
        {
            DbDataReader resultado;
            Cliente entidad = new Cliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_ClienteEjecutivo where cod_persona=" + pIdCliente.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["barrio"] != DBNull.Value) entidad.Barrio = Convert.ToString(resultado["barrio"]);
                            if (resultado["ciudad"] != DBNull.Value) entidad.Ciudad = Convert.ToString(resultado["ciudad"]);
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.Calificacion = Convert.ToInt64(resultado["calificacion_cliente"]);
                            if (resultado["zona"] != DBNull.Value) entidad.NombreZona = Convert.ToString(resultado["zona"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.NombreOficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["asesor"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["asesor"]);
                            if (resultado["estado"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["estado"]);
                            if (resultado["tipo_cliente"] != DBNull.Value) entidad.tipo_cliente = Convert.ToString(resultado["tipo_cliente"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
        
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public Cliente ConsultarClienteEjecutivorepomora(Int64 radicado, Usuario pUsuario)
        {
            DbDataReader resultado;
            Cliente entidad = new Cliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select a.*,c.numero_radicacion from v_ClienteEjecutivo a inner join credito c on a.cod_persona=c.cod_deudor where c.numero_radicacion=" + radicado.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["barrio"] != DBNull.Value) entidad.Barrio = Convert.ToString(resultado["barrio"]);
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.Calificacion = Convert.ToInt64(resultado["calificacion_cliente"]);
                            if (resultado["zona"] != DBNull.Value) entidad.NombreZona = Convert.ToString(resultado["zona"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.NombreOficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["asesor"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["asesor"]);
                            if (resultado["estado"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["estado"]);
                            if (resultado["tipo_cliente"] != DBNull.Value) entidad.tipo_cliente = Convert.ToString(resultado["tipo_cliente"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }





        public List<Cliente> ListarClientesEjecutivo(Cliente pCliente, Usuario pUsuario, int opcion, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cliente> lstPrograma = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        switch (opcion)
                        {
                            case 1: sql = "select * from v_ClientePotencial " + filtro;
                                break;
                            case 2: sql = "select * from v_TipoCliente " + filtro;
                                break;
                            case 3: sql = "select * from v_TipoCliente " + filtro;
                                break;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["codigo"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["codigo"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.NombreCompleto = Convert.ToString(resultado["nombre"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);

                            if (resultado["cod_zona"] != DBNull.Value) entidad.CodigoZona = Convert.ToString(resultado["cod_zona"]);
                            if (resultado["nom_zona"] != DBNull.Value) entidad.NombreZona = Convert.ToString(resultado["nom_zona"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.Calificacion = Convert.ToInt64(resultado["calificacion_cliente"].ToString());

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
        public List<Cliente> ListarClientesEjecutivopotencial(Cliente pCliente, Usuario pUsuario, int opcion, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cliente> lstPrograma = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        
                            string sql = "select * from v_ClientePotencial " + filtro;
                              

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["codigo"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["codigo"]);
                           
                            if (resultado["nombre"] != DBNull.Value) entidad.NombreCompleto = Convert.ToString(resultado["nombre"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);

                            if (resultado["cod_zona"] != DBNull.Value) entidad.CodigoZona = Convert.ToString(resultado["cod_zona"]);
                            if (resultado["nom_zona"] != DBNull.Value) entidad.NombreZona = Convert.ToString(resultado["nom_zona"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.Calificacion = Convert.ToInt64(resultado["calificacion_cliente"].ToString());

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
        public Cliente ConsultarCodeudor(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            Cliente entidad = new Cliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_Codeudores where numero_radicacion=" + numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            //if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToInt64(resultado["telefono"]);
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"].ToString());

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCodeudor", ex);
                        return null;
                    }
                }
            }
        }




        public List<Cliente> ListarCodeudores(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cliente> lstPrograma = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_Codeudores where numero_radicacion=" + numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            //if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToInt64(resultado["telefono"]);
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"].ToString());

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

        public List<Cliente> ListarUbicacionClientes(AgendaActividad agActividad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cliente> lstPrograma = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_UbicacionClientes" + ObtenerFiltro(agActividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["nombrecompleto"] != DBNull.Value) entidad.NombreCompleto = Convert.ToString(resultado["nombrecompleto"]);
                            //if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToInt64(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["nomciudad"]);

                            lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarUbicacionClientes", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cliente> ListarClientesPersona(Cliente iCliente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cliente> lstPrograma = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_UBICACIONPERSONA" + ObtenerFiltro(iCliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["nombrecompleto"] != DBNull.Value) entidad.NombreCompleto = Convert.ToString(resultado["nombrecompleto"]);
                            //if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToInt64(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["nomciudad"]);

                            lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarClientesPersona", ex);
                        return null;
                    }
                }
            }
        }
        public List<Cliente> ListarClientesPersonageo(Cliente iCliente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cliente> lstPrograma = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                      
                        string sql = "Select * from V_UBICACIONGEOGRAFICAPERSONA " + ObtenerFiltro(iCliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();

                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["nombrecompleto"] != DBNull.Value) entidad.NombreCompleto = Convert.ToString(resultado["nombrecompleto"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["nomciudad"]);
                            if (resultado["latitud"] != DBNull.Value) entidad.latitud = Convert.ToString(resultado["latitud"]);
                            if (resultado["longitud"] != DBNull.Value) entidad.longitud = Convert.ToString(resultado["longitud"]);
                            lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarClientesPersona", ex);
                        return null;
                    }
                }
            }
        }

        public Cliente ActualizarCliente(Cliente pAseEntidadCliente, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "P_IDCODIGO";
                        pIdPrograma.Value = pAseEntidadCliente.IdCliente;

                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pPrimerNombre.Direction = ParameterDirection.Input;
                        pPrimerNombre.ParameterName = "P_SNOMBRE1";
                        pPrimerNombre.Value = pAseEntidadCliente.PrimerNombre;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pSegundoNombre.Direction = ParameterDirection.Input;
                        pSegundoNombre.ParameterName = "P_SNOMBRE2";
                        pSegundoNombre.Value = pAseEntidadCliente.SegundoNombre;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "P_SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntidadCliente.PrimerApellido;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.Direction = ParameterDirection.Input;
                        pSegundoApellido.ParameterName = "P_SAPELLIDO2";
                        pSegundoApellido.Value = pAseEntidadCliente.SegundoApellido;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "P_CODTIPOIDENTIFICACION";
                        pTipoDocumento.Value = pAseEntidadCliente.TipoDocumento;

                        DbParameter pNumeroDocumento = cmdTransaccionFactory.CreateParameter();
                        pNumeroDocumento.Direction = ParameterDirection.Input;
                        pNumeroDocumento.ParameterName = "P_SIDENTIFICACION";
                        pNumeroDocumento.Value = pAseEntidadCliente.NumeroDocumento;

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.Direction = ParameterDirection.Input;
                        pDireccion.ParameterName = "P_SDIRECCION";
                        pDireccion.Value = pAseEntidadCliente.Direccion;

                        DbParameter pZona = cmdTransaccionFactory.CreateParameter();
                        pZona.Direction = ParameterDirection.Input;
                        pZona.ParameterName = "P_ICODZONA";
                        pZona.Value = pAseEntidadCliente.CodigoZona;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.Direction = ParameterDirection.Input;
                        pTelefono.ParameterName = "P_STELEFONO";
                        pTelefono.Value = pAseEntidadCliente.Telefono;

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.Direction = ParameterDirection.Input;
                        pEmail.ParameterName = "P_SMAIL";
                        pEmail.Value = pAseEntidadCliente.Email;

                        DbParameter pFechaRegistro = cmdTransaccionFactory.CreateParameter();
                        pFechaRegistro.ParameterName = "P_FREGISTRO";
                        pFechaRegistro.Direction = ParameterDirection.Input;
                        pFechaRegistro.Value = pAseEntidadCliente.FechaRegistro;

                        DbParameter pRazonSocial = cmdTransaccionFactory.CreateParameter();
                        pRazonSocial.Direction = ParameterDirection.Input;
                        pRazonSocial.ParameterName = "P_SRAZONSOC";
                        pRazonSocial.Value = pAseEntidadCliente.RazonSocial;

                        DbParameter pSiglaNegocio = cmdTransaccionFactory.CreateParameter();
                        pSiglaNegocio.Direction = ParameterDirection.Input;
                        pSiglaNegocio.ParameterName = "P_SSIGLA";
                        pSiglaNegocio.Value = pAseEntidadCliente.SiglaNegocio;

                        DbParameter pCodigoActividad = cmdTransaccionFactory.CreateParameter();
                        pCodigoActividad.Direction = ParameterDirection.Input;
                        pCodigoActividad.ParameterName = "P_CODACTIVIDAD";
                        pCodigoActividad.Value = pAseEntidadCliente.CodigoActividad;

                        DbParameter P_codusuario = cmdTransaccionFactory.CreateParameter();
                        P_codusuario.Direction = ParameterDirection.Input;
                        P_codusuario.ParameterName = "P_codusuario";
                        P_codusuario.Value = pAseEntidadCliente.cod_asesor;

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
                        cmdTransaccionFactory.Parameters.Add(pCodigoActividad);
                        cmdTransaccionFactory.Parameters.Add(P_codusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_ACTUALIZAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAseEntidadCliente, "ASCLIENTES", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        DAauditoria.InsertarLog(pAseEntidadCliente, "CLIENTE", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pAseEntidadCliente;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "CrearCliente", ex);
                        return null;
                    }
                }
            }
        }//end actualizars

        public Cliente guardarobservacion(Usuario pUsuario, DateTime fecha, string observacion, string cod_persona, string numero_radicacion, string usuario, int TIPO)
        {
            DbDataReader resultado;
            Cliente entidad = new Cliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "INSERT INTO COLOCACION_OBSERVACION ( FECHA, COD_PERSONA, NUMERO_RADICACION, OBSERVACION, USUARIO, TIPO_CLIENTE) VALUES (sysdate, "+cod_persona+", "+numero_radicacion+", '"+observacion+"', '"+usuario+"', "+TIPO+")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                       
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }

    }
}