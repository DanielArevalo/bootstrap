using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class OficinaBusiness : GlobalData
    {
        private OficinaData DAOficina;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public OficinaBusiness()
        {
            DAOficina = new OficinaData();
        }


        public Int32 UsuarioPuedeConsultarCreditosOficinas(int cod, Usuario pUsuario)
        {
            try
            {
                return DAOficina.UsuarioPuedeConsultarCreditosOficinas(cod, pUsuario);
            }
            catch 
            { 
                return 0;
            }
        }

        public Int32 UsuarioPuedecambiartasas(int cod, Usuario pUsuario)
        {
            return DAOficina.UsuarioPuedecambiartasas(cod, pUsuario);
        }

        /// <summary>
        /// Obtiene la lista de Oficinas
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Oficinas obtenidas</returns> 
        public List<Oficina> ListarOficinas(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ListarOficinas(pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ListarOficinas", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Oficinas para asesores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Oficinas obtenidas</returns> 
        public List<Oficina> ListarOficinasAsesores(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ListarOficinasAsesores(pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ListarOficinasAsesores", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Oficinas para usuarios
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Oficinas obtenidas</returns> 
        public List<Oficina> ListarOficinasUsuarios(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ListarOficinasUsuarios(pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ListarOficinasUsuarios", ex);
                return null;
            }
        }


        public void ValidarComisionAporte(string pCod_Linea, ref bool comision, ref bool aporte, ref bool seguro, Usuario pUsuario)
        {
            try
            {
                DAOficina.ValidarComisionAporte(pCod_Linea, ref comision, ref aporte, ref seguro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ListarOficinasUsuarios", ex);                
            }
        }

    }
}
