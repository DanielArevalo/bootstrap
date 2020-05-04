using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Xpinn.Comun.Entities;
using Xpinn.Util;

namespace Xpinn.Comun.Data
{
    public class HomologacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public HomologacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Homologacion ConsultarHomologacionTipoIdentificacionPorCodigoPersona(string cod_persona, Usuario usuario)
        {
            DbDataReader resultado;
            Homologacion entidad = new Homologacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select homo.*
                                        from persona per
                                        join homologa_tipos_iden homo on per.TIPO_IDENTIFICACION = homo.TIPO_IDENTIFICACION
                                        where per.cod_persona = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CIFIN"] != DBNull.Value) entidad.tipo_identificacion_cifin = Convert.ToInt32(resultado["COD_CIFIN"]);
                            if (resultado["COD_DATA"] != DBNull.Value) entidad.tipo_identificacion_data = Convert.ToInt32(resultado["COD_DATA"]);
                            if (resultado["COD_BANCOLOMBIA"] != DBNull.Value) entidad.tipo_identificacion_bancolombia = Convert.ToInt32(resultado["COD_BANCOLOMBIA"]);
                            if (resultado["COD_BANBOGOTA"] != DBNull.Value) entidad.tipo_identificacion_banbogota = Convert.ToString(resultado["COD_BANBOGOTA"]);
                            if (resultado["COD_ENPACTO"] != DBNull.Value) entidad.tipo_identificacion_enpacto = Convert.ToString(resultado["COD_ENPACTO"]);
                            if (resultado["COD_BANPOPULAR"] != DBNull.Value) entidad.tipo_identificacion_banpopular = Convert.ToInt32(resultado["COD_BANPOPULAR"]);
                            if (resultado["COD_BANAGRARIO"] != DBNull.Value) entidad.tipo_identificacion_banagrario = Convert.ToInt32(resultado["COD_BANAGRARIO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HomologacionData", "ConsultarHomologacionTipoIdentificacionPorCodigoPersona", ex);
                        return null;
                    }
                }
            }
        }

        public Homologacion ConsultarHomologacionTipoIdentificacion(string tipoIdentificacion, Usuario usuario)
        {
            DbDataReader resultado;
            Homologacion entidad = new Homologacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from HOMOLOGA_TIPOS_IDEN where tipo_identificacion = " + tipoIdentificacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CIFIN"] != DBNull.Value) entidad.tipo_identificacion_cifin = Convert.ToInt32(resultado["COD_CIFIN"]);
                            if (resultado["COD_DATA"] != DBNull.Value) entidad.tipo_identificacion_data = Convert.ToInt32(resultado["COD_DATA"]);
                            if (resultado["COD_BANCOLOMBIA"] != DBNull.Value) entidad.tipo_identificacion_bancolombia = Convert.ToInt32(resultado["COD_BANCOLOMBIA"]);
                            if (resultado["COD_BANBOGOTA"] != DBNull.Value) entidad.tipo_identificacion_banbogota = Convert.ToString(resultado["COD_BANBOGOTA"]);
                            if (resultado["COD_ENPACTO"] != DBNull.Value) entidad.tipo_identificacion_enpacto = Convert.ToString(resultado["COD_ENPACTO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HomologacionData", "ConsultarHomologacionTipoIdentificacion", ex);
                        return null;
                    }
                }
            }
        }


        public Persona PersonaDetalle(string cod_persona, Usuario usuario)
        {
            Persona entidad = new Persona();
            DbDataReader resultado;


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select COD_PERSONA, PRIMER_APELLIDO ||' '||SEGUNDO_APELLIDO as APELLIDOS, PRIMER_NOMBRE || ' ' || SEGUNDO_NOMBRE AS NOMBRES,
                                       IDENTIFICACION, DIGITO_VERIFICACION, NVL(DIRECCION, '') AS DIRECCION, NVL(TELEFONO, '') AS TELEFONO
                                       from persona where COD_PERSONA = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToString(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HomologacionData", "PersonaDetalle", ex);
                        return null;
                    }
                }
            }
        }


    }
}
