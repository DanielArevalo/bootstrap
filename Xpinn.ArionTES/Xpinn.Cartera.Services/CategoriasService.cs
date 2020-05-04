using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;

namespace Xpinn.Cartera.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CategoriasService
    {

        private CategoriasBusiness BOCategorias;
        private ExcepcionBusiness BOExcepcion;

        public CategoriasService()
        {
            BOCategorias = new CategoriasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60401"; } }

        public Categorias CrearCategorias(Categorias pCategorias, Usuario pusuario)
        {
            try
            {
                pCategorias = BOCategorias.CrearCategorias(pCategorias, pusuario);
                return pCategorias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasService", "CrearCategorias", ex);
                return null;
            }
        }


        public Categorias ModificarCategorias(Categorias pCategorias, Usuario pusuario)
        {
            try
            {
                pCategorias = BOCategorias.ModificarCategorias(pCategorias, pusuario);
                return pCategorias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasService", "ModificarCategorias", ex);
                return null;
            }
        }


        public void EliminarCategorias(string pId, Usuario pusuario)
        {
            try
            {
                BOCategorias.EliminarCategorias(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasService", "EliminarCategorias", ex);
            }
        }


        public Categorias ConsultarCategorias(string pId, Usuario pusuario)
        {
            try
            {
                Categorias Categorias = new Categorias();
                Categorias = BOCategorias.ConsultarCategorias(pId, pusuario);
                return Categorias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasService", "ConsultarCategorias", ex);
                return null;
            }
        }


        public List<Categorias> ListarCategorias(Categorias pCategorias, Usuario pusuario)
        {
            try
            {
                return BOCategorias.ListarCategorias(pCategorias, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasService", "ListarCategorias", ex);
                return null;
            }
        }


    }
}
