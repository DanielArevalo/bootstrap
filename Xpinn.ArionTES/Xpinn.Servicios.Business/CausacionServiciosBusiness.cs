using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Data;
using Xpinn.Util;
using System.Transactions;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Data;

namespace Xpinn.Servicios.Business
{
    public class CausacionServiciosBusiness :GlobalBusiness
    {
        private CausacionServiciosData DAServicio;

        public CausacionServiciosBusiness()
        {
            DAServicio = new CausacionServiciosData();
        }



        public List<Servicio> ListarServiciosCausacion(string filtro, DateTime pFechaCausa, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ListarServiciosCausacion(filtro, pFechaCausa, vUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosBusiness", "ListarServiciosCausacion", ex);
                return null;
            }
        }
        

        public void CrearCausacionServicios(ref Int64 COD_OPE, ref string pError, Xpinn.Tesoreria.Entities.Operacion pOperacion, string pCod_linea_servicio, List<CausacionServicios> lstCausacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Grabacion de Operacion
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion Entidad = new Tesoreria.Entities.Operacion();
                    Entidad = DAOperacion.GrabarOperacion(pOperacion,vUsuario);
                    COD_OPE = Entidad.cod_ope;
                    //Crear Causaciones seleccionadas
                    foreach (CausacionServicios nCausacion in lstCausacion)
                    {
                        CausacionServicios entidad = new CausacionServicios();
                        nCausacion.cod_ope = COD_OPE;
                        entidad = DAServicio.CrearCausacionServicios(nCausacion, ref pError, vUsuario);
                        if (!string.IsNullOrEmpty(pError))
                            break;
                    }
                    if (!string.IsNullOrEmpty(pError))
                        return;

                    //Agregado para que se haga el registro en CIEREA solamente si ya se causaron todas las líneas de servicio
                    bool regCausacion = DAServicio.ValidarCausacion(pOperacion.fecha_oper, vUsuario);
                    if (regCausacion == true)
                    {   
                        //Crear registro de control en Cierea
                        CiereaData DACierea = new CiereaData();
                        Cierea pCierea = new Cierea();
                        pCierea.fecha = pOperacion.fecha_oper;
                        pCierea.tipo = "X";
                        pCierea.estado = "D";
                        pCierea.campo1 = pCod_linea_servicio;
                        pCierea.campo2 = COD_OPE.ToString();
                        pCierea.fecrea = DateTime.Now;
                        pCierea.codusuario = vUsuario.codusuario;
                        DACierea.CrearCierea(pCierea, vUsuario);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        //RENOVACION
        public List<Servicio> ListarServiciosRenovacion(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ListarServiciosRenovacion(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosBusiness", "ListarServiciosRenovacion", ex);
                return null;
            }
        }


        public void CrearRenovacionServicios(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, List<Servicio> lstServicio,Servicio pDatosServ,RenovacionServicios pDatosRenova, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion Entidad = new Tesoreria.Entities.Operacion();
                    Entidad = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    COD_OPE = Entidad.cod_ope;

                    foreach (Servicio nServi in lstServicio)
                    {
                        //ACTUALIZACION DE TABLA SERVICIO
                        Servicio entServicio = new Servicio();
                        Servicio Serv = new Servicio();
                        Serv.numero_servicio = nServi.numero_servicio;
                        Serv.fecha_inicio_vigencia = pDatosServ.fecha_inicio_vigencia;
                        Serv.fecha_final_vigencia = pDatosServ.fecha_final_vigencia;

                        decimal valor = 0,ValorRenovacion = 0;
                        ValorRenovacion = pDatosRenova.valor_cuota != null ? Convert.ToDecimal(pDatosRenova.valor_cuota) : 0;
                        nServi.valor_cuota = nServi.valor_cuota != null ? nServi.valor_cuota : 0;
                        if (pDatosRenova.tipo == 0) //PORCENTAJE DE LA CUOTA
                        {                            
                            valor = Convert.ToDecimal(nServi.valor_cuota) * (1 + (ValorRenovacion / 100));
                        }
                        else if (pDatosRenova.tipo == 1) //VR DE LA CUOTA
                        {
                            valor = Convert.ToDecimal(nServi.valor_cuota) + ValorRenovacion;
                        }
                        else if (pDatosRenova.tipo == 2) //CUOTA GENERAL
                        {
                            valor = ValorRenovacion;
                        }
                        Serv.valor_cuota = valor;
                        Serv.numero_cuotas = pDatosServ.numero_cuotas;
                        Serv.valor_total = valor * pDatosServ.numero_cuotas;
                        Serv.saldo = Serv.valor_total;
                        Serv.cuotas_pendientes = Convert.ToInt32(pDatosServ.numero_cuotas);

                        entServicio = DAServicio.ModificarRenovacionServicio(Serv, vUsuario);

                        //RENOVACION
                        RenovacionServicios entidad = new RenovacionServicios();
                        RenovacionServicios pRenova = new RenovacionServicios();
                        pRenova.idrenovacion = 0;
                        pRenova.numero_servicio = nServi.numero_servicio;
                        pRenova.fecha_renovacion = DateTime.Now;
                        pRenova.cod_ope = COD_OPE;
                        pRenova.fecha_inicial_vigencia = pDatosServ.fecha_inicio_vigencia;
                        pRenova.fecha_final_vigencia = pDatosServ.fecha_final_vigencia;
                        pRenova.valor_total = valor * pDatosServ.numero_cuotas;
                        pRenova.plazo = pDatosServ.numero_cuotas;
                        pRenova.tipo = 1;
                        entidad = DAServicio.CrearRenovacionServicios(pRenova, vUsuario);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosBusiness", "CrearCausacionServicios", ex);
            }
        }


        public List<Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            CiereaData DACierea = new CiereaData();
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DACierea.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            DateTime FecUltCierre;
            Cierea pCierre = new Cierea();
            FecUltCierre = DACierea.FechaUltimoCierre("X", pUsuario);
            if (FecUltCierre == DateTime.MinValue)
            {
                int año = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                if (mes <= 1)
                {
                    mes = 12;
                    año = año - 1;
                }
                else
                {
                    mes = mes - 1;
                }
                pCierre.fecha = new DateTime(año, mes, 1, 0, 0, 0).AddDays(-1);
            }
            else
                pCierre.fecha = FecUltCierre;
            DateTime FecIni = pCierre.fecha;
            if (FecIni > DateTime.Now.AddDays(15))
                return null;
            // Calcular fechas de cierre inicial
            DateTime FecFin = DateTime.MinValue;
            FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

            if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
            {
                bool control = true;
                do
                {
                    FecFin = FecFin.AddDays(1);
                    if (FecFin.Day == 1)
                    {
                        FecFin = FecFin.AddDays(-1);
                        control = false;
                    }
                } while (control == true);
            }

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15))
            {
                Cierea cieRea = new Cierea();
                cieRea.fecha = FecFin;
                LstCierre.Add(cieRea);
                FecIni = FecFin;
                FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

                if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
                {
                    bool control = true;
                    do
                    {
                        FecFin = FecFin.AddDays(1);
                        if (FecFin.Day == 1)
                        {
                            FecFin = FecFin.AddDays(-1);
                            control = false;
                        }
                    } while (control == true);
                }
            }
            return LstCierre;
        }

        public string ValidarCausacionXFecha( DateTime pFechaCausacion, Int64 cod_linea_servicio, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ValidarCausacionXFecha(pFechaCausacion, cod_linea_servicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosBusiness", "ValidarCausacionXFecha", ex);
                return null;
            }
        }

    }
}
