using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Comun.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncPersonaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncPersonaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Xpinn.Sincronizacion.Entities.Persona> ListarSyncPersona(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Xpinn.Sincronizacion.Entities.Persona> lstPersona = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // CONSULTANDO SI SE GENERA EXPORTACION GENERAL O SOLO AFILIADOS
                        Xpinn.Comun.Data.GeneralData _generalData = new Comun.Data.GeneralData();
                        General general = _generalData.ConsultarGeneral(90167, vUsuario);
                        int solo_afiliado = general == null ? 1 : string.IsNullOrEmpty(general.valor) ? 1 : Convert.ToInt32(general.valor);
                        string sql = string.Empty;
                        if (solo_afiliado == 1)
                            sql = @"SELECT p.*, 1 es_afiliado FROM PERSONA p inner join PERSONA_AFILIACION a on p.cod_persona = a.cod_persona " + pFiltro + " ORDER BY p.COD_PERSONA";
                        else
                            sql = @"SELECT p.*, NVL((Select 1 From persona_afiliacion a where a.cod_persona = p.cod_persona), 0) es_afiliado FROM PERSONA p " + pFiltro + " ORDER BY p.COD_PERSONA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstPersona = new List<Xpinn.Sincronizacion.Entities.Persona>();
                            Xpinn.Sincronizacion.Entities.Persona entidad;
                            while (resultado.Read())
                            {
                                entidad = new Xpinn.Sincronizacion.Entities.Persona();
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt32(resultado["DIGITO_VERIFICACION"]);
                                if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.codtipoidentificacion = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                                if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                                if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value) entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                                if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                                if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                                if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                                if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                                if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                                if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                                if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                                if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.codciudadnacimiento = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                                if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt32(resultado["CODESTADOCIVIL"]);
                                if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt32(resultado["CODESCOLARIDAD"]);
                                if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                                if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                                if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                                if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                                if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                                if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                                if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                                if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt32(resultado["CODTIPOCONTRATO"]);
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                                if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_ASESOR"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                                if (resultado["COD_ZONA"] != DBNull.Value) entidad.cod_zona = Convert.ToInt64(resultado["COD_ZONA"]);
                                if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                                if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                                if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                                if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                                if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                                if (resultado["ES_AFILIADO"] != DBNull.Value) entidad.es_afiliado = Convert.ToInt32(resultado["ES_AFILIADO"]);
                                lstPersona.Add(entidad);
                            }
                            resultado.Close();
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncPersonaData", "ListarSyncPersona", ex);
                        return null;
                    }
                }
            }
        }

        public int CantidadRegistrosPersona(Usuario vUsuario)
        {
            DbDataReader resultado;
            int CantRegistros = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COUNT(P.COD_PERSONA) AS CANTIDAD FROM PERSONA P INNER JOIN PERSONA_AFILIACION A ON P.COD_PERSONA = A.COD_PERSONA ORDER BY P.COD_PERSONA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CANTIDAD"] != DBNull.Value) CantRegistros = Convert.ToInt32(resultado["CANTIDAD"].ToString());
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return CantRegistros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncPersonaData", "CantidadRegistrosPersona", ex);
                        return 0;
                    }
                }
            }
        }


    }
}
