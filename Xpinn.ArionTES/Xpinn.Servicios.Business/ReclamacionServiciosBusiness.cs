 using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Servicios.Data;
using Xpinn.Servicios.Entities;

 
namespace Xpinn.Servicios.Business
{
 
        public class ReclamacionServiciosBussiness : GlobalBusiness
        {
 
            private ReclamacionServiciosData DAservicios;
 
            public ReclamacionServiciosBussiness()
            {
                DAservicios = new ReclamacionServiciosData();
            }

            public ReclamacionServicios Crearservicios(DateTime pFechaIni, ReclamacionServicios pservicios, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pservicios = DAservicios.Crearservicios(pFechaIni,pservicios, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pservicios;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("serviciosBusiness", "Crearservicios", ex);
                    return null;
                }
            }
 
 
            public ReclamacionServicios Modificarservicios(DateTime  pFechaIni,ReclamacionServicios pservicios, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pservicios = DAservicios.Modificarservicios(pFechaIni,pservicios, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pservicios;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("serviciosBusiness", "Modificarservicios", ex);
                    return null;
                }
            }
 
           public bool ValidarFallecido(string pIdentificacion, Int64? pIdReclamacion, Usuario pUsuario)
                {
                    try
                    {
                        return  DAservicios.ValidarFallecido(pIdentificacion,pIdReclamacion, pUsuario);
               
                    }
                        
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("serviciosBusiness", "Eliminarservicios", ex);
                        return false;
                    }
                   
                }
            public void Eliminarservicios(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAservicios.Eliminarservicios(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("serviciosBusiness", "Eliminarservicios", ex);
                }
            }


            public ReclamacionServicios Consultarservicios(int pid,  Usuario pusuario)
            {
                try
                {
                    ReclamacionServicios servicios = new ReclamacionServicios();
                    servicios = DAservicios.Consultarservicios(pid, pusuario);
                    return servicios;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("serviciosBusiness", "Consultarservicios", ex);
                    return null;
                }
            }


            public List<ReclamacionServicios> Listarservicios(string filtro, string pOrden, DateTime pFechaIni, Usuario pusuario)
            {
                try
                {
                    List<ReclamacionServicios> servicios = new List<ReclamacionServicios>();
                    servicios = DAservicios.Listarservicios(filtro, pOrden, pFechaIni, pusuario);
                    return servicios;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("serviciosBusiness", "Listarservicios", ex);
                    return null;
                }
            }
 
 
        }
    }

