using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Integracion.Business;
using Xpinn.Integracion.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.Integracion.Services
{
    public class PqrService
    {

        private PqrBusiness BOPqr;
        private ExcepcionBusiness BOExcepcion;

        public PqrService()
        {
            BOPqr = new PqrBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        //Codigos 
        public string CodigoProgramaGestionPQR { get { return "170137"; } }

        public PQR obtenerPQR(int id_pqr, Usuario pUsuario)
        {
            try
            {
                return BOPqr.obtenerPQR(id_pqr, pUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PqrService", "obtenerPQR", ex);
                return null;
            }
        }

        public Int32 crearPQR(PQR peticion, Usuario vUsuario)
        {
            try
            {
                return BOPqr.crearPQR(peticion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PqrService", "crearPQR", ex);
                return 0;
            }
        }

        public Int32 actualizarPQR(PQR peticion, Usuario vUsuario)
        {
            try
            {
                return BOPqr.actualizarPQR(peticion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PqrService", "actualizarPQR", ex);
                return 0;
            }
        }

        public List<PQR> listarPQR(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOPqr.listarPQR(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PqrService", "listarPQR", ex);
                return null;
            }
        }

        public List<PQR_Respuesta> listarSeguimientoPQR(int id_pqr, Usuario vUsuario)
        {
            try
            {
                return BOPqr.listarSeguimientoPQR(id_pqr, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PqrService", "listarSeguimientoPQR", ex);
                return null;
            }
        }
    }
}