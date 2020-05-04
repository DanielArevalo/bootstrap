using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class NegocioData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public NegocioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Negocio> Listar(Negocio pEntityNegocio, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Negocio> lstNegocio = new List<Negocio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From Negocio Inner Join Ciudades On Negocio.Barrio=Ciudades.Codciudad Where Ciudades.Tipo=6 and cod_persona= " + pEntityNegocio.Persona.IdPersona;
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Negocio entidad = new Negocio();

                            if (resultado["COD_NEGOCIO"] != DBNull.Value)           entidad.IdNegocio           = Convert.ToInt64(resultado["COD_NEGOCIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value)           entidad.Persona.IdPersona   = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DIRECCION"] != DBNull.Value)             entidad.Direccion           = resultado["DIRECCION"].ToString();
                            if (resultado["TELEFONO"] != DBNull.Value)              entidad.Telefono            = resultado["TELEFONO"].ToString();
                            if (resultado["NOMCIUDAD"] != DBNull.Value)             entidad.Localidad = resultado["NOMCIUDAD"].ToString();
                            if (resultado["NOMBRENEGOCIO"] != DBNull.Value)         entidad.NombreNegocio       = resultado["NOMBRENEGOCIO"].ToString();
                            if (resultado["DESCRIPCION"] != DBNull.Value)           entidad.Descripcion         = resultado["DESCRIPCION"].ToString();
                            if (resultado["ANTIGUEDAD"] != DBNull.Value)            entidad.Antiguedad          = Convert.ToInt64(resultado["ANTIGUEDAD"]);
                            if (resultado["PROPIA"] != DBNull.Value)                entidad.Propia              = Convert.ToInt64(resultado["PROPIA"]);
                            if (resultado["ARRENDADOR"] != DBNull.Value)            entidad.Arrendador          = resultado["ARRENDADOR"].ToString();
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value)    entidad.TelefonoArrendador  = resultado["TELEFONOARRENDADOR"].ToString();
                            if (resultado["CODACTIVIDAD"] != DBNull.Value)          entidad.Actividad.IdActividad = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                            if (resultado["EXPERIENCIA"] != DBNull.Value)           entidad.Experencia          = Convert.ToInt64(resultado["EXPERIENCIA"]);
                            if (resultado["EMPLPERM"] != DBNull.Value)              entidad.EmpleadosPermanentes = Convert.ToInt64(resultado["EMPLPERM"]);
                            if (resultado["EMPLTEM"] != DBNull.Value)               entidad.EmpleadosTemporales = Convert.ToInt64(resultado["EMPLTEM"]);
                            if (resultado["FECHACREACION"] != DBNull.Value)         entidad.FechaCreacion       = Convert.ToDateTime(resultado["FECHACREACION"].ToString());
                            if (resultado["USUARIOCREACION"] != DBNull.Value)       entidad.UsuarioCreacion     = resultado["USUARIOCREACION"].ToString();
                            if (resultado["FECHACREACION"] != DBNull.Value)         entidad.FechaCreacion       = Convert.ToDateTime(resultado["FECHACREACION"].ToString());
                            if (resultado["FECULTMOD"] != DBNull.Value)             entidad.FechaUltMod         = Convert.ToDateTime(resultado["FECULTMOD"].ToString());
                            if (resultado["USUULTMOD"] != DBNull.Value)             entidad.UsuarioUltMod       = resultado["USUULTMOD"].ToString();

                            lstNegocio.Add(entidad);
                        }

                        return lstNegocio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NegocioData", "Listar", ex);
                        return null;
                    }
                }
            }
        }
    }
}