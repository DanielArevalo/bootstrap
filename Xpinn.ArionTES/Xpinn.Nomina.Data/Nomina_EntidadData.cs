using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class Nomina_EntidadData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Nomina_EntidadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Nomina_Entidad CrearNomina_Entidad(Nomina_Entidad pNomina_Entidad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNomina_Entidad.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnom_persona = cmdTransaccionFactory.CreateParameter();
                        pnom_persona.ParameterName = "p_nom_persona";
                        pnom_persona.Value = pNomina_Entidad.nom_persona;
                        pnom_persona.Direction = ParameterDirection.Input;
                        pnom_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_persona);

                        DbParameter pnit = cmdTransaccionFactory.CreateParameter();
                        pnit.ParameterName = "p_nit";
                        if (pNomina_Entidad.nit == null)
                            pnit.Value = DBNull.Value;
                        else
                            pnit.Value = pNomina_Entidad.nit;
                        pnit.Direction = ParameterDirection.Input;
                        pnit.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnit);


                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pNomina_Entidad.email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pNomina_Entidad.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        if (pNomina_Entidad.clase == null)
                            pclase.Value = DBNull.Value;
                        else
                            pclase.Value = pNomina_Entidad.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pNomina_Entidad.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pNomina_Entidad.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                        pciudad.ParameterName = "p_ciudad";
                        if (pNomina_Entidad.ciudad == null)
                            pciudad.Value = DBNull.Value;
                        else
                            pciudad.Value = pNomina_Entidad.ciudad;
                        pciudad.Direction = ParameterDirection.Input;
                        pciudad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pciudad);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pNomina_Entidad.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pNomina_Entidad.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter presponsable = cmdTransaccionFactory.CreateParameter();
                        presponsable.ParameterName = "p_responsable";
                        if (pNomina_Entidad.responsable == null)
                            presponsable.Value = DBNull.Value;
                        else
                            presponsable.Value = pNomina_Entidad.responsable;
                        presponsable.Direction = ParameterDirection.Input;
                        presponsable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(presponsable);

                        DbParameter pcodigociiu = cmdTransaccionFactory.CreateParameter();
                        pcodigociiu.ParameterName = "p_codigociiu";
                        if (pNomina_Entidad.codigociiu == null)
                            pcodigociiu.Value = DBNull.Value;
                        else
                            pcodigociiu.Value = pNomina_Entidad.codigociiu;
                        pcodigociiu.Direction = ParameterDirection.Input;
                        pcodigociiu.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigociiu);

                        DbParameter pcodigopila = cmdTransaccionFactory.CreateParameter();
                        pcodigopila.ParameterName = "p_codigopila";
                        if (pNomina_Entidad.codigopila == null)
                            pcodigopila.Value = DBNull.Value;
                        else
                            pcodigopila.Value = pNomina_Entidad.codigopila;
                        pcodigopila.Direction = ParameterDirection.Input;
                        pcodigopila.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigopila);



                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pNomina_Entidad.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pNomina_Entidad.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);


                        DbParameter pcod_cuenta_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_contra.ParameterName = "p_cod_cuenta_contra";
                        if (pNomina_Entidad.cod_cuenta_contra == null)
                            pcod_cuenta_contra.Value = DBNull.Value;
                        else
                            pcod_cuenta_contra.Value = pNomina_Entidad.cod_cuenta_contra;
                        pcod_cuenta_contra.Direction = ParameterDirection.Input;
                        pcod_cuenta_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_contra);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOMINA_ENT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pconsecutivo.Value != DBNull.Value)
                            pNomina_Entidad.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pNomina_Entidad;
                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Nomina_EntidadData", "CrearNomina_Entidad", ex);
                        return null;
                    }
                }
            }
        }


        public Nomina_Entidad ModificarNomina_Entidad(Nomina_Entidad pNomina_Entidad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNomina_Entidad.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnom_persona = cmdTransaccionFactory.CreateParameter();
                        pnom_persona.ParameterName = "p_nom_persona";
                        pnom_persona.Value = pNomina_Entidad.nom_persona;
                        pnom_persona.Direction = ParameterDirection.Input;
                        pnom_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_persona);

                        DbParameter pnit = cmdTransaccionFactory.CreateParameter();
                        pnit.ParameterName = "p_nit";
                        if (pNomina_Entidad.nit == null)
                            pnit.Value = DBNull.Value;
                        else
                            pnit.Value = pNomina_Entidad.nit;
                        pnit.Direction = ParameterDirection.Input;
                        pnit.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnit);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pNomina_Entidad.email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pNomina_Entidad.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        if (pNomina_Entidad.clase == null)
                            pclase.Value = DBNull.Value;
                        else
                            pclase.Value = pNomina_Entidad.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pNomina_Entidad.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pNomina_Entidad.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                        pciudad.ParameterName = "p_ciudad";
                        if (pNomina_Entidad.ciudad == null)
                            pciudad.Value = DBNull.Value;
                        else
                            pciudad.Value = pNomina_Entidad.ciudad;
                        pciudad.Direction = ParameterDirection.Input;
                        pciudad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pciudad);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pNomina_Entidad.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pNomina_Entidad.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter presponsable = cmdTransaccionFactory.CreateParameter();
                        presponsable.ParameterName = "p_responsable";
                        if (pNomina_Entidad.responsable == null)
                            presponsable.Value = DBNull.Value;
                        else
                            presponsable.Value = pNomina_Entidad.responsable;
                        presponsable.Direction = ParameterDirection.Input;
                        presponsable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(presponsable);

                        DbParameter pcodigociiu = cmdTransaccionFactory.CreateParameter();
                        pcodigociiu.ParameterName = "p_codigociiu";
                        if (pNomina_Entidad.codigociiu == null)
                            pcodigociiu.Value = DBNull.Value;
                        else
                            pcodigociiu.Value = pNomina_Entidad.codigociiu;
                        pcodigociiu.Direction = ParameterDirection.Input;
                        pcodigociiu.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigociiu);

                        DbParameter pcodigopila = cmdTransaccionFactory.CreateParameter();
                        pcodigopila.ParameterName = "p_codigopila";
                        if (pNomina_Entidad.codigopila == null)
                            pcodigopila.Value = DBNull.Value;
                        else
                            pcodigopila.Value = pNomina_Entidad.codigopila;
                        pcodigopila.Direction = ParameterDirection.Input;
                        pcodigopila.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigopila);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pNomina_Entidad.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pNomina_Entidad.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);


                        DbParameter pcod_cuenta_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_contra.ParameterName = "p_cod_cuenta_contra";
                        if (pNomina_Entidad.cod_cuenta_contra == null)
                            pcod_cuenta_contra.Value = DBNull.Value;
                        else
                            pcod_cuenta_contra.Value = pNomina_Entidad.cod_cuenta_contra;
                        pcod_cuenta_contra.Direction = ParameterDirection.Input;
                        pcod_cuenta_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_contra);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ENT_ENTIDAD_NO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pNomina_Entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Nomina_EntidadData", "ModificarNomina_Entidad", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarNomina_Entidad(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Nomina_Entidad pNomina_Entidad = new Nomina_Entidad();
                        pNomina_Entidad = ConsultarNomina_Entidad(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNomina_Entidad.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOMINA_ENT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Nomina_EntidadData", "EliminarNomina_Entidad", ex);
                    }
                }
            }
        }


        public Nomina_Entidad ConsultarNomina_Entidad(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Nomina_Entidad entidad = new Nomina_Entidad();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Nomina_Entidad WHERE CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToString(resultado["CLASE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["RESPONSABLE"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["RESPONSABLE"]);
                            if (resultado["CODIGOCIIU"] != DBNull.Value) entidad.codigociiu = Convert.ToString(resultado["CODIGOCIIU"]);
                            if (resultado["CODIGOPILA"] != DBNull.Value) entidad.codigopila = Convert.ToString(resultado["CODIGOPILA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);

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
                        BOExcepcion.Throw("Nomina_EntidadData", "ConsultarNomina_Entidad", ex);
                        return null;
                    }
                }
            }
        }

        public Nomina_Entidad ConsultaDatosEntidad(string pidentificacion, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Nomina_Entidad entidad = new Nomina_Entidad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from Nomina_Entidad where CONSECUTIVO = " + pidentificacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToString(resultado["CLASE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["RESPONSABLE"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["RESPONSABLE"]);
                            if (resultado["CODIGOCIIU"] != DBNull.Value) entidad.codigociiu = Convert.ToString(resultado["CODIGOCIIU"]);
                            if (resultado["CODIGOPILA"] != DBNull.Value) entidad.codigopila = Convert.ToString(resultado["CODIGOPILA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Nomina_EntidadData", "ConsultaDatosEntidad", ex);
                        return null;
                    }
                }
            }
        }

        public List<Nomina_Entidad> ListarNomina_Entidad(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Nomina_Entidad> lstNomina_Entidad = new List<Nomina_Entidad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CONSECUTIVO, NOM_PERSONA, NIT, 
                        EMAIL, CLASE, DIRECCION, CIUDAD, TELEFONO,RESPONSABLE,
                        CODIGOCIIU, CODIGOPILA,COD_CUENTA,COD_CUENTA_CONTRA FROM Nomina_Entidad " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Nomina_Entidad entidad = new Nomina_Entidad();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToString(resultado["CLASE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["RESPONSABLE"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["RESPONSABLE"]);
                            if (resultado["CODIGOCIIU"] != DBNull.Value) entidad.codigociiu = Convert.ToString(resultado["CODIGOCIIU"]);
                            if (resultado["CODIGOPILA"] != DBNull.Value) entidad.codigopila = Convert.ToString(resultado["CODIGOPILA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);
                            lstNomina_Entidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina_Entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Nomina_EntidadData", "ListarNomina_Entidad", ex);
                        return null;
                    }
                }
            }
        }


    }
}