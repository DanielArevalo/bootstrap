using System;
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
    public class PerfilData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor para el acceso a base de datos
        /// </summary>
        public PerfilData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public void CrearPerfilPersona(Persona1 pPersona, Afiliacion pAfiliacion, bool reafiliacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pPersona != null ? pPersona.cod_persona : pAfiliacion.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_cod_perfil = cmdTransaccionFactory.CreateParameter();
                        p_cod_perfil.ParameterName = "p_cod_perfil";
                        p_cod_perfil.Value = DeterminarCodPerfil(pPersona, pAfiliacion, reafiliacion);
                        p_cod_perfil.Direction = ParameterDirection.Input;
                        p_cod_perfil.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_perfil);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_PERFILPER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //DAauditoria.InsertarLog(pPersona, "GR_PERSONA_PERFIL", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion perfil persona " + pPersona.cod_persona); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "CrearPerfilPersona", ex);
                    }
                }
            }
        }

        public int DeterminarCodPerfil(Persona1 pPersona, Afiliacion pAfiliacion, bool reafiliacion)
        {
            int codigo = 0;
            // Determinar que tipo de asociado especial es
            if (pPersona != null)
            {
                // Familiar de PEPS
                if (pPersona.parentesco_pep == 1)
                    codigo = 1;
                // Realizar operaciones en moneda extranjera
                if (pPersona.lstMonedaExt != null)
                {
                    if (pPersona.lstMonedaExt.Count > 0)
                        codigo = 4;
                }
                // Tiene productos en el exterior
                if (pPersona.lstProductosFinExt != null)
                {
                    if (pPersona.lstProductosFinExt.Count > 0)
                        codigo = 4;
                }
                // Es extranjero
                if (pPersona.nacionalidad != 57)
                    codigo = 5;
            }
            // Si es reafiliacion
            if (reafiliacion)
                codigo = 6;
            if (pAfiliacion != null)
            {
                // Es PEPS
                if (pAfiliacion.Es_PEPS)
                    codigo = 1;
                // Administra Recursos Publicos
                if (pAfiliacion.Administra_recursos_publicos)
                    codigo = 3;
                // Es asociado especial
                if (pAfiliacion.cod_asociado_especial != null && pAfiliacion.cod_asociado_especial > 0)
                    codigo = 7;
            }

            return codigo;
        }
        //metodo  para  asignar  un perfil de riesgo 
        public List<SegmentacionPerfil> Calificarpersona(List<SegmentacionPerfil> pSegmetacion, Usuario Vusuario)
        {
            Xpinn.Riesgo.Data.RangoPerfilData DARango = new RangoPerfilData();
            List<Xpinn.Riesgo.Entities.RangoPerfil> lstRango = new List<RangoPerfil>();
            Xpinn.Riesgo.Entities.RangoPerfil rangos = new RangoPerfil();
            lstRango = DARango.ListarRangosPerfil(rangos, Vusuario);
            if (pSegmetacion != null)
            {
                foreach (SegmentacionPerfil item in pSegmetacion)
                {
                    Int64 Calificacion = 0;
                    // Si la persona es familiar de PEPS se clasifica de riesgo alto.
                    if (item.Parentesco_PEPS == 1)
                    {
                        Calificacion = 10;
                        item.valoracion = Calificacion;
                        item.perfil = "Riesgo Muy Alto";
                        continue;
                    }
                    // Si la persona es PEPS  se clasifica de riesgo alto.
                    if (item.Es_peps == 1)
                    {
                        Calificacion = 10;
                        item.valoracion = Calificacion;
                        item.perfil = "Riesgo Muy Alto";
                        continue;
                    }
                    if (item.cod_especial > 0 || item.miembro_administracion==1 || item.miembro_administracion == 1)
                    {
                        Calificacion = 10;
                        item.valoracion = Calificacion;
                        item.perfil = "Riesgo Muy Alto";
                        continue;
                    }
                    // Consulta actividad  y valoracion
                    Xpinn.Riesgo.Entities.ActividadEco actividad = new ActividadEco();
                    Xpinn.Riesgo.Data.ActividadEcoData verificaractividad = new ActividadEcoData();
                    actividad.Cod_actividad = Convert.ToString(item.act_ciiu_empresa);
                    actividad = verificaractividad.ConsultarActividades(actividad, Vusuario);
                    if (actividad != null)
                        Calificacion = Calificacion + Convert.ToInt64(actividad.valoracion);
                    // Consulta jurisdiccion  y valoracion
                    Xpinn.Riesgo.Entities.JurisdiccionDepa personadepcal = new JurisdiccionDepa();
                    Xpinn.Riesgo.Data.JurisdiccionDepaData verificarjusrisdicion = new JurisdiccionDepaData();
                    personadepcal.Cod_Depa = Convert.ToInt64(item.codciudadresidencia);
                    if (verificarjusrisdicion.ConsultarJurisdiccionDepa(personadepcal, Vusuario) != null)
                    { 
                        personadepcal = verificarjusrisdicion.ConsultarJurisdiccionDepa(personadepcal, Vusuario);
                    }
                    else
                    {
                        personadepcal.Cod_Depa = Convert.ToInt64(item.coddeparesidencia);
                        personadepcal = verificarjusrisdicion.ConsultarJurisdiccionDepa(personadepcal, Vusuario);
                    }
                    if (personadepcal != null)
                        Calificacion = Calificacion + Convert.ToInt64(personadepcal.valoracion);
                    // Si es persona juridica incrementa la calificación
                    if (item.tipo_persona == "N")
                    {
                        Calificacion = Calificacion + 1;
                    }
                    else if (item.tipo_persona == "J")
                    {
                        Calificacion = Calificacion + 3;
                    }
                    // -----------------------------------------------------------------------------------------------------------------------------------------------------
                    // Según la calificación establecer el nivel de riesgo
                    // -----------------------------------------------------------------------------------------------------------------------------------------------------
                    if (lstRango.Count <= 0)
                    { 
                        if (Calificacion <= 4)
                        {
                            item.perfil = "Riesgo Normal";
                            item.valoracion = Calificacion;
                        }
                        else if (Calificacion >= 5 && Calificacion <= 6)
                        {
                            item.perfil = "Riesgo Moderado";
                            item.valoracion = Calificacion;
                        }
                        else if (Calificacion >= 7 && Calificacion <= 9)
                        {
                            item.perfil = "Riesgo Alto";
                            item.valoracion = Calificacion;
                        }
                        else if (Calificacion == 10)
                        {
                            item.perfil = "Riesgo Muy Alto";
                            item.valoracion = Calificacion;
                        }
                        else if (Calificacion > 10)
                        {
                            item.perfil = "Fuera de Rango";
                        }
                    }
                    else
                    {
                        foreach (RangoPerfil rang in lstRango)
                        {
                            if (Calificacion >= rang.rango_minimo && Calificacion <= rang.rango_maximo)
                            {
                                if (rang.calificacion == 1) item.perfil = "Riesgo Normal";
                                if (rang.calificacion == 2) item.perfil = "Riesgo Moderado";
                                if (rang.calificacion == 3) item.perfil = "Riesgo Alto";
                                if (rang.calificacion == 4) item.perfil = "Riesgo Muy Alto";
                                item.valoracion = Calificacion;
                            }
                        }
                    }

                }
            }
            return pSegmetacion;
        }

    }
}
