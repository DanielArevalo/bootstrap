using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
using Xpinn.Integracion.Data;
using Xpinn.Imagenes.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Xpinn.Integracion.Business
{
    public class PqrBusiness : GlobalData
    {
        private PqrData BOPqrData;

        public PqrBusiness()
        {
            BOPqrData = new PqrData();
        }

        public PQR obtenerPQR(int id_pqr, Usuario pUsuario)
        {
            try
            {
                PQR entidad = BOPqrData.obtenerPQR(id_pqr, pUsuario);
                if(entidad != null && entidad.id > 0)
                {
                    entidad.lstRespuestas = BOPqrData.listarSeguimientoPQR(entidad.id, pUsuario);
                    return entidad;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPqrData", "obtenerPQR", ex);
                return null;
            }
        }

        public Int32 crearPQR(PQR peticion, Usuario vUsuario)
        {
            try
            {
                int salida = BOPqrData.crearPQR(peticion, vUsuario);
                if (salida > 0 && peticion.adjunto != null)
                {
                    DocumentosSolicitud ORA = new DocumentosSolicitud();
                    ORA.CrearAdjuntoPQR(peticion.adjunto, salida, vUsuario);
                }
                return salida;   
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPqrData", "crearPQR", ex);
                return 0;
            }
        }

        public Int32 actualizarPQR(PQR peticion, Usuario vUsuario)
        {
            try
            {
                return BOPqrData.actualizarPQR(peticion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPqrData", "actualizarPQR", ex);
                return 0;
            }
        }


        public List<PQR> listarPQR(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOPqrData.listarPQR(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPqrData", "listarPQR", ex);
                return null;
            }
        }


        public List<PQR_Respuesta> listarSeguimientoPQR(int id_pqr, Usuario vUsuario)
        {
            try
            {
                return BOPqrData.listarSeguimientoPQR(id_pqr, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPqrData", "listarSeguimientoPQR", ex);
                return null;
            }
        }
    }
}


