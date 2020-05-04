using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class DotacionBusiness : GlobalBusiness
    {

        private DotacionData DADotacion;

        public DotacionBusiness()
        {
            DADotacion = new DotacionData();
        }

        public Dotacion CrearDotacion(Dotacion pDotacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDotacion = DADotacion.CrearDotacion(pDotacion, pusuario);

                    ts.Complete();

                }

                return pDotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionBusiness", "CrearDotacion", ex);
                return null;
            }
        }


        public Dotacion ModificarDotacion(Dotacion pDotacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDotacion = DADotacion.ModificarDotacion(pDotacion, pusuario);

                    ts.Complete();

                }

                return pDotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionBusiness", "ModificarDotacion", ex);
                return null;
            }
        }


        public void EliminarDotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADotacion.EliminarDotacion(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionBusiness", "EliminarDotacion", ex);
            }
        }

        public Dotacion ConsultarDatosDotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Dotacion Dotacion = new Dotacion();
                Dotacion = DADotacion.ConsultarDatosDotacion(pId, pusuario);
                return Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionBusiness", "ConsultarDotacion", ex);
                return null;
            }
        }


        public Dotacion ConsultarDotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Dotacion Dotacion = new Dotacion();
                Dotacion = DADotacion.ConsultarDotacion(pId, pusuario);
                return Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionBusiness", "ConsultarDotacion", ex);
                return null;
            }
        }


        public List<Dotacion> ListarDotacion(string filtro, Usuario pusuario)
        {
            try
            {
                return DADotacion.ListarDotacion(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionBusiness", "ListarDotacion", ex);
                return null;
            }
        }


    }
}

