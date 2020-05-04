using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DirectivoService
    {

        private DirectivoBusiness BODirectivo;
        private ExcepcionBusiness BOExcepcion;

        public DirectivoService()
        {
            BODirectivo = new DirectivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170126"; } }

        public Directivo CrearDirectivo(Directivo pDirectivo, Usuario pusuario)
        {
            try
            {
                pDirectivo = BODirectivo.CrearDirectivo(pDirectivo, pusuario);
                return pDirectivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoService", "CrearDirectivo", ex);
                return null;
            }
        }


        public Directivo ModificarDirectivo(Directivo pDirectivo, Usuario pusuario)
        {
            try
            {
                pDirectivo = BODirectivo.ModificarDirectivo(pDirectivo, pusuario);
                return pDirectivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoService", "ModificarDirectivo", ex);
                return null;
            }
        }


        public void EliminarDirectivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                BODirectivo.EliminarDirectivo(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoService", "EliminarDirectivo", ex);
            }
        }


        public Directivo ConsultarDirectivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                Directivo Directivo = new Directivo();
                Directivo = BODirectivo.ConsultarDirectivo(pId, pusuario);
                return Directivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoService", "ConsultarDirectivo", ex);
                return null;
            }
        }


        public bool ValidarPersonaNoSeaDirectivoYa(string identificacion, Usuario pusuario)
        {
            try
            {
                return BODirectivo.ValidarPersonaNoSeaDirectivoYa(identificacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoService", "ValidarPersonaNoSeaDirectivoYa", ex);
                return false;
            }
        }


        public List<Directivo> ListarDirectivo(string filtro, Usuario pusuario)
        {
            try
            {
                return BODirectivo.ListarDirectivo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoService", "ListarDirectivo", ex);
                return null;
            }
        }


    }
}