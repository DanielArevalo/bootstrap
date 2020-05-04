using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Business
{

    public class Oficina_ciudadBusiness : GlobalBusiness
    {

        private oficina_ciudadAportesData DAOficina_ciudad;

        public Oficina_ciudadBusiness()
        {
            DAOficina_ciudad = new oficina_ciudadAportesData();
        }

        public Oficina_ciudad CrearOficina_ciudad(Oficina_ciudad pOficina_ciudad, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOficina_ciudad = DAOficina_ciudad.Crearoficina_ciudad(pOficina_ciudad, pusuario);

                    ts.Complete();

                }

                return pOficina_ciudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "CrearOficina_ciudad", ex);
                return null;
            }
        }


        public Oficina_ciudad ModificarOficina_ciudad(Oficina_ciudad pOficina_ciudad, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOficina_ciudad = DAOficina_ciudad.Modificaroficina_ciudad(pOficina_ciudad, pusuario);

                    ts.Complete();

                }

                return pOficina_ciudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "ModificarOficina_ciudad", ex);
                return null;
            }
        }


        public void EliminarOficina_ciudad(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAOficina_ciudad.Eliminaroficina_ciudad(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "EliminarOficina_ciudad", ex);
            }
        }


        public Oficina_ciudad ConsultarOficina_ciudad(Int64 pId, Usuario pusuario)
        {
            try
            {
                Oficina_ciudad Oficina_ciudad = new Oficina_ciudad();
                Oficina_ciudad = DAOficina_ciudad.Consultaroficina_ciudad(pId, pusuario);
                return Oficina_ciudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "ConsultarOficina_ciudad", ex);
                return null;
            }
        }


        public List<Oficina_ciudad> ListarOficina_ciudad(Oficina_ciudad pOficina_ciudad, Usuario pusuario)
        {
            try
            {
                return DAOficina_ciudad.Listaroficina_ciudad(pOficina_ciudad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "ListarOficina_ciudad", ex);
                return null;
            }
        }

        public List<Oficina_ciudad> listaOficinaCiudadBussines(Usuario pusuario , String pFiltro)
        {
            try
            {
                return DAOficina_ciudad.listaOficinaCiudad(pusuario, pFiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "listaOficinaCiudadBussines", ex);
                return null;
            }
        }
        public Oficina_ciudad validaOficinaGuardaBusines(Usuario vUsuario, Oficina_ciudad entiti, int opcion) 
        {
            try
            {
                return DAOficina_ciudad.validaOficinaGuarda(vUsuario, entiti, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "validaOficinaGuardaBusines", ex);
                return null;
            }
        }
    }
}
