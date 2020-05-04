using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

namespace Xpinn.Aportes.Business
{

    public class DirectivoBusiness : GlobalBusiness
    {

        private DirectivoData DADirectivo;

        public DirectivoBusiness()
        {
            DADirectivo = new DirectivoData();
        }

        public Directivo CrearDirectivo(Directivo pDirectivo, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDirectivo = DADirectivo.CrearDirectivo(pDirectivo, pusuario);

                    ts.Complete();

                }

                return pDirectivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoBusiness", "CrearDirectivo", ex);
                return null;
            }
        }


        public Directivo ModificarDirectivo(Directivo pDirectivo, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDirectivo = DADirectivo.ModificarDirectivo(pDirectivo, pusuario);

                    ts.Complete();

                }

                return pDirectivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoBusiness", "ModificarDirectivo", ex);
                return null;
            }
        }


        public void EliminarDirectivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADirectivo.EliminarDirectivo(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoBusiness", "EliminarDirectivo", ex);
            }
        }


        public Directivo ConsultarDirectivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                Directivo Directivo = new Directivo();
                Directivo = DADirectivo.ConsultarDirectivo(pId, pusuario);
                return Directivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoBusiness", "ConsultarDirectivo", ex);
                return null;
            }
        }

        public bool ValidarPersonaNoSeaDirectivoYa(string identificacion, Usuario pusuario)
        {
            try
            {
                return DADirectivo.ValidarPersonaNoSeaDirectivoYa(identificacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoBusiness", "ValidarPersonaNoSeaDirectivoYa", ex);
                return false;
            }
        }

        public List<Directivo> ListarDirectivo(string filtro, Usuario pusuario)
        {
            try
            {
                return DADirectivo.ListarDirectivo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DirectivoBusiness", "ListarDirectivo", ex);
                return null;
            }
        }


    }
}