using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;
using Xpinn.FabricaCreditos.Entities;
using System.Transactions;

namespace Xpinn.Seguridad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class UsuarioService
    {
        private UsuarioBusiness BOUsuario;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Usuario
        /// </summary>
        public UsuarioService()
        {
            BOUsuario = new UsuarioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90106"; } }
        public string CodigoProgramaCarga { get { return "90113"; } }

        public Boolean CrearPersonaUsuario(PersonaUsuario pAPP, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.CrearPersonaUsuario(pAPP, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "CrearPersonaUsuario", ex);
                return false;
            }
        }


        public PersonaUsuario ConsultarPersonaUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ConsultarPersonaUsuario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultarPersonaUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear Usuario
        /// </summary>
        /// <param name="pEntity">Entidad Usuario</param>
        /// <returns>Entidad Usuario creada</returns>
        public Usuario CrearUsuario(Usuario vUsuario, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.CrearUsuario(vUsuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "CrearUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Usuario
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario modificada</returns>
        public Usuario ModificarUsuario(Usuario vUsuario, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ModificarUsuario(vUsuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ModificarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Usuario
        /// </summary>
        /// <param name="pId">identificador de Usuario</param>
        public void EliminarUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOUsuario.EliminarUsuario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarUsuario", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Usuario
        /// </summary>
        /// <param name="pId">identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public Usuario ConsultarUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ConsultarUsuario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Usuarios a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<Usuario> ListarUsuario(Usuario vUsuario, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ListarUsuario(vUsuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ListarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Valida la existencia y acceso del usuario en el sistema
        /// </summary>
        /// <param name="pUsuario">nombre de usuario</param>
        /// <param name="pPassword">clave de acceso</param>
        /// <returns>Entidad Usuario</returns>
        public Usuario ValidarUsuario(string pUsuario, string pPassword, string ip, string mac, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ValidarUsuario(pUsuario, pPassword, ip, mac, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ValidarUsuario", ex);
                return null;
            }
        }

        public byte[] ObtenerLogoEmpresaIniciar(Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ObtenerLogoEmpresaIniciar(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ObtenerLogoEmpresaIniciar", ex);
                return null;
            }
        }

        public Usuario ValidarUsuarioSinClave(string pUsuario, string ip, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ValidarUsuarioSinClave(pUsuario, ip, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ValidarUsuarioSinClave", ex);
                return null;
            }
        }

        public Usuario ValidarUsuarioSinClave(string pUsuario, string ip, ref string error, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ValidarUsuarioSinClave(pUsuario, ip, ref error, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ValidarUsuarioSinClave", ex);
                return null;
            }
        }

        public Usuario ValidarUsuarioOficina(string pUsuario, string password, string ip, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ValidarUsuarioOficina(pUsuario, password, ip, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ValidarUsuarioOficina", ex);
                return null;
            }
        }

        public Xpinn.FabricaCreditos.Entities.Persona1 ValidarPersonaUsuario(string pUsuario, string pPassword, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ValidarPersonaUsuario(pUsuario, pPassword, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ValidarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para cambiar la clave del usuario
        /// </summary>
        /// <param name="pAnteriorClave">clave anterior</param>
        /// <param name="pNuevaClave">nueva clave</param>
        /// <param name="pUsuario">usuario en sesion</param>
        public string CambiarClave(string pAnteriorClave, string pNuevaClave, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.CambiarClave(pAnteriorClave, pNuevaClave, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "CambiarClave", ex);
                return "";
            }
        }

        public bool CambiarClavePersona(string pIdentificacion, string pAntiguaClave, string pNuevaClave, ref string pError)
        {
            try
            {
                return BOUsuario.CambiarClavePersona(pIdentificacion, pAntiguaClave, pNuevaClave, ref pError);
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
                return BOUsuario.ConsultarPersona1(persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultarPersona1", ex);
                return null;
            }
        }

        public Persona1 ConsultaPersonaAcceso(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ConsultaPersonaAcceso(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultaPersonaAcceso", ex);
                return null;
            }
        }


        public Perfil ConsultarFechaperiodicidad(Int64 CodUsuario, Int64 CodPerfil, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ConsultarFechaperiodicidad(CodUsuario, CodPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosServices", "ConsultarFechaperiodicidad", ex);
                return null;
            }
        }

        public Usuario ConsultarEmpresa(Int32 codigo, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ConsultarEmpresa(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultarEmpresa", ex);
                return null;
            }
        }


        public Ingresos CrearUsuarioIngreso(Ingresos pIngreso, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.CrearUsuarioIngreso(pIngreso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "CrearUsuarioIngreso", ex);
                return null;
            }
        }

        public Ingresos ModificarUsuarioIngreso(Ingresos pIngreso, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ModificarUsuarioIngreso(pIngreso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ModificarUsuarioIngreso", ex);
                return null;
            }
        }

        public bool ValidarActualizacion(Int64 cod_persona, string fecha, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ValidarActualizacion(cod_persona, fecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ValidarActualizacion", ex);
                return false;
            }
        }

        public PersonaUsuario ConsultarPersonaUsuarioGeneral(PersonaUsuario pEntidad, Usuario vUsuario, string pFiltro = null)
        {
            try
            {
                return BOUsuario.ConsultarPersonaUsuarioGeneral(pEntidad, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultarPersonaUsuarioGeneral", ex);
                return null;
            }
        }


        public bool CrearPersonasAsociadas(List<PersonaUsuario> lstAsociados, List<Xpinn.Tesoreria.Entities.ErroresCarga> lstErrores, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.CrearPersonasAsociadas(lstAsociados, lstErrores, ref pError, vUsuario);
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
                return BOUsuario.ProbarConexión(pUsuario);
            }
            catch
            {
                return null;
            }
        }


    }
}