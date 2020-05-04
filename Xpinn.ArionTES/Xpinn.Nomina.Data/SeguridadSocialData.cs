using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Nomina.Entities;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

namespace Xpinn.Nomina.Data
{
    public class SeguridadSocialData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public SeguridadSocialData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        
        public SeguridadSocial CrearSeguridadSocial(SeguridadSocial pSeguridadSocial,Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSEGURIDAD = cmdTransaccionFactory.CreateParameter();
                        pIDSEGURIDAD.ParameterName = "p_IDSEGURIDAD";
                        pIDSEGURIDAD.Value = pSeguridadSocial.IDSEGURIDAD;
                        pIDSEGURIDAD.Direction = ParameterDirection.Input;
                        pIDSEGURIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIDSEGURIDAD);

                        DbParameter pPorcentajeSalud = cmdTransaccionFactory.CreateParameter();
                        pPorcentajeSalud.ParameterName = "p_PORCENTAJE_SALUD";
                        pPorcentajeSalud.Value = pSeguridadSocial.PORCENTAJE_SALUD;       
                        pPorcentajeSalud.Direction = ParameterDirection.Input;
                        pPorcentajeSalud.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajeSalud);

                        DbParameter pPorcentajePension = cmdTransaccionFactory.CreateParameter();
                        pPorcentajePension.ParameterName = "p_PORCENTAJE_PENSION";
                        pPorcentajePension.Value = pSeguridadSocial.PORCENTAJE_PENSION;
                        pPorcentajePension.Direction = ParameterDirection.Input;
                        pPorcentajePension.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajePension);

                        DbParameter pPorcEmpladorSalud = cmdTransaccionFactory.CreateParameter();
                        pPorcEmpladorSalud.ParameterName = "p_PORC_EMPLEADOR_SALUD";
                        pPorcEmpladorSalud.Value = pSeguridadSocial.PORC_EMPLEADOR_SALUD;
                        pPorcEmpladorSalud.Direction = ParameterDirection.Input;
                        pPorcEmpladorSalud.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcEmpladorSalud);

                        DbParameter pPorcEmpladorPension = cmdTransaccionFactory.CreateParameter();
                        pPorcEmpladorPension.ParameterName = "p_PORC_EMPLEADOR_PENSION";
                        pPorcEmpladorPension.Value = pSeguridadSocial.PORC_EMPLEADOR_PENSION;
                        pPorcEmpladorPension.Direction = ParameterDirection.Input;
                        pPorcEmpladorPension.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcEmpladorPension);

                        DbParameter pPorcentajeIncapacidad = cmdTransaccionFactory.CreateParameter();
                        pPorcentajeIncapacidad.ParameterName = "p_PORCENTAJE_INCAPACIDAD";
                        pPorcentajeIncapacidad.Value = pSeguridadSocial.PORCENTAJE_INCAPACIDAD;
                        pPorcentajeIncapacidad.Direction = ParameterDirection.Input;
                        pPorcentajeIncapacidad.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajeIncapacidad);

                        DbParameter pPermIncapacidadTope = cmdTransaccionFactory.CreateParameter();
                        pPermIncapacidadTope.ParameterName = "p_PERMITE_INCAPACIDAD_TOPE";
                        pPermIncapacidadTope.Value = pSeguridadSocial.PERMITE_INCAPACIDAD_TOPE;
                        pPermIncapacidadTope.Direction = ParameterDirection.Input;
                        pPermIncapacidadTope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pPermIncapacidadTope);

                        DbParameter pDescontarAportes = cmdTransaccionFactory.CreateParameter();
                        pDescontarAportes.ParameterName = "p_DESCONTAR_APORTES";
                        pDescontarAportes.Value = pSeguridadSocial.DESCONTAR_APORTES;
                        pDescontarAportes.Direction = ParameterDirection.Input;
                        pDescontarAportes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDescontarAportes);

                        DbParameter pMarcarVST = cmdTransaccionFactory.CreateParameter();
                        pMarcarVST.ParameterName = "p_MARCAR_VST";
                        pMarcarVST.Value = pSeguridadSocial.MARCAR_VST;
                        pMarcarVST.Direction = ParameterDirection.Input;
                        pMarcarVST.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pMarcarVST);

                        DbParameter pDescontarAporEmpl = cmdTransaccionFactory.CreateParameter();
                        pDescontarAporEmpl.ParameterName = "p_DESCONTAR_APORTE_EMPL";
                        pDescontarAporEmpl.Value = pSeguridadSocial.DESCONTAR_APORTE_EMPL;
                        pDescontarAporEmpl.Direction = ParameterDirection.Input;
                        pDescontarAporEmpl.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDescontarAporEmpl);

                        DbParameter pInactDiasCal = cmdTransaccionFactory.CreateParameter();
                        pInactDiasCal.ParameterName = "p_INACTIVIDAD_DIAS_CAL";
                        pInactDiasCal.Value = pSeguridadSocial.INACTIVIDAD_DIAS_CAL;
                        pInactDiasCal.Direction = ParameterDirection.Input;
                        pInactDiasCal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pInactDiasCal);

                        DbParameter pDescDiasCastigo = cmdTransaccionFactory.CreateParameter();
                        pDescDiasCastigo.ParameterName = "p_DESCUENTA_DIAS_CASTIGO";
                        pDescDiasCastigo.Value = pSeguridadSocial.DESCUENTA_DIAS_CASTIGO;
                        pDescDiasCastigo.Direction = ParameterDirection.Input;
                        pDescDiasCastigo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDescDiasCastigo);

                        DbParameter pNitArchivo = cmdTransaccionFactory.CreateParameter();
                        pNitArchivo.ParameterName = "p_NIT_ARCHIVO";
                        pNitArchivo.Value = pSeguridadSocial.NIT_ARCHIVO;
                        pNitArchivo.Direction = ParameterDirection.Input;
                        pNitArchivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pNitArchivo);

                        DbParameter pBaseInactDias = cmdTransaccionFactory.CreateParameter();
                        pBaseInactDias.ParameterName = "p_BASE_INACTIVIDAD_DIAS";
                        pBaseInactDias.Value = pSeguridadSocial.BASE_INACTIVIDAD_DIAS;
                        pBaseInactDias.Direction = ParameterDirection.Input;
                        pBaseInactDias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pBaseInactDias);

                        DbParameter pProcediminCentroARP = cmdTransaccionFactory.CreateParameter();
                        pProcediminCentroARP.ParameterName = "p_PROCEDIMIENTO_CENTRO_ARP";
                        pProcediminCentroARP.Value = pSeguridadSocial.PROCEDIMIENTO_CENTRO_ARP;
                        pProcediminCentroARP.Direction = ParameterDirection.Input;
                        pProcediminCentroARP.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pProcediminCentroARP);

                        DbParameter pIBCinact = cmdTransaccionFactory.CreateParameter();
                        pIBCinact.ParameterName = "p_IBC_INACTIVIDADES";
                        pIBCinact.Value = pSeguridadSocial.IBC_INACTIVIDADES;
                        pIBCinact.Direction = ParameterDirection.Input;
                        pIBCinact.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIBCinact);

                        DbParameter pCalPrimdias = cmdTransaccionFactory.CreateParameter();
                        pCalPrimdias.ParameterName = "p_CALCULO_PRIMDIAS";
                        pCalPrimdias.Value = pSeguridadSocial.CALCULO_PRIMDIAS;
                        pCalPrimdias.Direction = ParameterDirection.Input;
                        pCalPrimdias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCalPrimdias);

                        DbParameter pSalpenVacaciones = cmdTransaccionFactory.CreateParameter();
                        pSalpenVacaciones.ParameterName = "p_SALPEN_VACACIONES";
                        pSalpenVacaciones.Value = pSeguridadSocial.SALPEN_VACACIONES;
                        pSalpenVacaciones.Direction = ParameterDirection.Input;
                        pSalpenVacaciones.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pSalpenVacaciones);

                        DbParameter pPORCENTAJE_SALUD_PENSIONADO = cmdTransaccionFactory.CreateParameter();
                        pPORCENTAJE_SALUD_PENSIONADO.ParameterName = "p_PORCENTAJE_SALUD_PENSIONADO";
                        pPORCENTAJE_SALUD_PENSIONADO.Value = pSeguridadSocial.PORCENTAJE_SALUD_PENSIONADO;
                        pPORCENTAJE_SALUD_PENSIONADO.Direction = ParameterDirection.Input;
                        pPORCENTAJE_SALUD_PENSIONADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pPORCENTAJE_SALUD_PENSIONADO);


                        DbParameter pPERIODOS_MAX_LEY_VACACIONES = cmdTransaccionFactory.CreateParameter();
                        pPERIODOS_MAX_LEY_VACACIONES.ParameterName = "p_PERIODOS_MAX_LEY_VACACIONES";
                        pPERIODOS_MAX_LEY_VACACIONES.Value = pSeguridadSocial.PERIODOS_MAXIMOS_VACACIONES;
                        pPERIODOS_MAX_LEY_VACACIONES.Direction = ParameterDirection.Input;
                        pPERIODOS_MAX_LEY_VACACIONES.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pPERIODOS_MAX_LEY_VACACIONES);

                        DbParameter PMAXSALARIOSARL = cmdTransaccionFactory.CreateParameter();
                        PMAXSALARIOSARL.ParameterName = "p_MAXSALARIOSARL";
                        PMAXSALARIOSARL.Value = pSeguridadSocial.MAXSALARIOSARL;
                        PMAXSALARIOSARL.Direction = ParameterDirection.Input;
                        PMAXSALARIOSARL.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PMAXSALARIOSARL);

                        DbParameter pMAXSALARIOSPARAFISCALES = cmdTransaccionFactory.CreateParameter();
                        pMAXSALARIOSPARAFISCALES.ParameterName = "p_MAXSALARIOSPARAFISCALES";
                        pMAXSALARIOSPARAFISCALES.Value = pSeguridadSocial.MAXSALARIOSPARAFISCALES;
                        pMAXSALARIOSPARAFISCALES.Direction = ParameterDirection.Input;
                        pMAXSALARIOSPARAFISCALES.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pMAXSALARIOSPARAFISCALES);

                        DbParameter pMAXSALARIOSSALUDPENSION = cmdTransaccionFactory.CreateParameter();
                        pMAXSALARIOSSALUDPENSION.ParameterName = "p_MAXSALARIOSSALUDPENSION";
                        pMAXSALARIOSSALUDPENSION.Value = pSeguridadSocial.MAXSALARIOSSALUDPENSION;
                        pMAXSALARIOSSALUDPENSION.Direction = ParameterDirection.Input;
                        pMAXSALARIOSSALUDPENSION.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pMAXSALARIOSSALUDPENSION);


                        DbParameter pPorCajaCompensacion =cmdTransaccionFactory.CreateParameter();
                        pPorCajaCompensacion.ParameterName = "p_PORC_CAJA_COMPENSACION";
                        pPorCajaCompensacion.Value = pSeguridadSocial.CajaCompensacion;
                        pPorCajaCompensacion.Direction = ParameterDirection.Input;
                        pPorCajaCompensacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorCajaCompensacion);
                        

                        DbParameter pPorSena = cmdTransaccionFactory.CreateParameter();
                        pPorSena.ParameterName = "p_PORC_SENA";
                        pPorSena.Value = pSeguridadSocial.sena;
                        pPorSena.Direction = ParameterDirection.Input;
                        pPorSena.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorSena);



                        DbParameter pPorIcbf= cmdTransaccionFactory.CreateParameter();
                        pPorIcbf.ParameterName = "p_PORC_ICBF";
                        pPorIcbf.Value = pSeguridadSocial.icbf;
                        pPorIcbf.Direction = ParameterDirection.Input;
                        pPorIcbf.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorIcbf);



                        DbParameter pPorPrima= cmdTransaccionFactory.CreateParameter();
                        pPorPrima.ParameterName = "p_PORC_PRIMA";
                        pPorPrima.Value = pSeguridadSocial.prima;
                        pPorPrima.Direction = ParameterDirection.Input;
                        pPorPrima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorPrima);

                        DbParameter pPorCesantias= cmdTransaccionFactory.CreateParameter();
                        pPorCesantias.ParameterName = "p_PORC_CESANTIAS";
                        pPorCesantias.Value = pSeguridadSocial.cesantias;
                        pPorCesantias.Direction = ParameterDirection.Input;
                        pPorCesantias.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorCesantias);

                        DbParameter pPorIntCesantias = cmdTransaccionFactory.CreateParameter();
                        pPorIntCesantias.ParameterName = "p_PORC_INT_CESANTIAS";
                        pPorIntCesantias.Value = pSeguridadSocial.interescesantias;
                        pPorIntCesantias.Direction = ParameterDirection.Input;
                        pPorIntCesantias.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorIntCesantias);



                        DbParameter pPorvacaciones = cmdTransaccionFactory.CreateParameter();
                        pPorvacaciones.ParameterName = "p_PORC_VACACIONES";
                        pPorvacaciones.Value = pSeguridadSocial.vacaciones;
                        pPorvacaciones.Direction = ParameterDirection.Input;
                        pPorvacaciones.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorvacaciones);



                        DbParameter p_DIASVACACIONES = cmdTransaccionFactory.CreateParameter();
                        p_DIASVACACIONES.ParameterName = "p_DIAS_VACACIONES";
                        p_DIASVACACIONES.Value = pSeguridadSocial.diasvacaciones;
                        p_DIASVACACIONES.Direction = ParameterDirection.Input;
                        p_DIASVACACIONES.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_DIASVACACIONES);


                        DbParameter p_DIASPRIMA = cmdTransaccionFactory.CreateParameter();
                        p_DIASPRIMA.ParameterName = "p_DIAS_PRIMA";
                        p_DIASPRIMA.Value = pSeguridadSocial.diasminimoprima;
                        p_DIASPRIMA.Direction = ParameterDirection.Input;
                        p_DIASPRIMA.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_DIASPRIMA);



                        DbParameter paprobador = cmdTransaccionFactory.CreateParameter();
                        paprobador.ParameterName = "p_Aprobador";
                        paprobador.Value = pSeguridadSocial.aprobador;
                        paprobador.Direction = ParameterDirection.Input;
                        paprobador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(paprobador);

                        DbParameter previsor = cmdTransaccionFactory.CreateParameter();
                        previsor.ParameterName = "p_Revisor";
                        previsor.Value = pSeguridadSocial.revisor;
                        previsor.Direction = ParameterDirection.Input;
                        previsor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(previsor);



                        DbParameter pPorcentajesalariointegral = cmdTransaccionFactory.CreateParameter();
                        pPorcentajesalariointegral.ParameterName = "p_POR_SALARIO_INTEGRAL";
                        pPorcentajesalariointegral.Value = pSeguridadSocial.PORCENTAJE_SALARIO_INTEGRAL;
                        pPorcentajesalariointegral.Direction = ParameterDirection.Input;
                        pPorcentajesalariointegral.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajesalariointegral);

                        DbParameter p_PORCRETENCION = cmdTransaccionFactory.CreateParameter();
                        p_PORCRETENCION.ParameterName = "p_PORC_RETENCION";
                        p_PORCRETENCION.Value = pSeguridadSocial.porcentaje_retencion;
                        p_PORCRETENCION.Direction = ParameterDirection.Input;
                        p_PORCRETENCION.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_PORCRETENCION);


                        DbParameter p_CANTIDADSALARETENCION= cmdTransaccionFactory.CreateParameter();
                        p_CANTIDADSALARETENCION.ParameterName = "p_CANTIDADSALARETENCION";
                        p_CANTIDADSALARETENCION.Value = pSeguridadSocial.cantidadsalretencion;
                        p_CANTIDADSALARETENCION.Direction = ParameterDirection.Input;
                        p_CANTIDADSALARETENCION.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_CANTIDADSALARETENCION);

                        DbParameter p_RegimenTEspecial = cmdTransaccionFactory.CreateParameter();
                        p_RegimenTEspecial.ParameterName = "p_Regimen_T_Especial";
                        p_RegimenTEspecial.Value = pSeguridadSocial.RegimenTEspecial;
                        p_RegimenTEspecial.Direction = ParameterDirection.Input;
                        p_RegimenTEspecial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_RegimenTEspecial);


                        DbParameter pContribuyente = cmdTransaccionFactory.CreateParameter();
                        pContribuyente.ParameterName = "p_Contribuyente";
                        pContribuyente.Value = pSeguridadSocial.Contribuyente;
                        pContribuyente.Direction = ParameterDirection.Input;
                        pContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pContribuyente);



                        DbParameter P_BASE_REGIMENTRIBESPECIAL = cmdTransaccionFactory.CreateParameter();
                        P_BASE_REGIMENTRIBESPECIAL.ParameterName = "P_BASEREGIMENTRIBESPECIAL";
                        P_BASE_REGIMENTRIBESPECIAL.Value = pSeguridadSocial.baseRTE;
                        P_BASE_REGIMENTRIBESPECIAL.Direction = ParameterDirection.Input;
                        P_BASE_REGIMENTRIBESPECIAL.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_BASE_REGIMENTRIBESPECIAL);

                        DbParameter pSaludContribuyente = cmdTransaccionFactory.CreateParameter();
                        pSaludContribuyente.ParameterName = "pSalud_Contribuyente";
                        pSaludContribuyente.Value = pSeguridadSocial.SaludContribuyente;
                        pSaludContribuyente.Direction = ParameterDirection.Input;
                        pSaludContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pSaludContribuyente);

                        DbParameter pSenaContribuyente = cmdTransaccionFactory.CreateParameter();
                        pSenaContribuyente.ParameterName = "pSena_Contribuyente";
                        pSenaContribuyente.Value = pSeguridadSocial.SenaContribuyente;
                        pSenaContribuyente.Direction = ParameterDirection.Input;
                        pSenaContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pSenaContribuyente);

                        DbParameter pIcbfContribuyente = cmdTransaccionFactory.CreateParameter();
                        pIcbfContribuyente.ParameterName = "pIcbf_Contribuyente";
                        pIcbfContribuyente.Value = pSeguridadSocial.icbfContribuyente;
                        pIcbfContribuyente.Direction = ParameterDirection.Input;
                        pIcbfContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIcbfContribuyente);

                        DbParameter pccfContribuyente = cmdTransaccionFactory.CreateParameter();
                        pccfContribuyente.ParameterName = "pccf_Contribuyente";
                        pccfContribuyente.Value = pSeguridadSocial.ccfContribuyente;
                        pccfContribuyente.Direction = ParameterDirection.Input;
                        pccfContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pccfContribuyente);

                        DbParameter pManejaAproximacion = cmdTransaccionFactory.CreateParameter();
                        pManejaAproximacion.ParameterName = "p_ManejaAproximacion";
                        pManejaAproximacion.Value = pSeguridadSocial.ManejaAproximacion;
                        pManejaAproximacion.Direction = ParameterDirection.Input;
                        pManejaAproximacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pManejaAproximacion);

                        DbParameter pAproxCentesima = cmdTransaccionFactory.CreateParameter();
                        pAproxCentesima.ParameterName = "p_Aprox_Centesima";
                        pAproxCentesima.Value = pSeguridadSocial.AproxCentesima;
                        pAproxCentesima.Direction = ParameterDirection.Input;
                        pAproxCentesima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pAproxCentesima);

                        DbParameter pAproxMilesima = cmdTransaccionFactory.CreateParameter();
                        pAproxMilesima.ParameterName = "p_Aprox_Milesima";
                        pAproxMilesima.Value = pSeguridadSocial.AproxMilesima;
                        pAproxMilesima.Direction = ParameterDirection.Input;
                        pAproxMilesima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pAproxMilesima);


                        DbParameter pAprox50mascercano = cmdTransaccionFactory.CreateParameter();
                        pAprox50mascercano.ParameterName = "P_APROX_APROX_50CERCANO";
                        pAprox50mascercano.Value = pSeguridadSocial.Aprox50mascercano;
                        pAprox50mascercano.Direction = ParameterDirection.Input;
                        pAprox50mascercano.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pAprox50mascercano);


                        DbParameter p_DIASINCAPACIDADES = cmdTransaccionFactory.CreateParameter();
                        p_DIASINCAPACIDADES.ParameterName = "p_DIAS_INCAPACIDADES";
                        p_DIASINCAPACIDADES.Value = pSeguridadSocial.diasincapacidades;
                        p_DIASINCAPACIDADES.Direction = ParameterDirection.Input;
                        p_DIASINCAPACIDADES.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_DIASINCAPACIDADES);


                        DbParameter p_Tercero = cmdTransaccionFactory.CreateParameter();
                        p_Tercero.ParameterName = "P_TERCERO";
                        p_Tercero.Value = pSeguridadSocial.Tercero;
                        p_Tercero.Direction = ParameterDirection.Input;
                        p_Tercero.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_Tercero);


                        DbParameter p_Novvacaciones = cmdTransaccionFactory.CreateParameter();
                        p_Novvacaciones.ParameterName = "p_NOVVACACIONES";
                        p_Novvacaciones.Value = pSeguridadSocial.novvacaciones;
                        p_Novvacaciones.Direction = ParameterDirection.Input;
                        p_Novvacaciones.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_Novvacaciones);



                        DbParameter p_VACACIONES_ANT = cmdTransaccionFactory.CreateParameter();
                        p_VACACIONES_ANT.ParameterName = "p_VACACIONES_ANT";
                        p_VACACIONES_ANT.Value = pSeguridadSocial.vacacionesanticipadas;
                        p_VACACIONES_ANT.Direction = ParameterDirection.Input;
                        p_VACACIONES_ANT.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_VACACIONES_ANT);



                        DbParameter p_uvt = cmdTransaccionFactory.CreateParameter();
                        p_uvt.ParameterName = "p_uvt";
                        p_uvt.Value = pSeguridadSocial.uvt;
                        p_uvt.Direction = ParameterDirection.Input;
                        p_uvt.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_uvt);

                        DbParameter p_BASEMAXIMA = cmdTransaccionFactory.CreateParameter();
                        p_BASEMAXIMA.ParameterName = "p_BASEMAXIMA";
                        p_BASEMAXIMA.Value = pSeguridadSocial.basemax;
                        p_BASEMAXIMA.Direction = ParameterDirection.Input;
                        p_BASEMAXIMA.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_BASEMAXIMA);



                        DbParameter P_RETROACTIVO = cmdTransaccionFactory.CreateParameter();
                        P_RETROACTIVO.ParameterName = "P_RETROACTIVO";
                        P_RETROACTIVO.Value = pSeguridadSocial.retroactivo;
                        P_RETROACTIVO.Direction = ParameterDirection.Input;
                        P_RETROACTIVO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_RETROACTIVO);



                        DbParameter P_BANCO_GIRO = cmdTransaccionFactory.CreateParameter();
                        P_BANCO_GIRO.ParameterName = "P_BANCO_GIRO";
                        P_BANCO_GIRO.Value = pSeguridadSocial.codigobanco;
                        P_BANCO_GIRO.Direction = ParameterDirection.Input;
                        P_BANCO_GIRO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_BANCO_GIRO);


                        DbParameter P_CUENTA_GIRO = cmdTransaccionFactory.CreateParameter();
                        P_CUENTA_GIRO.ParameterName = "P_CUENTA_GIRO";
                        P_CUENTA_GIRO.Value = pSeguridadSocial.Cuentabancaria;
                        P_CUENTA_GIRO.Direction = ParameterDirection.Input;
                        P_CUENTA_GIRO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_CUENTA_GIRO);

                        DbParameter P_INCA_SMLV = cmdTransaccionFactory.CreateParameter();
                        P_INCA_SMLV.ParameterName = "P_INCA_SMLV";
                        P_INCA_SMLV.Value = pSeguridadSocial.incap_smlv;
                        P_INCA_SMLV.Direction = ParameterDirection.Input;
                        P_INCA_SMLV.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_INCA_SMLV);


                        DbParameter P_FORMATO_DESPRENDIBLE = cmdTransaccionFactory.CreateParameter();
                        P_FORMATO_DESPRENDIBLE.ParameterName = "P_FORMATO_DESPRENDIBLE";
                        P_FORMATO_DESPRENDIBLE.Value = pSeguridadSocial.formato_desprendible;
                        P_FORMATO_DESPRENDIBLE.Direction = ParameterDirection.Input;
                        P_FORMATO_DESPRENDIBLE.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_FORMATO_DESPRENDIBLE);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_SEGSOCIAL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        pSeguridadSocial.IDSEGURIDAD = pIDSEGURIDAD.Value != DBNull.Value ? Convert.ToInt64(pIDSEGURIDAD.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pSeguridadSocial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguridadSocialData", "CrearSeguridadSocial", ex);
                        return null;
                    }
                }
            }
        }
        public SeguridadSocial ConsultarSeguridadSocial(Usuario vUsuario)
        {
            DbDataReader resultado;
            SeguridadSocial Entidad = new SeguridadSocial();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from NOMINA_SEGURIDAD_SOCIAL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                        
                            if (resultado["IDSEGURIDAD"] != DBNull.Value) Entidad.IDSEGURIDAD = Convert.ToInt32(resultado["IDSEGURIDAD"]);
                            if (resultado["PORCENTAJE_SALUD"] != DBNull.Value) Entidad.PORCENTAJE_SALUD = Convert.ToDecimal(resultado["PORCENTAJE_SALUD"]);
                            if (resultado["PORCENTAJE_PENSION"] != DBNull.Value) Entidad.PORCENTAJE_PENSION = Convert.ToDecimal(resultado["PORCENTAJE_PENSION"]);
                            if (resultado["PORC_EMPLEADOR_SALUD"] != DBNull.Value) Entidad.PORC_EMPLEADOR_SALUD = Convert.ToDecimal(resultado["PORC_EMPLEADOR_SALUD"]);
                            if (resultado["PORC_EMPLEADOR_PENSION"] != DBNull.Value) Entidad.PORC_EMPLEADOR_PENSION = Convert.ToDecimal(resultado["PORC_EMPLEADOR_PENSION"]);
                            if (resultado["PORCENTAJE_INCAPACIDAD"] != DBNull.Value) Entidad.PORCENTAJE_INCAPACIDAD = Convert.ToDecimal(resultado["PORCENTAJE_INCAPACIDAD"]);
                            if (resultado["PERMITE_INCAPACIDAD_TOPE"] != DBNull.Value) Entidad.PERMITE_INCAPACIDAD_TOPE = Convert.ToInt32(resultado["PERMITE_INCAPACIDAD_TOPE"]);
                            if (resultado["DESCONTAR_APORTES"] != DBNull.Value) Entidad.DESCONTAR_APORTES = Convert.ToInt32(resultado["DESCONTAR_APORTES"]);
                            if (resultado["MARCAR_VST"] != DBNull.Value) Entidad.MARCAR_VST = Convert.ToInt32(resultado["MARCAR_VST"]);
                            if (resultado["DESCONTAR_APORTE_EMPL"] != DBNull.Value) Entidad.DESCONTAR_APORTE_EMPL = Convert.ToInt32(resultado["DESCONTAR_APORTE_EMPL"]);
                            if (resultado["INACTIVIDAD_DIAS_CAL"] != DBNull.Value) Entidad.INACTIVIDAD_DIAS_CAL = Convert.ToInt32(resultado["INACTIVIDAD_DIAS_CAL"]);
                            if (resultado["DESCUENTA_DIAS_CASTIGO"] != DBNull.Value) Entidad.DESCUENTA_DIAS_CASTIGO = Convert.ToInt32(resultado["DESCUENTA_DIAS_CASTIGO"]);
                            if (resultado["NIT_ARCHIVO"] != DBNull.Value) Entidad.NIT_ARCHIVO = Convert.ToInt32(resultado["NIT_ARCHIVO"]);
                            if (resultado["BASE_INACTIVIDAD_DIAS"] != DBNull.Value) Entidad.BASE_INACTIVIDAD_DIAS= Convert.ToInt32(resultado["BASE_INACTIVIDAD_DIAS"]);
                            if (resultado["PROCEDIMIENTO_CENTRO_ARP"] != DBNull.Value) Entidad.PROCEDIMIENTO_CENTRO_ARP= Convert.ToInt32(resultado["PROCEDIMIENTO_CENTRO_ARP"]);
                            if (resultado["IBC_INACTIVIDADES"] != DBNull.Value) Entidad.IBC_INACTIVIDADES = Convert.ToInt32(resultado["IBC_INACTIVIDADES"]);
                            if (resultado["CALCULO_PRIMDIAS"] != DBNull.Value) Entidad.CALCULO_PRIMDIAS = Convert.ToInt32(resultado["CALCULO_PRIMDIAS"]);
                            if (resultado["SALPEN_VACACIONES"] != DBNull.Value) Entidad.SALPEN_VACACIONES = Convert.ToInt32(resultado["SALPEN_VACACIONES"]);
                            if (resultado["PORCENTAJE_SALUD_PENSIONADO"] != DBNull.Value) Entidad.PORCENTAJE_SALUD_PENSIONADO = Convert.ToInt32(resultado["PORCENTAJE_SALUD_PENSIONADO"]);
                            if (resultado["PERIODOS_MAX_VACAC"] != DBNull.Value) Entidad.PERIODOS_MAXIMOS_VACACIONES = Convert.ToInt32(resultado["PERIODOS_MAX_VACAC"]);
                            if (resultado["MAXSALARIOSARL"] != DBNull.Value) Entidad.MAXSALARIOSARL = Convert.ToInt32(resultado["MAXSALARIOSARL"]);
                            if (resultado["MAXSALARIOSPARAFISCALES"] != DBNull.Value) Entidad.MAXSALARIOSPARAFISCALES = Convert.ToInt32(resultado["MAXSALARIOSPARAFISCALES"]);
                            if (resultado["MAXSALARIOSSALUDPENSION"] != DBNull.Value) Entidad.MAXSALARIOSSALUDPENSION = Convert.ToInt32(resultado["MAXSALARIOSSALUDPENSION"]);
                            if (resultado["POR_CAJA_COMPENSACION"] != DBNull.Value) Entidad.CajaCompensacion = Convert.ToDecimal(resultado["POR_CAJA_COMPENSACION"]);
                            if (resultado["POR_SENA"] != DBNull.Value) Entidad.sena = Convert.ToDecimal(resultado["POR_SENA"]);
                            if (resultado["POR_ICBF"] != DBNull.Value) Entidad.icbf = Convert.ToDecimal(resultado["POR_ICBF"]);
                            if (resultado["POR_PRIMA"] != DBNull.Value) Entidad.prima = Convert.ToDecimal(resultado["POR_PRIMA"]);
                            if (resultado["POR_CESANTIAS"] != DBNull.Value) Entidad.cesantias = Convert.ToDecimal(resultado["POR_CESANTIAS"]);
                            if (resultado["POR_INT_CESANTIAS"] != DBNull.Value) Entidad.interescesantias = Convert.ToDecimal(resultado["POR_INT_CESANTIAS"]);
                            if (resultado["POR_VACACIONES"] != DBNull.Value) Entidad.vacaciones = Convert.ToDecimal(resultado["POR_VACACIONES"]);
                            if (resultado["DIAS_VACACIONES"] != DBNull.Value) Entidad.diasvacaciones = Convert.ToInt16(resultado["DIAS_VACACIONES"]);
                            if (resultado["DIAS_MIN_PRIMA"] != DBNull.Value) Entidad.diasminimoprima = Convert.ToInt16(resultado["DIAS_MIN_PRIMA"]);
                            if (resultado["APROBADOR_PLANILLA"] != DBNull.Value) Entidad.aprobador = Convert.ToString(resultado["APROBADOR_PLANILLA"]);
                            if (resultado["REVISOR_PLANILLA"] != DBNull.Value) Entidad.revisor = Convert.ToString(resultado["REVISOR_PLANILLA"]);
                            if (resultado["POR_SALARIO_INTEGRAL"] != DBNull.Value) Entidad.PORCENTAJE_SALARIO_INTEGRAL = Convert.ToDecimal(resultado["POR_SALARIO_INTEGRAL"]);
                            if (resultado["POR_RETENCION"] != DBNull.Value) Entidad.porcentaje_retencion = Convert.ToDecimal(resultado["POR_RETENCION"]);
                            if (resultado["CANTIDADSALRETENCION"] != DBNull.Value) Entidad.cantidadsalretencion = Convert.ToInt16(resultado["CANTIDADSALRETENCION"]);
                            if (resultado["REGIMEN_TRIB_ESPECIAL"] != DBNull.Value) Entidad.RegimenTEspecial = Convert.ToInt32(resultado["REGIMEN_TRIB_ESPECIAL"]);
                            if (resultado["CONTRIBUYENTE"] != DBNull.Value) Entidad.Contribuyente = Convert.ToInt32(resultado["CONTRIBUYENTE"]);
                            if (resultado["BASEREG_TRIB_ESPECIAL"] != DBNull.Value) Entidad.baseRTE = Convert.ToDecimal(resultado["BASEREG_TRIB_ESPECIAL"]);

                            if (resultado["SALUDCONTRIBUYENTE"] != DBNull.Value) Entidad.SaludContribuyente = Convert.ToInt32(resultado["SALUDCONTRIBUYENTE"]);
                            if (resultado["SENACONTRIBUYENTE"] != DBNull.Value) Entidad.SenaContribuyente = Convert.ToInt32(resultado["SENACONTRIBUYENTE"]);
                            if (resultado["ICBFCONTRIBUYENTE"] != DBNull.Value) Entidad.icbfContribuyente = Convert.ToInt32(resultado["ICBFCONTRIBUYENTE"]);
                            if (resultado["CCFCONTRIBUYENTE"] != DBNull.Value) Entidad.ccfContribuyente = Convert.ToInt32(resultado["CCFCONTRIBUYENTE"]);
                            if (resultado["MANEJA_APROXIMACION"] != DBNull.Value) Entidad.ManejaAproximacion = Convert.ToInt32(resultado["MANEJA_APROXIMACION"]);

                            if (resultado["APROX_CENTESIMA"] != DBNull.Value) Entidad.AproxCentesima = Convert.ToInt32(resultado["APROX_CENTESIMA"]);
                            if (resultado["APROX_MILESIMA"] != DBNull.Value) Entidad.AproxMilesima = Convert.ToInt32(resultado["APROX_MILESIMA"]);
                            if (resultado["DIAS_INCAPACIDADES"] != DBNull.Value) Entidad.diasincapacidades = Convert.ToInt16(resultado["DIAS_INCAPACIDADES"]);
                            if (resultado["APROX_50CERCANO"] != DBNull.Value) Entidad.Aprox50mascercano = Convert.ToInt32(resultado["APROX_50CERCANO"]);
                            if (resultado["TERCERO"] != DBNull.Value) Entidad.Tercero = Convert.ToInt32(resultado["TERCERO"]);
                            if (resultado["NOV_VACACIONES"] != DBNull.Value) Entidad.novvacaciones = Convert.ToInt32(resultado["NOV_VACACIONES"]);
                            if (resultado["VACACIONES_ANTICIPADAS"] != DBNull.Value) Entidad.vacacionesanticipadas = Convert.ToInt32(resultado["VACACIONES_ANTICIPADAS"]);
                            if (resultado["uvt"] != DBNull.Value) Entidad.uvt = Convert.ToDecimal(resultado["uvt"]);
                            if (resultado["BASE_MAXIMA"] != DBNull.Value) Entidad.basemax = Convert.ToInt32(resultado["BASE_MAXIMA"]);
                            if (resultado["RECONOCE_RETROACTIVO"] != DBNull.Value) Entidad.retroactivo = Convert.ToInt32(resultado["RECONOCE_RETROACTIVO"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) Entidad.codigobanco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) Entidad.Cuentabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["INCAP_SLMV"] != DBNull.Value) Entidad.incap_smlv = Convert.ToInt32(resultado["INCAP_SLMV"]);
                            if (resultado["FORMATO_DESPRENDIBLE"] != DBNull.Value) Entidad.formato_desprendible = Convert.ToInt32(resultado["FORMATO_DESPRENDIBLE"]);


                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguridadSocialData", "ConsultarSeguridadSocial", ex);
                        return null;
                    }
                }
            }

                    
        }
        public SeguridadSocial ModificarSeguridadSocial(SeguridadSocial pSeguridadSocial, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSEGURIDAD = cmdTransaccionFactory.CreateParameter();
                        pIDSEGURIDAD.ParameterName = "p_IDSEGURIDAD";
                        pIDSEGURIDAD.Value = pSeguridadSocial.IDSEGURIDAD;
                        pIDSEGURIDAD.Direction = ParameterDirection.Input;
                        pIDSEGURIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIDSEGURIDAD);

                        DbParameter pPorcentajeSalud = cmdTransaccionFactory.CreateParameter();
                        pPorcentajeSalud.ParameterName = "p_PORCENTAJE_SALUD";
                        pPorcentajeSalud.Value = pSeguridadSocial.PORCENTAJE_SALUD;
                        pPorcentajeSalud.Direction = ParameterDirection.Input;
                        pPorcentajeSalud.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajeSalud);

                        DbParameter pPorcentajePension = cmdTransaccionFactory.CreateParameter();
                        pPorcentajePension.ParameterName = "p_PORCENTAJE_PENSION";
                        pPorcentajePension.Value = pSeguridadSocial.PORCENTAJE_PENSION;
                        pPorcentajePension.Direction = ParameterDirection.Input;
                        pPorcentajePension.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajePension);

                        DbParameter pPorcEmpladorSalud = cmdTransaccionFactory.CreateParameter();
                        pPorcEmpladorSalud.ParameterName = "p_PORC_EMPLEADOR_SALUD";
                        pPorcEmpladorSalud.Value = pSeguridadSocial.PORC_EMPLEADOR_SALUD;
                        pPorcEmpladorSalud.Direction = ParameterDirection.Input;
                        pPorcEmpladorSalud.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcEmpladorSalud);

                        DbParameter pPorcEmpladorPension = cmdTransaccionFactory.CreateParameter();
                        pPorcEmpladorPension.ParameterName = "p_PORC_EMPLEADOR_PENSION";
                        pPorcEmpladorPension.Value = pSeguridadSocial.PORC_EMPLEADOR_PENSION;
                        pPorcEmpladorPension.Direction = ParameterDirection.Input;
                        pPorcEmpladorPension.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcEmpladorPension);

                        DbParameter pPorcentajeIncapacidad = cmdTransaccionFactory.CreateParameter();
                        pPorcentajeIncapacidad.ParameterName = "p_PORCENTAJE_INCAPACIDAD";
                        pPorcentajeIncapacidad.Value = pSeguridadSocial.PORCENTAJE_INCAPACIDAD;
                        pPorcentajeIncapacidad.Direction = ParameterDirection.Input;
                        pPorcentajeIncapacidad.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajeIncapacidad);

                        DbParameter pPermIncapacidadTope = cmdTransaccionFactory.CreateParameter();
                        pPermIncapacidadTope.ParameterName = "p_PERMITE_INCAPACIDAD_TOPE";
                        pPermIncapacidadTope.Value = pSeguridadSocial.PERMITE_INCAPACIDAD_TOPE;
                        pPermIncapacidadTope.Direction = ParameterDirection.Input;
                        pPermIncapacidadTope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pPermIncapacidadTope);

                        DbParameter pDescontarAportes = cmdTransaccionFactory.CreateParameter();
                        pDescontarAportes.ParameterName = "p_DESCONTAR_APORTES";
                        pDescontarAportes.Value = pSeguridadSocial.DESCONTAR_APORTES;
                        pDescontarAportes.Direction = ParameterDirection.Input;
                        pDescontarAportes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDescontarAportes);

                        DbParameter pMarcarVST = cmdTransaccionFactory.CreateParameter();
                        pMarcarVST.ParameterName = "p_MARCAR_VST";
                        pMarcarVST.Value = pSeguridadSocial.MARCAR_VST;
                        pMarcarVST.Direction = ParameterDirection.Input;
                        pMarcarVST.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pMarcarVST);

                        DbParameter pDescontarAporEmpl = cmdTransaccionFactory.CreateParameter();
                        pDescontarAporEmpl.ParameterName = "p_DESCONTAR_APORTE_EMPL";
                        pDescontarAporEmpl.Value = pSeguridadSocial.DESCONTAR_APORTE_EMPL;
                        pDescontarAporEmpl.Direction = ParameterDirection.Input;
                        pDescontarAporEmpl.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDescontarAporEmpl);

                        DbParameter pInactDiasCal = cmdTransaccionFactory.CreateParameter();
                        pInactDiasCal.ParameterName = "p_INACTIVIDAD_DIAS_CAL";
                        pInactDiasCal.Value = pSeguridadSocial.INACTIVIDAD_DIAS_CAL;
                        pInactDiasCal.Direction = ParameterDirection.Input;
                        pInactDiasCal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pInactDiasCal);

                        DbParameter pDescDiasCastigo = cmdTransaccionFactory.CreateParameter();
                        pDescDiasCastigo.ParameterName = "p_DESCUENTA_DIAS_CASTIGO";
                        pDescDiasCastigo.Value = pSeguridadSocial.DESCUENTA_DIAS_CASTIGO;
                        pDescDiasCastigo.Direction = ParameterDirection.Input;
                        pDescDiasCastigo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDescDiasCastigo);

                        DbParameter pNitArchivo = cmdTransaccionFactory.CreateParameter();
                        pNitArchivo.ParameterName = "p_NIT_ARCHIVO";
                        pNitArchivo.Value = pSeguridadSocial.NIT_ARCHIVO;
                        pNitArchivo.Direction = ParameterDirection.Input;
                        pNitArchivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pNitArchivo);

                        DbParameter pBaseInactDias = cmdTransaccionFactory.CreateParameter();
                        pBaseInactDias.ParameterName = "p_BASE_INACTIVIDAD_DIAS";
                        pBaseInactDias.Value = pSeguridadSocial.BASE_INACTIVIDAD_DIAS;
                        pBaseInactDias.Direction = ParameterDirection.Input;
                        pBaseInactDias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pBaseInactDias);

                        DbParameter pProcediminCentroARP = cmdTransaccionFactory.CreateParameter();
                        pProcediminCentroARP.ParameterName = "p_PROCEDIMIENTO_CENTRO_ARP";
                        pProcediminCentroARP.Value = pSeguridadSocial.PROCEDIMIENTO_CENTRO_ARP;
                        pProcediminCentroARP.Direction = ParameterDirection.Input;
                        pProcediminCentroARP.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pProcediminCentroARP);

                        DbParameter pIBCinact = cmdTransaccionFactory.CreateParameter();
                        pIBCinact.ParameterName = "p_IBC_INACTIVIDADES";
                        pIBCinact.Value = pSeguridadSocial.IBC_INACTIVIDADES;
                        pIBCinact.Direction = ParameterDirection.Input;
                        pIBCinact.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIBCinact);

                        DbParameter pCalPrimdias = cmdTransaccionFactory.CreateParameter();
                        pCalPrimdias.ParameterName = "p_CALCULO_PRIMDIAS";
                        pCalPrimdias.Value = pSeguridadSocial.CALCULO_PRIMDIAS;
                        pCalPrimdias.Direction = ParameterDirection.Input;
                        pCalPrimdias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCalPrimdias);

                        DbParameter pSalpenVacaciones = cmdTransaccionFactory.CreateParameter();
                        pSalpenVacaciones.ParameterName = "p_SALPEN_VACACIONES";
                        pSalpenVacaciones.Value = pSeguridadSocial.SALPEN_VACACIONES;
                        pSalpenVacaciones.Direction = ParameterDirection.Input;
                        pSalpenVacaciones.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pSalpenVacaciones);


                        DbParameter pPORCENTAJE_SALUD_PENSIONADO = cmdTransaccionFactory.CreateParameter();
                        pPORCENTAJE_SALUD_PENSIONADO.ParameterName = "p_PORCENTAJE_SALUD_PENSIONADO";
                        pPORCENTAJE_SALUD_PENSIONADO.Value = pSeguridadSocial.PORCENTAJE_SALUD_PENSIONADO;
                        pPORCENTAJE_SALUD_PENSIONADO.Direction = ParameterDirection.Input;
                        pPORCENTAJE_SALUD_PENSIONADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pPORCENTAJE_SALUD_PENSIONADO);



                        DbParameter pPERIODOS_MAX_LEY_VACACIONES = cmdTransaccionFactory.CreateParameter();
                        pPERIODOS_MAX_LEY_VACACIONES.ParameterName = "p_PERIODOS_MAX_LEY_VACACIONES";
                        pPERIODOS_MAX_LEY_VACACIONES.Value = pSeguridadSocial.PERIODOS_MAXIMOS_VACACIONES;
                        pPERIODOS_MAX_LEY_VACACIONES.Direction = ParameterDirection.Input;
                        pPERIODOS_MAX_LEY_VACACIONES.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pPERIODOS_MAX_LEY_VACACIONES);


                        DbParameter PMAXSALARIOSARL = cmdTransaccionFactory.CreateParameter();
                        PMAXSALARIOSARL.ParameterName = "p_MAXSALARIOSARL";
                        PMAXSALARIOSARL.Value = pSeguridadSocial.MAXSALARIOSARL;
                        PMAXSALARIOSARL.Direction = ParameterDirection.Input;
                        PMAXSALARIOSARL.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PMAXSALARIOSARL);

                        DbParameter pMAXSALARIOSPARAFISCALES = cmdTransaccionFactory.CreateParameter();
                        pMAXSALARIOSPARAFISCALES.ParameterName = "p_MAXSALARIOSPARAFISCALES";
                        pMAXSALARIOSPARAFISCALES.Value = pSeguridadSocial.MAXSALARIOSPARAFISCALES;
                        pMAXSALARIOSPARAFISCALES.Direction = ParameterDirection.Input;
                        pMAXSALARIOSPARAFISCALES.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pMAXSALARIOSPARAFISCALES);

                        DbParameter pMAXSALARIOSSALUDPENSION = cmdTransaccionFactory.CreateParameter();
                        pMAXSALARIOSSALUDPENSION.ParameterName = "p_MAXSALARIOSSALUDPENSION";
                        pMAXSALARIOSSALUDPENSION.Value = pSeguridadSocial.MAXSALARIOSSALUDPENSION;
                        pMAXSALARIOSSALUDPENSION.Direction = ParameterDirection.Input;
                        pMAXSALARIOSSALUDPENSION.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pMAXSALARIOSSALUDPENSION);


                        DbParameter pPorCajaCompensacion = cmdTransaccionFactory.CreateParameter();
                        pPorCajaCompensacion.ParameterName = "p_PORC_CAJA_COMPENSACION";
                        pPorCajaCompensacion.Value = pSeguridadSocial.CajaCompensacion;
                        pPorCajaCompensacion.Direction = ParameterDirection.Input;
                        pPorCajaCompensacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorCajaCompensacion);


                        DbParameter pPorSena = cmdTransaccionFactory.CreateParameter();
                        pPorSena.ParameterName = "p_PORC_SENA";
                        pPorSena.Value = pSeguridadSocial.sena;
                        pPorSena.Direction = ParameterDirection.Input;
                        pPorSena.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorSena);



                        DbParameter pPorIcbf = cmdTransaccionFactory.CreateParameter();
                        pPorIcbf.ParameterName = "p_PORC_ICBF";
                        pPorIcbf.Value = pSeguridadSocial.icbf;
                        pPorIcbf.Direction = ParameterDirection.Input;
                        pPorIcbf.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorIcbf);

                        DbParameter pPorPrima = cmdTransaccionFactory.CreateParameter();
                        pPorPrima.ParameterName = "p_PORC_PRIMA";
                        pPorPrima.Value = pSeguridadSocial.prima;
                        pPorPrima.Direction = ParameterDirection.Input;
                        pPorPrima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorPrima);

                        DbParameter pPorCesantias = cmdTransaccionFactory.CreateParameter();
                        pPorCesantias.ParameterName = "p_PORC_CESANTIAS";
                        pPorCesantias.Value = pSeguridadSocial.cesantias;
                        pPorCesantias.Direction = ParameterDirection.Input;
                        pPorCesantias.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorCesantias);

                        DbParameter pPorIntCesantias = cmdTransaccionFactory.CreateParameter();
                        pPorIntCesantias.ParameterName = "p_PORC_INT_CESANTIAS";
                        pPorIntCesantias.Value = pSeguridadSocial.interescesantias;
                        pPorIntCesantias.Direction = ParameterDirection.Input;
                        pPorIntCesantias.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorIntCesantias);



                        DbParameter pPorvacaciones = cmdTransaccionFactory.CreateParameter();
                        pPorvacaciones.ParameterName = "p_PORC_VACACIONES";
                        pPorvacaciones.Value = pSeguridadSocial.vacaciones;
                        pPorvacaciones.Direction = ParameterDirection.Input;
                        pPorvacaciones.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorvacaciones);





                        DbParameter p_DIASVACACIONES = cmdTransaccionFactory.CreateParameter();
                        p_DIASVACACIONES.ParameterName = "p_DIAS_VACACIONES";
                        p_DIASVACACIONES.Value = pSeguridadSocial.diasvacaciones;
                        p_DIASVACACIONES.Direction = ParameterDirection.Input;
                        p_DIASVACACIONES.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_DIASVACACIONES);

                        DbParameter p_DIASPRIMA = cmdTransaccionFactory.CreateParameter();
                        p_DIASPRIMA.ParameterName = "p_DIAS_PRIMA";
                        p_DIASPRIMA.Value = pSeguridadSocial.diasminimoprima;
                        p_DIASPRIMA.Direction = ParameterDirection.Input;
                        p_DIASPRIMA.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_DIASPRIMA);



                        DbParameter paprobador = cmdTransaccionFactory.CreateParameter();
                        paprobador.ParameterName = "p_APROBADOR";
                        paprobador.Value = pSeguridadSocial.aprobador.ToUpper();
                        paprobador.Direction = ParameterDirection.Input;
                        paprobador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(paprobador);

                        DbParameter previsor = cmdTransaccionFactory.CreateParameter();
                        previsor.ParameterName = "p_REVISOR";
                        previsor.Value = pSeguridadSocial.revisor.ToUpper();
                        previsor.Direction = ParameterDirection.Input;
                        previsor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(previsor);




                        DbParameter pPorcentajesalariointegral = cmdTransaccionFactory.CreateParameter();
                        pPorcentajesalariointegral.ParameterName = "p_POR_SALARIO_INTEGRAL";
                        pPorcentajesalariointegral.Value = pSeguridadSocial.PORCENTAJE_SALARIO_INTEGRAL;
                        pPorcentajesalariointegral.Direction = ParameterDirection.Input;
                        pPorcentajesalariointegral.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pPorcentajesalariointegral);

                        DbParameter p_PORCRETENCION = cmdTransaccionFactory.CreateParameter();
                        p_PORCRETENCION.ParameterName = "p_PORC_RETENCION";
                        p_PORCRETENCION.Value = pSeguridadSocial.porcentaje_retencion;
                        p_PORCRETENCION.Direction = ParameterDirection.Input;
                        p_PORCRETENCION.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_PORCRETENCION);

                        DbParameter p_CANTIDADSALARETENCION = cmdTransaccionFactory.CreateParameter();
                        p_CANTIDADSALARETENCION.ParameterName = "p_CANTIDADSALARETENCION";
                        p_CANTIDADSALARETENCION.Value = pSeguridadSocial.cantidadsalretencion;
                        p_CANTIDADSALARETENCION.Direction = ParameterDirection.Input;
                        p_CANTIDADSALARETENCION.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_CANTIDADSALARETENCION);




                        DbParameter p_RegimenTEspecial = cmdTransaccionFactory.CreateParameter();
                        p_RegimenTEspecial.ParameterName = "p_Regimen_T_Especial";
                        p_RegimenTEspecial.Value = pSeguridadSocial.RegimenTEspecial;
                        p_RegimenTEspecial.Direction = ParameterDirection.Input;
                        p_RegimenTEspecial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_RegimenTEspecial);


                        DbParameter pContribuyente = cmdTransaccionFactory.CreateParameter();
                        pContribuyente.ParameterName = "p_Contribuyente";
                        pContribuyente.Value = pSeguridadSocial.Contribuyente;
                        pContribuyente.Direction = ParameterDirection.Input;
                        pContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pContribuyente);


                        DbParameter P_BASE_REGIMENTRIBESPECIAL = cmdTransaccionFactory.CreateParameter();
                        P_BASE_REGIMENTRIBESPECIAL.ParameterName = "P_BASEREGIMENTRIBESPECIAL";
                        P_BASE_REGIMENTRIBESPECIAL.Value = pSeguridadSocial.baseRTE;
                        P_BASE_REGIMENTRIBESPECIAL.Direction = ParameterDirection.Input;
                        P_BASE_REGIMENTRIBESPECIAL.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_BASE_REGIMENTRIBESPECIAL);

                        DbParameter pSaludContribuyente = cmdTransaccionFactory.CreateParameter();
                        pSaludContribuyente.ParameterName = "pSalud_Contribuyente";
                        pSaludContribuyente.Value = pSeguridadSocial.SaludContribuyente;
                        pSaludContribuyente.Direction = ParameterDirection.Input;
                        pSaludContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pSaludContribuyente);

                        DbParameter pSenaContribuyente = cmdTransaccionFactory.CreateParameter();
                        pSenaContribuyente.ParameterName = "pSena_Contribuyente";
                        pSenaContribuyente.Value = pSeguridadSocial.SenaContribuyente;
                        pSenaContribuyente.Direction = ParameterDirection.Input;
                        pSenaContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pSenaContribuyente);

                        DbParameter pIcbfContribuyente = cmdTransaccionFactory.CreateParameter();
                        pIcbfContribuyente.ParameterName = "pIcbf_Contribuyente";
                        pIcbfContribuyente.Value = pSeguridadSocial.icbfContribuyente;
                        pIcbfContribuyente.Direction = ParameterDirection.Input;
                        pIcbfContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIcbfContribuyente);

                        DbParameter pccfContribuyente = cmdTransaccionFactory.CreateParameter();
                        pccfContribuyente.ParameterName = "pccf_Contribuyente";
                        pccfContribuyente.Value = pSeguridadSocial.ccfContribuyente;
                        pccfContribuyente.Direction = ParameterDirection.Input;
                        pccfContribuyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pccfContribuyente);



                        
                        DbParameter pManejaAproximacion = cmdTransaccionFactory.CreateParameter();
                        pManejaAproximacion.ParameterName = "p_ManejaAproximacion";
                        pManejaAproximacion.Value = pSeguridadSocial.ManejaAproximacion;
                        pManejaAproximacion.Direction = ParameterDirection.Input;
                        pManejaAproximacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pManejaAproximacion);

                        DbParameter pAproxCentesima = cmdTransaccionFactory.CreateParameter();
                        pAproxCentesima.ParameterName = "p_Aprox_Centesima";
                        pAproxCentesima.Value = pSeguridadSocial.AproxCentesima;
                        pAproxCentesima.Direction = ParameterDirection.Input;
                        pAproxCentesima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pAproxCentesima);

                        DbParameter pAproxMilesima = cmdTransaccionFactory.CreateParameter();
                        pAproxMilesima.ParameterName = "p_Aprox_Milesima";
                        pAproxMilesima.Value = pSeguridadSocial.AproxMilesima;
                        pAproxMilesima.Direction = ParameterDirection.Input;
                        pAproxMilesima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pAproxMilesima);


                        DbParameter pAprox50mascercano = cmdTransaccionFactory.CreateParameter();
                        pAprox50mascercano.ParameterName = "P_APROX_APROX_50CERCANO";
                        pAprox50mascercano.Value = pSeguridadSocial.Aprox50mascercano;
                        pAprox50mascercano.Direction = ParameterDirection.Input;
                        pAprox50mascercano.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pAprox50mascercano);



                        DbParameter p_DIASINCAPACIDADES = cmdTransaccionFactory.CreateParameter();
                        p_DIASINCAPACIDADES.ParameterName = "p_DIAS_INCAPACIDADES";
                        p_DIASINCAPACIDADES.Value = pSeguridadSocial.diasincapacidades;
                        p_DIASINCAPACIDADES.Direction = ParameterDirection.Input;
                        p_DIASINCAPACIDADES.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_DIASINCAPACIDADES);




                        DbParameter p_Tercero = cmdTransaccionFactory.CreateParameter();
                        p_Tercero.ParameterName = "P_TERCERO";
                        p_Tercero.Value = pSeguridadSocial.Tercero;
                        p_Tercero.Direction = ParameterDirection.Input;
                        p_Tercero.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_Tercero);


                        DbParameter p_Novvacaciones = cmdTransaccionFactory.CreateParameter();
                        p_Novvacaciones.ParameterName = "p_Novvacaciones";
                        p_Novvacaciones.Value = pSeguridadSocial.novvacaciones;
                        p_Novvacaciones.Direction = ParameterDirection.Input;
                        p_Novvacaciones.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_Novvacaciones);



                        DbParameter p_VACACIONES_ANT = cmdTransaccionFactory.CreateParameter();
                        p_VACACIONES_ANT.ParameterName = "p_VACACIONES_ANT";
                        p_VACACIONES_ANT.Value = pSeguridadSocial.vacacionesanticipadas;
                        p_VACACIONES_ANT.Direction = ParameterDirection.Input;
                        p_VACACIONES_ANT.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_VACACIONES_ANT);



                        DbParameter p_uvt = cmdTransaccionFactory.CreateParameter();
                        p_uvt.ParameterName = "p_uvt";
                        p_uvt.Value = pSeguridadSocial.uvt;
                        p_uvt.Direction = ParameterDirection.Input;
                        p_uvt.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_uvt);


                        DbParameter p_BASEMAXIMA = cmdTransaccionFactory.CreateParameter();
                        p_BASEMAXIMA.ParameterName = "p_BASEMAXIMA";
                        p_BASEMAXIMA.Value = pSeguridadSocial.basemax;
                        p_BASEMAXIMA.Direction = ParameterDirection.Input;
                        p_BASEMAXIMA.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_BASEMAXIMA);


                        DbParameter P_RETROACTIVO = cmdTransaccionFactory.CreateParameter();
                        P_RETROACTIVO.ParameterName = "P_RETROACTIVO";
                        P_RETROACTIVO.Value = pSeguridadSocial.retroactivo;
                        P_RETROACTIVO.Direction = ParameterDirection.Input;
                        P_RETROACTIVO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_RETROACTIVO);


                        DbParameter P_BANCO_GIRO = cmdTransaccionFactory.CreateParameter();
                        P_BANCO_GIRO.ParameterName = "P_BANCO_GIRO";
                        P_BANCO_GIRO.Value = pSeguridadSocial.codigobanco;
                        P_BANCO_GIRO.Direction = ParameterDirection.Input;
                        P_BANCO_GIRO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_BANCO_GIRO);


                        DbParameter P_CUENTA_GIRO = cmdTransaccionFactory.CreateParameter();
                        P_CUENTA_GIRO.ParameterName = "P_CUENTA_GIRO";
                        P_CUENTA_GIRO.Value = pSeguridadSocial.Cuentabancaria;
                        P_CUENTA_GIRO.Direction = ParameterDirection.Input;
                        P_CUENTA_GIRO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_CUENTA_GIRO);


                        DbParameter P_INCA_SMLV = cmdTransaccionFactory.CreateParameter();
                        P_INCA_SMLV.ParameterName = "P_INCA_SMLV";
                        P_INCA_SMLV.Value = pSeguridadSocial.incap_smlv;
                        P_INCA_SMLV.Direction = ParameterDirection.Input;
                        P_INCA_SMLV.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_INCA_SMLV);



                        DbParameter P_FORMATO_DESPRENDIBLE = cmdTransaccionFactory.CreateParameter();
                        P_FORMATO_DESPRENDIBLE.ParameterName = "P_FORMATO_DESPRENDIBLE";
                        P_FORMATO_DESPRENDIBLE.Value = pSeguridadSocial.formato_desprendible;
                        P_FORMATO_DESPRENDIBLE.Direction = ParameterDirection.Input;
                        P_FORMATO_DESPRENDIBLE.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(P_FORMATO_DESPRENDIBLE);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_SEGSOCIAL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        pSeguridadSocial.IDSEGURIDAD = pIDSEGURIDAD.Value != DBNull.Value ? Convert.ToInt64(pIDSEGURIDAD.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pSeguridadSocial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguridadSocialData", "CrearSeguridadSocial", ex);
                        return null;
                    }
                }
            }
        }
    }
}
