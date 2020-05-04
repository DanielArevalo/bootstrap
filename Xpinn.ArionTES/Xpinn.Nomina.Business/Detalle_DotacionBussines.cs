using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class Detalle_DotacionBusiness : GlobalBusiness
    {

        private Detalle_DotacionData DADetalle_Dotacion;

        public Detalle_DotacionBusiness()
        {
            DADetalle_Dotacion = new Detalle_DotacionData();
        }

        public Detalle_Dotacion CrearDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDetalle_Dotacion = DADetalle_Dotacion.CrearDetalle_Dotacion(pDetalle_Dotacion, pusuario);

                    ts.Complete();

                }

                return pDetalle_Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionBusiness", "CrearDetalle_Dotacion", ex);
                return null;
            }
        }


        public Detalle_Dotacion ModificarDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDetalle_Dotacion = DADetalle_Dotacion.ModificarDetalle_Dotacion(pDetalle_Dotacion, pusuario);

                    ts.Complete();

                }

                return pDetalle_Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionBusiness", "ModificarDetalle_Dotacion", ex);
                return null;
            }
        }


        public void EliminarDetalle_Dotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADetalle_Dotacion.EliminarDetalle_Dotacion(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionBusiness", "EliminarDetalle_Dotacion", ex);
            }
        }


        public Detalle_Dotacion ConsultarDetalle_Dotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Detalle_Dotacion Detalle_Dotacion = new Detalle_Dotacion();
                Detalle_Dotacion = DADetalle_Dotacion.ConsultarDetalle_Dotacion(pId, pusuario);
                return Detalle_Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionBusiness", "ConsultarDetalle_Dotacion", ex);
                return null;
            }
        }


        public List<Detalle_Dotacion> ListarDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario pusuario)
        {
            try
            {
                return DADetalle_Dotacion.ListarDetalle_Dotacion(pDetalle_Dotacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionBusiness", "ListarDetalle_Dotacion", ex);
                return null;
            }
        }


    }
}

