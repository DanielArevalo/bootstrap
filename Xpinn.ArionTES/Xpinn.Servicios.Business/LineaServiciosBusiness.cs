using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Data;
using Xpinn.Util;
using System.Transactions;
using Xpinn.Imagenes.Data;

namespace Xpinn.Servicios.Business
{
    public class LineaServiciosBusiness : GlobalBusiness
    {
        private LineaServiciosData BALinea;
        private ImagenesORAData DAImagenes;

        public LineaServiciosBusiness()
        {
            BALinea = new LineaServiciosData();
        }

        public LineaServicios CrearLineaServicio(LineaServicios pLinea, byte[] foto, byte[] banner, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    string cod;
                    pLinea = BALinea.CrearLineaServicio(pLinea, foto, banner, vUsuario);
                    cod = pLinea.cod_linea_servicio;

                    if (pLinea.lstPlan != null && pLinea.lstPlan.Count != 0)
                    {
                        int num = 0;
                        int cont = 0;
                        cont = Convert.ToInt32(BALinea.ObtenerUltimoCodigo(vUsuario).ToString());
                        foreach (planservicios ePlan in pLinea.lstPlan)
                        {
                            planservicios nDetalle = new planservicios();
                            ePlan.cod_linea_servicio = cod;
                            cont++;
                            ePlan.cod_plan_servicio = cont.ToString();
                            nDetalle = BALinea.CrearPlanServicio(ePlan, vUsuario);
                            num += 1;
                        }
                    }

                    if (pLinea.lstdestinacion != null && pLinea.lstdestinacion.Count > 0)
                    {
                        foreach (LineaServicios nDest in pLinea.lstdestinacion)
                        {
                            LineaServicios nDestino = new LineaServicios();
                            nDest.cod_linea_servicio = cod;
                            nDestino = BALinea.CrearDestino_Linea(nDest, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pLinea;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "CrearLineaServicio", ex);
                return null;
            }
        }

        public LineaServicios ModificarLineaServicio(LineaServicios pLinea, byte[] foto, byte[] banner, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLinea = BALinea.ModificarLineaServicio(pLinea, foto, banner, vUsuario);

                    string Cod;
                    Cod = pLinea.cod_linea_servicio;

                    if (pLinea.lstPlan != null && pLinea.lstPlan.Count != 0)
                    {
                        int num = 0;
                        int cont = 0;
                        cont = Convert.ToInt32(BALinea.ObtenerUltimoCodigo(vUsuario).ToString());
                        foreach (planservicios ePlan in pLinea.lstPlan)
                        {
                            ePlan.cod_linea_servicio = Cod;
                            planservicios nPlanServ = new planservicios();
                            if (Convert.ToInt32(ePlan.cod_plan_servicio) <= 0 || ePlan.cod_plan_servicio == null)
                            {
                                cont++;
                                ePlan.cod_plan_servicio = cont.ToString();
                                nPlanServ = BALinea.CrearPlanServicio(ePlan, vUsuario);
                            }
                            else
                                nPlanServ = BALinea.ModificarPlanServicio(ePlan, vUsuario);
                            num += 1;
                        }
                    }

                    BALinea.EliminarDestinacion_Linea(Cod, vUsuario);
                    if (pLinea.lstdestinacion != null && pLinea.lstdestinacion.Count > 0)
                    {
                        foreach (LineaServicios nDest in pLinea.lstdestinacion)
                        {
                            LineaServicios nDestino = new LineaServicios();
                            nDest.cod_linea_servicio = Cod;
                            nDestino = BALinea.CrearDestino_Linea(nDest, vUsuario);
                        }
                    }


                    ts.Complete();
                }

                return pLinea;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "ModificarLineaServicio", ex);
                return null;
            }
        }

        public List<LineaServicios> ListarLineaServicios(LineaServicios pLinea, Usuario vUsuario, string filtro)
        {
            try
            {
                return BALinea.ListarLineaServicios(pLinea, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "ListarLineaServicios", ex);
                return null;
            }
        }

        public void EliminarLineaServicio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BALinea.EliminarLineaServicio(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "EliminarLineaServicio", ex);
            }
        }

        public LineaServicios ConsultarLineaSERVICIO(string pId, Usuario vUsuario)
        {
            try
            {
                return BALinea.ConsultarLineaSERVICIO(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "ConsultarLineaSERVICIO", ex);
                return null;
            }
        }

        public void EliminarDETALLELineaSERVICIO(string pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BALinea.EliminarDETALLELineaSERVICIO(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "EliminarDETALLELineaSERVICIO", ex);
            }
        }

        public List<planservicios> ConsultarDETALLELineaSERVICIO(string pId, Usuario vUsuario)
        {
            try
            {
                return BALinea.ConsultarDETALLELineaSERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "ConsultarDETALLELineaSERVICIO", ex);
                return null;
            }
        }

        #region destinacion

        public List<LineaServicios> ConsultarDestinacion_Linea(string pId, Usuario vUsuario)
        {
            try
            {
                return BALinea.ConsultarDestinacion_Linea(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarDestinacion_Linea", ex);
                return null;
            }
        }

        public LineaServ_Destinacion consultaDestinacionservicio(string pId, string pIdLin, Usuario vUsuario)
        {
            try
            {
                return BALinea.consultaDestinacionservicio(pId, pIdLin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "consultaDestinacionservicio", ex);
                return null;
            }
        }

        #endregion

    }
}
