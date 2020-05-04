using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class EmpleadoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public EmpleadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Empleados CrearEmpleados(Empleados pEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEmpleados.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        //DbParameter pnomina_emp = cmdTransaccionFactory.CreateParameter();
                        //pnomina_emp.ParameterName = "p_cod_nomi_emp";
                        //pnomina_emp.Value = Convert.ToInt64(pEmpleados.cod_nomina_emp);
                        //pnomina_emp.Direction = ParameterDirection.Input;
                        //pnomina_emp.DbType = DbType.Int64;
                        //cmdTransaccionFactory.Parameters.Add(pnomina_emp);

                        //DbParameter ptipo_sueldo = cmdTransaccionFactory.CreateParameter();
                        //ptipo_sueldo.ParameterName = "p_tipo_sueldo";
                        //if (pEmpleados.tipo_sueldo == null)
                        //    ptipo_sueldo.Value = DBNull.Value;
                        //else
                        //    ptipo_sueldo.Value = pEmpleados.tipo_sueldo;
                        //ptipo_sueldo.Direction = ParameterDirection.Input;
                        //ptipo_sueldo.DbType = DbType.String;
                        //cmdTransaccionFactory.Parameters.Add(ptipo_sueldo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEADOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEmpleados.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "CrearEmpleados", ex);
                        return null;
                    }
                }
            }
        }
        public Empleados CrearPersonaEmpleados(Empleados pEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo_empleado = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo_empleado.ParameterName = "p_consecutivo_empleado";
                        pconsecutivo_empleado.Value = pEmpleados.consecutivo_empleado;
                        pconsecutivo_empleado.Direction = ParameterDirection.Input;
                        pconsecutivo_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo_empleado);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEmpleados.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_sangre = cmdTransaccionFactory.CreateParameter();
                        ptipo_sangre.ParameterName = "p_tipo_sangre";
                        ptipo_sangre.Value = pEmpleados.tipo_sangre;
                        ptipo_sangre.Direction = ParameterDirection.Input;
                        ptipo_sangre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_sangre);


                        DbParameter pvisa = cmdTransaccionFactory.CreateParameter();
                        pvisa.ParameterName = "p_visa";
                        pvisa.Value = pEmpleados.visa;
                        pvisa.Direction = ParameterDirection.Input;
                        pvisa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvisa);



                        DbParameter pfecha_ven_visa = cmdTransaccionFactory.CreateParameter();
                        pfecha_ven_visa.ParameterName = "p_fecha_ven_visa";
                        pfecha_ven_visa.Value = pEmpleados.fecha_ven_visa;
                        pfecha_ven_visa.Direction = ParameterDirection.Input;
                        pfecha_ven_visa.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ven_visa);


                        DbParameter ppasaporte = cmdTransaccionFactory.CreateParameter();
                        ppasaporte.ParameterName = "p_pasaporte";
                        ppasaporte.Value = pEmpleados.pasaporte;
                        ppasaporte.Direction = ParameterDirection.Input;
                        ppasaporte.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppasaporte);



                        DbParameter pfecha_ven_pasaporte = cmdTransaccionFactory.CreateParameter();
                        pfecha_ven_pasaporte.ParameterName = "p_fecha_ven_pasaporte";
                        pfecha_ven_pasaporte.Value = pEmpleados.fecha_ven_pasaporte;
                        pfecha_ven_pasaporte.Direction = ParameterDirection.Input;
                        pfecha_ven_pasaporte.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ven_pasaporte);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMP_PERSO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEmpleados.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "CrearPersonaEmpleados", ex);
                        return null;
                    }
                }
            }
        }

        public List<NominaEmpleado> ListarConceptoNominasQueSeanHorasExtas(Usuario usuario)
        {
            DbDataReader resultado;
            List<NominaEmpleado> lstEntidad = new List<NominaEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select CONSECUTIVO, DESCRIPCION from concepto_nomina where TIPOCONCEPTO = 9 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            NominaEmpleado entidad = new NominaEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ListarConceptoNominasQueSeanHorasExtas", ex);
                        return null;
                    }
                }
            }
        }

        public Empleados ConsultarInformacionPersonaEmpleadoPorIdentificacion(string identificacion, Usuario usuario)
        {
            DbDataReader resultado;
            Empleados entidad = new Empleados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select emp.COD_PERSONA, emp.CONSECUTIVO,per.nombre,i.codigonomina,i.codigotipocontrato,i.codigocentrocosto
                                        from empleados emp
                                        JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                        LEFT JOIN  ingresopersonal i on i.codigopersona=per.COD_PERSONA
                                        where per.IDENTIFICACION = '" + identificacion.Trim() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                        }
                        else
                        {
                            // NO QUITAAAAAAARRRR
                            throw new InvalidOperationException("El registro no existe!.");
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ConsultarInformacionPersonaEmpleadoPorIdentificacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<NominaEmpleado> ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(long consecutivoEmpleado, Usuario usuario)
        {
            DbDataReader resultado;
            List<NominaEmpleado> lstEntidad = new List<NominaEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select nom.consecutivo, nom.descripcion
                                        from nomina_empleado nom
                                        JOIN IngresoPersonal ing on ing.CODIGONOMINA = nom.consecutivo 
                                        WHERE ing.CODIGOEMPLEADO = " + consecutivoEmpleado +
                                        " AND ing.ESTAACTIVOCONTRATO = 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            NominaEmpleado entidad = new NominaEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo", ex);
                        return null;
                    }
                }
            }
        }
        public List<NominaEmpleado> ListarNominasALasQuePerteneceUnEmpleado(long consecutivoEmpleado, Usuario usuario)
        {
            DbDataReader resultado;
            List<NominaEmpleado> lstEntidad = new List<NominaEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select nom.consecutivo, nom.descripcion
                                        from nomina_empleado nom
                                        JOIN IngresoPersonal ing on ing.CODIGONOMINA = nom.consecutivo 
                                        WHERE ing.CODIGOEMPLEADO = " + consecutivoEmpleado 
                                        ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            NominaEmpleado entidad = new NominaEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ListarNominasALasQuePerteneceUnEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public List<NominaEmpleado> ListarNominaEmpleados(Usuario pusuario)
        {
            DbDataReader resultado;
            List<NominaEmpleado> lstEntidad = new List<NominaEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * from nomina_empleado ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            NominaEmpleado entidad = new NominaEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ListarNominaEmpleados", ex);
                        return null;
                    }
                }
            }
        }

        public Empleados ConsultarInformacionPersonaEmpleado(long consecutivoEmpleado, Usuario usuario)
        {
            DbDataReader resultado;
            Empleados empleados = new Empleados();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select per.identificacion, per.TIPO_IDENTIFICACION, per.nombre, emp.consecutivo,
                                        emp.cod_persona,ip.fechaingreso, FECSUMDIA(ip.fechaingreso,360,1) as fechaprox ,IP.CODIGOCENTROCOSTO
                                        from empleados emp
                                        join v_persona per on per.cod_persona = emp.cod_persona
                                        left join INGRESOPERSONAL ip on ip.codigoempleado=emp.consecutivo
                                        where emp.consecutivo = " + consecutivoEmpleado;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["identificacion"] != DBNull.Value) empleados.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) empleados.cod_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["nombre"] != DBNull.Value) empleados.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["consecutivo"] != DBNull.Value) empleados.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["cod_persona"] != DBNull.Value) empleados.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["fechaingreso"] != DBNull.Value) empleados.fechainicioperiodo = Convert.ToDateTime(resultado["fechaingreso"]);
                            if (resultado["fechaprox"] != DBNull.Value) empleados.fechaterminacionperiodo = Convert.ToDateTime(resultado["fechaprox"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) empleados.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return empleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ConsultarInformacionPersonaEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarPersonaQueNoSeaEmpleadoYa(string cod_persona, Usuario pusuario)
        {
            DbDataReader resultado;
            bool existe = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT *
                                        FROM EMPLEADOS
                                        WHERE COD_PERSONA = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            existe = true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "VerificarPersonaQueNoSeaEmpleadoYa", ex);
                        return false;
                    }
                }
            }
        }

        public Empleados ModificarEmpleados(Empleados pEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEmpleados.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        //DbParameter pnomina_emp = cmdTransaccionFactory.CreateParameter();
                        //pnomina_emp.ParameterName = "p_cod_nomi_emp";
                        //pnomina_emp.Value = Convert.ToInt64(pEmpleados.cod_nomina_emp);
                        //pnomina_emp.Direction = ParameterDirection.Input;
                        //pnomina_emp.DbType = DbType.Int64;
                        //cmdTransaccionFactory.Parameters.Add(pnomina_emp);

                        //DbParameter ptipo_sueldo = cmdTransaccionFactory.CreateParameter();
                        //ptipo_sueldo.ParameterName = "p_tipo_sueldo";
                        //if (pEmpleados.tipo_sueldo == null)
                        //    ptipo_sueldo.Value = DBNull.Value;
                        //else
                        //    ptipo_sueldo.Value = pEmpleados.tipo_sueldo;
                        //ptipo_sueldo.Direction = ParameterDirection.Input;
                        //ptipo_sueldo.DbType = DbType.String;
                        //cmdTransaccionFactory.Parameters.Add(ptipo_sueldo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEADOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ModificarEmpleados", ex);
                        return null;
                    }
                }
            }
        }
        public Empleados ModificarPersonaEmpleados(Empleados pEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo_empleado = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo_empleado.ParameterName = "p_consecutivo_empleado";
                        pconsecutivo_empleado.Value = pEmpleados.consecutivo_empleado;
                        pconsecutivo_empleado.Direction = ParameterDirection.Input;
                        pconsecutivo_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo_empleado);
                       

                        DbParameter ptipo_sangre = cmdTransaccionFactory.CreateParameter();
                        ptipo_sangre.ParameterName = "p_tipo_sangre";
                        ptipo_sangre.Value = pEmpleados.tipo_sangre;
                        ptipo_sangre.Direction = ParameterDirection.Input;
                        ptipo_sangre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_sangre);


                        DbParameter pvisa = cmdTransaccionFactory.CreateParameter();
                        pvisa.ParameterName = "p_visa";
                        pvisa.Value = pEmpleados.visa;
                        pvisa.Direction = ParameterDirection.Input;
                        pvisa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvisa);



                        DbParameter pfecha_ven_visa = cmdTransaccionFactory.CreateParameter();
                        pfecha_ven_visa.ParameterName = "p_fecha_ven_visa";
                        pfecha_ven_visa.Value = pEmpleados.fecha_ven_visa;
                        pfecha_ven_visa.Direction = ParameterDirection.Input;
                        pfecha_ven_visa.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ven_visa);


                        DbParameter ppasaporte = cmdTransaccionFactory.CreateParameter();
                        ppasaporte.ParameterName = "p_pasaporte";
                        ppasaporte.Value = pEmpleados.pasaporte;
                        ppasaporte.Direction = ParameterDirection.Input;
                        ppasaporte.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppasaporte);



                        DbParameter pfecha_ven_pasaporte = cmdTransaccionFactory.CreateParameter();
                        pfecha_ven_pasaporte.ParameterName = "p_fecha_ven_pasaporte";
                        pfecha_ven_pasaporte.Value = pEmpleados.fecha_ven_pasaporte;
                        pfecha_ven_pasaporte.Direction = ParameterDirection.Input;
                        pfecha_ven_pasaporte.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ven_pasaporte);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMP_PERSO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                      
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ModificarPersonaEmpleados", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarEmpleados(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pId;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEADOS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "EliminarEmpleados", ex);
                    }
                }
            }
        }

        public Empleados ConsultarEmpleadosCodigoEmpleado(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empleados entidad = new Empleados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT per.FECHAEXPEDICION, per.DIRECCION, per.CELULAR, per.CODESTADOCIVIL, per.TELEFONO, per.FECHANACIMIENTO,per.CODCARGO, per.CODCIUDADNACIMIENTO, 
                                       per.COD_OFICINA, per.CODTIPOCONTRATO, per.FECHA_INGRESOEMPRESA, per.SALARIO, per.JORNADA_LABORAL,
                                       tip.CODTIPOIDENTIFICACION as Cod_identificacion, emp.CONSECUTIVO,
                                       per.PRIMER_NOMBRE || ' ' || per.SEGUNDO_NOMBRE || ' ' || per.PRIMER_APELLIDO || ' ' || per.SEGUNDO_APELLIDO as Nombre,
                                       per.sexo, per.identificacion, per.cod_persona
                                      FROM persona per 
                                      JOIN tipoidentificacion tip on tip.CODTIPOIDENTIFICACION = per.tipo_identificacion
                                      JOIN EMPLEADOS emp on emp.COD_PERSONA = per.COD_PERSONA
                                      WHERE emp.Consecutivo =  " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fecha_expedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.cod_estado_civil = Convert.ToString(resultado["CODESTADOCIVIL"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.cod_cargo = Convert.ToString(resultado["CODCARGO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.cod_ciudad_nac = Convert.ToString(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.cod_tipo_contrato = Convert.ToString(resultado["CODTIPOCONTRATO"]);
                            if (resultado["FECHA_INGRESOEMPRESA"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESOEMPRESA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["Cod_identificacion"] != DBNull.Value) entidad.cod_identificacion = Convert.ToString(resultado["Cod_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["sexo"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["sexo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ConsultarEmpleadosCodigoEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public Empleados ConsultarEmpleadosCodigoPersona(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empleados entidad = new Empleados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT per.FECHAEXPEDICION, per.DIRECCION, per.CELULAR, per.CODESTADOCIVIL, per.TELEFONO, per.FECHANACIMIENTO,per.CODCARGO, per.CODCIUDADNACIMIENTO, 
                                       per.COD_OFICINA, per.CODTIPOCONTRATO, per.FECHA_INGRESOEMPRESA, per.SALARIO, per.JORNADA_LABORAL,
                                       tip.CODTIPOIDENTIFICACION as Cod_identificacion, emp.CONSECUTIVO,
                                       per.PRIMER_NOMBRE || ' ' || per.SEGUNDO_NOMBRE || ' ' || per.PRIMER_APELLIDO || ' ' || per.SEGUNDO_APELLIDO as Nombre,
                                       per.sexo, per.identificacion, per.cod_persona
                                      FROM persona per 
                                      JOIN tipoidentificacion tip on tip.CODTIPOIDENTIFICACION = per.tipo_identificacion
                                      LEFT JOIN EMPLEADOS emp on emp.COD_PERSONA = per.COD_PERSONA
                                      WHERE per.COD_PERSONA =  " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fecha_expedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.cod_estado_civil = Convert.ToString(resultado["CODESTADOCIVIL"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.cod_cargo = Convert.ToString(resultado["CODCARGO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.cod_ciudad_nac = Convert.ToString(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.cod_tipo_contrato = Convert.ToString(resultado["CODTIPOCONTRATO"]);
                            if (resultado["FECHA_INGRESOEMPRESA"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESOEMPRESA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["Cod_identificacion"] != DBNull.Value) entidad.cod_identificacion = Convert.ToString(resultado["Cod_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["sexo"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["sexo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ConsultarEmpleadosCodigoPersona", ex);
                        return null;
                    }
                }
            }
        }


        public List<Empleados> ListarEmpleados(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Empleados> lstEntidad = new List<Empleados>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT per.COD_PERSONA, per.IDENTIFICACION, per.PRIMER_NOMBRE || ' ' || per.PRIMER_APELLIDO as Nombre,
                                        per.FECHA_INGRESOEMPRESA, ofi.NOMBRE as Nom_Oficina, emp.CONSECUTIVO, per.profesion, per.email, per.CELULAR
                                    from empleados emp
                                    join persona per on per.cod_persona = emp.cod_persona 
                                    join v_persona per2 on per2.cod_persona = per.cod_persona
                                    LEFT join oficina ofi on per.COD_OFICINA = ofi.COD_OFICINA "
                                    + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Empleados entidad = new Empleados();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["FECHA_INGRESOEMPRESA"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESOEMPRESA"]);
                            if (resultado["Nom_Oficina"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["Nom_Oficina"]);

                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["profesion"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["profesion"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ListarEmpleados", ex);
                        return null;
                    }
                }
            }
        }

        public List<Empleados> ListarEmpleadosConContratoActivo(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Empleados> lstEntidad = new List<Empleados>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT(emp.consecutivo), per.COD_PERSONA, per.IDENTIFICACION, per.PRIMER_NOMBRE || ' ' || per.PRIMER_APELLIDO as Nombre,
                                        per.FECHA_INGRESOEMPRESA, ofi.NOMBRE as Nom_Oficina, per.profesion, per.email, per.CELULAR
                                    from empleados emp
                                    join persona per on per.cod_persona = emp.cod_persona 
                                    join v_persona per2 on per2.cod_persona = per.cod_persona
                                    join INGRESOPERSONAL ing on ing.CODIGOEMPLEADO = emp.consecutivo and ing.ESTAACTIVOCONTRATO = 1
                                    LEFT join oficina ofi on per.COD_OFICINA = ofi.COD_OFICINA "
                                    + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Empleados entidad = new Empleados();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["FECHA_INGRESOEMPRESA"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESOEMPRESA"]);
                            if (resultado["Nom_Oficina"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["Nom_Oficina"]);

                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["profesion"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["profesion"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ListarEmpleados", ex);
                        return null;
                    }
                }
            }
        }
        public Empleados ConsultarInformacioPorcentajeArl(long consecutivo, Usuario usuario)
        {
            DbDataReader resultado;
            Empleados entidad = new Empleados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select porcentaje from PORCENTAJESARL where consecutivo=" + consecutivo ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentajearl = Convert.ToDecimal(resultado["porcentaje"]);

                        }
                      

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ConsultarInformacioPorcentajeArl", ex);
                        return null;
                    }
                }
            }
        }
        public Empleados ConsultarInformacioPorcentajeArlContrato(long consecutivo, Usuario usuario)
        {
            DbDataReader resultado;
            Empleados entidad = new Empleados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select TIPORIESGOARL,porcentajearl from INGRESOPERSONAL where consecutivo=" + consecutivo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["porcentajearl"] != DBNull.Value) entidad.porcentajearl = Convert.ToDecimal(resultado["porcentajearl"]);
                            if (resultado["TIPORIESGOARL"] != DBNull.Value) entidad.tipo_riesgo = Convert.ToInt64(resultado["TIPORIESGOARL"]);

                        }
                        else
                        {
                         
                           
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpleadosData", "ConsultarInformacioPorcentajeArlContrato", ex);
                        return null;
                    }
                }
            }
        }

    }
}