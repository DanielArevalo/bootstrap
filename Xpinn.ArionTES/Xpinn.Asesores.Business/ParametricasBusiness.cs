using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    public class ParametricasBusiness : GlobalData
    {
        private ZonaData dataZona;
        private OficinaData dataOficina;
        private EstadoData dataEstado;
        private ActividadData dataActvidad;
        private MotivoAfiliacionData dataMotAfilia;
        private MotivoModificacionData dataMotModi;
        private TipoIdentificacionData dataTipoIdent;
        private BarriosData barrios;

        public ParametricasBusiness()
        {
            dataTipoIdent = new TipoIdentificacionData();
            dataZona = new ZonaData();
            dataActvidad = new ActividadData();
            dataMotAfilia = new MotivoAfiliacionData();
            dataMotModi = new MotivoModificacionData();
            dataOficina = new OficinaData();
            dataEstado = new EstadoData();
            barrios = new BarriosData();

        }

        public List<Barrios> ListarZonasBarrios(string barrio, Usuario pUsuario)
        {
           return dataZona.ListarZonasBarrios(barrio, pUsuario);
           
        }

        public List<Zona> ListarZonas(Zona pAseEntiZona, Usuario pUsuario)
        {
            try
            {
                return dataZona.ListarZonas(pAseEntiZona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListarZonas", ex);
                return null;
            }
        }

        public List<Xpinn.Asesores.Entities.Common.Actividad> ListarActividades(Xpinn.Asesores.Entities.Common.Actividad pAseEntiAct, Usuario pUsuario)
        {
            try
            {
                return dataActvidad.ListarActividades(pAseEntiAct, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListarActividades", ex);
                return null;
            }
        }

        public List<TipoIdentificacion> ListarTipoIdentificacion(TipoIdentificacion pAseEntTipoDocs, Usuario pUsuario)
        {
            try
            {
                return dataTipoIdent.ListarTipoDocs(pAseEntTipoDocs, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListAListarTiposDocs", ex);
                return null;
            }
        }
        public List<Barrios> ListarBarrios(int tipos, Usuario pUsuario)
        {
            return barrios.ListarBarrios(tipos, pUsuario);
                      
        }

        public List<Ejecutivo> listarejecutivoszona(int cod, Usuario pUsuario)
        {
            return barrios.listarejecutivoszona(cod, pUsuario);
                      
        }
        public List<MotivoAfiliacion> ListarMotivoAfiliacion(MotivoAfiliacion pEntMotAfilia, Usuario pUsuario)
        {
            try
            {
                return dataMotAfilia.ListarMotivoAfiliacion(pEntMotAfilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListarMotivoAfiliacion", ex);
                return null;
            }
        }

        public List<MotivoModificacion> ListarMotivoModificacion(MotivoModificacion pEntMotModif, Usuario pUsuario)
        {
            try
            {
                return dataMotModi.ListarMotivoModificacion(pEntMotModif, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListarMotivoAfiliacion", ex);
                return null;
            }
        }

        public List<Estado> ListarEstado(Estado pEntEstado, Usuario pUsuario)
        {
            try
            {
                return dataEstado.ListarEstado(pEntEstado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListarEstado", ex);
                return null;
            }
        }

        public List<Oficina> ListarOficina(Oficina pEntOficina, Usuario pUsuario)
        {
            try
            {
                return dataOficina.ListarOficina(pEntOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListarOficina", ex);
                return null;
            }
        }

        public List<Oficina> ListarDireccionesOficinas(Oficina pEntOficina, Usuario pUsuario)
        {
            try
            {
                return dataOficina.ListarDireccionesOficinas(pEntOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ListarDireccionesOficinas", ex);
                return null;
            }
        }

        public EntEmpresa ConsultarEmpresa(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return dataOficina.ConsultarEmpresa(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessParametricas", "ConsultarEmpresa", ex);
                return null;
            }
        }


    }
}