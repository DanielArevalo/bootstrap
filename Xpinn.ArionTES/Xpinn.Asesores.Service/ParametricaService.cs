using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametricaService
    {
        private ParametricasBusiness busParametricas;
        private ExcepcionBusiness excepBusinnes;
        
        public ParametricaService()
        {
            busParametricas = new ParametricasBusiness();
            excepBusinnes = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110111"; } }

        public List<TipoIdentificacion> ListarTipoIdentificacion(TipoIdentificacion pAseEntTipoDocs, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarTipoIdentificacion(pAseEntTipoDocs, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarTipoIdentificacion", ex);
                return null;
            }
        }
        public List<Barrios> ListarBarrios(int tipo, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarBarrios(tipo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarTipoIdentificacion", ex);
                return null;
            }
        }
        public List<Ejecutivo> listarejecutivoszona(int cod, Usuario pUsuario)
        {
            try
            {
                return busParametricas.listarejecutivoszona(cod, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarTipoIdentificacion", ex);
                return null;
            }
        }
        public List<Barrios> ListarZonasBarrios(string barrio, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarZonasBarrios(barrio, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarTipoIdentificacion", ex);
                return null;
            }
        }
        public List<Zona> ListarZonas(Zona pAseEntiZona, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarZonas(pAseEntiZona, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarZonas", ex);
                return null;
            }
        }

        public List<Xpinn.Asesores.Entities.Common.Actividad> ListarActividades(Xpinn.Asesores.Entities.Common.Actividad pASeEntiActividad, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarActividades(pASeEntiActividad, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarActividades", ex);
                return null;
            }
        }

        public List<MotivoAfiliacion> ListarMotivoAfiliacion(MotivoAfiliacion pEntMotAfilia, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarMotivoAfiliacion(pEntMotAfilia, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarMotivoAfiliacion", ex);
                return null;
            }
        }

        public List<MotivoModificacion> ListarMotivoModificacion(MotivoModificacion pEntMotMod, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarMotivoModificacion(pEntMotMod, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarMotivoModificacion", ex);
                return null;
            }
        }

        public List<Estado> ListarEstado(Estado pEntEstado, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarEstado(pEntEstado, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarEstado", ex);
                return null;
            }
        }

        public List<Oficina> ListarOficina(Oficina pEntOficina, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarOficina(pEntOficina, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarOficina", ex);
                return null;
            }
        }

        public List<Zona> ListarZonas(string v, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public List<Oficina> ListarDireccionesOficinas(Oficina pEntOficina, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ListarDireccionesOficinas(pEntOficina, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarDireccionesOficinas", ex);
                return null;
            }
        }
        public EntEmpresa ConsultarEmpresa(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return busParametricas.ConsultarEmpresa(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ConsultarEmpresa", ex);
                return null;
            }
        }


    }//end class
}
