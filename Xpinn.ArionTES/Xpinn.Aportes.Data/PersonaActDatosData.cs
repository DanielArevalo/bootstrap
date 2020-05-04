using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

namespace Xpinn.Aportes.Data
{
    public class PersonaActDatosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public PersonaActDatosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public PersonaActDatos CrearPersonaActDatos(PersonaActDatos pPersonaActDatos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_update = cmdTransaccionFactory.CreateParameter();
                        pid_update.ParameterName = "p_id_update";
                        pid_update.Value = pPersonaActDatos.id_update;
                        pid_update.Direction = ParameterDirection.Input;
                        pid_update.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_update);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pPersonaActDatos.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pPersonaActDatos.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_act = cmdTransaccionFactory.CreateParameter();
                        pfecha_act.ParameterName = "p_fecha_act";
                        if (pPersonaActDatos.fecha_act == null)
                            pfecha_act.Value = DBNull.Value;
                        else
                            pfecha_act.Value = pPersonaActDatos.fecha_act;
                        pfecha_act.Direction = ParameterDirection.Input;
                        pfecha_act.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_act);

                        DbParameter pcod_usua = cmdTransaccionFactory.CreateParameter();
                        pcod_usua.ParameterName = "p_cod_usua";
                        if (pPersonaActDatos.cod_usua == null)
                            pcod_usua.Value = DBNull.Value;
                        else
                            pcod_usua.Value = pPersonaActDatos.cod_usua;
                        pcod_usua.Direction = ParameterDirection.Input;
                        pcod_usua.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usua);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSOUPDATEDATA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersonaActDatos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaActDatosData", "CrearPersonaActDatos", ex);
                        return null;
                    }
                }
            }
        }

        public SolicitudPersonaAfi ActualizarDatosPersona(SolicitudPersonaAfi pPersona, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                                //DATOS ASOCIADO
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona = cmdTransaccionFactory.CreateParameter();
                                pid_persona.ParameterName = "p_id_persona";
                                pid_persona.Value = pPersona.id_persona;
                                pid_persona.Direction = ParameterDirection.Input;
                                pid_persona.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona);                                
                              
                                //--pEntidad.direccion 19
                                DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                                pdireccion.ParameterName = "p_direccion";
                                if (pPersona.direccion == null)
                                    pdireccion.Value = "";
                                else
                                    pdireccion.Value = pPersona.direccion;
                                pdireccion.Direction = ParameterDirection.Input;
                                pdireccion.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pdireccion);


                                //--pEntidad.barrio 21
                                DbParameter pbarrio = cmdTransaccionFactory.CreateParameter();
                                pbarrio.ParameterName = "p_barrio";
                                if (pPersona.barrio == null)
                                    pbarrio.Value = 0;
                                else
                                    pbarrio.Value = pPersona.barrio;
                                pbarrio.Direction = ParameterDirection.Input;
                                pbarrio.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pbarrio);

                                //--pEntidad.departamento 23
                                DbParameter pdepartamento = cmdTransaccionFactory.CreateParameter();
                                pdepartamento.ParameterName = "p_departamento";
                                if (pPersona.departamento == null)
                                    pdepartamento.Value = 0;
                                else
                                    pdepartamento.Value = pPersona.departamento;
                                pdepartamento.Direction = ParameterDirection.Input;
                                pdepartamento.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pdepartamento);

                                //--pEntidad.ciudad 22
                                DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                                pciudad.ParameterName = "p_ciudad";
                                if (pPersona.ciudad == null)
                                    pciudad.Value = 0;
                                else
                                    pciudad.Value = pPersona.ciudad;
                                pciudad.Direction = ParameterDirection.Input;
                                pciudad.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pciudad);

                                //--pEntidad.tipoVivienda  63
                                DbParameter ptipoVivienda = cmdTransaccionFactory.CreateParameter();
                                ptipoVivienda.ParameterName = "ptipoVivienda";
                                if (pPersona.tipoVivienda == null)
                                    ptipoVivienda.Value = "";
                                else
                                    ptipoVivienda.Value = pPersona.tipoVivienda;
                                ptipoVivienda.Direction = ParameterDirection.Input;
                                ptipoVivienda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(ptipoVivienda);


                                //--pEntidad.estrato 20
                                DbParameter pestrato = cmdTransaccionFactory.CreateParameter();
                                pestrato.ParameterName = "p_estrato";
                                if (pPersona.estrato == null)
                                    pestrato.Value = 0;
                                else
                                    pestrato.Value = pPersona.estrato;
                                pestrato.Direction = ParameterDirection.Input;
                                pestrato.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pestrato);

                                //--pEntidad.afecta_vivienda 64
                                DbParameter pAfectaVivienda = cmdTransaccionFactory.CreateParameter();
                                pAfectaVivienda.ParameterName = "pAfectaVivienda";
                                pAfectaVivienda.Value = pPersona.afecta_vivienda;
                                pAfectaVivienda.Direction = ParameterDirection.Input;
                                pAfectaVivienda.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pAfectaVivienda);

                                //--pEntidad.años_vivienda 65
                                DbParameter pAniosVivienda = cmdTransaccionFactory.CreateParameter();
                                pAniosVivienda.ParameterName = "pAniosVivienda";
                                if (pPersona.años_vivienda == null)
                                    pAniosVivienda.Value = 0;
                                else
                                    pAniosVivienda.Value = pPersona.años_vivienda;
                                pAniosVivienda.Direction = ParameterDirection.Input;
                                pAniosVivienda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pAniosVivienda);

                                //--pEntidad.meses_vivienda  66
                                DbParameter pMesesVivienda = cmdTransaccionFactory.CreateParameter();
                                pMesesVivienda.ParameterName = "pMesesVivienda";
                                if (pPersona.meses_vivienda == null)
                                    pMesesVivienda.Value = 0;
                                else
                                    pMesesVivienda.Value = pPersona.meses_vivienda;
                                pMesesVivienda.Direction = ParameterDirection.Input;
                                pMesesVivienda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pMesesVivienda);


                                //--pEntidad.email
                                DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                                pemail.ParameterName = "p_email";
                                if (pPersona.email == null)
                                    pemail.Value = "";
                                else
                                    pemail.Value = pPersona.email;
                                pemail.Direction = ParameterDirection.Input;
                                pemail.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pemail);

                                //--pEntidad.telefono
                                DbParameter P_TELEFONO = cmdTransaccionFactory.CreateParameter();
                                P_TELEFONO.ParameterName = "P_TELEFONO";
                                if (pPersona.telefono == null)
                                    P_TELEFONO.Value = "";
                                else
                                    P_TELEFONO.Value = pPersona.telefono;
                                P_TELEFONO.Direction = ParameterDirection.Input;
                                P_TELEFONO.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(P_TELEFONO);


                                //--pEntidad.celular
                                DbParameter P_CELULAR = cmdTransaccionFactory.CreateParameter();
                                P_CELULAR.ParameterName = "P_CELULAR";
                                if (pPersona.celular == null)
                                    P_CELULAR.Value = "";
                                else
                                    P_CELULAR.Value = pPersona.celular;
                                P_CELULAR.Direction = ParameterDirection.Input;
                                P_CELULAR.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(P_CELULAR);

                                //--pEntidad.fecha_inicio 35
                                DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                                pfecha_inicio.ParameterName = "p_fecha_inicio";
                                if (pPersona.fecha_inicio == null)
                                    pfecha_inicio.Value = DBNull.Value;
                                else
                                    pfecha_inicio.Value = pPersona.fecha_inicio;
                                pfecha_inicio.Direction = ParameterDirection.Input;
                                pfecha_inicio.DbType = DbType.Date;
                                cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                                //--pEntidad.codcargo 51
                                DbParameter p_codcargo = cmdTransaccionFactory.CreateParameter();
                                p_codcargo.ParameterName = "p_codcargo";
                                if (pPersona.codcargo == 0)
                                    p_codcargo.Value = 0;
                                else
                                    p_codcargo.Value = pPersona.codcargo;
                                p_codcargo.Direction = ParameterDirection.Input;
                                p_codcargo.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(p_codcargo);

                                //--pEntidad.estado 27
                                DbParameter pestado_empresa = cmdTransaccionFactory.CreateParameter();
                                pestado_empresa.ParameterName = "p_estado_empresa";
                                if (pPersona.estado_empresa == null)
                                    pestado_empresa.Value = "";
                                else
                                    pestado_empresa.Value = pPersona.estado_empresa;
                                pestado_empresa.Direction = ParameterDirection.Input;
                                pestado_empresa.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pestado_empresa);


                                //--pEntidad.codtipocontrato 34
                                DbParameter pcodtipocontrato = cmdTransaccionFactory.CreateParameter();
                                pcodtipocontrato.ParameterName = "p_codtipocontrato";
                                if (pPersona.codtipocontrato == null)
                                    pcodtipocontrato.Value = 0;
                                else
                                    pcodtipocontrato.Value = pPersona.codtipocontrato;
                                pcodtipocontrato.Direction = ParameterDirection.Input;
                                pcodtipocontrato.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pcodtipocontrato);

                                //--pEntidad.salario  36
                                DbParameter psalario = cmdTransaccionFactory.CreateParameter();
                                psalario.ParameterName = "p_salario";
                                if (pPersona.salario == null)
                                    psalario.Value = 0;
                                else
                                    psalario.Value = pPersona.salario;
                                psalario.Direction = ParameterDirection.Input;
                                psalario.DbType = DbType.Decimal;
                                cmdTransaccionFactory.Parameters.Add(psalario);

                                //--pEntidad.email_contacto  69
                                DbParameter pemail_contacto = cmdTransaccionFactory.CreateParameter();
                                pemail_contacto.ParameterName = "pemail_contacto";
                                if (pPersona.email_contacto == null)
                                    pemail_contacto.Value = "";
                                else
                                    pemail_contacto.Value = pPersona.email_contacto;
                                pemail_contacto.Direction = ParameterDirection.Input;
                                pemail_contacto.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pemail_contacto);

                                //--pEntidad.telefono_empresa 31
                                DbParameter ptelefono_empresa = cmdTransaccionFactory.CreateParameter();
                                ptelefono_empresa.ParameterName = "p_telefono_empresa";
                                if (pPersona.telefono_empresa == null)
                                    ptelefono_empresa.Value = "";
                                else
                                    ptelefono_empresa.Value = pPersona.telefono_empresa;
                                ptelefono_empresa.Direction = ParameterDirection.Input;
                                ptelefono_empresa.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(ptelefono_empresa);

                                //--pEntidad.profesion 18
                                DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                                pprofesion.ParameterName = "p_profesion";
                                if (pPersona.profesion == null)
                                    pprofesion.Value = "";
                                else
                                    pprofesion.Value = pPersona.profesion;
                                pprofesion.Direction = ParameterDirection.Input;
                                pprofesion.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pprofesion);

                                //--pEntidad.profesion 18
                                DbParameter p_zona = cmdTransaccionFactory.CreateParameter();
                                p_zona.ParameterName = "p_zona";
                                if (pPersona.cod_pagaduria == 0)
                                    p_zona.Value = 0;
                                else
                                    p_zona.Value = pPersona.cod_pagaduria;
                                p_zona.Direction = ParameterDirection.Input;
                                p_zona.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(p_zona);

                        //EJECUCIÓN DEL PL
                        connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONAWEB_MOD";                                
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        pPersona.rpta = false;
                        pPersona.mensaje_error = ex.Message;
                    }
                    return pPersona;
            }
        }
    }

    }
}
