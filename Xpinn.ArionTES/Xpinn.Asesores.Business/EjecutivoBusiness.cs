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
    public class EjecutivoBusiness : GlobalData
    {
        private EjecutivoData dataEjecutivo;

        public EjecutivoBusiness()
        {
            dataEjecutivo = new EjecutivoData();
        }

        public Ejecutivo CrearEjecutivo(Ejecutivo pAseEntEjecutivo, Usuario pUsuario)
        {
            //try
            //{
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    pAseEntEjecutivo = dataEjecutivo.Crear(pAseEntEjecutivo, pUsuario);
                   // ts.Complete();
                //}
                return pAseEntEjecutivo;
            //}
            //catch (TransactionException ex)
            //{
            //    BOExcepcion.Throw("BusinessEjecutivo", "CrearCliente", ex);
            //    return null;
            //}
        }

        public void EliminarEjecutivo(Int64 pIdEjecutivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    dataEjecutivo.Eliminar(pIdEjecutivo, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "EliminarEjecutivo", ex);
            }
        }
        public string DetalleBarriosEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.DetalleBarriosEjecutivo(pIdAseEntiEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "DetalleBarriosEjecutivo", ex);
                return "";
            }

        }
        public string DetalleZonasEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.DetalleZonasEjecutivo(pIdAseEntiEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ConsultarEjecutivo", ex);
                return "";
            }

        }
        public Ejecutivo ConsultarEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.Consultar(pIdAseEntiEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ConsultarEjecutivo", ex);
                return null;
            }

        }
        public Ejecutivo ConsultarDatosEjecutivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ConsultarDatosEjecutivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ConsultarDatosEjecutivo", ex);
                return null;
            }

        }
        public Ejecutivo ConsultarDatosDirector(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ConsultarDatosDirector(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ConsultarDatosDirector", ex);
                return null;
            }

        }
        public List<Ejecutivo> guardarZonasEjecutivo(List<Ejecutivo> zonas, Usuario pUsuario)
        {
            try
            {
                dataEjecutivo.eliminarZonasEjecutivo(zonas[0].IdEjecutivo, pUsuario);
                foreach (Ejecutivo z in zonas)
                {
                    dataEjecutivo.guardarZonasEjecutivo(z, pUsuario);
                }
                return zonas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "guardarZonasEjecutivo", ex);
                return null;
            }
        }
        public List<Ejecutivo> guardarBarriosEjecutivo(List<Ejecutivo> barrios, Usuario pUsuario)
        {
            try
            {
                foreach (Ejecutivo b in barrios)
                {
                    dataEjecutivo.guardarBarriosEjecutivo(b, pUsuario);
                }
                return barrios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "guardarBarriosEjecutivo", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListarZonasEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ListarZonasEjecutivo(idEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarZonasEjecutivo", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarZonasDeEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ListarZonasDeEjecutivo(idEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarZonasEjecutivo", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarBarriosEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ListarBarriosEjecutivo(idEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarBarriosEjecutivo", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarEjecutivo(Ejecutivo pAseEntEjecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.Listar(pAseEntEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarCliente", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarEjecutivo(Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.Listar(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarCliente", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarAsesores(Ejecutivo ejecutivo, Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ListarAsesores(ejecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarAsesores", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListartodosAsesores(Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ListartodosAsesores(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarAsesores", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListartodosAsesoresRuta(Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ListartodosAsesoresRuta(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListartodosAsesoresRuta", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarAsesoresgeoreferencia(Ejecutivo ejecutivo, Usuario pUsuario,string filtro)
        {
            try
            {
                return dataEjecutivo.ListarAsesoresgeoreferencia(ejecutivo, pUsuario,filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListarAsesores", ex);
                return null;
            }
        }
        public Ejecutivo ActualizarEjecutivo(Ejecutivo pEntityEjecutivo, Usuario pUsuario)
        {
            //try
            //{
            //    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            //    {
                    pEntityEjecutivo = dataEjecutivo.Actualizar(pEntityEjecutivo, pUsuario);
                //    ts.Complete();
                //}
                return pEntityEjecutivo;
            //}
            //catch (TransactionException ex)
            //{
            //    BOExcepcion.Throw("EjecutivoBusiness", "ActualizarEjecutivo", ex);
            //    return null;
            //}
        }

        public List<Ejecutivo> ListartodosUsuarios(Usuario pUsuario)
        {
            try
            {
                return dataEjecutivo.ListartodosUsuarios(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivo", "ListartodosUsuarios", ex);
                return null;
            }
        }
    }
}
