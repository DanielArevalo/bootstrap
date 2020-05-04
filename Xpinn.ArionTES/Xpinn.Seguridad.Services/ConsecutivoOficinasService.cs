using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Business;

namespace Xpinn.Seguridad.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConsecutivoOficinasService
    {

        private ConsecutivoOficinasBusiness BOOficina;
        private ExcepcionBusiness BOExcepcion;

        public ConsecutivoOficinasService()
        {
            BOOficina = new ConsecutivoOficinasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90108"; } }


        public ConsecutivoOficinas CrearConsecutivoOficinas(ConsecutivoOficinas pOficina, Usuario pusuario)
        {
            try
            {
                pOficina = BOOficina.CrearConsecutivoOficinas(pOficina, pusuario);
                return pOficina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsecutivoOficinasService", "CrearConsecutivoOficinas", ex);
                return null;
            }
        }


        public ConsecutivoOficinas ModificarConsecutivoOficinas(ConsecutivoOficinas pOficina, Usuario pusuario)
        {
            try
            {
                pOficina = BOOficina.ModificarConsecutivoOficinas(pOficina, pusuario);
                return pOficina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsecutivoOficinaService", "ModificarConsecutivoOficinas", ex);
                return null;
            }
        }


        public void EliminarConsecutivoOficinas(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOOficina.EliminarConsecutivoOficinas(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsecutivoOficinaService", "EliminarConsecutivoOficinas", ex);
            }
        }


        public ConsecutivoOficinas ConsultarConsecutivoOficinas(Int64 pId, Usuario pusuario)
        {
            try
            {
                ConsecutivoOficinas ConsecutivoOficinas = new ConsecutivoOficinas();
                ConsecutivoOficinas = BOOficina.ConsultarConsecutivoOficinas(pId, pusuario);
                return ConsecutivoOficinas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsecutivoOficinaService", "ConsultarConsecutivoOficinas", ex);
                return null;
            }
        }
        public ConsecutivoOficinas ConsultarConsOfiXOfyTabla(String pIdTabla, Int64 pIdOficina, Int64 prangoin, Int64 prangfin, Usuario vUsuario)
        {
            try
            {
                ConsecutivoOficinas ConsecutivoOficinas = new ConsecutivoOficinas();
                ConsecutivoOficinas = BOOficina.ConsultarConsOfiXOfyTabla(pIdTabla, pIdOficina, prangoin, prangfin,vUsuario);
                return ConsecutivoOficinas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsecutivoOficinaService", "ConsultarConsOfiXOfyTabla", ex);
                return null;
            }
        }


        public List<ConsecutivoOficinas> ListarConsecutivoOficinas(String filtro, Usuario pusuario)
        {
            try
            {
                return BOOficina.ListarConsecutivoOficinas(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsecutivoOficinaService", "ListarConsecutivoOficinas", ex);
                return null;
            }
        }


    }
}