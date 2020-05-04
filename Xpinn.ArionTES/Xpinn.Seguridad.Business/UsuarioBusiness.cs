using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Usuario
    /// </summary>
    public class UsuarioBusiness : GlobalBusiness
    {
        private UsuarioData DAUsuario;
        private CifradoBusiness cifrar;

        /// <summary>
        /// Constructor del objeto de negocio para Usuario
        /// </summary>
        public UsuarioBusiness()
        {
            DAUsuario = new UsuarioData();
            cifrar = new CifradoBusiness();
        }


        public Boolean CrearPersonaUsuario(PersonaUsuario pAPP, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    pAPP.clave_sinencriptar = pAPP.clave;
                    pAPP.clave = cifrar.Encriptar(pAPP.clave);
                    pAPP = DAUsuario.CrearPersonaUsuario(pAPP, vUsuario);                    
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "CrearPersonaUsuario", ex);
                return false;
            }
        }


        public PersonaUsuario ConsultarPersonaUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                PersonaUsuario pPersona = new PersonaUsuario();
                pPersona = DAUsuario.ConsultarPersonaUsuario(pId, pUsuario);
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultarPersonaUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea un Usuario
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario creada</returns>
        public Usuario CrearUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuario.clave_sinencriptar = pUsuario.login;
                    pUsuario.login = cifrar.Encriptar(pUsuario.login);                    
                    pUsuario = DAUsuario.CrearUsuario(pUsuario, vUsuario);
                    int tipoAtribucion = 0;
                    foreach (int sAtribucion in pUsuario.LstAtribuciones)
                    {
                        if (sAtribucion == 1)
                            DAUsuario.CrearAtribucion(pUsuario.codusuario, tipoAtribucion, vUsuario);
                        else
                            DAUsuario.EliminarAtribucion(pUsuario.codusuario, tipoAtribucion, vUsuario);
                        tipoAtribucion += 1;
                    }
                    ts.Complete();
                }

                DAUsuario.CrearUsuarioBD(pUsuario, vUsuario);

                return pUsuario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "CrearUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Usuario
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario modificada</returns>
        public Usuario ModificarUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    pUsuario.clave_sinencriptar = pUsuario.login;
                    pUsuario.login = cifrar.Encriptar(pUsuario.login);
                    pUsuario = DAUsuario.ModificarUsuario(pUsuario, vUsuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("UsuarioBusiness", "ModificarUsuario", ex);
                    return null;
                }

                DAUsuario.EliminarDireccionIp(pUsuario.codusuario, vUsuario);
                foreach (string sDireccionIp in pUsuario.LstIP)
                {
                    DAUsuario.CrearDireccionIp(pUsuario.codusuario, sDireccionIp, vUsuario);
                }


                DAUsuario.EliminarDireccionMac(pUsuario.codusuario, vUsuario);
                foreach (string sDireccionMac in pUsuario.LstMac)
                {
                    DAUsuario.CrearDireccionMac(pUsuario.codusuario, sDireccionMac, vUsuario);
                }


                int tipoAtribucion = 0;
                foreach (int sAtribucion in pUsuario.LstAtribuciones)
                {
                    if (sAtribucion == 1)                      
                        DAUsuario.CrearAtribucion(pUsuario.codusuario, tipoAtribucion, vUsuario);
                    else
                        DAUsuario.EliminarAtribucion(pUsuario.codusuario, tipoAtribucion, vUsuario);
                    tipoAtribucion += 1;
                }

                ts.Complete();
            }

            DAUsuario.CrearUsuarioBD(pUsuario, vUsuario);
            //Si el usuario tiene estado ACTIVO debloquear de base de datos
            if(pUsuario.estado == 1)
                DAUsuario.DesbloquearUsuario(pUsuario, vUsuario);

            return pUsuario;
        }

        /// <summary>
        /// Elimina un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        public void EliminarUsuario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUsuario.EliminarUsuario(pId, vUsuario);
                    for (int tipoAtribucion = 0; tipoAtribucion <= 3; tipoAtribucion+=1)
                    {
                        DAUsuario.EliminarAtribucion(pId, tipoAtribucion, vUsuario);
                        tipoAtribucion += 1;
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "EliminarUsuario", ex);
            }
        }

        public Usuario ValidarUsuarioOficina(string pUsuario, string password, string ip, Usuario vUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    Usuario usuario = new Usuario();
                    try
                    {
                        usuario = DAUsuario.ValidarUsuarioOficina(pUsuario, password, ip, vUsuario);
                    }
                    catch (ExceptionBusiness ex)
                    {
                        throw new ExceptionBusiness("Usuario o clave no encontrado. Puede que se encuentre asociado a una IP para su acceso");
                    }

                    if (usuario.nombre == "error")
                        throw new ExceptionBusiness("Usuario o clave no encontrado. Puede que se encuentre asociado a una IP para su acceso");
                    if (usuario.estado == 2 || usuario.estado == 3)  // Bloqueado
                        throw new ExceptionBusiness("El usuario ingresado se encuentra Bloqueado.");
                    if (!string.IsNullOrEmpty(usuario.login))
                    {
                        usuario.login = cifrar.Desencriptar(usuario.login);
                        if(usuario.login == password)
                        {
                            return usuario;
                        }
                        else
                        {
                            throw new ExceptionBusiness("identidad de usuario invalida");
                        }
                    }
                    return usuario;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("UsuarioBusiness", "ValidarUsuarioSinClave", ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtiene un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public Usuario ConsultarUsuario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Usuario usuario = new Usuario();

                usuario = DAUsuario.ConsultarUsuario(pId, vUsuario);
                usuario.login = cifrar.Desencriptar(usuario.login);

                usuario = DAUsuario.ListarIPUsuario(usuario, vUsuario);
                usuario = DAUsuario.ListarAtribuciones(usuario, vUsuario);
                usuario = DAUsuario.ListarMACUsuario(usuario, vUsuario);

                return usuario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<Usuario> ListarUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            try
            {
                return DAUsuario.ListarUsuario(pUsuario, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ListarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public Usuario ValidarUsuario(string pUsuario, string pClave, string ip, string mac, Usuario vUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    Usuario usuario = new Usuario();
                    string proceso = "";
                    string validarip = "";
                    string validarmac = "";
                    string claveSinEncriptar = "";
                    try
                    {
                        claveSinEncriptar = pClave;
                        pClave = cifrar.Encriptar(pClave);
                        proceso = "validarip";
                        validarip = DAUsuario.ValidarUsuarioip(pUsuario, vUsuario);
                        proceso = "validarmac";
                        validarmac = DAUsuario.ValidarUsuarioMac(pUsuario, vUsuario);
                        proceso = "ValidarUsuario. "; //+ "Usu:" + pUsuario + " Cla:" + claveSinEncriptar + " Ip:" + validarip + " Mac:" +  validarmac;
                        usuario = DAUsuario.ValidarUsuario(pUsuario, pClave, validarip, ip, validarmac, mac, vUsuario);
                        usuario.clave_sinencriptar = claveSinEncriptar;
                    }
                    catch (ExceptionBusiness ex)
                    {
                        if (ex.Message.Contains("El registro no existe. Verifique por favor."))
                        {
                            if (validarmac == "0")
                                throw new ExceptionBusiness("Usuario o clave no encontrado." + proceso);
                            else
                            {
                                throw new ExceptionBusiness("Usuario o clave no encontrado. Puede que se encuentre asociado a una IP o MAC para su acceso");
                            }
                        }
                        else
                            throw new ExceptionBusiness(ex.Message);
                    }

                    if (usuario.estado == 0)  // Inactivo
                        throw new ExceptionBusiness("El usuario ingresado Se encuentra Inactivo.");
                    if (usuario.estado == 2 || usuario.estado == 3)  // Bloqueado
                        throw new ExceptionBusiness("El usuario ingresado se encuentra Bloqueado.");

                    return usuario;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("UsuarioBusiness", "ValidarUsuario", ex);
                    return null;
                }
            }
               
        }

        public bool ValidarActualizacion(Int64 cod_persona, string fecha, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ValidarActualizacion(cod_persona, fecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ValidarActualizacion", ex);
                return false;
            }
        }

        public byte[] ObtenerLogoEmpresaIniciar(Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ObtenerLogoEmpresaIniciar(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ObtenerLogoEmpresaIniciar", ex);
                return null;
            }
        }

        public Usuario ValidarUsuarioSinClave(string pUsuario, string ip, Usuario vUsuario)
        {
            string error = "";
            return ValidarUsuarioSinClave(pUsuario, ip, ref error, vUsuario);
        }

        public Usuario ValidarUsuarioSinClave(string pUsuario, string ip, ref string error, Usuario vUsuario)
        {
            try
            {
                Usuario usuario = new Usuario();
                string validarip = "";
                string claveSinEncriptar = "";
                try
                {
                    validarip = DAUsuario.ValidarUsuarioip(pUsuario, vUsuario);
                    usuario = DAUsuario.ValidarUsuarioSinClave(pUsuario, validarip, ip, vUsuario);
                    usuario.clave_sinencriptar = claveSinEncriptar;
                }
                catch (ExceptionBusiness ex)
                {
                    error = ex.Message;
                    if (ex.Message.Contains("El registro no existe. Verifique por favor."))
                    {
                        if (validarip == "0")
                            throw new ExceptionBusiness("Usuario o clave no encontrado.");
                        else
                        {
                            throw new ExceptionBusiness("Usuario o clave no encontrado. Puede que se encuentre asociado a una IP para su acceso");
                        }
                    }
                    else
                        throw new ExceptionBusiness(ex.Message);
                }

                if (usuario.estado == 0)  // Inactivo
                    throw new ExceptionBusiness("El usuario ingresado Se encuentra Inactivo.");
                if (usuario.estado == 2 || usuario.estado == 3)  // Bloqueado
                    throw new ExceptionBusiness("El usuario ingresado se encuentra Bloqueado.");

                return usuario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ValidarUsuarioSinClave", ex);
                error = ex.Message;
                return null;
            }
        }


        public Xpinn.FabricaCreditos.Entities.Persona1 ValidarPersonaUsuario(string pUsuario, string pClave, Usuario vUsuario)
        {
            try
            {
                Xpinn.FabricaCreditos.Entities.Persona1 PersonaUsuario = new Xpinn.FabricaCreditos.Entities.Persona1();
                
                string claveSinEncriptar = "";                
                try
                {
                    PersonaUsuario.rptaingreso = false;
                    claveSinEncriptar = pClave;
                    pClave = cifrar.Encriptar(pClave);
                    PersonaUsuario = DAUsuario.ValidarPersonaUsuario(pUsuario, pClave, vUsuario);
                    PersonaUsuario.clavesinecriptar = claveSinEncriptar;
                    PersonaUsuario.rptaingreso = true;
                }
                catch (ExceptionBusiness ex)
                {
                    PersonaUsuario.rptaingreso = false;
                    throw new ExceptionBusiness(ex.Message);                    
                }
                return PersonaUsuario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ValidarPersonaUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Metodo para cambiar la clave del usuario
        /// </summary>
        /// <param name="pAnteriorClave">clave anterior</param>
        /// <param name="pNuevaClave">nueva clave</param>
        /// <param name="pUsuario">usuario en sesion</param>
        public string CambiarClave(string pAnteriorClave, string pNuevaClave, Usuario pUsuario)
        {
            try
            {
                Xpinn.Util.CifradoBusiness cifrar = new CifradoBusiness();
                string anteriorClave = "", nuevaClave = "";

                anteriorClave = cifrar.Encriptar(pAnteriorClave.Trim());
                nuevaClave = cifrar.Encriptar(pNuevaClave.Trim());
                //pUsuario.clave_sinencriptar = pNuevaClave.Trim();

                if (anteriorClave != pUsuario.login)
                    throw new ExceptionBusiness("La clave anterior digitada, no coincide con la del usuario. Verifique por favor.");

                DAUsuario.CambiarClave(nuevaClave, pNuevaClave, pUsuario);

                return nuevaClave;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "CambiarClave", ex);
                return "";
            }
        }

        public bool CambiarClavePersona(string pIdentificacion, string pAntiguaClave, string pNuevaClave, ref string pError)
        {
            try
            {
                Xpinn.Util.CifradoBusiness cifrar = new CifradoBusiness();
                string nuevaClave = "", antiguaclave = "";
                if (pAntiguaClave == pNuevaClave)
                {
                    pError = "LA CLAVE INGRESADA DEBE SER DIFERENTE A LA ACTUAL";
                    return false;
                }
                if(pNuevaClave != null)
                    nuevaClave = cifrar.Encriptar(pNuevaClave.Trim());
                if(pAntiguaClave != null)
                    antiguaclave = cifrar.Encriptar(pAntiguaClave.Trim());
                bool rpta = false;
                rpta = DAUsuario.CambiarClavePersona(pIdentificacion, antiguaclave, nuevaClave, ref pError);
                return rpta;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }

        public Persona1 ConsultarPersona1(string persona, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ConsultarPersona1(persona, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultarPersona1", ex);
                return null;
            }
        }

        public Persona1 ConsultaPersonaAcceso(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ConsultaPersonaAcceso(pFiltro, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultaPersonaAcceso", ex);
                return null;
            }
        }



        public Perfil ConsultarFechaperiodicidad(Int64 CodUsuario, Int64 CodPerfil, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ConsultarFechaperiodicidad(CodUsuario,CodPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ConsultarFechaperiodicidad", ex);
                return null;
            }
        }


        public Usuario ConsultarEmpresa(Int32 codigo, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ConsultarEmpresa(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultarEmpresa", ex);
                return null;
            }
        }


        public Ingresos CrearUsuarioIngreso(Ingresos pIngreso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pIngreso = DAUsuario.CrearUsuarioIngreso(pIngreso, vUsuario);                   
                    ts.Complete();
                }
                return pIngreso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "CrearUsuarioIngreso", ex);
                return null;
            }
        }


        public Ingresos ModificarUsuarioIngreso(Ingresos pIngreso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pIngreso = DAUsuario.ModificarUsuarioIngreso(pIngreso, vUsuario);
                    ts.Complete();
                }
                return pIngreso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ModificarUsuarioIngreso", ex);
                return null;
            }
        }


        public PersonaUsuario ConsultarPersonaUsuarioGeneral(PersonaUsuario pEntidad, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAUsuario.ConsultarPersonaUsuarioGeneral(pEntidad, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultarPersonaUsuarioGeneral", ex);
                return null;
            }
        }

        public bool CrearPersonasAsociadas(List<PersonaUsuario> lstAsociados, List<Xpinn.Tesoreria.Entities.ErroresCarga> lstErrores, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstAsociados.Count > 0)
                    {
                        PersonaUsuario pResult;
                        Persona1 pConsult;
                        int cont = 0;
                        foreach (PersonaUsuario pEntidad in lstAsociados)
                        {
                            cont++;
                            try
                            {
                                pResult = new PersonaUsuario();
                                pEntidad.clave_sinencriptar = pEntidad.clave;
                                pEntidad.clave = cifrar.Encriptar(pEntidad.clave);
                                pEntidad.fecha_creacion = DateTime.Now;
                                pEntidad.fecultmod = DateTime.Now;
                                pConsult = DAUsuario.ConsultarPersona1(pEntidad.identificacion, vUsuario);
                                pEntidad.cod_persona = pConsult.cod_persona;
                                pResult = DAUsuario.CrearPersonaUsuario(pEntidad, vUsuario);
                            }
                            catch (Exception ex)
                            {
                                Xpinn.Tesoreria.Entities.ErroresCarga registro = new Xpinn.Tesoreria.Entities.ErroresCarga();
                                registro.numero_registro = cont.ToString();
                                registro.datos = pEntidad.identificacion;
                                registro.datos += " - ";
                                registro.datos += pEntidad.clave_sinencriptar;
                                registro.error = ex.Message;
                                lstErrores.Add(registro);
                            }
                        }
                    }

                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }

        public string ProbarConexión(Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ProbarConexión(pUsuario);
            }
            catch 
            {
                return null;
            }
        }


    }
}