using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para centros de costo
    /// </summary>    
    public class ParametrosAfiliacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para centros de costo
        /// </summary>
        public ParametrosAfiliacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ParametrosAfiliacion CrearParametrosAfiliacion(ParametrosAfiliacion pParam, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametros = cmdTransaccionFactory.CreateParameter();
                        pidparametros.ParameterName = "p_idparametros";
                        pidparametros.Value = pParam.idparametros;
                        if (opcion == 1)
                            pidparametros.Direction = ParameterDirection.Output;
                        else
                            pidparametros.Direction = ParameterDirection.Input;
                        pidparametros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametros);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pParam.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter ptipo_calculo = cmdTransaccionFactory.CreateParameter();
                        ptipo_calculo.ParameterName = "p_tipo_calculo";
                        ptipo_calculo.Value = pParam.tipo_calculo;
                        ptipo_calculo.Direction = ParameterDirection.Input;
                        ptipo_calculo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_calculo);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pParam.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pParam.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pParam.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1) // crear
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PARAMAFILI_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PARAMAFILI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pParam.idparametros = Convert.ToInt32(pidparametros.Value);
                        return pParam;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAfiliacionData", "CrearParametrosAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public ParametrosAfiliacion ConsultarParametrosAfiliacion(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ParametrosAfiliacion entidad = new ParametrosAfiliacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Parametrosafiliacion";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETROS"] != DBNull.Value) entidad.idparametros = Convert.ToInt32(resultado["IDPARAMETROS"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["TIPO_CALCULO"] != DBNull.Value) entidad.tipo_calculo = Convert.ToInt32(resultado["TIPO_CALCULO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAfiliacionData", "ConsultarParametrosAfiliacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<ParametrosAfiliacion> ListarParametrosAfiliacion(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrosAfiliacion> lstParam = new List<ParametrosAfiliacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select A.*,P.Descripcion From Parametrosafiliacion A left join Periodicidad P on P.Cod_Periodicidad = A.Cod_Periodicidad "
                                      +"where 1 = 1 " + filtro + " ORDER BY A.IDPARAMETROS ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ParametrosAfiliacion entidad = new ParametrosAfiliacion();
                            if (resultado["IDPARAMETROS"] != DBNull.Value) entidad.idparametros = Convert.ToInt32(resultado["IDPARAMETROS"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["TIPO_CALCULO"] != DBNull.Value) entidad.tipo_calculo = Convert.ToInt32(resultado["TIPO_CALCULO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["DESCRIPCION"]);
                            lstParam.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParam;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAfiliacionData", "ListarParametrosAfiliacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarParametrosAfiliacion(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "delete from Parametrosafiliacion where Idparametros = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAfiliacionData", "EliminarParametrosAfiliacion", ex);
                    }
                }
            }
        }


        public PersonaActualizacion CrearPersona_Actualizacion(PersonaActualizacion pPersona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pPersona.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Output;
                        pidconsecutivo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pPersona.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pPersona.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        if (pPersona.primer_nombre == null)
                            pprimer_nombre.Value = DBNull.Value;
                        else
                            pprimer_nombre.Value = pPersona.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pPersona.segundo_nombre == null)
                            psegundo_nombre.Value = DBNull.Value;
                        else
                            psegundo_nombre.Value = pPersona.segundo_nombre;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        if (pPersona.primer_apellido == null)
                            pprimer_apellido.Value = DBNull.Value;
                        else
                            pprimer_apellido.Value = pPersona.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pPersona.segundo_apellido == null)
                            psegundo_apellido.Value = DBNull.Value;
                        else
                            psegundo_apellido.Value = pPersona.segundo_apellido;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter pcodciudadresidencia = cmdTransaccionFactory.CreateParameter();
                        pcodciudadresidencia.ParameterName = "p_codciudadresidencia";
                        if (pPersona.codciudadresidencia == 0)
                            pcodciudadresidencia.Value = DBNull.Value;
                        else
                            pcodciudadresidencia.Value = pPersona.codciudadresidencia;
                        pcodciudadresidencia.Direction = ParameterDirection.Input;
                        pcodciudadresidencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadresidencia);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pPersona.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pPersona.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pPersona.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pPersona.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pciudadempresa = cmdTransaccionFactory.CreateParameter();
                        pciudadempresa.ParameterName = "p_ciudadempresa";
                        if (pPersona.ciudadempresa == 0)
                            pciudadempresa.Value = DBNull.Value;
                        else
                            pciudadempresa.Value = pPersona.ciudadempresa;
                        pciudadempresa.Direction = ParameterDirection.Input;
                        pciudadempresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pciudadempresa);

                        DbParameter pdireccionempresa = cmdTransaccionFactory.CreateParameter();
                        pdireccionempresa.ParameterName = "p_direccionempresa";
                        if (pPersona.direccionempresa == null)
                            pdireccionempresa.Value = DBNull.Value;
                        else
                            pdireccionempresa.Value = pPersona.direccionempresa;
                        pdireccionempresa.Direction = ParameterDirection.Input;
                        pdireccionempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccionempresa);

                        DbParameter ptelefonoempresa = cmdTransaccionFactory.CreateParameter();
                        ptelefonoempresa.ParameterName = "p_telefonoempresa";
                        if (pPersona.telefonoempresa == null)
                            ptelefonoempresa.Value = DBNull.Value;
                        else
                            ptelefonoempresa.Value = pPersona.telefonoempresa;
                        ptelefonoempresa.Direction = ParameterDirection.Input;
                        ptelefonoempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefonoempresa);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pPersona.email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pPersona.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pPersona.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcelular = cmdTransaccionFactory.CreateParameter();
                        pcelular.ParameterName = "p_celular";
                        if (string.IsNullOrEmpty(pPersona.celular)) pcelular.Value = DBNull.Value; else pcelular.Value = pPersona.celular;
                        pcelular.Direction = ParameterDirection.Input;
                        pcelular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelular);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_AC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAfiliacionData", "CrearPersona_Actualizacion", ex);
                        return null;
                    }
                }
            }
        }


        public PersonaActualizacion ConsultarPersona_actualizacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            PersonaActualizacion entidad = new PersonaActualizacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PERSONA_ACTUALIZACION WHERE COD_PERSONA = " + pId.ToString() + " AND ROWNUM = 1 ORDER BY 1 DESC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToDecimal(resultado["IDCONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt32(resultado["CIUDADEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("personaData", "ConsultarPersona_actualizacion", ex);
                        return null;
                    }
                }
            }
        }



        public PersonaActualizacion ModificarPersona_Actualizacion(PersonaActualizacion pPersona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pPersona.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Input;
                        pidconsecutivo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter(); 
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pPersona.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pPersona.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        if (pPersona.primer_nombre == null)
                            pprimer_nombre.Value = DBNull.Value;
                        else
                            pprimer_nombre.Value = pPersona.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pPersona.segundo_nombre == null)
                            psegundo_nombre.Value = DBNull.Value;
                        else
                            psegundo_nombre.Value = pPersona.segundo_nombre;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        if (pPersona.primer_apellido == null)
                            pprimer_apellido.Value = DBNull.Value;
                        else
                            pprimer_apellido.Value = pPersona.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pPersona.segundo_apellido == null)
                            psegundo_apellido.Value = DBNull.Value;
                        else
                            psegundo_apellido.Value = pPersona.segundo_apellido;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter pcodciudadresidencia = cmdTransaccionFactory.CreateParameter();
                        pcodciudadresidencia.ParameterName = "p_codciudadresidencia";
                        if (pPersona.codciudadresidencia == 0)
                            pcodciudadresidencia.Value = DBNull.Value;
                        else
                            pcodciudadresidencia.Value = pPersona.codciudadresidencia;
                        pcodciudadresidencia.Direction = ParameterDirection.Input;
                        pcodciudadresidencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadresidencia);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pPersona.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pPersona.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pPersona.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pPersona.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pciudadempresa = cmdTransaccionFactory.CreateParameter();
                        pciudadempresa.ParameterName = "p_ciudadempresa";
                        if (pPersona.ciudadempresa == 0)
                            pciudadempresa.Value = DBNull.Value;
                        else
                            pciudadempresa.Value = pPersona.ciudadempresa;
                        pciudadempresa.Direction = ParameterDirection.Input;
                        pciudadempresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pciudadempresa);

                        DbParameter pdireccionempresa = cmdTransaccionFactory.CreateParameter();
                        pdireccionempresa.ParameterName = "p_direccionempresa";
                        if (pPersona.direccionempresa == null)
                            pdireccionempresa.Value = DBNull.Value;
                        else
                            pdireccionempresa.Value = pPersona.direccionempresa;
                        pdireccionempresa.Direction = ParameterDirection.Input;
                        pdireccionempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccionempresa);

                        DbParameter ptelefonoempresa = cmdTransaccionFactory.CreateParameter();
                        ptelefonoempresa.ParameterName = "p_telefonoempresa";
                        if (pPersona.telefonoempresa == null)
                            ptelefonoempresa.Value = DBNull.Value;
                        else
                            ptelefonoempresa.Value = pPersona.telefonoempresa;
                        ptelefonoempresa.Direction = ParameterDirection.Input;
                        ptelefonoempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefonoempresa);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pPersona.email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pPersona.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pPersona.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = vUsuario.nombre;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pcelular = cmdTransaccionFactory.CreateParameter();
                        pcelular.ParameterName = "p_celular";
                        if (string.IsNullOrEmpty(pPersona.celular)) pcelular.Value = DBNull.Value; else pcelular.Value = pPersona.celular;
                        pcelular.Direction = ParameterDirection.Input;
                        pcelular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelular);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_AC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAfiliacionData", "ModificarPersona_Actualizacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPersona_Actualizacion(decimal pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pId;
                        pidconsecutivo.Direction = ParameterDirection.Input;
                        pidconsecutivo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_AC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActualizarData", "EliminarPersona_Actualizacion", ex);
                    }
                }
            }
        }



    }
}
