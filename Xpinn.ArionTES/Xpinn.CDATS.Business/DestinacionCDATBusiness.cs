using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Business
{
    public class DestinacionBusiness : GlobalBusiness
    {

        private DestinacionCDATData DADestinacion;

        public DestinacionBusiness()
        {
            DADestinacion = new DestinacionCDATData();
        }

        public Destinacion CrearDestinacion(Destinacion pDestinacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDestinacion = DADestinacion.CrearDestinacion(pDestinacion, pusuario);

                    ts.Complete();

                }

                return pDestinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "CrearDestinacion", ex);
                return null;
            }
        }


        public Destinacion ModificarDestinacion(Destinacion pDestinacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDestinacion = DADestinacion.ModificarDestinacion(pDestinacion, pusuario);

                    ts.Complete();

                }

                return pDestinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "ModificarDestinacion", ex);
                return null;
            }
        }


        public void EliminarDestinacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADestinacion.EliminarDestinacion(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "EliminarDestinacion", ex);
            }
        }


        public Destinacion ConsultarDestinacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Destinacion Destinacion = new Destinacion();
                Destinacion = DADestinacion.ConsultarDestinacion(pId, pusuario);
                return Destinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "ConsultarDestinacion", ex);
                return null;
            }
        }


        public List<Destinacion> ListarDestinacion(Destinacion pDestinacion, Usuario pusuario)
        {
            try
            {
                return DADestinacion.ListarDestinacion(pDestinacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "ListarDestinacion", ex);
                return null;
            }
        }


    }

}
