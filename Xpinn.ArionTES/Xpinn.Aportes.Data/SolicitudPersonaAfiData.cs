using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Data
{
    public class SolicitudPersonaAfiData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public SolicitudPersonaAfiData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }        


        public SolicitudPersonaAfi CrearSolicitudPersonaAfi(SolicitudPersonaAfi pSolicitudPersonaAfi, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        switch (pOpcion)
                        {
                            //Crea cliente potencial desde app
                            case 0:

                                DbParameter pid_persona0 = cmdTransaccionFactory.CreateParameter();
                                pid_persona0.ParameterName = "p_id_persona";
                                pid_persona0.Value = pSolicitudPersonaAfi.id_persona;
                                if (pOpcion == 1) //CREAR
                                    pid_persona0.Direction = ParameterDirection.Output;
                                else
                                    pid_persona0.Direction = ParameterDirection.Input;
                                pid_persona0.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona0);

                                //--pEntidad.primer_nombre 3
                                DbParameter pprimer_nombre0 = cmdTransaccionFactory.CreateParameter();
                                pprimer_nombre0.ParameterName = "p_primer_nombre";
                                pprimer_nombre0.Value = pSolicitudPersonaAfi.primer_nombre;
                                pprimer_nombre0.Direction = ParameterDirection.Input;
                                pprimer_nombre0.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pprimer_nombre0);

                                //--pEntidad.segundo_nombre 4
                                DbParameter psegundo_nombre0 = cmdTransaccionFactory.CreateParameter();
                                psegundo_nombre0.ParameterName = "p_segundo_nombre";
                                if (pSolicitudPersonaAfi.segundo_nombre == null)
                                    psegundo_nombre0.Value = "";
                                else
                                    psegundo_nombre0.Value = pSolicitudPersonaAfi.segundo_nombre;
                                psegundo_nombre0.Direction = ParameterDirection.Input;
                                psegundo_nombre0.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(psegundo_nombre0);

                                //--pEntidad.primer_apellido 5
                                DbParameter pprimer_apellido0 = cmdTransaccionFactory.CreateParameter();
                                pprimer_apellido0.ParameterName = "p_primer_apellido";
                                if (pSolicitudPersonaAfi.primer_apellido == null)
                                    pprimer_apellido0.Value = "";
                                else
                                    pprimer_apellido0.Value = pSolicitudPersonaAfi.primer_apellido;
                                pprimer_apellido0.Direction = ParameterDirection.Input;
                                pprimer_apellido0.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pprimer_apellido0);

                                //--pEntidad.segundo_apellido 6
                                DbParameter psegundo_apellido0 = cmdTransaccionFactory.CreateParameter();
                                psegundo_apellido0.ParameterName = "p_segundo_apellido";
                                if (pSolicitudPersonaAfi.segundo_apellido == null)
                                    psegundo_apellido0.Value = "";
                                else
                                    psegundo_apellido0.Value = pSolicitudPersonaAfi.segundo_apellido;
                                psegundo_apellido0.Direction = ParameterDirection.Input;
                                psegundo_apellido0.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(psegundo_apellido0);

                                //--pEntidad.tipo_identificacion 8
                                DbParameter ptipo_identificacion0 = cmdTransaccionFactory.CreateParameter();
                                ptipo_identificacion0.ParameterName = "p_tipo_identificacion";
                                ptipo_identificacion0.Value = pSolicitudPersonaAfi.tipo_identificacion;
                                ptipo_identificacion0.Direction = ParameterDirection.Input;
                                ptipo_identificacion0.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(ptipo_identificacion0);

                                //--pEntidad.identificacion 9
                                DbParameter pidentificacion0 = cmdTransaccionFactory.CreateParameter();
                                pidentificacion0.ParameterName = "p_identificacion";
                                pidentificacion0.Value = pSolicitudPersonaAfi.identificacion;
                                pidentificacion0.Direction = ParameterDirection.Input;
                                pidentificacion0.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pidentificacion0);

                                //--pEntidad.fecha_expedicion 10
                                DbParameter pfecha_expedicion0 = cmdTransaccionFactory.CreateParameter();
                                pfecha_expedicion0.ParameterName = "p_fecha_expedicion";
                                pfecha_expedicion0.Value = pSolicitudPersonaAfi.fecha_expedicion;
                                pfecha_expedicion0.Direction = ParameterDirection.Input;
                                pfecha_expedicion0.DbType = DbType.DateTime;
                                cmdTransaccionFactory.Parameters.Add(pfecha_expedicion0);

                                //--pEntidad.email
                                DbParameter pemail0 = cmdTransaccionFactory.CreateParameter();
                                pemail0.ParameterName = "p_email";
                                if (pSolicitudPersonaAfi.email == null)
                                    pemail0.Value = "";
                                else
                                    pemail0.Value = pSolicitudPersonaAfi.email;
                                pemail0.Direction = ParameterDirection.Input;
                                pemail0.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pemail0);

                                //--pEntidad.celular
                                DbParameter P_CELULAR0 = cmdTransaccionFactory.CreateParameter();
                                P_CELULAR0.ParameterName = "P_CELULAR";
                                if (pSolicitudPersonaAfi.celular == null)
                                    P_CELULAR0.Value = "";
                                else
                                    P_CELULAR0.Value = pSolicitudPersonaAfi.celular;
                                P_CELULAR0.Direction = ParameterDirection.Input;
                                P_CELULAR0.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(P_CELULAR0);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA0";                                
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                if (pid_persona0.Value != null)
                                {
                                    pSolicitudPersonaAfi.id_persona = Convert.ToInt64(pid_persona0.Value);
                                    pSolicitudPersonaAfi.rpta = true;
                                }
                                break;

                            case 1:
                                //DATOS ASOCIADO
                                //--pEntidad.tipo_persona  50
                                DbParameter p_tipo_persona = cmdTransaccionFactory.CreateParameter();
                                p_tipo_persona.ParameterName = "p_tipo_persona";
                                if (pSolicitudPersonaAfi.tipo_persona == null)
                                    p_tipo_persona.Value = "";
                                else
                                    p_tipo_persona.Value = pSolicitudPersonaAfi.tipo_persona;
                                p_tipo_persona.Direction = ParameterDirection.Input;
                                p_tipo_persona.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_tipo_persona);

                                //--pEntidad.id_persona 1
                                DbParameter pid_persona = cmdTransaccionFactory.CreateParameter();
                                pid_persona.ParameterName = "p_id_persona";
                                pid_persona.Value = pSolicitudPersonaAfi.id_persona;
                                if (pOpcion == 1) //CREAR
                                    pid_persona.Direction = ParameterDirection.Output;
                                else
                                    pid_persona.Direction = ParameterDirection.Input;
                                pid_persona.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona);

                                //--PENTIDAD FECHA 2
                                DbParameter p_fecha_creacion = cmdTransaccionFactory.CreateParameter();
                                p_fecha_creacion.ParameterName = "p_fecha_creacion";
                                if (pSolicitudPersonaAfi.fecha_creacion != DateTime.MinValue)
                                    p_fecha_creacion.Value = pSolicitudPersonaAfi.fecha_expedicion;
                                else p_fecha_creacion.Value = DateTime.Now;
                                p_fecha_creacion.Direction = ParameterDirection.Input;
                                p_fecha_creacion.DbType = DbType.DateTime;
                                cmdTransaccionFactory.Parameters.Add(p_fecha_creacion);


                                //--pEntidad.primer_nombre 3
                                DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                                pprimer_nombre.ParameterName = "p_primer_nombre";
                                pprimer_nombre.Value = pSolicitudPersonaAfi.primer_nombre;
                                pprimer_nombre.Direction = ParameterDirection.Input;
                                pprimer_nombre.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                                //--pEntidad.segundo_nombre 4
                                DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                                psegundo_nombre.ParameterName = "p_segundo_nombre";
                                if (pSolicitudPersonaAfi.segundo_nombre == null)
                                    psegundo_nombre.Value = "";
                                else
                                    psegundo_nombre.Value = pSolicitudPersonaAfi.segundo_nombre;
                                psegundo_nombre.Direction = ParameterDirection.Input;
                                psegundo_nombre.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                                //--pEntidad.primer_apellido 5
                                DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                                pprimer_apellido.ParameterName = "p_primer_apellido";
                                if (pSolicitudPersonaAfi.primer_apellido == null)
                                    pprimer_apellido.Value = "";
                                else
                                    pprimer_apellido.Value = pSolicitudPersonaAfi.primer_apellido;
                                pprimer_apellido.Direction = ParameterDirection.Input;
                                pprimer_apellido.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                                //--pEntidad.segundo_apellido 6
                                DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                                psegundo_apellido.ParameterName = "p_segundo_apellido";
                                if (pSolicitudPersonaAfi.segundo_apellido == null)
                                    psegundo_apellido.Value = "";
                                else
                                    psegundo_apellido.Value = pSolicitudPersonaAfi.segundo_apellido;
                                psegundo_apellido.Direction = ParameterDirection.Input;
                                psegundo_apellido.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                                //--pEntidad.tipo_identificacion 8
                                DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                                ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                                ptipo_identificacion.Value = pSolicitudPersonaAfi.tipo_identificacion;
                                ptipo_identificacion.Direction = ParameterDirection.Input;
                                ptipo_identificacion.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                                //--pEntidad.identificacion 9
                                DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                                pidentificacion.ParameterName = "p_identificacion";
                                pidentificacion.Value = pSolicitudPersonaAfi.identificacion;
                                pidentificacion.Direction = ParameterDirection.Input;
                                pidentificacion.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pidentificacion);

                                //-- out mensaje error 61
                                DbParameter pMENSAJE = cmdTransaccionFactory.CreateParameter();
                                pMENSAJE.ParameterName = "p_mensaje_error";
                                pMENSAJE.Size = 200;
                                pMENSAJE.Direction = ParameterDirection.Output;
                                pMENSAJE.Value = pSolicitudPersonaAfi.mensaje_error;
                                cmdTransaccionFactory.Parameters.Add(pMENSAJE);


                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA1";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                pSolicitudPersonaAfi.mensaje_error = Convert.ToString(pMENSAJE.Value);
                                pSolicitudPersonaAfi.rpta = false;
                                if (pSolicitudPersonaAfi.mensaje_error == null || pSolicitudPersonaAfi.mensaje_error == "")
                                {
                                    if (pid_persona.Value != null)
                                    {
                                        pSolicitudPersonaAfi.id_persona = Convert.ToInt64(pid_persona.Value);                                            
                                        pSolicitudPersonaAfi.rpta = true;
                                    }
                                    else
                                    {
                                        pSolicitudPersonaAfi.rpta = false;
                                    }
                                }
                                break;

                            case 2:
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona2 = cmdTransaccionFactory.CreateParameter();
                                pid_persona2.ParameterName = "p_id_persona";
                                pid_persona2.Value = pSolicitudPersonaAfi.id_persona;
                                pid_persona2.Direction = ParameterDirection.Input;                                
                                pid_persona2.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona2);

                                //--pEntidad.sexo 7
                                DbParameter psexo = cmdTransaccionFactory.CreateParameter();
                                psexo.ParameterName = "p_sexo";
                                if (pSolicitudPersonaAfi.sexo == null)
                                    psexo.Value = "";
                                else
                                    psexo.Value = pSolicitudPersonaAfi.sexo;
                                psexo.Direction = ParameterDirection.Input;
                                psexo.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(psexo);


                                //--pEntidad.fecha_expedicion 10
                                DbParameter pfecha_expedicion = cmdTransaccionFactory.CreateParameter();
                                pfecha_expedicion.ParameterName = "p_fecha_expedicion";
                                pfecha_expedicion.Value = pSolicitudPersonaAfi.fecha_expedicion;
                                pfecha_expedicion.Direction = ParameterDirection.Input;
                                pfecha_expedicion.DbType = DbType.DateTime;
                                cmdTransaccionFactory.Parameters.Add(pfecha_expedicion);

                                //--pEntidad.ciudad_expedicion  11                      
                                DbParameter pciudad_expedicion = cmdTransaccionFactory.CreateParameter();
                                pciudad_expedicion.ParameterName = "p_ciudad_expedicion";
                                if (pSolicitudPersonaAfi.ciudad_expedicion != null && pSolicitudPersonaAfi.ciudad_expedicion != 0)
                                    pciudad_expedicion.Value = pSolicitudPersonaAfi.ciudad_expedicion;
                                else pciudad_expedicion.Value = 0;
                                pciudad_expedicion.Direction = ParameterDirection.Input;
                                pciudad_expedicion.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pciudad_expedicion);


                                //--pEntidad.ciudad_nacimiento    13                    
                                DbParameter pciudad_nacimiento = cmdTransaccionFactory.CreateParameter();
                                pciudad_nacimiento.ParameterName = "p_ciudad_nacimiento";
                                if (pSolicitudPersonaAfi.ciudad_nacimiento == null)
                                    pciudad_nacimiento.Value = 0;
                                else
                                    pciudad_nacimiento.Value = pSolicitudPersonaAfi.ciudad_nacimiento;
                                pciudad_nacimiento.Direction = ParameterDirection.Input;
                                pciudad_nacimiento.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pciudad_nacimiento);


                                //--pEntidad.fecha_nacimiento
                                DbParameter pfecha_nacimiento = cmdTransaccionFactory.CreateParameter();
                                pfecha_nacimiento.ParameterName = "p_fecha_nacimiento";
                                if (pSolicitudPersonaAfi.fecha_nacimiento == null)
                                    pfecha_nacimiento.Value = DBNull.Value;
                                else
                                    pfecha_nacimiento.Value = pSolicitudPersonaAfi.fecha_nacimiento;
                                pfecha_nacimiento.Direction = ParameterDirection.Input;
                                pfecha_nacimiento.DbType = DbType.Date;
                                cmdTransaccionFactory.Parameters.Add(pfecha_nacimiento);


                                //--pEntidad.pais -- nacionalidad
                                DbParameter p_nacionalidad = cmdTransaccionFactory.CreateParameter();
                                p_nacionalidad.ParameterName = "p_nacionalidad";
                                if (string.IsNullOrEmpty(pSolicitudPersonaAfi.pais))
                                    p_nacionalidad.Value = "";
                                else
                                    p_nacionalidad.Value = pSolicitudPersonaAfi.pais;
                                p_nacionalidad.Direction = ParameterDirection.Input;
                                p_nacionalidad.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_nacionalidad);

                                //--pEntidad.codestadocivil 14
                                DbParameter pcodestadocivil = cmdTransaccionFactory.CreateParameter();
                                pcodestadocivil.ParameterName = "p_codestadocivil";
                                if (pSolicitudPersonaAfi.codestadocivil == null || pSolicitudPersonaAfi.codestadocivil == 0)
                                    pcodestadocivil.Value = DBNull.Value;
                                else
                                    pcodestadocivil.Value = pSolicitudPersonaAfi.codestadocivil;
                                pcodestadocivil.Direction = ParameterDirection.Input;
                                pcodestadocivil.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pcodestadocivil);

                                //--pEntidad.cabeza_familia 15
                                DbParameter pcabeza_familia = cmdTransaccionFactory.CreateParameter();
                                pcabeza_familia.ParameterName = "p_cabeza_familia";
                                if (pSolicitudPersonaAfi.cabeza_familia == null)
                                    pcabeza_familia.Value = 0;
                                else
                                    pcabeza_familia.Value = pSolicitudPersonaAfi.cabeza_familia;
                                pcabeza_familia.Direction = ParameterDirection.Input;
                                pcabeza_familia.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pcabeza_familia);

                                //--pEntidad.personas_cargo 16
                                DbParameter ppersonas_cargo = cmdTransaccionFactory.CreateParameter();
                                ppersonas_cargo.ParameterName = "p_personas_cargo";
                                if (pSolicitudPersonaAfi.personas_cargo == null)
                                    ppersonas_cargo.Value = 0;
                                else
                                    ppersonas_cargo.Value = pSolicitudPersonaAfi.personas_cargo;
                                ppersonas_cargo.Direction = ParameterDirection.Input;
                                ppersonas_cargo.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(ppersonas_cargo);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA2";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA2";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                limpiarBeneficiarioAfi(pSolicitudPersonaAfi.id_persona, vUsuario);
                                foreach (BeneficiarioPersonaAfi benef in pSolicitudPersonaAfi.lstBeneficiarios)
                                {
                                    benef.cod_solicitud = pSolicitudPersonaAfi.id_persona;
                                    CrearBeneficiarioAfi(benef, vUsuario);
                                }

                                pSolicitudPersonaAfi.rpta = true;                                
                                break;
                            case 3:
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona3 = cmdTransaccionFactory.CreateParameter();
                                pid_persona3.ParameterName = "p_id_persona";
                                pid_persona3.Value = pSolicitudPersonaAfi.id_persona;
                                pid_persona3.Direction = ParameterDirection.Input;
                                pid_persona3.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona3);

                                //--DATOS CONTACTO-----------------------------------------------------------------------------------------

                                //--pEntidad.direccion 19
                                DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                                pdireccion.ParameterName = "p_direccion";
                                if (pSolicitudPersonaAfi.direccion == null)
                                    pdireccion.Value = "";
                                else
                                    pdireccion.Value = pSolicitudPersonaAfi.direccion;
                                pdireccion.Direction = ParameterDirection.Input;
                                pdireccion.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pdireccion);


                                //--pEntidad.barrio 21
                                DbParameter pbarrio = cmdTransaccionFactory.CreateParameter();
                                pbarrio.ParameterName = "p_barrio";
                                if (pSolicitudPersonaAfi.barrio == null)
                                    pbarrio.Value = 0;
                                else
                                    pbarrio.Value = pSolicitudPersonaAfi.barrio;
                                pbarrio.Direction = ParameterDirection.Input;
                                pbarrio.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pbarrio);

                                //--pEntidad.departamento 23
                                DbParameter pdepartamento = cmdTransaccionFactory.CreateParameter();
                                pdepartamento.ParameterName = "p_departamento";
                                if (pSolicitudPersonaAfi.departamento == null)
                                    pdepartamento.Value = 0;
                                else
                                    pdepartamento.Value = pSolicitudPersonaAfi.departamento;
                                pdepartamento.Direction = ParameterDirection.Input;
                                pdepartamento.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pdepartamento);

                                //--pEntidad.ciudad 22
                                DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                                pciudad.ParameterName = "p_ciudad";
                                if (pSolicitudPersonaAfi.ciudad == null)
                                    pciudad.Value = 0;
                                else
                                    pciudad.Value = pSolicitudPersonaAfi.ciudad;
                                pciudad.Direction = ParameterDirection.Input;
                                pciudad.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pciudad);

                                //--pEntidad.tipoVivienda  63
                                DbParameter ptipoVivienda = cmdTransaccionFactory.CreateParameter();
                                ptipoVivienda.ParameterName = "ptipoVivienda";
                                if (pSolicitudPersonaAfi.tipoVivienda == null)
                                    ptipoVivienda.Value = "";
                                else
                                    ptipoVivienda.Value = pSolicitudPersonaAfi.tipoVivienda;
                                ptipoVivienda.Direction = ParameterDirection.Input;
                                ptipoVivienda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(ptipoVivienda);


                                //--pEntidad.estrato 20
                                DbParameter pestrato = cmdTransaccionFactory.CreateParameter();
                                pestrato.ParameterName = "p_estrato";
                                if (pSolicitudPersonaAfi.estrato == null)
                                    pestrato.Value = 0;
                                else
                                    pestrato.Value = pSolicitudPersonaAfi.estrato;
                                pestrato.Direction = ParameterDirection.Input;
                                pestrato.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pestrato);

                                //--pEntidad.afecta_vivienda 64
                                DbParameter pAfectaVivienda = cmdTransaccionFactory.CreateParameter();
                                pAfectaVivienda.ParameterName = "pAfectaVivienda";
                                pAfectaVivienda.Value = pSolicitudPersonaAfi.afecta_vivienda;
                                pAfectaVivienda.Direction = ParameterDirection.Input;
                                pAfectaVivienda.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pAfectaVivienda);

                                //--pEntidad.años_vivienda 65
                                DbParameter pAniosVivienda = cmdTransaccionFactory.CreateParameter();
                                pAniosVivienda.ParameterName = "pAniosVivienda";
                                if (pSolicitudPersonaAfi.años_vivienda == null)
                                    pAniosVivienda.Value = 0;
                                else
                                    pAniosVivienda.Value = pSolicitudPersonaAfi.años_vivienda;
                                pAniosVivienda.Direction = ParameterDirection.Input;
                                pAniosVivienda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pAniosVivienda);

                                //--pEntidad.meses_vivienda  66
                                DbParameter pMesesVivienda = cmdTransaccionFactory.CreateParameter();
                                pMesesVivienda.ParameterName = "pMesesVivienda";
                                if (pSolicitudPersonaAfi.meses_vivienda == null)
                                    pMesesVivienda.Value = 0;
                                else
                                    pMesesVivienda.Value = pSolicitudPersonaAfi.meses_vivienda;
                                pMesesVivienda.Direction = ParameterDirection.Input;
                                pMesesVivienda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pMesesVivienda);


                                //--pEntidad.email
                                DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                                pemail.ParameterName = "p_email";
                                if (pSolicitudPersonaAfi.email == null)
                                    pemail.Value = "";
                                else
                                    pemail.Value = pSolicitudPersonaAfi.email;
                                pemail.Direction = ParameterDirection.Input;
                                pemail.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pemail);

                                //--pEntidad.telefono
                                DbParameter P_TELEFONO = cmdTransaccionFactory.CreateParameter();
                                P_TELEFONO.ParameterName = "P_TELEFONO";
                                if (pSolicitudPersonaAfi.telefono == null)
                                    P_TELEFONO.Value = "";
                                else
                                    P_TELEFONO.Value = pSolicitudPersonaAfi.telefono;
                                P_TELEFONO.Direction = ParameterDirection.Input;
                                P_TELEFONO.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(P_TELEFONO);


                                //--pEntidad.celular
                                DbParameter P_CELULAR = cmdTransaccionFactory.CreateParameter();
                                P_CELULAR.ParameterName = "P_CELULAR";
                                if (pSolicitudPersonaAfi.celular == null)
                                    P_CELULAR.Value = "";
                                else
                                    P_CELULAR.Value = pSolicitudPersonaAfi.celular;
                                P_CELULAR.Direction = ParameterDirection.Input;
                                P_CELULAR.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(P_CELULAR);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA3";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA3";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                pSolicitudPersonaAfi.rpta = true;
                                break;

                            case 4:
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona4 = cmdTransaccionFactory.CreateParameter();
                                pid_persona4.ParameterName = "p_id_persona";
                                pid_persona4.Value = pSolicitudPersonaAfi.id_persona;
                                pid_persona4.Direction = ParameterDirection.Input;
                                pid_persona4.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona4);

                                //-- DATOS LABORALES --------------------------------------------------------------------------------------------------

                                //--pEntidad.empresa 28
                                DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                                pempresa.ParameterName = "p_empresa";
                                if (pSolicitudPersonaAfi.empresa == null)
                                    pempresa.Value = "";
                                else
                                    pempresa.Value = pSolicitudPersonaAfi.empresa;
                                pempresa.Direction = ParameterDirection.Input;
                                pempresa.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pempresa);


                                //--pEntidad.cod_pagaduria  67
                                DbParameter pcod_pagaduria = cmdTransaccionFactory.CreateParameter();
                                pcod_pagaduria.ParameterName = "pcod_pagaduria";
                                if (pSolicitudPersonaAfi.cod_pagaduria == 0)
                                    pcod_pagaduria.Value = 0;
                                else
                                    pcod_pagaduria.Value = pSolicitudPersonaAfi.cod_pagaduria;
                                pcod_pagaduria.Direction = ParameterDirection.Input;
                                pcod_pagaduria.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pcod_pagaduria);

                                //--pEntidad.nit 29
                                DbParameter pnit = cmdTransaccionFactory.CreateParameter();
                                pnit.ParameterName = "p_nit";
                                if (pSolicitudPersonaAfi.nit == null)
                                    pnit.Value = "";
                                else
                                    pnit.Value = pSolicitudPersonaAfi.nit;
                                pnit.Direction = ParameterDirection.Input;
                                pnit.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pnit);


                                //--pEntidad.direccion_empresa 30
                                DbParameter pdireccion_empresa = cmdTransaccionFactory.CreateParameter();
                                pdireccion_empresa.ParameterName = "p_direccion_empresa";
                                if (pSolicitudPersonaAfi.direccion_empresa == null)
                                    pdireccion_empresa.Value = "";
                                else
                                    pdireccion_empresa.Value = pSolicitudPersonaAfi.direccion_empresa;
                                pdireccion_empresa.Direction = ParameterDirection.Input;
                                pdireccion_empresa.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pdireccion_empresa);

                                //--pEntidad.ciudad_empresa 32
                                DbParameter pciudad_empresa = cmdTransaccionFactory.CreateParameter();
                                pciudad_empresa.ParameterName = "p_ciudad_empresa";
                                if (pSolicitudPersonaAfi.ciudad_empresa == null)
                                    pciudad_empresa.Value = 0;
                                else
                                    pciudad_empresa.Value = pSolicitudPersonaAfi.ciudad_empresa;
                                pciudad_empresa.Direction = ParameterDirection.Input;
                                pciudad_empresa.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pciudad_empresa);

                                //--pEntidad.departamento_empresa 33
                                DbParameter pdepartamento_empresa = cmdTransaccionFactory.CreateParameter();
                                pdepartamento_empresa.ParameterName = "p_departamento_empresa";
                                if (pSolicitudPersonaAfi.departamento_empresa == null)
                                    pdepartamento_empresa.Value = 0;
                                else
                                    pdepartamento_empresa.Value = pSolicitudPersonaAfi.departamento_empresa;
                                pdepartamento_empresa.Direction = ParameterDirection.Input;
                                pdepartamento_empresa.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pdepartamento_empresa);


                                //--pEntidad.fecha_inicio 35
                                DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                                pfecha_inicio.ParameterName = "p_fecha_inicio";
                                if (pSolicitudPersonaAfi.fecha_inicio == null)
                                    pfecha_inicio.Value = DBNull.Value;
                                else
                                    pfecha_inicio.Value = pSolicitudPersonaAfi.fecha_inicio;
                                pfecha_inicio.Direction = ParameterDirection.Input;
                                pfecha_inicio.DbType = DbType.Date;
                                cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                                //--pEntidad.codcargo 51
                                DbParameter p_codcargo = cmdTransaccionFactory.CreateParameter();
                                p_codcargo.ParameterName = "p_codcargo";
                                if (pSolicitudPersonaAfi.codcargo == 0)
                                    p_codcargo.Value = 0;
                                else
                                    p_codcargo.Value = pSolicitudPersonaAfi.codcargo;
                                p_codcargo.Direction = ParameterDirection.Input;
                                p_codcargo.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(p_codcargo);

                                //--pEntidad.estado 27
                                DbParameter pestado_empresa = cmdTransaccionFactory.CreateParameter();
                                pestado_empresa.ParameterName = "p_estado_empresa";
                                if (pSolicitudPersonaAfi.estado_empresa == null)
                                    pestado_empresa.Value = "";
                                else
                                    pestado_empresa.Value = pSolicitudPersonaAfi.estado_empresa;
                                pestado_empresa.Direction = ParameterDirection.Input;
                                pestado_empresa.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pestado_empresa);


                                //--pEntidad.codtipocontrato 34
                                DbParameter pcodtipocontrato = cmdTransaccionFactory.CreateParameter();
                                pcodtipocontrato.ParameterName = "p_codtipocontrato";
                                if (pSolicitudPersonaAfi.codtipocontrato == null)
                                    pcodtipocontrato.Value = 0;
                                else
                                    pcodtipocontrato.Value = pSolicitudPersonaAfi.codtipocontrato;
                                pcodtipocontrato.Direction = ParameterDirection.Input;
                                pcodtipocontrato.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pcodtipocontrato);

                                //--pEntidad.salario  36
                                DbParameter psalario = cmdTransaccionFactory.CreateParameter();
                                psalario.ParameterName = "p_salario";
                                if (pSolicitudPersonaAfi.salario == null)
                                    psalario.Value = 0;
                                else
                                    psalario.Value = pSolicitudPersonaAfi.salario;
                                psalario.Direction = ParameterDirection.Input;
                                psalario.DbType = DbType.Decimal;
                                cmdTransaccionFactory.Parameters.Add(psalario);

                                //--pEntidad.cod_nomina 68
                                DbParameter pcod_nomina = cmdTransaccionFactory.CreateParameter();
                                pcod_nomina.ParameterName = "pcod_nomina";
                                if (pSolicitudPersonaAfi.cod_nomina == null)
                                    pcod_nomina.Value = "";
                                else
                                    pcod_nomina.Value = pSolicitudPersonaAfi.cod_nomina;
                                pcod_nomina.Direction = ParameterDirection.Input;
                                pcod_nomina.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pcod_nomina);

                                //--pEntidad.email_contacto  69
                                DbParameter pemail_contacto = cmdTransaccionFactory.CreateParameter();
                                pemail_contacto.ParameterName = "pemail_contacto";
                                if (pSolicitudPersonaAfi.email_contacto == null)
                                    pemail_contacto.Value = "";
                                else
                                    pemail_contacto.Value = pSolicitudPersonaAfi.email_contacto;
                                pemail_contacto.Direction = ParameterDirection.Input;
                                pemail_contacto.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pemail_contacto);

                                //--pEntidad.telefono_empresa 31
                                DbParameter ptelefono_empresa = cmdTransaccionFactory.CreateParameter();
                                ptelefono_empresa.ParameterName = "p_telefono_empresa";
                                if (pSolicitudPersonaAfi.telefono_empresa == null)
                                    ptelefono_empresa.Value = "";
                                else
                                    ptelefono_empresa.Value = pSolicitudPersonaAfi.telefono_empresa;
                                ptelefono_empresa.Direction = ParameterDirection.Input;
                                ptelefono_empresa.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(ptelefono_empresa);

                                //--pEntidad.profesion 18
                                DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                                pprofesion.ParameterName = "p_profesion";
                                if (pSolicitudPersonaAfi.profesion == null)
                                    pprofesion.Value = "";
                                else
                                    pprofesion.Value = pSolicitudPersonaAfi.profesion;
                                pprofesion.Direction = ParameterDirection.Input;
                                pprofesion.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pprofesion);

                                //--pEntidad.codescolaridad 17
                                DbParameter pcodescolaridad = cmdTransaccionFactory.CreateParameter();
                                pcodescolaridad.ParameterName = "p_codescolaridad";
                                if (pSolicitudPersonaAfi.codescolaridad == null)
                                    pcodescolaridad.Value = 0;
                                else
                                    pcodescolaridad.Value = pSolicitudPersonaAfi.codescolaridad;
                                pcodescolaridad.Direction = ParameterDirection.Input;
                                pcodescolaridad.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pcodescolaridad);

                                //--pEntidad.actividad_economica 42
                                DbParameter p_actividad_economica = cmdTransaccionFactory.CreateParameter();
                                p_actividad_economica.ParameterName = "p_actividad_economica";
                                if (pSolicitudPersonaAfi.actividad_economica == null)
                                    p_actividad_economica.Value = "";
                                else
                                    p_actividad_economica.Value = pSolicitudPersonaAfi.actividad_economica;
                                p_actividad_economica.Direction = ParameterDirection.Input;
                                p_actividad_economica.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_actividad_economica);

                                //--pEntidad.ciiu 43
                                DbParameter p_ciiu = cmdTransaccionFactory.CreateParameter();
                                p_ciiu.ParameterName = "p_ciiu";
                                if (pSolicitudPersonaAfi.ciiu == null)
                                    p_ciiu.Value = "";
                                else
                                    p_ciiu.Value = pSolicitudPersonaAfi.ciiu;
                                p_ciiu.Direction = ParameterDirection.Input;
                                p_ciiu.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_ciiu);

                                //--pEntidad.numero_empleados  70
                                DbParameter pnum_empleados = cmdTransaccionFactory.CreateParameter();
                                pnum_empleados.ParameterName = "pnum_empleados";
                                if (pSolicitudPersonaAfi.numero_empleados == 0)
                                    pnum_empleados.Value = 0;
                                else
                                    pnum_empleados.Value = pSolicitudPersonaAfi.numero_empleados;
                                pnum_empleados.Direction = ParameterDirection.Input;
                                pnum_empleados.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pnum_empleados);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA4";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA4";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                pSolicitudPersonaAfi.rpta = true;
                                break;
                            case 5:
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona5 = cmdTransaccionFactory.CreateParameter();
                                pid_persona5.ParameterName = "p_id_persona";
                                pid_persona5.Value = pSolicitudPersonaAfi.id_persona;
                                pid_persona5.Direction = ParameterDirection.Input;
                                pid_persona5.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona5);

                                //DATOS PEP
                                //--pEntidad.admrecursos 52
                                DbParameter p_ADMRECUSRSOS = cmdTransaccionFactory.CreateParameter();
                                p_ADMRECUSRSOS.ParameterName = "p_ADMRECUSRSOS";
                                p_ADMRECUSRSOS.Value = pSolicitudPersonaAfi.admrecursos;
                                p_ADMRECUSRSOS.Direction = ParameterDirection.Input;
                                p_ADMRECUSRSOS.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(p_ADMRECUSRSOS);

                                //--pEntidad.peps 53
                                DbParameter p_peps = cmdTransaccionFactory.CreateParameter();
                                p_peps.ParameterName = "p_peps";
                                p_peps.Value = pSolicitudPersonaAfi.peps;
                                p_peps.Direction = ParameterDirection.Input;
                                p_peps.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(p_peps);

                                //--pEntidad.funcion_publica  71
                                DbParameter pfun_publica = cmdTransaccionFactory.CreateParameter();
                                pfun_publica.ParameterName = "pfun_publica";
                                pfun_publica.Value = pSolicitudPersonaAfi.funcion_publica;
                                pfun_publica.Direction = ParameterDirection.Input;
                                pfun_publica.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pfun_publica);

                                //--pEntidad.familiares_cargos_pub  72
                                DbParameter pfamiliares_cargo = cmdTransaccionFactory.CreateParameter();
                                pfamiliares_cargo.ParameterName = "pfamiliares_cargo";
                                pfamiliares_cargo.Value = pSolicitudPersonaAfi.familiares_cargos_pub;
                                pfamiliares_cargo.Direction = ParameterDirection.Input;
                                pfamiliares_cargo.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(pfamiliares_cargo);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA5";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA5";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                pSolicitudPersonaAfi.rpta = true;
                                break;
                            case 6:
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona6 = cmdTransaccionFactory.CreateParameter();
                                pid_persona6.ParameterName = "p_id_persona";
                                pid_persona6.Value = pSolicitudPersonaAfi.id_persona;
                                pid_persona6.Direction = ParameterDirection.Input;
                                pid_persona6.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona6);

                                //DATOS FINANCIEROS
                                //--pEntidad.total_activos 47
                                DbParameter p_total_activos = cmdTransaccionFactory.CreateParameter();
                                p_total_activos.ParameterName = "p_total_activos";
                                if (pSolicitudPersonaAfi.total_activos == 0)
                                    p_total_activos.Value = 0;
                                else
                                    p_total_activos.Value = pSolicitudPersonaAfi.total_activos;
                                p_total_activos.Direction = ParameterDirection.Input;
                                p_total_activos.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(p_total_activos);


                                //--pEntidad.otros_ingresos  37
                                DbParameter potros_ingresos = cmdTransaccionFactory.CreateParameter();
                                potros_ingresos.ParameterName = "p_otros_ingresos";
                                if (pSolicitudPersonaAfi.otros_ingresos == null)
                                    potros_ingresos.Value = 0;
                                else
                                    potros_ingresos.Value = pSolicitudPersonaAfi.otros_ingresos;
                                potros_ingresos.Direction = ParameterDirection.Input;
                                potros_ingresos.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(potros_ingresos);

                                //--pEntidad.total_pasivos 48
                                DbParameter p_total_pasivos = cmdTransaccionFactory.CreateParameter();
                                p_total_pasivos.ParameterName = "p_total_pasivos";
                                if (pSolicitudPersonaAfi.total_pasivos == 0)
                                    p_total_pasivos.Value = 0;
                                else
                                    p_total_pasivos.Value = pSolicitudPersonaAfi.total_pasivos;
                                p_total_pasivos.Direction = ParameterDirection.Input;
                                p_total_pasivos.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(p_total_pasivos);


                                //--pEntidad.total_patrimonio 49
                                DbParameter p_total_patrimonio = cmdTransaccionFactory.CreateParameter();
                                p_total_patrimonio.ParameterName = "p_total_patrimonio";
                                if (pSolicitudPersonaAfi.total_patrimonio == 0)
                                    p_total_patrimonio.Value = 0;
                                else
                                    p_total_patrimonio.Value = pSolicitudPersonaAfi.total_patrimonio;
                                p_total_patrimonio.Direction = ParameterDirection.Input;
                                p_total_patrimonio.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(p_total_patrimonio);

                                //--pEntidad.deducciones 38
                                DbParameter pdeducciones = cmdTransaccionFactory.CreateParameter();
                                pdeducciones.ParameterName = "p_deducciones";
                                if (pSolicitudPersonaAfi.deducciones == null)
                                    pdeducciones.Value = 0;
                                else
                                    pdeducciones.Value = pSolicitudPersonaAfi.deducciones;
                                pdeducciones.Direction = ParameterDirection.Input;
                                pdeducciones.DbType = DbType.Decimal;
                                cmdTransaccionFactory.Parameters.Add(pdeducciones);

                                //--pEntidad.ingresos_mensuales 44
                                DbParameter P_ingresos_mensuales = cmdTransaccionFactory.CreateParameter();
                                P_ingresos_mensuales.ParameterName = "P_ingresos_mensuales";
                                if (pSolicitudPersonaAfi.ingresos_mensuales == 0)
                                    P_ingresos_mensuales.Value = 0;
                                else
                                    P_ingresos_mensuales.Value = pSolicitudPersonaAfi.ingresos_mensuales;
                                P_ingresos_mensuales.Direction = ParameterDirection.Input;
                                P_ingresos_mensuales.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(P_ingresos_mensuales);

                                //--pEntidad.detotros_ingresos 45
                                DbParameter p_detotros_ingresos = cmdTransaccionFactory.CreateParameter();
                                p_detotros_ingresos.ParameterName = "p_detotros_ingresos";
                                if (pSolicitudPersonaAfi.detotros_ingresos == null)
                                    p_detotros_ingresos.Value = "";
                                else
                                    p_detotros_ingresos.Value = pSolicitudPersonaAfi.detotros_ingresos;
                                p_detotros_ingresos.Direction = ParameterDirection.Input;
                                p_detotros_ingresos.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_detotros_ingresos);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA6";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA6";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                pSolicitudPersonaAfi.rpta = true;
                                break;
                            case 7:
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona9 = cmdTransaccionFactory.CreateParameter();
                                pid_persona9.ParameterName = "p_id_persona";
                                pid_persona9.Value = pSolicitudPersonaAfi.id_persona;
                                pid_persona9.Direction = ParameterDirection.Input;
                                pid_persona9.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona9);

                                //SECCION INTERNACIONAL
                                //--    operaciones_extrang 78
                                DbParameter p_operacion = cmdTransaccionFactory.CreateParameter();
                                p_operacion.ParameterName = "p_operacion";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0)
                                    p_operacion.Value = "0";
                                p_operacion.Value = Convert.ToString(pSolicitudPersonaAfi.operaciones_extrang);
                                p_operacion.Direction = ParameterDirection.Input;
                                p_operacion.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_operacion);

                                //--    pEntidad.nom_banco 55
                                DbParameter pNom_Banco = cmdTransaccionFactory.CreateParameter();
                                pNom_Banco.ParameterName = "pNom_Banco";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0 || pSolicitudPersonaAfi.nom_banco == null)
                                    pNom_Banco.Value = "";
                                else
                                    pNom_Banco.Value = pSolicitudPersonaAfi.nom_banco;
                                pNom_Banco.Direction = ParameterDirection.Input;
                                pNom_Banco.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(pNom_Banco);


                                //--    pEntidad.tipo_cuenta_ext  73
                                DbParameter p_tipoCuenta = cmdTransaccionFactory.CreateParameter();
                                p_tipoCuenta.ParameterName = "p_tipoCuenta";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0 || pSolicitudPersonaAfi.tipo_cuenta_ext == 0)
                                    p_tipoCuenta.Value = 0;
                                else
                                    p_tipoCuenta.Value = pSolicitudPersonaAfi.tipo_cuenta_ext;
                                p_tipoCuenta.Direction = ParameterDirection.Input;
                                p_tipoCuenta.DbType = DbType.Int32;
                                cmdTransaccionFactory.Parameters.Add(p_tipoCuenta);

                                ////--    pEntidad.ncuenta 54
                                DbParameter p_NCuenta = cmdTransaccionFactory.CreateParameter();
                                p_NCuenta.ParameterName = "p_NCuenta";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0 || pSolicitudPersonaAfi.ncuenta == 0)
                                    p_NCuenta.Value = 0;
                                else
                                    p_NCuenta.Value = pSolicitudPersonaAfi.ncuenta;
                                p_NCuenta.Direction = ParameterDirection.Input;
                                p_NCuenta.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(p_NCuenta);

                                //--    pEntidad.promedio  74
                                DbParameter p_promedio = cmdTransaccionFactory.CreateParameter();
                                p_promedio.ParameterName = "p_promedio";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0 || pSolicitudPersonaAfi.promedio == 0)
                                    p_promedio.Value = 0;
                                else
                                    p_promedio.Value = pSolicitudPersonaAfi.promedio;
                                p_promedio.Direction = ParameterDirection.Input;
                                p_promedio.DbType = DbType.Int16;
                                cmdTransaccionFactory.Parameters.Add(p_promedio);

                                //--    pEntidad.moneda  58
                                DbParameter p_moneda = cmdTransaccionFactory.CreateParameter();
                                p_moneda.ParameterName = "p_moneda";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0 || pSolicitudPersonaAfi.moneda == null)
                                    p_moneda.Value = "";
                                else
                                    p_moneda.Value = pSolicitudPersonaAfi.moneda;
                                p_moneda.Direction = ParameterDirection.Input;
                                p_moneda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_moneda);

                                //--    pEntidad.ciudadmoneda 57
                                DbParameter p_ciudadmoneda = cmdTransaccionFactory.CreateParameter();
                                p_ciudadmoneda.ParameterName = "p_ciudadmoneda";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0 || pSolicitudPersonaAfi.ciudadmoneda == null)
                                    p_ciudadmoneda.Value = "";
                                else
                                    p_ciudadmoneda.Value = pSolicitudPersonaAfi.ciudadmoneda;
                                p_ciudadmoneda.Direction = ParameterDirection.Input;
                                p_ciudadmoneda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_ciudadmoneda);

                                //--    pEntidad.paismoneda 56
                                DbParameter p_paismoneda = cmdTransaccionFactory.CreateParameter();
                                p_paismoneda.ParameterName = "p_paismoneda";
                                if (pSolicitudPersonaAfi.operaciones_extrang == 0 || pSolicitudPersonaAfi.paismoneda == null)
                                    p_paismoneda.Value = "";
                                else
                                    p_paismoneda.Value = pSolicitudPersonaAfi.paismoneda;
                                p_paismoneda.Direction = ParameterDirection.Input;
                                p_paismoneda.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(p_paismoneda);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA7";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA7";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                pSolicitudPersonaAfi.rpta = true;
                                break;
                            case 8:
                                //--pEntidad.id_persona 1
                                DbParameter pid_persona8 = cmdTransaccionFactory.CreateParameter();
                                pid_persona8.ParameterName = "p_id_persona";
                                pid_persona8.Value = pSolicitudPersonaAfi.id_persona;
                                pid_persona8.Direction = ParameterDirection.Input;
                                pid_persona8.DbType = DbType.Int64;
                                cmdTransaccionFactory.Parameters.Add(pid_persona8);

                                //--PARAEMTROS DE SALIDA PARA ENVÍO DE CORREOS ---------------------------------------------------------------------
                                //-- out mensaje error 61
                                DbParameter pMENSAJE2 = cmdTransaccionFactory.CreateParameter();
                                pMENSAJE2.ParameterName = "p_mensaje_error";
                                pMENSAJE2.Size = 200;
                                pMENSAJE2.Direction = ParameterDirection.Output;
                                pMENSAJE2.Value = pSolicitudPersonaAfi.mensaje_error;
                                cmdTransaccionFactory.Parameters.Add(pMENSAJE2);

                                //PARAMERTROS DE SALIDA 62
                                DbParameter P_EMAIL_ASESOR = cmdTransaccionFactory.CreateParameter();
                                P_EMAIL_ASESOR.ParameterName = "P_EMAIL_ASESOR";
                                P_EMAIL_ASESOR.Size = 200;
                                P_EMAIL_ASESOR.Direction = ParameterDirection.Output;
                                P_EMAIL_ASESOR.Value = pSolicitudPersonaAfi.email_asesor;
                                cmdTransaccionFactory.Parameters.Add(P_EMAIL_ASESOR);

                                //-- out envia asociado 75
                                DbParameter P_ENVIA_ASOCIADO = cmdTransaccionFactory.CreateParameter();
                                P_ENVIA_ASOCIADO.ParameterName = "P_ENVIA_ASOCIADO";
                                P_ENVIA_ASOCIADO.Size = 8;
                                P_ENVIA_ASOCIADO.Direction = ParameterDirection.Output;
                                P_ENVIA_ASOCIADO.Value = pSolicitudPersonaAfi.envia_asociado;
                                cmdTransaccionFactory.Parameters.Add(P_ENVIA_ASOCIADO);

                                //-- out envia asesor 76
                                DbParameter P_ENVIA_ASESOR = cmdTransaccionFactory.CreateParameter();
                                P_ENVIA_ASESOR.ParameterName = "P_ENVIA_ASESOR";
                                P_ENVIA_ASESOR.Size = 8;
                                P_ENVIA_ASESOR.Direction = ParameterDirection.Output;
                                P_ENVIA_ASESOR.Value = pSolicitudPersonaAfi.envia_asesor;
                                cmdTransaccionFactory.Parameters.Add(P_ENVIA_ASESOR);


                                //-- out envia otro  77
                                DbParameter P_ENVIA_OTRO = cmdTransaccionFactory.CreateParameter();
                                P_ENVIA_OTRO.ParameterName = "P_ENVIA_OTRO";
                                P_ENVIA_OTRO.Size = 200;
                                P_ENVIA_OTRO.Direction = ParameterDirection.Output;
                                P_ENVIA_OTRO.Value = pSolicitudPersonaAfi.envia_otro;
                                cmdTransaccionFactory.Parameters.Add(P_ENVIA_OTRO);

                                //EJECUCIÓN DEL PL
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                if (pOpcion == 1) //crear
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA8";
                                else // modificar
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_CREA8";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                dbConnectionFactory.CerrarConexion(connection);

                                pSolicitudPersonaAfi.rpta = true;
                                break;                           
                            default:
                                pSolicitudPersonaAfi.rpta = true;
                                break;
                        }                                                                                              
                        
                    }
                    catch (Exception ex)
                    {
                        pSolicitudPersonaAfi.rpta = false;
                        pSolicitudPersonaAfi.mensaje_error = ex.Message;
                    }
                    return pSolicitudPersonaAfi;
                }
            }
        }

        public List<BeneficiarioPersonaAfi> consultarBeneficiarios(long id_persona, Usuario usuario)
        {
            DbDataReader resultado;
            List<BeneficiarioPersonaAfi> lista = new List<BeneficiarioPersonaAfi>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select distinct * from SOLICITUD_PERSONA_BENEF where Cod_Solicitud = " + id_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BeneficiarioPersonaAfi entidad = new BeneficiarioPersonaAfi();
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["TIPO_ID"] != DBNull.Value) entidad.tipo_id = Convert.ToInt32(resultado["TIPO_ID"]);
                            if (resultado["ID_BENEF"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["ID_BENEF"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToInt32(resultado["SEXO"]);
                            if (resultado["FECHA_NAC"] != DBNull.Value) entidad.fecha_nac = Convert.ToDateTime(resultado["FECHA_NAC"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacion = Convert.ToString(resultado["OCUPACION"]);
                            if (resultado["ESCOLARIDAD"] != DBNull.Value) entidad.nivel_educativo = Convert.ToInt32(resultado["ESCOLARIDAD"]);
                            if (resultado["COD_PARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt32(resultado["COD_PARENTESCO"]);
                            if (resultado["COD_SOLICITUD"] != DBNull.Value) entidad.cod_solicitud = Convert.ToInt32(resultado["COD_SOLICITUD"]);

                            lista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        private void limpiarBeneficiarioAfi(long id_persona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter P_COD_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        P_COD_SOLICITUD.ParameterName = "P_COD_SOLICITUD";
                        P_COD_SOLICITUD.Value = id_persona;
                        P_COD_SOLICITUD.Direction = ParameterDirection.Input;
                        P_COD_SOLICITUD.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_COD_SOLICITUD);                        

                        //Ejecuta consulta de almacenado
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERBENEF_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudPersonaAfiServices", "guardarPersonaTema", ex);
                    }
                }
            }
        }

        public SolicitudPersonaAfi ConsultarPersona1(string filtro, Usuario usuario)
        {

            DbDataReader resultado;
            SolicitudPersonaAfi entidad = new SolicitudPersonaAfi();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.* FROM SOLICITUD_PERSONA_AFI S WHERE s.id_persona is not null " + filtro + " ORDER BY S.ID_PERSONA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();                        
                        if (resultado.Read())
                        {                            
                            if (resultado["ID_PERSONA"] != DBNull.Value) entidad.id_persona = Convert.ToInt64(resultado["ID_PERSONA"]);
                            if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["FECHA_EXPEDICION"] != DBNull.Value) entidad.fecha_expedicion = Convert.ToDateTime(resultado["FECHA_EXPEDICION"]);
                            if (resultado["CIUDAD_EXPEDICION"] != DBNull.Value) entidad.ciudad_expedicion = Convert.ToInt64(resultado["CIUDAD_EXPEDICION"]);
                            if (resultado["FECHA_NACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHA_NACIMIENTO"]);
                            if (resultado["CIUDAD_NACIMIENTO"] != DBNull.Value) entidad.ciudad_nacimiento = Convert.ToInt64(resultado["CIUDAD_NACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt32(resultado["CODESCOLARIDAD"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.barrio = Convert.ToInt64(resultado["BARRIO"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["DEPARTAMENTO"] != DBNull.Value) entidad.departamento = Convert.ToInt64(resultado["DEPARTAMENTO"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMAIL_CONTACTO"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL_CONTACTO"]);                            
                            if (resultado["ESTADO_EMPRESA"] != DBNull.Value) entidad.estado_empresa = Convert.ToString(resultado["ESTADO_EMPRESA"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["CIUDAD_EMPRESA"] != DBNull.Value) entidad.ciudad_empresa = Convert.ToInt64(resultado["CIUDAD_EMPRESA"]);
                            if (resultado["DEPARTAMENTO_EMPRESA"] != DBNull.Value) entidad.departamento_empresa = Convert.ToInt64(resultado["DEPARTAMENTO_EMPRESA"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["COD_PERIODICIDAD_PAGO"] != DBNull.Value) entidad.cod_periodicidad_pago = Convert.ToString(resultado["COD_PERIODICIDAD_PAGO"]);
                            if (resultado["TIPO_VIVIENDA"] != DBNull.Value) entidad.tipoVivienda = Convert.ToString(resultado["TIPO_VIVIENDA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt32(resultado["CODCARGO"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["Cod_Nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["Cod_Nomina"]);
                            if (resultado["total_patrimonio"] != DBNull.Value) entidad.total_patrimonio = Convert.ToInt64(resultado["total_patrimonio"]);
                            if (resultado["total_pasivos"] != DBNull.Value) entidad.total_pasivos = Convert.ToInt64(resultado["total_pasivos"]);
                            if (resultado["total_activos"] != DBNull.Value) entidad.total_activos = Convert.ToInt64(resultado["total_activos"]);
                            if (resultado["egresos_mensuales"] != DBNull.Value) entidad.egresos_mensuales = Convert.ToInt64(resultado["egresos_mensuales"]);
                            if (resultado["ingresos_mensuales"] != DBNull.Value) entidad.ingresos_mensuales = Convert.ToInt64(resultado["ingresos_mensuales"]);
                            if (resultado["Otros_Ingresos"] != DBNull.Value) entidad.otros_ingresos = Convert.ToInt64(resultado["Otros_Ingresos"]);
                            if (resultado["detotros_ingresos"] != DBNull.Value) entidad.detotros_ingresos = Convert.ToString(resultado["detotros_ingresos"]);
                            if (resultado["CABEZA_FAMILIA"] != DBNull.Value) entidad.cabeza_familia = Convert.ToInt32(resultado["CABEZA_FAMILIA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public void CrearBeneficiarioAfi(BeneficiarioPersonaAfi benef, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        //P_COD_SOLICITUD SOLICITUD_PERSONA_BENEF.COD_SOLICITUD % TYPE,                         
                        DbParameter P_COD_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        P_COD_SOLICITUD.ParameterName = "P_COD_SOLICITUD";
                        P_COD_SOLICITUD.Value = benef.cod_solicitud;
                        P_COD_SOLICITUD.Direction = ParameterDirection.Input;
                        P_COD_SOLICITUD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_SOLICITUD);
                        
                        //P_NOMBRES SOLICITUD_PERSONA_BENEF.NOMBRES % TYPE, 
                        //--//b1.nombres                        
                        DbParameter P_NOMBRES = cmdTransaccionFactory.CreateParameter();
                        P_NOMBRES.ParameterName = "P_NOMBRES";
                        P_NOMBRES.Value = benef.nombres;
                        P_NOMBRES.Direction = ParameterDirection.Input;
                        P_NOMBRES.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NOMBRES);

                        //P_APELLIDOS SOLICITUD_PERSONA_BENEF.APELLIDOS % TYPE, 
                        //b1.apellidos                                                
                        DbParameter P_APELLIDOS = cmdTransaccionFactory.CreateParameter();
                        P_APELLIDOS.ParameterName = "P_APELLIDOS";
                        if (benef.apellidos == null)
                            P_APELLIDOS.Value = DBNull.Value;
                        else
                            P_APELLIDOS.Value = benef.apellidos;
                        P_APELLIDOS.Direction = ParameterDirection.Input;
                        P_APELLIDOS.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_APELLIDOS);
                        
                        //P_TIPO_ID SOLICITUD_PERSONA_BENEF.TIPO_ID % TYPE, 
                        //b1.tipo_id                        
                        DbParameter P_TIPO_ID = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_ID.ParameterName = "P_TIPO_ID";
                        P_TIPO_ID.Value = benef.tipo_id;
                        P_TIPO_ID.Direction = ParameterDirection.Input;
                        P_TIPO_ID.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_ID);

                        //P_NUM_DOCUMENTO SOLICITUD_PERSONA_BENEF.NUM_DOCUMENTO % TYPE, 
                        //b1.identificacion
                        DbParameter P_NUM_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        P_NUM_DOCUMENTO.ParameterName = "P_NUM_DOCUMENTO";
                        P_NUM_DOCUMENTO.Value = benef.identificacion;
                        P_NUM_DOCUMENTO.Direction = ParameterDirection.Input;
                        P_NUM_DOCUMENTO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NUM_DOCUMENTO);
                        
                        //P_SEXO SOLICITUD_PERSONA_BENEF.SEXO % TYPE, 
                        //b1.sexo
                        DbParameter P_SEXO = cmdTransaccionFactory.CreateParameter();
                        P_SEXO.ParameterName = "P_SEXO";
                        P_SEXO.Value = benef.sexo;
                        P_SEXO.Direction = ParameterDirection.Input;
                        P_SEXO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_SEXO);


                        //P_FECHA_NAC SOLICITUD_PERSONA_BENEF.FECHA_NAC % TYPE, 
                        //b1.fecha_nac
                        DbParameter P_FECHA_NAC = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_NAC.ParameterName = "P_FECHA_NAC";
                        if (benef.fecha_nac == null)
                            P_FECHA_NAC.Value = DBNull.Value;
                        else
                            P_FECHA_NAC.Value = benef.fecha_nac;
                        P_FECHA_NAC.Direction = ParameterDirection.Input;
                        P_FECHA_NAC.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_NAC);
                                                
                        //P_OCUPACION SOLICITUD_PERSONA_BENEF.OCUPACION % TYPE, 
                        //b1.ocupacion                        
                        DbParameter P_OCUPACION = cmdTransaccionFactory.CreateParameter();
                        P_OCUPACION.ParameterName = "P_OCUPACION";
                        if (benef.ocupacion == null)
                            P_OCUPACION.Value = DBNull.Value;
                        else
                            P_OCUPACION.Value = benef.ocupacion;
                        P_OCUPACION.Direction = ParameterDirection.Input;
                        P_OCUPACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_OCUPACION);
                       
                        //P_ESCOLARIDAD SOLICITUD_PERSONA_BENEF.ESCOLARIDAD % TYPE, 
                        //b1.nivel_educativo                        
                        DbParameter P_ESCOLARIDAD = cmdTransaccionFactory.CreateParameter();
                        P_ESCOLARIDAD.ParameterName = "P_ESCOLARIDAD";
                        P_ESCOLARIDAD.Value = benef.nivel_educativo;
                        P_ESCOLARIDAD.Direction = ParameterDirection.Input;
                        P_ESCOLARIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESCOLARIDAD);

                        //P_COD_PARENTESCO SOLICITUD_PERSONA_BENEF.COD_PARENTESCO % TYPE) 
                        //b1.codparentesco
                        DbParameter P_COD_PARENTESCO = cmdTransaccionFactory.CreateParameter();
                        P_COD_PARENTESCO.ParameterName = "P_COD_PARENTESCO";
                        if (benef.codparentesco == null)
                            P_COD_PARENTESCO.Value = DBNull.Value;
                        else
                            P_COD_PARENTESCO.Value = benef.codparentesco;
                        P_COD_PARENTESCO.Direction = ParameterDirection.Input;
                        P_COD_PARENTESCO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PARENTESCO);
                                                                        
                        //Ejecuta consulta de almacenado
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERBENEF_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudPersonaAfiServices", "guardarPersonaTema", ex);
                    }
                }
            }
        }

        public void guardarPersonaTema(Persona1 tema, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                            DbParameter P_ID_TEMA = cmdTransaccionFactory.CreateParameter();
                            P_ID_TEMA.ParameterName = "P_ID_TEMA";
                            P_ID_TEMA.Value = tema.id_solicitud;
                            P_ID_TEMA.Direction = ParameterDirection.Input;
                            P_ID_TEMA.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(P_ID_TEMA);
                            
                            DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                            P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                            P_IDENTIFICACION.Value = tema.identificacion;
                            P_IDENTIFICACION.Direction = ParameterDirection.Input;
                            P_IDENTIFICACION.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);

                            DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                            P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                            if(tema.cod_persona > 0)
                                P_COD_PERSONA.Value = tema.cod_persona;
                            else
                                P_COD_PERSONA.Value = 0;
                            P_COD_PERSONA.Direction = ParameterDirection.Input;
                            P_COD_PERSONA.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                            DbParameter P_OTRO = cmdTransaccionFactory.CreateParameter();
                            P_OTRO.ParameterName = "P_OTRO";
                            P_OTRO.Value = tema.descripcion;
                            P_OTRO.Direction = ParameterDirection.Input;
                            P_OTRO.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(P_OTRO);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERTEMA_CRE";
                            cmdTransaccionFactory.ExecuteNonQuery();
                            dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudPersonaAfiServices", "guardarPersonaTema", ex);
                    }
                }
            }
        }

        public SolicitudPersonaAfi ListarPersonasRepresentante(Int64 pIdentPersona, Usuario vUsuario)
        {
            DbDataReader resultado;

            SolicitudPersonaAfi entidad = new SolicitudPersonaAfi();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                        string sql = "select * from persona where identificacion=" + pIdentPersona;
                      
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                   
                      
                        if (resultado.Read())
                        {
                          
                          
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.id_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            //if (resultado["COD_OFICINA"] != DBNull.Value) entidad.o = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadoper = Convert.ToString(resultado["ESTADO"]);
                            //if (resultado["COD_ZONA"] != DBNull.Value) entidad.zona = Convert.ToInt64(resultado["COD_ZONA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudad_empresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            //if (resultado["NOMCIUDADRESIDEN"] != DBNull.Value) entidad = Convert.ToString(resultado["NOMCIUDADRESIDEN"]);
                            //if (resultado["NOMCIUDADEMPRESA"] != DBNull.Value) entidad.nomciudadempresa = Convert.ToString(resultado["NOMCIUDADEMPRESA"]);
                            //if (resultado["CLAVE"] != DBNull.Value) entidad.clavesinencriptar = Convert.ToString(resultado["CLAVE"]);
                            
                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudPersonaAfi", "ListarPersonasRepresentante", ex);
                        return null;
                    }
                }
            }
        }

    }
}