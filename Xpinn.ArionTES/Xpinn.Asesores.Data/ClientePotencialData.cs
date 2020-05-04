using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;
using System.Reflection;

namespace Xpinn.Asesores.Data
{
    public class ClientePotencialData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;


        public ClientePotencialData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ClientePotencial CrearCliente(ClientePotencial pAseEntidadCliente, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pPrimerNombre.Direction = ParameterDirection.Input;
                        pPrimerNombre.ParameterName = "P_SNOMBRE1";
                        pPrimerNombre.Value = pAseEntidadCliente.PrimerNombre;
                        pPrimerNombre.DbType = DbType.String;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pSegundoNombre.Direction = ParameterDirection.Input;
                        pSegundoNombre.ParameterName = "P_SNOMBRE2";
                        if (pAseEntidadCliente.SegundoNombre != null) pSegundoNombre.Value = pAseEntidadCliente.SegundoNombre; else pSegundoNombre.Value = DBNull.Value;
                        pPrimerNombre.DbType = DbType.String;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "P_SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntidadCliente.PrimerApellido;
                        pPrimerNombre.DbType = DbType.String;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.Direction = ParameterDirection.Input;
                        pSegundoApellido.ParameterName = "P_SAPELLIDO2";
                        if (pAseEntidadCliente.SegundoApellido != null) pSegundoApellido.Value = pAseEntidadCliente.SegundoApellido; else pSegundoApellido.Value = DBNull.Value;
                        pPrimerNombre.DbType = DbType.String;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "P_CODTIPOIDENTIFICACION";
                        pTipoDocumento.Value = pAseEntidadCliente.TipoIdentificacion.IdTipoIdentificacion;
                        pTipoDocumento.DbType = DbType.Int64;

                        DbParameter pNumeroDocumento = cmdTransaccionFactory.CreateParameter();
                        pNumeroDocumento.Direction = ParameterDirection.Input;
                        pNumeroDocumento.ParameterName = "P_SIDENTIFICACION";
                        pNumeroDocumento.Value = pAseEntidadCliente.NumeroDocumento;
                        pNumeroDocumento.DbType = DbType.String;

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.Direction = ParameterDirection.Input;
                        pDireccion.ParameterName = "P_SDIRECCION";
                        pDireccion.Value = pAseEntidadCliente.Direccion;
                        pDireccion.DbType = DbType.String;

                        DbParameter pZona = cmdTransaccionFactory.CreateParameter();
                        pZona.Direction = ParameterDirection.Input;
                        pZona.ParameterName = "P_ICODZONA";
                        pZona.Value = pAseEntidadCliente.Zona.IdZona;
                        pZona.DbType = DbType.Int64;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.Direction = ParameterDirection.Input;
                        pTelefono.ParameterName = "P_STELEFONO";
                        pTelefono.Value = pAseEntidadCliente.Telefono;
                        pTelefono.DbType = DbType.String;

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.Direction = ParameterDirection.Input;
                        pEmail.ParameterName = "P_SMAIL";
                        pEmail.Value = pAseEntidadCliente.Email;
                        pEmail.DbType = DbType.String;

                        DbParameter pFechaRegistro = cmdTransaccionFactory.CreateParameter();
                        pFechaRegistro.ParameterName = "P_FREGISTRO";
                        pFechaRegistro.Direction = ParameterDirection.Input;
                        pFechaRegistro.Value = pAseEntidadCliente.FechaRegistro;
                        pFechaRegistro.DbType = DbType.Date;

                        DbParameter pRazonSocial = cmdTransaccionFactory.CreateParameter();
                        pRazonSocial.Direction = ParameterDirection.Input;
                        pRazonSocial.ParameterName = "P_SRAZONSOC";
                        if (pAseEntidadCliente.RazonSocial != null) pRazonSocial.Value = pAseEntidadCliente.RazonSocial; else pRazonSocial.Value = DBNull.Value;
                        pRazonSocial.DbType = DbType.String;

                        DbParameter pSiglaNegocio = cmdTransaccionFactory.CreateParameter();
                        pSiglaNegocio.Direction = ParameterDirection.Input;
                        pSiglaNegocio.ParameterName = "P_SSIGLA";
                        if (pAseEntidadCliente.SiglaNegocio != null) pSiglaNegocio.Value = pAseEntidadCliente.SiglaNegocio; else pSiglaNegocio.Value = DBNull.Value;
                        pSiglaNegocio.DbType = DbType.String;

                        DbParameter pCodigoActividad = cmdTransaccionFactory.CreateParameter();
                        pCodigoActividad.Direction = ParameterDirection.Input;
                        pCodigoActividad.ParameterName = "P_CODACTIVIDAD";
                        pCodigoActividad.Value = pAseEntidadCliente.Actividad.IdActividad;
                        pCodigoActividad.DbType = DbType.Int64;

                        DbParameter P_codusuario = cmdTransaccionFactory.CreateParameter();
                        P_codusuario.Direction = ParameterDirection.Input;
                        P_codusuario.ParameterName = "P_CODUSUARIO";
                        P_codusuario.Value = pAseEntidadCliente.codasesor;
                        P_codusuario.DbType = DbType.Int64;
                            
                        DbParameter pIdCliente = cmdTransaccionFactory.CreateParameter();
                        pIdCliente.Direction = ParameterDirection.Output;
                        pIdCliente.ParameterName = "P_IDCODIGO";
                        pIdCliente.DbType = DbType.Int32;

                       

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
                            DAauditoria.InsertarLog(pAseEntidadCliente, "ASCLIENTES", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

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
                        ClientePotencial pAseEntidadCliente = new ClientePotencial();

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "P_IDCODIGO";
                        pIdPrograma.Value = pIdCliente;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "EliminarCliente", ex);
                    }
                }
            }
        }
        public void LimpiarClientes( Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM ASAUDAFILIACION";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        sql= "DELETE FROM ASCLIENTES";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "LimpiarClientes", ex);
                    }
                }
            }
        }

        public ClientePotencial ConsultarCliente(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            ClientePotencial EntityCliente = new ClientePotencial();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pIdAseCliente = cmdTransaccionFactory.CreateParameter();
                        //pIdAseCliente.ParameterName = "P_IDCLIENTE";
                        //pIdAseCliente.Direction = ParameterDirection.Input;
                        //pIdAseCliente.Value = pIdAseEntiCliente;

                        //OracleParameter pData = (OracleParameter)cmdTransaccionFactory.CreateParameter();
                        //pData.ParameterName = "P_DATA";
                        //pData.Direction = ParameterDirection.Output;
                        //pData.OracleType = OracleType.Cursor;

                        //cmdTransaccionFactory.Parameters.Add(pIdAseCliente);
                        //cmdTransaccionFactory.Parameters.Add(pData);

                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_CONSULTAR";
                        //reader = cmdTransaccionFactory.ExecuteReader();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT  c.icodigo, c.snombre1, c.snombre2, c.sapellido1, c.sapellido2, c.codtipoidentificacion, t.descripcion nombIdentificacion,
		                        c.sidentificacion, c.sdireccion, c.icodzona, z.nomciudad nombZona, c.stelefono, c.semail, c.fregistro, c.srazonsoc, 		  
                                c.ssigla, c.codactividad, (select a.descripcion from actividad a where a.codactividad = to_char( c.codactividad))as nombActividad
                                FROM    asclientes c INNER JOIN tipoidentificacion t ON c.codtipoidentificacion = t.codtipoidentificacion 
                                INNER JOIN ciudades z ON z.CODCIUDAD = c.icodzona
                                WHERE c.ICODIGO = " + pIdAseEntiCliente;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["ICODIGO"] != DBNull.Value) EntityCliente.IdCliente = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["SNOMBRE1"] != DBNull.Value) EntityCliente.PrimerNombre = reader["SNOMBRE1"].ToString();
                            if (reader["SNOMBRE2"] != DBNull.Value) EntityCliente.SegundoNombre = reader["SNOMBRE2"].ToString();
                            if (reader["SAPELLIDO1"] != DBNull.Value) EntityCliente.PrimerApellido = reader["SAPELLIDO1"].ToString();
                            if (reader["SAPELLIDO2"] != DBNull.Value) EntityCliente.SegundoApellido = reader["SAPELLIDO2"].ToString();
                            if (reader["SIDENTIFICACION"] != DBNull.Value) EntityCliente.NumeroDocumento = Convert.ToInt64(reader["SIDENTIFICACION"].ToString());
                            if (reader["SDIRECCION"] != DBNull.Value) EntityCliente.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) EntityCliente.Telefono = Convert.ToString(reader["STELEFONO"].ToString());
                            if (reader["SEMAIL"] != DBNull.Value) EntityCliente.Email = reader["SEMAIL"].ToString();
                            if (reader["FREGISTRO"] != DBNull.Value) EntityCliente.FechaRegistro = Convert.ToDateTime(reader["FREGISTRO"].ToString());
                            if (reader["SRAZONSOC"] != DBNull.Value) EntityCliente.RazonSocial = reader["SRAZONSOC"].ToString();
                            if (reader["SSIGLA"] != DBNull.Value) EntityCliente.SiglaNegocio = reader["SSIGLA"].ToString();
                            if (reader["icodzona"] != DBNull.Value) EntityCliente.Zona.IdZona = Convert.ToInt64(reader["icodzona"].ToString());
                            if (reader["nombZona"] != DBNull.Value) EntityCliente.Zona.NombreZona = reader["nombZona"].ToString();
                            if (reader["codactividad"] != DBNull.Value) EntityCliente.Actividad.IdActividad = Convert.ToInt64(reader["codactividad"].ToString());
                            if (reader["nombActividad"] != DBNull.Value) EntityCliente.Actividad.NombreActividad = reader["nombActividad"].ToString();
                            if (reader["codtipoidentificacion"] != DBNull.Value) EntityCliente.TipoIdentificacion.IdTipoIdentificacion = Convert.ToInt64(reader["codtipoidentificacion"].ToString());
                            if (reader["nombIdentificacion"] != DBNull.Value) EntityCliente.TipoIdentificacion.NombreTipoIdentificacion = reader["nombIdentificacion"].ToString();

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return EntityCliente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ConsultarPrograma", ex);
                        return null;
                    }
                }
            }

        }

        public List<ClientePotencial> ListarCliente(ClientePotencial pAseEntidadCliente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClientePotencial> lstPrograma = new List<ClientePotencial>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM VAsesoresCliente " + Filtrar(pAseEntidadCliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClientePotencial entidad = new ClientePotencial();

                            if (resultado["icodigo"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["icodigo"]);
                            if (resultado["SNOMBRE1"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["SNOMBRE1"]);
                            if (resultado["SNOMBRE2"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["SNOMBRE2"]);
                            if (resultado["SAPELLIDO1"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["SAPELLIDO1"]);
                            if (resultado["SAPELLIDO2"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["SAPELLIDO2"]);
                            if (resultado["SDIRECCION"] != DBNull.Value) entidad.Direccion = (resultado["SDIRECCION"]).ToString();
                            if (resultado["STELEFONO"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["STELEFONO"]);
                            if (resultado["SEMAIL"] != DBNull.Value) entidad.Email = resultado["SEMAIL"].ToString();
                            if (resultado["FREGISTRO"] != DBNull.Value) entidad.FechaRegistro = Convert.ToDateTime(resultado["FREGISTRO"]);
                            if (resultado["SRAZONSOC"] != DBNull.Value) entidad.RazonSocial = resultado["SRAZONSOC"].ToString();
                            if (resultado["SSIGLA"] != DBNull.Value) entidad.SiglaNegocio = resultado["SSIGLA"].ToString();
                            if (resultado["SIDENTIFICACION"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToInt64(resultado["SIDENTIFICACION"].ToString());
                            if (resultado["codtipoidentificacion"] != DBNull.Value) entidad.TipoIdentificacion.IdTipoIdentificacion = Convert.ToInt64(resultado["codtipoidentificacion"].ToString());
                            if (resultado["nombIdentificacion"] != DBNull.Value) entidad.TipoIdentificacion.NombreTipoIdentificacion = resultado["nombIdentificacion"].ToString();
                            if (resultado["ICODZONA"] != DBNull.Value) entidad.Zona.IdZona = Convert.ToInt64(resultado["ICODZONA"].ToString());
                            if (resultado["codactividad"] != DBNull.Value) entidad.Actividad.IdActividad = Convert.ToInt64(resultado["codactividad"].ToString());

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

        public ClientePotencial ActualizarCliente(ClientePotencial pAseEntidadCliente, Usuario pUsuario)
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
                        if (pAseEntidadCliente.SegundoNombre != null) pSegundoNombre.Value = pAseEntidadCliente.SegundoNombre; else pSegundoNombre.Value = DBNull.Value;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "P_SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntidadCliente.PrimerApellido;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.Direction = ParameterDirection.Input;
                        pSegundoApellido.ParameterName = "P_SAPELLIDO2";
                        if (pAseEntidadCliente.SegundoApellido != null) pSegundoApellido.Value = pAseEntidadCliente.SegundoApellido; else pSegundoApellido.Value = DBNull.Value;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "P_CODTIPOIDENTIFICACION";
                        pTipoDocumento.Value = pAseEntidadCliente.TipoIdentificacion.IdTipoIdentificacion;

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
                        pZona.Value = pAseEntidadCliente.Zona.IdZona;

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
                        if (pAseEntidadCliente.RazonSocial != null) pRazonSocial.Value = pAseEntidadCliente.RazonSocial; else pRazonSocial.Value = DBNull.Value;

                        DbParameter pSiglaNegocio = cmdTransaccionFactory.CreateParameter();
                        pSiglaNegocio.Direction = ParameterDirection.Input;
                        pSiglaNegocio.ParameterName = "P_SSIGLA";
                        if (pAseEntidadCliente.SiglaNegocio != null) pSiglaNegocio.Value = pAseEntidadCliente.SiglaNegocio; else pSiglaNegocio.Value = DBNull.Value;
                        
                        DbParameter pCodigoActividad = cmdTransaccionFactory.CreateParameter();
                        pCodigoActividad.Direction = ParameterDirection.Input;
                        pCodigoActividad.ParameterName = "P_CODACTIVIDAD";
                        pCodigoActividad.Value = pAseEntidadCliente.Actividad.IdActividad;

                        DbParameter P_codusuario = cmdTransaccionFactory.CreateParameter();
                        P_codusuario.Direction = ParameterDirection.Input;
                        P_codusuario.ParameterName = "P_codusuario";
                        P_codusuario.Value = pAseEntidadCliente.codasesor;

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

        public string Filtrar(ClientePotencial pEntidad)
        {

            string iniSql = " WHERE ";
            string str = "";
            bool flag = false;
            PropertyInfo[] propertyInfos;

            if (pEntidad != null)
            {
                propertyInfos = pEntidad.GetType().GetProperties();

                foreach (PropertyInfo property in propertyInfos)
                {
                    if (property.Name.Equals("PrimerNombre") && property.GetValue(pEntidad, null) != null)
                    {
                        if (flag) str += " AND ";
                        str += " SNOMBRE1 like '%" + property.GetValue(pEntidad, null) + "%' ";
                        flag = true;
                    }

                    if (property.Name.Equals("PrimerApellido") && property.GetValue(pEntidad, null) != null)
                    {
                        if (flag) str += " AND ";
                        str += " SAPELLIDO1 like '%" + property.GetValue(pEntidad, null) + "%' ";
                        flag = true;
                    }

                    if (property.Name.Equals("NumeroDocumento") && property.GetValue(pEntidad, null) != null && property.GetValue(pEntidad, null).ToString().Trim() != "0")
                    {
                        if (flag) str += " AND ";
                        str += " SIDENTIFICACION = " + property.GetValue(pEntidad, null);
                        flag = true;
                    }
                    //aseEntCliente.Zona.IdZona
                    if (property.Name.Equals("Zona"))
                    {
                        if (!pEntidad.Zona.IdZona.Equals(null) && !pEntidad.Zona.IdZona.Equals(0))
                        {
                            if (flag) str += " AND ";
                            str += " ICODZONA = " + pEntidad.Zona.IdZona;
                            flag = true;
                        }
                    }
                }
            }
            if (str != "")
                return iniSql + str;
            else
                return "";
        }
        public ClientePotencial ConsultarClienteyaExistente(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            ClientePotencial EntityCliente = new ClientePotencial();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pIdAseCliente = cmdTransaccionFactory.CreateParameter();
                        //pIdAseCliente.ParameterName = "P_IDCLIENTE";
                        //pIdAseCliente.Direction = ParameterDirection.Input;
                        //pIdAseCliente.Value = pIdAseEntiCliente;

                        //OracleParameter pData = (OracleParameter)cmdTransaccionFactory.CreateParameter();
                        //pData.ParameterName = "P_DATA";
                        //pData.Direction = ParameterDirection.Output;
                        //pData.OracleType = OracleType.Cursor;

                        //cmdTransaccionFactory.Parameters.Add(pIdAseCliente);
                        //cmdTransaccionFactory.Parameters.Add(pData);

                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTE_CONSULTAR";
                        //reader = cmdTransaccionFactory.ExecuteReader();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT  c.icodigo, c.snombre1, c.snombre2, c.sapellido1, c.sapellido2, c.codtipoidentificacion, t.descripcion nombIdentificacion,
		                        c.sidentificacion, c.sdireccion, c.icodzona, z.nomciudad nombZona, c.stelefono, c.semail, c.fregistro, c.srazonsoc, 		  
                                c.ssigla, c.codactividad, (select a.descripcion from actividad a where a.codactividad = c.codactividad)as nombActividad
                                FROM    asclientes c INNER JOIN tipoidentificacion t ON c.codtipoidentificacion = t.codtipoidentificacion 
                                INNER JOIN ciudades z ON z.CODCIUDAD = c.icodzona
                                WHERE c.ICODIGO = " + pIdAseEntiCliente;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["ICODIGO"] != DBNull.Value) EntityCliente.IdCliente = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["SNOMBRE1"] != DBNull.Value) EntityCliente.PrimerNombre = reader["SNOMBRE1"].ToString();
                            if (reader["SNOMBRE2"] != DBNull.Value) EntityCliente.SegundoNombre = reader["SNOMBRE2"].ToString();
                            if (reader["SAPELLIDO1"] != DBNull.Value) EntityCliente.PrimerApellido = reader["SAPELLIDO1"].ToString();
                            if (reader["SAPELLIDO2"] != DBNull.Value) EntityCliente.SegundoApellido = reader["SAPELLIDO2"].ToString();
                            if (reader["SIDENTIFICACION"] != DBNull.Value) EntityCliente.NumeroDocumento = Convert.ToInt64(reader["SIDENTIFICACION"].ToString());
                            if (reader["SDIRECCION"] != DBNull.Value) EntityCliente.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) EntityCliente.Telefono = Convert.ToString(reader["STELEFONO"].ToString());
                            if (reader["SEMAIL"] != DBNull.Value) EntityCliente.Email = reader["SEMAIL"].ToString();
                            if (reader["FREGISTRO"] != DBNull.Value) EntityCliente.FechaRegistro = Convert.ToDateTime(reader["FREGISTRO"].ToString());
                            if (reader["SRAZONSOC"] != DBNull.Value) EntityCliente.RazonSocial = reader["SRAZONSOC"].ToString();
                            if (reader["SSIGLA"] != DBNull.Value) EntityCliente.SiglaNegocio = reader["SSIGLA"].ToString();
                            if (reader["icodzona"] != DBNull.Value) EntityCliente.Zona.IdZona = Convert.ToInt64(reader["icodzona"].ToString());
                            if (reader["nombZona"] != DBNull.Value) EntityCliente.Zona.NombreZona = reader["nombZona"].ToString();
                            if (reader["codactividad"] != DBNull.Value) EntityCliente.Actividad.IdActividad = Convert.ToInt64(reader["codactividad"].ToString());
                            if (reader["nombActividad"] != DBNull.Value) EntityCliente.Actividad.NombreActividad = reader["nombActividad"].ToString();
                            if (reader["codtipoidentificacion"] != DBNull.Value) EntityCliente.TipoIdentificacion.IdTipoIdentificacion = Convert.ToInt64(reader["codtipoidentificacion"].ToString());
                            if (reader["nombIdentificacion"] != DBNull.Value) EntityCliente.TipoIdentificacion.NombreTipoIdentificacion = reader["nombIdentificacion"].ToString();

                        }
                        else
                        {
                            EntityCliente.NumeroDocumento = 000;
                        }
                        return EntityCliente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ConsultarPrograma", ex);
                        return null;
                    }
                }
            }

        }

        public ClientePotencial ConsultarUsuario(Int64 pIdUsuario, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            ClientePotencial EntityCliente = new ClientePotencial();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                 
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT  CODUSUARIO, IDENTIFICACION,NOMBRE from usuarios WHERE identificacion = '" + pIdUsuario + "'";
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["CODUSUARIO"] != DBNull.Value) EntityCliente.IdUsuario = Convert.ToInt64(reader["CODUSUARIO"].ToString());
                            if (reader["IDENTIFICACION"] != DBNull.Value) EntityCliente.Identificacion = reader["IDENTIFICACION"].ToString();
                            if (reader["NOMBRE"] != DBNull.Value) EntityCliente.Nombre = reader["NOMBRE"].ToString();
                           
                        }
                        
                        return EntityCliente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }

        }
    }
}