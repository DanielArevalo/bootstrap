using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

namespace Xpinn.FabricaCreditos.Data
{
    public class PersonaEmpresaRecaudoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public PersonaEmpresaRecaudoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public PersonaEmpresaRecaudo CrearPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidempresarecaudo = cmdTransaccionFactory.CreateParameter();
                        pidempresarecaudo.ParameterName = "p_idempresarecaudo";
                        pidempresarecaudo.Value = 0;
                        pidempresarecaudo.Direction = ParameterDirection.Output;
                        pidempresarecaudo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidempresarecaudo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersonaEmpresaRecaudo.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pPersonaEmpresaRecaudo.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONAEMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pPersonaEmpresaRecaudo.idempresarecaudo = Convert.ToInt64(pidempresarecaudo.Value);

                        return pPersonaEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaEmpresaRecaudoData", "CrearPersonaEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public PersonaEmpresaRecaudo ModificarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidempresarecaudo = cmdTransaccionFactory.CreateParameter();
                        pidempresarecaudo.ParameterName = "p_idempresarecaudo";
                        pidempresarecaudo.Value = pPersonaEmpresaRecaudo.idempresarecaudo;
                        pidempresarecaudo.Direction = ParameterDirection.Input;
                        pidempresarecaudo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidempresarecaudo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersonaEmpresaRecaudo.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pPersonaEmpresaRecaudo.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONAEMP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pPersonaEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaEmpresaRecaudoData", "ModificarPersonaEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPersonaEmpresaRecaudo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PersonaEmpresaRecaudo pPersonaEmpresaRecaudo = new PersonaEmpresaRecaudo();
                        pPersonaEmpresaRecaudo = ConsultarPersonaEmpresaRecaudo(pId, vUsuario);

                        DbParameter pidempresarecaudo = cmdTransaccionFactory.CreateParameter();
                        pidempresarecaudo.ParameterName = "p_idempresarecaudo";
                        pidempresarecaudo.Value = pPersonaEmpresaRecaudo.idempresarecaudo;
                        pidempresarecaudo.Direction = ParameterDirection.Input;
                        pidempresarecaudo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidempresarecaudo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONAEMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaEmpresaRecaudoData", "EliminarPersonaEmpresaRecaudo", ex);
                    }
                }
            }
        }

        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaEmpresaRecaudo> lstPersonaEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Persona_Empresa_Recaudo " + ObtenerFiltro(pPersonaEmpresaRecaudo) + " ORDER BY IDEMPRESARECAUDO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaEmpresaRecaudo entidad = new PersonaEmpresaRecaudo();
                            if (resultado["IDEMPRESARECAUDO"] != DBNull.Value) entidad.idempresarecaudo = Convert.ToInt64(resultado["IDEMPRESARECAUDO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            lstPersonaEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaEmpresaRecaudoData", "ListarPersonaEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(Int64 pCodPersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaEmpresaRecaudo> lstPersonaEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Consulta modificada ya que se cargaba registros no correspondientes a la empresa de recaudo registrada del asociado
                        string sql = @"SELECT Empresa_Recaudo.cod_empresa, Empresa_Recaudo.nom_empresa, Persona_Empresa_Recaudo.cod_persona, Persona_Empresa_Recaudo.idempresarecaudo FROM Empresa_Recaudo left Join Persona_Empresa_Recaudo On Empresa_Recaudo.cod_empresa = Persona_Empresa_Recaudo.cod_empresa And Persona_Empresa_Recaudo.cod_persona = " + pCodPersona + " ORDER BY cod_empresa ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaEmpresaRecaudo entidad = new PersonaEmpresaRecaudo();
                            if (resultado["IDEMPRESARECAUDO"] != DBNull.Value) entidad.idempresarecaudo = Convert.ToInt64(resultado["IDEMPRESARECAUDO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (entidad.idempresarecaudo != null)
                                entidad.seleccionar = true;
                            else
                                entidad.seleccionar = false;
                            lstPersonaEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaEmpresaRecaudoData", "ListarPersonaEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public PersonaEmpresaRecaudo ConsultarPersonaEmpresaRecaudo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            PersonaEmpresaRecaudo entidad = new PersonaEmpresaRecaudo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Persona_Empresa_Recaudo WHERE IDEMPRESARECAUDO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDEMPRESARECAUDO"] != DBNull.Value) entidad.idempresarecaudo = Convert.ToInt64(resultado["IDEMPRESARECAUDO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
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
                        BOExcepcion.Throw("PersonaEmpresaRecaudoData", "ConsultarPersonaEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<PersonaEmpresaRecaudo> ListarEmpresaRecaudo(Usuario vUsuario)
        {
            return ListarEmpresaRecaudo(false, vUsuario);
        }

        public List<PersonaEmpresaRecaudo> ListarEmpresaRecaudo(bool alfabetico, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM empresa_recaudo WHERE estado = 1 ORDER BY ";
                        if (alfabetico) {
                            sql += "NOM_EMPRESA ASC";
                        }else { sql += "COD_EMPRESA"; }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaEmpresaRecaudo entidad = new PersonaEmpresaRecaudo();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOM_EMPRESA"]);                            
                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ListarEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }



    }
}
