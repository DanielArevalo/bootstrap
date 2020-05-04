using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Riesgo.Data
{
    public class SegmentacionPerfilData : GlobalData
    {


        protected ConnectionDataBase dbConnectionFactory;

        public SegmentacionPerfilData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        // realiza  la busqueda  de  todos los afliliados  para calificar
        public List<SegmentacionPerfil> ListarPersonaRiesgo(SegmentacionPerfil pPersonasperfil, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SegmentacionPerfil> lspersonas = new List<SegmentacionPerfil>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT persona.COD_PERSONA, persona.TIPO_PERSONA, persona.PRIMER_NOMBRE, persona.SEGUNDO_NOMBRE, persona.PRIMER_APELLIDO, 
                                persona.IDENTIFICACION, persona.PARENTESCO_PEP, persona.ACT_CIIU_EMPRESA, Persona_Afiliacion.ES_PEPS, CIUDADES.DEPENDE_DE AS DEPARECIDENCIA, persona.CODCIUDADRESIDENCIA
                                FROM persona LEFT JOIN  Persona_Afiliacion ON persona.COD_PERSONA = Persona_Afiliacion.COD_PERSONA LEFT JOIN  CIUDADES ON persona.CODCIUDADRESIDENCIA = CIUDADES.CODCIUDAD  
                                ORDER BY persona.COD_PERSONA";                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SegmentacionPerfil entidad = new SegmentacionPerfil();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["DEPARECIDENCIA"] != DBNull.Value) entidad.coddeparesidencia = Convert.ToInt64(resultado["DEPARECIDENCIA"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["ACT_CIIU_EMPRESA"] != DBNull.Value) entidad.act_ciiu_empresa = Convert.ToInt64(resultado["ACT_CIIU_EMPRESA"]);
                            if (resultado["PARENTESCO_PEP"] != DBNull.Value) entidad.Parentesco_PEPS = Convert.ToInt64(resultado["PARENTESCO_PEP"]);
                            if (resultado["ES_PEPS"] != DBNull.Value) entidad.Es_peps = Convert.ToInt64(resultado["ES_PEPS"]);

                            lspersonas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lspersonas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SegmentacionPerfilData", "ListarPersonaRiesgo", ex);
                        return null;
                    }
                }
            }
        }
        //inserta  todos  los  perfiles  calificados en la tabla GR_SEGMENTACION_PERFIL
        public bool Insertarcalificacionperfiles(List<SegmentacionPerfil> pSegmetacion, Usuario vUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    int respuesta = 0;
                    int cont = 0;
                    var pru = "";
                    try

                    {
                        SegmentacionPerfil calificacion = new SegmentacionPerfil();

                        foreach (SegmentacionPerfil item in pSegmetacion)
                        {


                            if (item.cod_persona != null)
                                calificacion.cod_persona = item.cod_persona;
                            else
                                calificacion.cod_persona = 0;

                            if (item.identificacion != null)
                                calificacion.identificacion = item.identificacion;
                            else
                                calificacion.identificacion = "";

                            if (item.primer_nombre != null)
                                calificacion.primer_nombre = item.primer_nombre;
                            else
                                calificacion.primer_nombre = "";
                            if (item.segundo_nombre != null)
                                calificacion.segundo_nombre = item.segundo_nombre;
                            else
                                calificacion.segundo_nombre = "";

                            if (item.primer_apellido != null)
                                calificacion.primer_apellido = item.primer_apellido;
                            else
                                calificacion.primer_apellido = "";
                            if (item.valoracion != null)
                                calificacion.valoracion = item.valoracion;
                            else
                                calificacion.valoracion = 0;

                            if (item.perfil != null)
                                calificacion.perfil = item.perfil;
                            else
                                calificacion.perfil = "";
                            if (calificacion.primer_nombre.Contains("'"))
                                calificacion.primer_nombre = calificacion.primer_nombre.Replace("'", " ");

                            if (calificacion.segundo_nombre.Contains("'"))
                                calificacion.segundo_nombre = calificacion.segundo_nombre.Replace("'", " ");

                            if (calificacion.primer_apellido.Contains("'"))
                                calificacion.primer_apellido = calificacion.primer_apellido.Replace("'", " ");

                            if (calificacion.segundo_nombre.Contains("´"))
                                calificacion.segundo_nombre = calificacion.segundo_nombre.Replace("´", " ");

                            if (calificacion.primer_nombre.Contains("´"))
                                calificacion.primer_nombre = calificacion.primer_nombre.Replace("´", " ");

                            if (calificacion.primer_apellido.Contains("´"))
                                calificacion.primer_apellido = calificacion.primer_apellido.Replace("´", " ");

                            string sql = "";
                            sql = @"INSERT INTO GR_SEGMENTACION_PERFIL (COD_PERSONA,identificacion,primer_nombre,segundo_nombre,primer_apellido,valoracion,perfil) VALUES(" + calificacion.cod_persona + ", '" + calificacion.identificacion + "', '" + calificacion.primer_nombre + "','" + calificacion.segundo_nombre + "','" + calificacion.primer_apellido + "'," + calificacion.valoracion + ", '" + calificacion.perfil + "')";
                            //cont++;

                            //if (cont == 7478)
                            //{
                            //    sql = pru;

                            //}

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            respuesta = cmdTransaccionFactory.ExecuteNonQuery();
                            dbConnectionFactory.CerrarConexion(connection);

                        }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SegmentacionPerfilData", "Insertarcalificacionperfiles", ex);
                        return false;
                    }
                }
            }


        }
        //trae  los registros de  la tabla  tabla GR_SEGMENTACION_PERFIL
        public List<PerfilRiesgoSeg> ListarPerfilPesonaRiesgo(PerfilRiesgoSeg pPerfilRiesgoSeg, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PerfilRiesgoSeg> lspersonasperfil = new List<PerfilRiesgoSeg>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string type = pPerfilRiesgoSeg.tipo_rol;
                        string sql = "";
                        PerfilRiesgoSeg pfrs = new PerfilRiesgoSeg();
                        pfrs.primer_nombre = pPerfilRiesgoSeg.primer_nombre;
                        pfrs.identificacion = pPerfilRiesgoSeg.identificacion;
                        pfrs.valoracion = pPerfilRiesgoSeg.valoracion;
                        pfrs.perfil = pPerfilRiesgoSeg.perfil;
                        sql = @"
                               SELECT SG.COD_PERSONA, SG.IDENTIFICACION, SG.PRIMER_NOMBRE, 
                               SG.SEGUNDO_NOMBRE, SG.PRIMER_APELLIDO, SG.VALORACION, 
                               SG.PERFIL, PE.ESTADO
                               FROM  GR_SEGMENTACION_PERFIL SG
                               LEFT JOIN PERSONA_AFILIACION PE ON PE.COD_PERSONA = SG.COD_PERSONA " + ObtenerFiltro(pfrs)+@" 
                            ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        List<Afiliacion> lspersonasafi = new List<Afiliacion>();
                        if (type == null || type == "") type = "NNN";
                        while (resultado.Read())
                        {
                            PerfilRiesgoSeg entidad = new PerfilRiesgoSeg();
                            if (type != null && type != "" && type != "NNN")
                            {
                                if (type == "NN") type = "";
                                if (Convert.ToString(resultado["ESTADO"])==type)
                                {
                                    if (resultado["COD_PERSONA"] != DBNull.Value) entidad.COD_PERSONA = Convert.ToInt64(resultado["COD_PERSONA"]);
                                    if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                    if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                                    if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                                    if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                                    if (resultado["VALORACION"] != DBNull.Value) entidad.valoracion = Convert.ToInt64(resultado["VALORACION"]);
                                    if (resultado["PERFIL"] != DBNull.Value) entidad.perfil = Convert.ToString(resultado["PERFIL"]);
                                    if (resultado["ESTADO"] != DBNull.Value)
                                        switch (Convert.ToString(resultado["ESTADO"]))
                                        {
                                            case "A": entidad.nom_tipo_rol = "Asociado"; break;
                                            case "R": entidad.nom_tipo_rol = "Exasociado"; break;
                                            case "": entidad.nom_tipo_rol = "Terceros"; break;
                                        }
                                    else entidad.nom_tipo_rol = "Terceros";
                                    lspersonasperfil.Add(entidad);
                                }
                            }
                            else
                            {
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.COD_PERSONA = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                                if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                                if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                                if (resultado["VALORACION"] != DBNull.Value) entidad.valoracion = Convert.ToInt64(resultado["VALORACION"]);
                                if (resultado["PERFIL"] != DBNull.Value) entidad.perfil = Convert.ToString(resultado["PERFIL"]);
                                if (resultado["ESTADO"] != DBNull.Value)
                                    switch(Convert.ToString(resultado["ESTADO"]))
                                    {
                                        case "A":entidad.nom_tipo_rol = "Asociado";break;
                                        case "R": entidad.nom_tipo_rol = "Exasociado"; break;
                                        case "": entidad.nom_tipo_rol = "Terceros"; break;
                                    }
                                else entidad.nom_tipo_rol = "Terceros";
                                lspersonasperfil.Add(entidad);
                            }
                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lspersonasperfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SegmentacionPerfilData", "ListarPersonaRiesgo", ex);
                        return null;
                    }
                }
            }
        }
        // elimina datos de la tabla GR_SEGMENTACION_PERFIL
        public void limpiarperfilesRiesgo(Usuario vUsuario)
        {


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    int respuesta = 0;
                    try
                    {
                        string sql = "";
                        sql = @"delete from GR_SEGMENTACION_PERFIL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        respuesta = cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SegmentacionPerfilData", "limpiarperfilesRiesgo", ex);
                    }
                }
            }



        }
        //consulta necesaria  para calificacion de perfil de riesgo
        public List<SegmentacionPerfil> ListarUnapersonaXriesgo(SegmentacionPerfil pPersonasperfil, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SegmentacionPerfil> lspersonas = new List<SegmentacionPerfil>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Int64 COD_PERSONA = 0;
                        COD_PERSONA = pPersonasperfil.cod_persona;
                        string sql = "";
                        sql = @"SELECT persona.COD_PERSONA,persona.TIPO_PERSONA ,persona.PRIMER_NOMBRE,persona.SEGUNDO_NOMBRE,persona.PRIMER_APELLIDO,persona.IDENTIFICACION,persona.PARENTESCO_PEP,persona.ACT_CIIU_EMPRESA,Persona_Afiliacion.ES_PEPS,CIUDADES.DEPENDE_DE AS DEPARECIDENCIA, Persona_Afiliacion.COD_ASOCIADO_ESPECIAL, Persona_Afiliacion.MIEMBRO_ADMINISTRACION, Persona_Afiliacion.MIEMBRO_CONTROL from persona left JOIN Persona_Afiliacion  ON persona.COD_PERSONA = Persona_Afiliacion.COD_PERSONA left JOIN  CIUDADES ON persona.CODCIUDADRESIDENCIA = CIUDADES.CODCIUDAD  WHERE persona.COD_PERSONA =" + COD_PERSONA + " order by persona.COD_PERSONA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SegmentacionPerfil entidad = new SegmentacionPerfil();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["DEPARECIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["DEPARECIDENCIA"]);
                            if (resultado["ACT_CIIU_EMPRESA"] != DBNull.Value) entidad.act_ciiu_empresa = Convert.ToInt64(resultado["ACT_CIIU_EMPRESA"]);
                            if (resultado["PARENTESCO_PEP"] != DBNull.Value) entidad.Parentesco_PEPS = Convert.ToInt64(resultado["PARENTESCO_PEP"]);
                            if (resultado["ES_PEPS"] != DBNull.Value) entidad.Es_peps = Convert.ToInt64(resultado["ES_PEPS"]);
                            if (resultado["COD_ASOCIADO_ESPECIAL"] != DBNull.Value) entidad.cod_especial = Convert.ToInt64(resultado["COD_ASOCIADO_ESPECIAL"]);
                            if (resultado["MIEMBRO_ADMINISTRACION"] != DBNull.Value) entidad.miembro_administracion = Convert.ToInt64(resultado["MIEMBRO_ADMINISTRACION"]);
                            if (resultado["MIEMBRO_CONTROL"] != DBNull.Value) entidad.miembro_control = Convert.ToInt64(resultado["MIEMBRO_CONTROL"]);
                            lspersonas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lspersonas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SegmentacionPerfilData", "ListarPersonaRiesgo", ex);
                        return null;
                    }
                }
            }
        }
        // metodo de  consulta  para reporte de historico de segmentacion 
        public int ExistePerfilPesonaRiesgo(PerfilRiesgoSeg pPerfilRiesgoSeg, Usuario vUsuario)
        {
            DbDataReader resultado;
            int cantidad = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT COUNT(*) AS CANTIDAD FROM GR_SEGMENTACION_PERFIL " + ObtenerFiltro(pPerfilRiesgoSeg) + " ORDER BY COD_PERSONA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["CANTIDAD"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["CANTIDAD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return cantidad;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }
        // actuliza  el registro  por persona  de perfil de Riesgo
        public bool Updateperfiles(List<SegmentacionPerfil> pSegmetacion, Usuario vUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    int respuesta = 0;
                    int cont = 0;
                    var pru = "";
                    try

                    {
                        SegmentacionPerfil calificacion = new SegmentacionPerfil();

                        foreach (SegmentacionPerfil item in pSegmetacion)
                        {


                            if (item.cod_persona != null)
                                calificacion.cod_persona = item.cod_persona;
                            else
                                calificacion.cod_persona = 0;

                            if (item.identificacion != null)
                                calificacion.identificacion = item.identificacion;
                            else
                                calificacion.identificacion = "";

                            if (item.primer_nombre != null)
                                calificacion.primer_nombre = item.primer_nombre;
                            else
                                calificacion.primer_nombre = "";
                            if (item.segundo_nombre != null)
                                calificacion.segundo_nombre = item.segundo_nombre;
                            else
                                calificacion.segundo_nombre = "";

                            if (item.primer_apellido != null)
                                calificacion.primer_apellido = item.primer_apellido;
                            else
                                calificacion.primer_apellido = "";
                            if (item.valoracion != null)
                                calificacion.valoracion = item.valoracion;
                            else
                                calificacion.valoracion = 0;

                            if (item.perfil != null)
                                calificacion.perfil = item.perfil;
                            else
                                calificacion.perfil = "";
                            if (calificacion.primer_nombre.Contains("'"))
                                calificacion.primer_nombre = calificacion.primer_nombre.Replace("'", " ");

                            if (calificacion.segundo_nombre.Contains("'"))
                                calificacion.segundo_nombre = calificacion.segundo_nombre.Replace("'", " ");

                            if (calificacion.primer_apellido.Contains("'"))
                                calificacion.primer_apellido = calificacion.primer_apellido.Replace("'", " ");

                            if (calificacion.segundo_nombre.Contains("´"))
                                calificacion.segundo_nombre = calificacion.segundo_nombre.Replace("´", " ");

                            if (calificacion.primer_nombre.Contains("´"))
                                calificacion.primer_nombre = calificacion.primer_nombre.Replace("´", " ");

                            if (calificacion.primer_apellido.Contains("´"))
                                calificacion.primer_apellido = calificacion.primer_apellido.Replace("´", " ");

                            string sql = "";
                            sql = @"UPDATE GR_SEGMENTACION_PERFIL SET  identificacion = '" + calificacion.identificacion + "',primer_nombre = '" + calificacion.segundo_nombre + "',segundo_nombre = '" + calificacion.segundo_nombre + "', primer_apellido = '" + calificacion.primer_apellido + "', valoracion = " + calificacion.valoracion + ", perfil = '" + calificacion.perfil + "' WHERE COD_PERSONA = " + calificacion.cod_persona + "";
                            //cont++;

                            //if (cont == 7478)
                            //{
                            //    sql = pru;

                            //}

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            respuesta = cmdTransaccionFactory.ExecuteNonQuery();
                            dbConnectionFactory.CerrarConexion(connection);

                        }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SegmentacionPerfilData", "Insertarcalificacionperfiles", ex);
                        return false;
                    }
                }
            }


        }


    }
}
