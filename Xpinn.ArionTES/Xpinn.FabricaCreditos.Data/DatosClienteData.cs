using System;
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
    /// Objeto de acceso a datos para la tabla Programa
    /// </summary>
    public class DatosClienteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public DatosClienteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Cliente en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cliente</param>
        /// <returns>Entidad creada</returns>
        public DatosCliente  CrearCliente(DatosCliente pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pId = cmdTransaccionFactory.CreateParameter();
                        pId.ParameterName = "pIdc";
                        pId.Value = 0;
                        pId.Direction = ParameterDirection.InputOutput;

                        DbParameter pNombres = cmdTransaccionFactory.CreateParameter();
                        pNombres.ParameterName = "pNombres";
                        pNombres.Value = pEntidad.nombres;

                        DbParameter pApellidos = cmdTransaccionFactory.CreateParameter();
                        pApellidos.ParameterName = "pApellidos";
                        pApellidos.Value = pEntidad.apellidos;

                        DbParameter pTipo = cmdTransaccionFactory.CreateParameter();
                        pTipo.ParameterName = "pTipo";
                        pTipo.Value = pEntidad.tipo;

                        DbParameter pNumero = cmdTransaccionFactory.CreateParameter();
                        pNumero.ParameterName = "pNumero";
                        pNumero.Value = pEntidad.numero;

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.ParameterName = "pDireccion";
                        pDireccion.Value = pEntidad.direccion;

                        DbParameter pTelefonos = cmdTransaccionFactory.CreateParameter();
                        pTelefonos.ParameterName = "pTelefono";
                        pTelefonos.Value = pEntidad.telefono;

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.ParameterName = "pEmail";
                        pEmail.Value = pEntidad.email;

                        DbParameter pRazon = cmdTransaccionFactory.CreateParameter();
                        pRazon.ParameterName = "pRazon";
                        pRazon.Value = pEntidad.razon;

                        DbParameter pActividad = cmdTransaccionFactory.CreateParameter();
                        pActividad.ParameterName = "pActividad";
                        pActividad.Value = pEntidad.actividad;                      

                        DbParameter pFechaCrea = cmdTransaccionFactory.CreateParameter();
                        pFechaCrea.ParameterName = "pFechaCrea";
                        pFechaCrea.Value = pEntidad.FechaCrea;

                        DbParameter pUsuarioCrea = cmdTransaccionFactory.CreateParameter();
                        pUsuarioCrea.ParameterName = "pUsuarioCrea";
                        pUsuarioCrea.Value = pEntidad.UsuarioCrea;

                        DbParameter pFechaEdita = cmdTransaccionFactory.CreateParameter();
                        pFechaEdita.ParameterName = "pFechaEdita";
                        pFechaEdita.Value = pEntidad.FechaEdita;

                        DbParameter pUsuarioEdita = cmdTransaccionFactory.CreateParameter();
                        pUsuarioEdita.ParameterName = "pUsuarioEdita";
                        pUsuarioEdita.Value = pEntidad.UsuarioEdita;                       

                        cmdTransaccionFactory.Parameters.Add(pId);
                        cmdTransaccionFactory.Parameters.Add(pNombres);
                        cmdTransaccionFactory.Parameters.Add(pApellidos);
                        cmdTransaccionFactory.Parameters.Add(pTipo);
                        cmdTransaccionFactory.Parameters.Add(pNumero);
                        cmdTransaccionFactory.Parameters.Add(pDireccion);
                        cmdTransaccionFactory.Parameters.Add(pTelefonos);
                        cmdTransaccionFactory.Parameters.Add(pEmail);
                        cmdTransaccionFactory.Parameters.Add(pRazon);
                        cmdTransaccionFactory.Parameters.Add(pActividad);
                        cmdTransaccionFactory.Parameters.Add(pFechaCrea);
                        cmdTransaccionFactory.Parameters.Add(pUsuarioCrea);
                        cmdTransaccionFactory.Parameters.Add(pFechaEdita);
                        cmdTransaccionFactory.Parameters.Add(pUsuarioEdita);

             
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "usp_xpinn_pre_DatosClie_Crear";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntidad.idc = Convert.ToInt64(pId.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch 
                    {
                        //BOExcepcion.Throw("DatosData", "CrearCliente", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene un registro de la tabla Cliente de la base de datos
        /// </summary>
        /// <param name="cId">identificador del registro</param>
        /// <returns>Programa consultado</returns>
        public DatosCliente ConsultarCliente(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DatosCliente entidad = new DatosCliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM PERSONA where IDENTIFICACION='" + pId.ToString() + "'";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        connection.Open();
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.idc = Convert.ToInt64(resultado["COD_PERSONA"]);                           
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.numero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.razon = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.actividad = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value) entidad.lugarExpedicionInt = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);

                            //Detalle Persona
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaExpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value) entidad.lugarExpedicionInt = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primerNombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundoNombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primerApellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundoApellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechaNacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.lugarNacimientoInt = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.estadoCivilInt = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.nivelEscolaridadInt = Convert.ToInt64(resultado["CODESCOLARIDAD"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedad = Convert.ToString(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipoVivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendatario = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoArrendatario = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon = Convert.ToString(resultado["RAZON_SOCIAL"]);   
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
                        BOExcepcion.Throw("ProgramaData", "ConsultarPrograma", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Modifica una entidad Cliente en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cliente</param>
        /// <returns>Entidad modificada</returns>
        public DatosCliente ModificarCliente(DatosCliente pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pId = cmdTransaccionFactory.CreateParameter();
                        pId.ParameterName = "pId";
                        pId.Value = pEntidad.idc;

                        DbParameter pTipo = cmdTransaccionFactory.CreateParameter();
                        pTipo.ParameterName = "pTipo";
                        pTipo.Value = pEntidad.tipo;

                        DbParameter pFechaExpedicion = cmdTransaccionFactory.CreateParameter();
                        pFechaExpedicion.ParameterName = "pFechaExpedicion";
                        pFechaExpedicion.Value = pEntidad.fechaExpedicion;

                        DbParameter pLugarExpedicionInt = cmdTransaccionFactory.CreateParameter();
                        pLugarExpedicionInt.ParameterName = "pLugarExpedicionInt";
                        pLugarExpedicionInt.Value = pEntidad.lugarExpedicionInt;

                        DbParameter pSexo = cmdTransaccionFactory.CreateParameter();
                        pSexo.ParameterName = "pSexo";
                        pSexo.Value = pEntidad.sexo;

                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pPrimerNombre.ParameterName = "pPrimerNombre";
                        pPrimerNombre.Value = pEntidad.primerNombre;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pSegundoNombre.ParameterName = "pSegundoNombre";
                        pSegundoNombre.Value = pEntidad.segundoNombre;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.ParameterName = "pPrimerApellido";
                        pPrimerApellido.Value = pEntidad.primerApellido;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.ParameterName = "pSegundoApellido";
                        pSegundoApellido.Value = pEntidad.segundoApellido;

                        DbParameter pFechaNacimiento = cmdTransaccionFactory.CreateParameter();
                        pFechaNacimiento.ParameterName = "pFechaNacimiento";
                        pFechaNacimiento.Value = pEntidad.fechaNacimiento;

                        DbParameter pLugarNacimientoInt = cmdTransaccionFactory.CreateParameter();
                        pLugarNacimientoInt.ParameterName = "pLugarNacimientoInt";
                        pLugarNacimientoInt.Value = pEntidad.lugarNacimientoInt;

                        DbParameter pEstadoCivilInt = cmdTransaccionFactory.CreateParameter();
                        pEstadoCivilInt.ParameterName = "pEstadoCivilInt";
                        pEstadoCivilInt.Value = pEntidad.estadoCivilInt;

                        DbParameter pNivelEscolaridadInt = cmdTransaccionFactory.CreateParameter();
                        pNivelEscolaridadInt.ParameterName = "pNivelEscolaridadInt";
                        pNivelEscolaridadInt.Value = pEntidad.nivelEscolaridadInt;

                        DbParameter pActividad = cmdTransaccionFactory.CreateParameter();
                        pActividad.ParameterName = "pActividad";
                        pActividad.Value = pEntidad.actividad;

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.ParameterName = "pDireccion";
                        pDireccion.Value = pEntidad.direccion;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.ParameterName = "pTelefono";
                        pTelefono.Value = pEntidad.telefono;

                        DbParameter pAantiguedad = cmdTransaccionFactory.CreateParameter();
                        pAantiguedad.ParameterName = "pAantiguedad";
                        pAantiguedad.Value = pEntidad.antiguedad;

                        DbParameter pTipoVivienda = cmdTransaccionFactory.CreateParameter();
                        pTipoVivienda.ParameterName = "pTipoVivienda";
                        pTipoVivienda.Value = pEntidad.tipoVivienda;

                        DbParameter pArrendatario = cmdTransaccionFactory.CreateParameter();
                        pArrendatario.ParameterName = "pArrendatario";
                        pArrendatario.Value = pEntidad.arrendatario;

                        DbParameter pTelefonoArrendatario = cmdTransaccionFactory.CreateParameter();
                        pTelefonoArrendatario.ParameterName = "pTelefonoArrendatario";
                        pTelefonoArrendatario.Value = pEntidad.telefonoArrendatario;
                     
                        cmdTransaccionFactory.Parameters.Add(pId);
                        cmdTransaccionFactory.Parameters.Add(pTipo);
                        cmdTransaccionFactory.Parameters.Add(pFechaExpedicion);
                        cmdTransaccionFactory.Parameters.Add(pLugarExpedicionInt);
                        cmdTransaccionFactory.Parameters.Add(pSexo);
                        cmdTransaccionFactory.Parameters.Add(pPrimerNombre);
                        cmdTransaccionFactory.Parameters.Add(pSegundoNombre);
                        cmdTransaccionFactory.Parameters.Add(pPrimerApellido);
                        cmdTransaccionFactory.Parameters.Add(pSegundoApellido);
                        cmdTransaccionFactory.Parameters.Add(pFechaNacimiento);
                        cmdTransaccionFactory.Parameters.Add(pLugarNacimientoInt);
                        cmdTransaccionFactory.Parameters.Add(pEstadoCivilInt);
                        cmdTransaccionFactory.Parameters.Add(pNivelEscolaridadInt);
                        cmdTransaccionFactory.Parameters.Add(pActividad);
                        cmdTransaccionFactory.Parameters.Add(pDireccion);
                        cmdTransaccionFactory.Parameters.Add(pTelefono);
                        cmdTransaccionFactory.Parameters.Add(pAantiguedad);
                        cmdTransaccionFactory.Parameters.Add(pTipoVivienda);
                        cmdTransaccionFactory.Parameters.Add(pArrendatario);
                        cmdTransaccionFactory.Parameters.Add(pTelefonoArrendatario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_DATOSCLIE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosData", "ModificarDatos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un Cliente en la base de datos
        /// </summary>
        /// <param name="pId">Identificador del cliente</param>
        public void EliminarCliente(Int64 pId, Usuario pUsuario)
        {
            //using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            //{
            //    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            //    {
            //        try
            //        {
            //            DatosCliente pEntidad = new DatosCliente();

            //            if (pUsuario.programaGeneraLog)
            //                pEntidad = ConsultarCliente(pId, pUsuario); //REGISTRO DE AUDITORIA

            //            DbParameter pIdCliente = cmdTransaccionFactory.CreateParameter();
            //            pIdCliente.ParameterName = "pIdc";
            //            pIdCliente.Value = pId;

            //            cmdTransaccionFactory.Parameters.Add(pIdCliente);

            //            connection.Open();
            //            cmdTransaccionFactory.Connection = connection;
            //            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
            //            cmdTransaccionFactory.CommandText = "usp_xpinn_pre_DatosClie_Elimi";
            //            cmdTransaccionFactory.ExecuteNonQuery();

            //            if (pUsuario.programaGeneraLog)
            //                DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
            //        }
            //        catch (Exception ex)
            //        {
            //            BOExcepcion.Throw("DatosClienteData", "ElimiCliente", ex);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Obtiene un registro de la tabla Cliente de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Programa consultado</returns>
        /// 

        public List<DatosCliente> ListarCliente(DatosCliente pEntidad, Usuario pUsuario, String pId)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosCliente> lstCliente = new List<DatosCliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM PERSONA where IDENTIFICACION = " + pId.ToString();
                        
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        connection.Open();
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            DatosCliente entidad = new DatosCliente();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.idc = Convert.ToInt64(resultado["COD_PERSONA"]);                           
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.numero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.razon = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.actividad = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value) entidad.lugarExpedicionInt = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                                      
                            //Detalle Persona
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaExpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value) entidad.lugarExpedicionInt = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primerNombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundoNombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primerApellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundoApellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechaNacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.lugarNacimientoInt = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.estadoCivilInt = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.nivelEscolaridadInt = Convert.ToInt64(resultado["CODESCOLARIDAD"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedad = Convert.ToString(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipoVivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendatario = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoArrendatario = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                          
                            lstCliente.Add(entidad);
                        
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCliente;
                    }
                    catch 
                    {
                        //BOExcepcion.Throw("DatosData", "ConsultarCliente", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista del menu de actividades
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de listas obtenidas</returns>
        public List<DatosCliente> ListarActividades(DatosCliente pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosCliente> lstActividades = new List<DatosCliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM ACTIVIDAD ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosCliente entidad = new DatosCliente();

                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstActividades.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActividades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClientenData", "ListarActividades", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista del menu de tipos de identificacion
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidas</returns>
        public List<DatosCliente> ListarTiposDoc(DatosCliente pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosCliente> lstTiposDoc = new List<DatosCliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM TIPOIDENTIFICACION " ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosCliente entidad = new DatosCliente();

                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTiposDoc.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTiposDoc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClientenData", "ListarTiposDoc", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene las listas de Datos Solicitud
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<DatosCliente> ListasDesplegables(DatosCliente pEntidad, Usuario pUsuario, String ListaSolicitada)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosCliente> lstDatosSolicitud = new List<DatosCliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        switch (ListaSolicitada)
                        {
                            case "TipoCredito":
                               sql = "select cod_linea_credito as ListaId, nombre as ListaDescripcion from lineascredito ";
                               break;

                            case "Periodicidad":
                               sql = "select cod_periodicidad as ListaId, descripcion as ListaDescripcion from periodicidad " ;
                               break;

                            case "Medio":
                               sql = "select idm as ListaId, descripcion as ListaDescripcion from medios ";
                               break;

                            case "TipoIdentificacion":
                               sql = "select CODTIPOIDENTIFICACION as ListaId, descripcion as ListaDescripcion from TIPOIDENTIFICACION " ;
                               break;

                            case "Lugares":
                               sql = "select CODCIUDAD  as ListaId, NOMCIUDAD as ListaDescripcion from CIUDADES order by NOMCIUDAD asc";
                               break;

                            case "Ciudades":
                               sql = "select CODCIUDAD  as ListaId, NOMCIUDAD as ListaDescripcion from CIUDADES Where tipo = 3 order by NOMCIUDAD asc";
                               break;

                            case "EstadoCivil":
                               sql = "select CODESTADOCIVIL as ListaId,  DESCRIPCION as ListaDescripcion from ESTADOCIVIL " ;
                               break;

                            case "NivelEscolaridad":
                               sql = "select CODESCOLARIDAD as ListaId, DESCRIPCION as ListaDescripcion from NIVELESCOLARIDAD ";
                               break;

                            case "Actividad":
                               sql = "select CODACTIVIDAD as ListaId, DESCRIPCION as ListaDescripcion from Actividad ";
                               break;

                            case "TipoCreditoMicro":
                               sql = "select cod_linea_credito as ListaId, nombre as ListaDescripcion from lineascredito where cod_clasifica = 3";
                               break;

                            case "TipoLiquidacion":
                               sql = "select tipo_liquidacion as ListaId, descripcion as ListaDescripcion from TipoLiquidacion";
                               break;

                        }
                        
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DatosCliente entidad = new DatosCliente();
                            if (ListaSolicitada == "TipoCreditoMicro" || ListaSolicitada == "TipoCredito" || ListaSolicitada == "Periodicidad" || ListaSolicitada == "Medio" || ListaSolicitada == "Lugares")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                                {if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]);}
                            else
                                {if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]);}                            

                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstDatosSolicitud.Add(entidad);
                        }
                        return lstDatosSolicitud; 

                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClientenData", "ListasDesplegables", ex);
                        return null;
                    }
                }
            }
        }
        
    }
}


