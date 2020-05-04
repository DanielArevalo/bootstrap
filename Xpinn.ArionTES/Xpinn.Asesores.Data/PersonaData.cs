using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class PersonaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public PersonaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Persona Consultar(Int64 pIdEntityPersona, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            DbDataReader result = default(DbDataReader);
            Persona EntityPersona = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                                @"Select c.nom_motivo, c.cod_motivo, c.cod_persona, c.tipo_identificacion, c.identificacion, c.nombres, 
                                  c.apellidos, c.fechanacimiento,c.nomciudad, c.direccion, c.telefono, c.email, c.estado, c.zona, c.oficina, 
                                  c.ejecutivo, c.calificacion, c.tipo_cliente, c.fecha_afiliacion, SYSDATE As fecha,
                                  P.descripcion as estadoafiliacion, c.idafiliacion, c.estado_afiliacion, c.cod_asesor, c.celular, c.CORREO_ASESOR
	                              From VAsesoresDatosCliente c Left Join estado_persona p on c.estado_afiliacion=p.estado
                                  Where c.cod_persona = " + pIdEntityPersona;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["cod_persona"] != DBNull.Value) EntityPersona.IdPersona = Convert.ToInt64(reader["cod_persona"].ToString());
                            if (reader["nombres"] != DBNull.Value) EntityPersona.PrimerNombre = reader["nombres"].ToString();
                            if (reader["apellidos"] != DBNull.Value) EntityPersona.PrimerApellido = reader["apellidos"].ToString();
                            if (reader["tipo_identificacion"] != DBNull.Value) EntityPersona.TipoIdentificacion.NombreTipoIdentificacion = reader["tipo_identificacion"].ToString();
                            if (reader["identificacion"] != DBNull.Value) EntityPersona.NumeroDocumento = Convert.ToString(reader["identificacion"].ToString());
                            if (reader["nomciudad"] != DBNull.Value) EntityPersona.Ciudad.nomciudad = Convert.ToString(reader["nomciudad"].ToString());
                            if (reader["fechanacimiento"] != DBNull.Value) EntityPersona.FechaNacimiento = Convert.ToDateTime(reader["fechanacimiento"].ToString());
                            if (reader["direccion"] != DBNull.Value) 
                                EntityPersona.Direccion = reader["direccion"].ToString();
                            if (EntityPersona.Direccion == null)
                                EntityPersona.Direccion = "  ";
                            if (reader["Telefono"] != DBNull.Value) EntityPersona.Telefono = Convert.ToString(reader["Telefono"].ToString());
                            if (reader["Celular"] != DBNull.Value) EntityPersona.Celular = Convert.ToString(reader["Celular"].ToString());
                            if (reader["email"] != DBNull.Value) EntityPersona.Email = reader["email"].ToString();
                            if (reader["estado"] != DBNull.Value) EntityPersona.Estado = reader["estado"].ToString();
                            if (reader["nom_motivo"] != DBNull.Value) EntityPersona.nommotivo = reader["nom_motivo"].ToString();
                            if (reader["zona"] != DBNull.Value) EntityPersona.Zona.NombreZona = reader["zona"].ToString();
                            if (reader["oficina"] != DBNull.Value) EntityPersona.Oficina.NombreOficina = reader["oficina"].ToString();
                            if (reader["ejecutivo"] != DBNull.Value) EntityPersona.Asesor.PrimerNombre = reader["ejecutivo"].ToString();
                            if (reader["COD_ASESOR"] != DBNull.Value) EntityPersona.Asesor.Codigo = Convert.ToString(reader["COD_ASESOR"].ToString());
                            if (reader["CORREO_ASESOR"] != DBNull.Value) EntityPersona.Asesor.Email = Convert.ToString(reader["CORREO_ASESOR"].ToString());
                            if (reader["calificacion"] != DBNull.Value) EntityPersona.Calificacion = Convert.ToInt16(reader["calificacion"].ToString());
                            if (reader["tipo_cliente"] != DBNull.Value) EntityPersona.TipoCliente = reader["tipo_cliente"].ToString();
                            if (reader["FECHA_AFILIACION"] != DBNull.Value) EntityPersona.FechaAfiliacion = Convert.ToDateTime(reader["FECHA_AFILIACION"].ToString());
                            if (reader["Fecha"] != DBNull.Value) EntityPersona.FechaEstadoCuenta = Convert.ToDateTime(reader["Fecha"].ToString());                            
                            if (reader["idafiliacion"] != DBNull.Value)
                            {
                                if (reader["estadoafiliacion"] != DBNull.Value) EntityPersona.EstadoAfiliacion = Convert.ToString(reader["estadoafiliacion"].ToString());
                                if (reader["estado_afiliacion"] != DBNull.Value) EntityPersona.EstadoJuridico = Convert.ToString(reader["estado_afiliacion"]);
                                if (EntityPersona.EstadoJuridico != null)
                                {
                                    if (EntityPersona.EstadoJuridico == "A")
                                    {
                                        string sql = "SELECT * FROM VACACIONES WHERE COD_PERSONA = " + EntityPersona.IdPersona  + " order by fecha_novedad desc";
                                        cmdTransaccionFactory.CommandType = CommandType.Text;
                                        cmdTransaccionFactory.CommandText = sql;
                                        result = cmdTransaccionFactory.ExecuteReader();
                                        EstadoCuenta pEntidad = new EstadoCuenta();
                                        if (result.Read())
                                        {
                                            if (result["FECHA_INICIAL"] != DBNull.Value) pEntidad.fecha_inicio = Convert.ToDateTime(result["FECHA_INICIAL"].ToString());
                                            if (result["FECHA_FINAL"] != DBNull.Value) pEntidad.fecha_final = Convert.ToDateTime(result["FECHA_FINAL"].ToString());
                                            if (pEntidad.fecha_inicio != DateTime.MinValue && pEntidad.fecha_final != DateTime.MinValue)
                                            {
                                                DateTime pFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                                if (pEntidad.fecha_inicio <= pFechaActual && pEntidad.fecha_final >= pFechaActual)
                                                    EntityPersona.EstadoAfiliacion = "VACACIONES";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (reader["estado"] != DBNull.Value) EntityPersona.EstadoAfiliacion = Convert.ToString(reader["estado"].ToString());
                            }
                        }
                        return EntityPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarPersona", ex);
                        return null;
                    }
                }
            }

        }

         public List<Persona> Listar(Persona pEntityPersona, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Persona> lstPrograma = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = ObtenerQuery(pEntityPersona);
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        if (sql.Contains("Ñ") || sql.Contains("ñ"))
                            sql = sql.Replace("Ñ", "'||CHR(209)||'").Replace("ñ", "'||CHR(209)||'");

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Persona entidad = new Persona();

                            if (reader["COD_PERSONA"] != DBNull.Value) entidad.IdPersona = Convert.ToInt64(reader["COD_PERSONA"].ToString());
                            if (reader["PRIMER_NOMBRE"] != DBNull.Value) entidad.PrimerNombre = reader["PRIMER_NOMBRE"].ToString();
                            if (reader["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.SegundoNombre = reader["SEGUNDO_NOMBRE"].ToString();
                            if (reader["PRIMER_APELLIDO"] != DBNull.Value) entidad.PrimerApellido = reader["PRIMER_APELLIDO"].ToString();
                            if (reader["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.SegundoApellido = reader["SEGUNDO_APELLIDO"].ToString();
                            if (reader["IDENTIFICACION"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(reader["IDENTIFICACION"].ToString());
                            if (reader["COD_NOMINA"] != DBNull.Value) entidad.CodigoNomina = Convert.ToString(reader["COD_NOMINA"].ToString());

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarPrograma", ex);
                        return null;
                    }
                }
            }
        }

        private static string ObtenerQuery(Persona pEntityPersona)
        {

            bool flag = false;
            string filtro = " WHERE ";

            string sql = " SELECT COD_PERSONA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, tipo_identificacion, identificacion, cod_nomina, fechanacimiento, direccion, telefono, email, estado " +
                         " FROM VAsesoresPersonaCredito ";       
    
            if (!string.IsNullOrEmpty(pEntityPersona.PrimerNombre))
            {
                if (!flag)
                {
                    filtro += " PRIMER_NOMBRE like '%" + pEntityPersona.PrimerNombre + "%'";
                    flag = true;
                }
                else
                {
                    filtro += " AND PRIMER_NOMBRE  like '%" + pEntityPersona.PrimerNombre + "%'";
                }
            }

            if (!string.IsNullOrEmpty(pEntityPersona.SegundoNombre))
            {
                if (!flag)
                {
                    filtro += " SEGUNDO_NOMBRE like '%" + pEntityPersona.SegundoNombre + "%'";
                    flag = true;
                }
                else
                {
                    filtro += " AND SEGUNDO_NOMBRE  like '%" + pEntityPersona.SegundoNombre + "%'";
                }
            }

            if (!string.IsNullOrEmpty(pEntityPersona.PrimerApellido))
            {
                if (!flag)
                {
                    filtro += " PRIMER_APELLIDO like '%" + pEntityPersona.PrimerApellido + "%'";
                    flag = true;
                }
                else
                {
                    filtro += " AND PRIMER_APELLIDO  like '%" + pEntityPersona.PrimerApellido + "%'";
                }
            }
            if (!string.IsNullOrEmpty(pEntityPersona.SegundoApellido))
            {
                if (!flag)
                {
                    filtro += " SEGUNDO_APELLIDO like '%" + pEntityPersona.SegundoApellido + "%'";
                    flag = true;
                }
                else
                {
                    filtro += " AND SEGUNDO_APELLIDO  like '%" + pEntityPersona.SegundoApellido + "%'";
                }
            }
            if (!string.IsNullOrEmpty(pEntityPersona.NumeroDocumento) && pEntityPersona.NumeroDocumento != "0")
            {
                if (!flag)
                {
                    filtro += " IDENTIFICACION  like '%" + pEntityPersona.NumeroDocumento + "%'";
                    flag = true;
                }
                else
                {
                    filtro += " AND IDENTIFICACION  like '%" + pEntityPersona.NumeroDocumento + "%'";
                }
            }
            if (!string.IsNullOrEmpty(pEntityPersona.CodigoNomina))
            {
                if (!flag)
                {
                    filtro += " COD_NOMINA  like '%" + pEntityPersona.CodigoNomina + "%'";
                    flag = true;
                }
                else
                {
                    filtro += " AND COD_NOMINA  like '%" + pEntityPersona.CodigoNomina + "%'";
                }
            }
            if (pEntityPersona.IdPersona != 0)
            {
                if (!flag)
                {
                    filtro += " cod_persona   = '" + pEntityPersona.IdPersona + "'";
                    flag = true;
                }
                else
                {
                    filtro += " AND cod_persona  = '" + pEntityPersona.IdPersona + "'";
                }
            }

            if (filtro != " WHERE ") sql = sql + filtro;
            sql = sql + " GROUP BY cod_persona, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, tipo_identificacion, identificacion, cod_nomina, fechanacimiento, direccion, TELEFONO, email, estado ";
            return sql;
        }
        


        public List<Persona> ListarConfiltro(Persona pEntityPersona, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            Persona entidad = null;
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = " SELECT DISTINCT(cod_persona),PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,tipo_identificacion,identificacion,fechanacimiento,direccion,TELEFONO,email,estado FROM VAsesoresPersonaCredito ";

                        sql = sql + " " + ObtenerQueryfiltro(pEntityPersona);

                        sql = sql + " GROUP BY cod_persona,PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, tipo_identificacion, identificacion, fechanacimiento, direccion, TELEFONO, email, estado ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            entidad = new Persona();

                            if (reader["PRIMER_NOMBRE"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(reader["PRIMER_NOMBRE"]);
                            if (reader["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(reader["SEGUNDO_NOMBRE"]);
                            if (reader["PRIMER_APELLIDO"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(reader["PRIMER_APELLIDO"]);
                            if (reader["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(reader["SEGUNDO_APELLIDO"]);
                            if (reader["IDENTIFICACION"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(reader["IDENTIFICACION"].ToString());

                            lstPersona.Add(entidad);
                        }

                        if (lstPersona.Count == 0)
                        {
                            throw new ExceptionBusiness("No existe información con dichos criterios de búsqueda.");
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "Listar", ex);
                        return null;
                    }

                }
            }
        }

        private static string ObtenerQueryfiltro(Persona pEntityPersona)
        {
            String filtro = "";
            String sql = "";
            sql = " where ";
            filtro = "";

            if (pEntityPersona.PrimerNombre.Trim().Length > 0)
            {
                if (filtro.Trim().Length > 0)
                {
                    filtro = filtro + " and ";
                }
                filtro += " RTRIM(LTRIM(PRIMER_NOMBRE)) = '" + pEntityPersona.PrimerNombre.ToString().Trim() + "'";

            }
            if (pEntityPersona.SegundoNombre.Trim().Length > 0)
            {
                if (filtro.Trim().Length > 0)
                {
                    filtro = filtro + " and ";
                }
                filtro += " RTRIM(LTRIM(SEGUNDO_NOMBRE)) = '" + pEntityPersona.SegundoNombre.ToString().Trim() + "'";

            }
            if (pEntityPersona.PrimerApellido.Trim().Length > 0)
            {
                if (filtro.Trim().Length > 0)
                {
                    filtro = filtro + " and ";
                }
                filtro += " RTRIM(LTRIM(PRIMER_APELLIDO)) = '" + pEntityPersona.PrimerApellido.ToString().Trim() + "'";

            }
            if (pEntityPersona.SegundoApellido.Trim().Length > 0)
            {
                if (filtro.Trim().Length > 0)
                {
                    filtro = filtro + " and ";
                }
                filtro += " RTRIM(LTRIM(SEGUNDO_APELLIDO)) = '" + pEntityPersona.SegundoApellido.ToString().Trim() + "'";

            }
            if (pEntityPersona.NumeroDocumento.Trim() != "")
            {
                if (filtro.Trim().Length > 0)
                {
                    filtro = filtro + " and ";
                }
                filtro = " IDENTIFICACION = " + pEntityPersona.NumeroDocumento.ToString().Trim();
            }
            if (pEntityPersona.IdPersona > 0)
            {
                if (filtro.Trim().Length > 0)
                {
                    filtro = filtro + " and ";
                }
                filtro = " COD_PERSONA  = " + pEntityPersona.IdPersona.ToString().Trim();
            }

            if (pEntityPersona.NoCredito > 0)
            {
                if (filtro.Trim().Length > 0)
                {
                    filtro = filtro + " and ";
                }
                filtro = " numero_radicacion  = " + pEntityPersona.NoCredito.ToString().Trim();
            }
            if (filtro.Trim().Length > 0)
            {
                filtro = sql + filtro;
            }

            return filtro;
        }

        public Persona ConsultaryaExistente(Int64 pIdEntityPersona, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            Persona EntityPersona = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                                @"SELECT	cod_persona, tipo_identificacion, identificacion, nombres, apellidos, fechanacimiento, 
                                    direccion, telefono, email, estado, zona, oficina, ejecutivo, calificacion, tipo_cliente, fechacreacion, SYSDATE As fecha
	                                FROM   VAsesoresDatosCliente 
	                                WHERE	Cod_Persona = " + pIdEntityPersona;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["cod_persona"] != DBNull.Value) EntityPersona.IdPersona = Convert.ToInt64(reader["cod_persona"].ToString());
                            if (reader["nombres"] != DBNull.Value) EntityPersona.PrimerNombre = reader["nombres"].ToString();
                            if (reader["apellidos"] != DBNull.Value) EntityPersona.PrimerApellido = reader["apellidos"].ToString();
                            if (reader["tipo_identificacion"] != DBNull.Value) EntityPersona.TipoIdentificacion.NombreTipoIdentificacion = reader["tipo_identificacion"].ToString();
                            if (reader["identificacion"] != DBNull.Value) EntityPersona.NumeroDocumento = Convert.ToString(reader["identificacion"].ToString());
                            if (reader["fechanacimiento"] != DBNull.Value) EntityPersona.FechaNacimiento = Convert.ToDateTime(reader["fechanacimiento"].ToString());
                            if (reader["direccion"] != DBNull.Value)
                                EntityPersona.Direccion = reader["direccion"].ToString();
                            if (EntityPersona.Direccion == null)
                                EntityPersona.Direccion = "  ";
                            if (reader["TELEFONO"] != DBNull.Value) EntityPersona.Telefono = Convert.ToString(reader["TELEFONO"].ToString());
                            if (reader["email"] != DBNull.Value) EntityPersona.Email = reader["email"].ToString();
                            if (reader["estado"] != DBNull.Value) EntityPersona.Estado = reader["estado"].ToString();
                            if (reader["zona"] != DBNull.Value) EntityPersona.Zona.NombreZona = reader["zona"].ToString();
                            if (reader["oficina"] != DBNull.Value) EntityPersona.Oficina.NombreOficina = reader["oficina"].ToString();
                            if (reader["ejecutivo"] != DBNull.Value) EntityPersona.Asesor.PrimerNombre = reader["ejecutivo"].ToString();
                            if (reader["calificacion"] != DBNull.Value) EntityPersona.Calificacion = Convert.ToInt16(reader["calificacion"].ToString());
                            if (reader["tipo_cliente"] != DBNull.Value) EntityPersona.TipoCliente = reader["tipo_cliente"].ToString();
                            if (reader["FECHACREACION"] != DBNull.Value) EntityPersona.FechaAfiliacion = Convert.ToDateTime(reader["FECHACREACION"].ToString());

                        }
                        else
                        {
                            EntityPersona.NumeroDocumento = "000";
                        }
                        return EntityPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarPersona", ex);
                        return null;
                    }
                }
            }

        }
    }
}
   
   