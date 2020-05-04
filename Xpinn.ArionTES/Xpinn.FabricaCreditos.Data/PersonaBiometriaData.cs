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
    /// Objeto de acceso a datos para la tabla PersonaBiometria
    /// </summary>
    public class PersonaBiometriaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PersonaBiometria
        /// </summary>
        public PersonaBiometriaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        
        /// <summary>
        /// Obtiene un registro en la tabla PersonaBiometria de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PersonaBiometria</param>
        /// <returns>Entidad PersonaBiometria consultado</returns>
        public byte[] ConsultarImagenPersona(Int64 pId, Int64 pNumeroDedo, ref Int64 pIdBiometria, Usuario pUsuario)
        {
            DbDataReader resultado;
            PersonaBiometria entidad = new PersonaBiometria();
            pIdBiometria = -1;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM Persona_Biometria WHERE COD_PERSONA = " + pId.ToString() + " AND numero_dedo = " + pNumeroDedo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        //System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            if (resultado["IDBIOMETRIA"] != DBNull.Value) pIdBiometria = Convert.ToInt64(resultado["IDBIOMETRIA"]);
                            if (resultado["HUELLA"] != DBNull.Value) entidad.huella = (byte[])resultado["HUELLA"];
                            if (resultado["HUELLA_SECUGEN"] != DBNull.Value) entidad.huella_secugen = (byte[])resultado["HUELLA_SECUGEN"];
                            if (resultado["TEMPLATE_HUELLA"] != DBNull.Value) entidad.template_huella = Convert.ToString(resultado["TEMPLATE_HUELLA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad.imagen;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }


        public PersonaBiometria ConsultarPersonaBiometria(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            DbDataReader resultado;
            PersonaBiometria entidad = new PersonaBiometria();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pNumeroDedo > 0)
                            sql = "SELECT b.*, p.identificacion, p.nombres, p.apellidos, p.direccion, p.telefono FROM Persona_Biometria b INNER JOIN v_persona p ON b.cod_persona = p.cod_persona WHERE b.cod_persona = " + pId.ToString() + " AND b.numero_dedo = " + pNumeroDedo;
                        else
                            sql = "SELECT b.*, p.identificacion, p.nombres, p.apellidos, p.direccion, p.telefono FROM Persona_Biometria b INNER JOIN v_persona p ON b.cod_persona = p.cod_persona WHERE b.cod_persona = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            if (resultado["IDBIOMETRIA"] != DBNull.Value) entidad.idbiometria = Convert.ToInt64(resultado["IDBIOMETRIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NUMERO_DEDO"] != DBNull.Value) entidad.numero_dedo = Convert.ToInt32(resultado["NUMERO_DEDO"]);
                            if (resultado["HUELLA"] != DBNull.Value) entidad.huella = (byte[])resultado["HUELLA"];
                            if (resultado["HUELLA_SECUGEN"] != DBNull.Value) entidad.huella_secugen = (byte[])resultado["HUELLA_SECUGEN"];
                            if (resultado["TEMPLATE_HUELLA"] != DBNull.Value) entidad.template_huella = Convert.ToString(resultado["TEMPLATE_HUELLA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaBiometriaData", "ConsultarPersonaBiometria", ex);
                        return null;
                    }
                }
            }
        }

        public List<PersonaBiometria> ListarPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            DbDataReader resultado;            
            List<PersonaBiometria> lstBiometria = new List<PersonaBiometria>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  Persona_Biometria " + ObtenerFiltro(pPersonaBiometria);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        while (resultado.Read())
                        {
                            PersonaBiometria entidad = new PersonaBiometria();
                            if (resultado["IDBIOMETRIA"] != DBNull.Value) entidad.idbiometria = Convert.ToInt64(resultado["IDBIOMETRIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_DEDO"] != DBNull.Value) entidad.numero_dedo = Convert.ToInt32(resultado["NUMERO_DEDO"]);
                            if (resultado["HUELLA"] != DBNull.Value) entidad.huella = (byte[])resultado["HUELLA"];
                            if (resultado["HUELLA_SECUGEN"] != DBNull.Value) entidad.huella_secugen = (byte[])resultado["HUELLA_SECUGEN"];
                            if (resultado["TEMPLATE_HUELLA"] != DBNull.Value) entidad.template_huella = Convert.ToString(resultado["TEMPLATE_HUELLA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            lstBiometria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBiometria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaBiometriaData", "ConsultarPersonaBiometria", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ExistePersonaBiometria(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64 entidad = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  Persona_Biometria WHERE COD_PERSONA = " + pId.ToString() + " AND numero_dedo = " + pNumeroDedo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            if (resultado["IDBIOMETRIA"] != DBNull.Value) entidad = Convert.ToInt64(resultado["IDBIOMETRIA"]);                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch 
                    {                        
                        return entidad;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PersonaBiometria dados unos filtros
        /// </summary>
        /// <param name="pPersonaBiometria">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PersonaBiometria obtenidos</returns>
        public List<PersonaBiometria> Handler(PersonaBiometria vPersonaBiometria, Usuario pUsuario)
        {
            List<PersonaBiometria> lstPersonaBiometria = new List<PersonaBiometria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Despues de poner los datos que no son imagen en el gridview, se selecciona la imagen y se raliza su respectiva relacion

                        string sql = "SELECT da.imagen FROM Persona_Biometria_PERSONA da WHERE da.idbiometria = " + vPersonaBiometria.idbiometria;
                        DbDataReader resultado = default(DbDataReader);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PersonaBiometria entidad = new PersonaBiometria();
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                            if (resultado["TEMPLATE_HUELLA"] != DBNull.Value) entidad.template_huella = Convert.ToString(resultado["TEMPLATE_HUELLA"]);
                            lstPersonaBiometria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaBiometria;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaBiometriaData", "ListarPersonaBiometria", ex);
                        return null;
                    }
                }
            }
        }

        public bool ExisteImagenPersona(Int64 CodPersona, int IdTipo, Usuario pUsuario)
        {
            DbDataReader resultado;
            bool bresultado = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  Persona_Biometria WHERE COD_PERSONA = " + CodPersona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                            bresultado = true;
                        else
                            bresultado = false;
                        dbConnectionFactory.CerrarConexion(connection);
                        return bresultado;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


        public PersonaBiometria ConsultarPersonaBiometriaSECUGEN(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            DbDataReader resultado;
            PersonaBiometria entidad = new PersonaBiometria();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT b.idbiometria, 1 As numero_dedo, p.cod_persona, p.identificacion, 
                                Decode(tipo_persona, 'N', Trim(Substr(p.primer_nombre || ' ' || p.segundo_nombre, 0, 240)), p.razon_social) As nombres, 
                                Decode(p.tipo_persona, 'N', Trim(Substr(p.primer_apellido || ' ' || p.segundo_apellido, 0, 240)), p.razon_social) As apellidos, 
                                p.direccion, p.telefono, b.huella_secugen, b.template_huella
                                FROM persona_biometria b INNER JOIN persona p ON b.cod_persona = p.cod_persona 
                                WHERE b.cod_persona = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDBIOMETRIA"] != DBNull.Value) entidad.idbiometria = Convert.ToInt64(resultado["IDBIOMETRIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NUMERO_DEDO"] != DBNull.Value) entidad.numero_dedo = Convert.ToInt32(resultado["NUMERO_DEDO"]);
                            if (resultado["HUELLA_SECUGEN"] != DBNull.Value) entidad.huella = (byte[])resultado["HUELLA_SECUGEN"];
                            if (resultado["HUELLA_SECUGEN"] != DBNull.Value) entidad.huella_secugen = (byte[])resultado["HUELLA_SECUGEN"];
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaBiometriaData", "ConsultarPersonaBiometriaSECUGEN", ex);
                        return null;
                    }
                }
            }
        }


    }
}