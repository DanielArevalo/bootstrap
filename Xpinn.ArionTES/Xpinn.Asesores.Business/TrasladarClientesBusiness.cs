using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    public class TrasladarClientesBusiness : GlobalData
    {
        private TrasladarClienteData trasladar;

        public TrasladarClientesBusiness()
        {
            trasladar = new TrasladarClienteData();
        }


        public List<Persona> ListarClientesAsesor(string filtro, Usuario pUsuario)
        {
            return trasladar.ListarClientesAsesor(filtro, pUsuario);
        }

        public List<Persona> ListarProductosAsesor(string filtro, Usuario pUsuario)
        {
            return trasladar.ListarProductosAsesor(filtro, pUsuario);
        }
        public List<Persona> ListarProductos(string filtro, Usuario pUsuario)
        {
            return trasladar.ListarProductos(filtro, pUsuario);
        }

        public void ModificarProductosAsesorTodos(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarProductosAsesorTodos(cod, condicion, pUsuario);
        }
        public void ModificarProductosOficinasTodos(int producto,int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarProductosOficinasTodos(producto,cod, condicion, pUsuario);
        }

        public void ModificarClientesAsesorTodos(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesAsesorTodos(cod, condicion, pUsuario);
        }
        public void ModificarClientesOficinasTodos(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesoficinaSTodos(cod, condicion, pUsuario);
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
        public void ModificarClientesAsesor(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesAsesor(cod, condicion, pUsuario);
        }
        public void ModificarClientesOficinas(int cod, int condicion, Usuario pUsuario)
        {
            trasladar.ModificarClientesOficinas(cod, condicion, pUsuario);
        }

        public void CrearRegistroAsesores(int tipo,int nuevo, int antiguo, string cod, string obser, Usuario pUsuario)
         {
             trasladar.CrearRegistroAsesores(tipo, nuevo, antiguo, cod, obser, pUsuario);
         }
        public void CrearRegistroOficinas(int tipo, int nuevo, int antiguo, string cod, string obser, Usuario pUsuario)
        {
            trasladar.CrearRegistroOficinas(tipo, nuevo, antiguo, cod, obser, pUsuario);
        }


           public List<TrasladarClientes> ListarMotivos(Usuario pUsuario)
         {
             return trasladar.ListarMotivos(pUsuario);
         }
    }
}
