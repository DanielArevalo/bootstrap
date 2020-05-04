using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;
using System.Data;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Services
{
  
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TrasladoClientesService
    {
        private TrasladarClientesBusiness trasladar;

        public TrasladoClientesService()
        {
            trasladar = new TrasladarClientesBusiness();
        }

        public string CodigoPrograma { get { return "110122"; } }

        public List<Persona> ListarProductos(string filtro, Usuario pUsuario)
        {
            return trasladar.ListarProductos(filtro, pUsuario);
        }

        public List<Persona> ListarProductosAsesor(string filtro, Usuario pUsuario)
        {
            return trasladar.ListarProductosAsesor(filtro, pUsuario);
        }

        public List<Persona> ListarClientesAsesor(string filtro, Usuario pUsuario)
        {
            return trasladar.ListarClientesAsesor(filtro, pUsuario);
        }

        public void ModificarProductosAsesorTodos(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarProductosAsesorTodos(cod, condicion, pUsuario);
        }
        public void ModificarProductosOficinasTodos(int producto, int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarProductosOficinasTodos(producto,cod, condicion, pUsuario);
        }
        public void ModificarClientesAsesorTodos(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesAsesorTodos(cod, condicion, pUsuario);
        }

        public void ModificarClientesOficinasTodos(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesOficinasTodos(cod, condicion, pUsuario);
        }
        public void ModificarProductosAsesor(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarProductosAsesor(cod, condicion, pUsuario);
        }
        public void ModificarProductosOficina(int producto,int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarProductosOficina(producto,cod, condicion, pUsuario);
        }
        public void ModificarProductosOficina2( int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarProductosOficina2( cod, condicion, pUsuario);
        }
        public void ModificarClientesOficinas(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesOficinas(cod, condicion, pUsuario);
        }
        public void ModificarClientesAsesor(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesAsesor(cod, condicion, pUsuario);
        }
        public void CrearRegistroAsesores(int tipo,int nuevo, int antiguo, string cod, string obser, Usuario pUsuario)
        {
            trasladar.CrearRegistroAsesores(tipo,nuevo, antiguo, cod, obser, pUsuario);
        }
        public void CrearRegistroOficinas(int tipo, int nuevo, int antiguo, string cod, string obser, Usuario pUsuario)
        {
            trasladar.CrearRegistroOficinas(tipo, nuevo, antiguo, cod, obser, pUsuario);
        }
        public List<TrasladarClientes> ListarMotivos(Usuario pUsuario)
        {
          return  trasladar.ListarMotivos(pUsuario);
        }
    }

    
       
}
