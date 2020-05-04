using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using System.Web;


namespace Xpinn.Comun.Business
{
    public class FechasBusiness : GlobalData
    {

        public DateTime SumarMeses(DateTime fecha, int meses)
        {
            int dia = fecha.Day;
            int mes = fecha.Month;
            int año = fecha.Year;
            int i = 1;
            while (i <= meses)
            {
                mes = mes + 1;
                if (mes > 12)
                {
                    mes = 1;
                    año = año + 1;
                }
                i = i + 1;
            }
            if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)
                if (dia > 31)
                    dia = 31;
            if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
                if (dia > 30)
                    dia = 30;
            if (mes == 2)
            {
                if (año % 4 == 0)
                {
                    if (dia > 29)
                        dia = 29;
                }
                else
                {
                    if (dia > 28)
                        dia = 28;
                }
            };
            DateTime fecha_ret = new DateTime(año, mes, dia, 0, 0, 0, 0);
            return fecha_ret;
        }


        public DateTime FecUltDia(DateTime f_fec_des)
        {
            int mes = f_fec_des.Month;
            int año = f_fec_des.Year;
            switch (mes)
            {
                case 1:
                    f_fec_des = new DateTime(año, mes, 31, 0, 0, 0);
                    break;
                case 2:
                    if (año % 4 == 0)
                        f_fec_des = new DateTime(año, mes, 29, 0, 0, 0);
                    else
                        f_fec_des = new DateTime(año, mes, 28, 0, 0, 0);
                    break;
                case 3:
                    f_fec_des = new DateTime(año, mes, 31, 0, 0, 0);
                    break;
                case 4:
                    f_fec_des = new DateTime(año, mes, 30, 0, 0, 0);
                    break;
                case 5:
                    f_fec_des = new DateTime(año, mes, 31, 0, 0, 0);
                    break;
                case 6:
                    f_fec_des = new DateTime(año, mes, 30, 0, 0, 0);
                    break;
                case 7:
                    f_fec_des = new DateTime(año, mes, 31, 0, 0, 0);
                    break;
                case 8:
                    f_fec_des = new DateTime(año, mes, 31, 0, 0, 0);
                    break;
                case 9:
                    f_fec_des = new DateTime(año, mes, 30, 0, 0, 0);
                    break;
                case 10:
                    f_fec_des = new DateTime(año, mes, 31, 0, 0, 0);
                    break;
                case 11:
                    f_fec_des = new DateTime(año, mes, 30, 0, 0, 0);
                    break;
                case 12:
                    f_fec_des = new DateTime(año, mes, 31, 0, 0, 0);
                    break;
                default:
                    f_fec_des = new DateTime(año, mes, 30, 0, 0, 0);
                    break;
            }
            return f_fec_des;
        }


        public DateTime FecSumDia(DateTime fecha, int dias, int tipo_cal)
        {
            int dato = 0;
            if (fecha == null)
                return fecha;
            if (tipo_cal == 1)
            {
                int año_fec = fecha.Year;
                int mes_fec = fecha.Month;
                int dia_fec = fecha.Day;
                int dias_febrero = 28;
                if (año_fec % 4 == 0)
                    dias_febrero = 29;
                if (dia_fec > 30 || (mes_fec == 2 && dia_fec >= dias_febrero))
                    dia_fec = 30;
                dato = (dias / 360);
                if (dato > 1)
                {
                    año_fec = año_fec + dato;
                    dias = dias - (360 * dato);
                }
                dato = (dias / 30);
                if (dato >= 1)
                {
                    mes_fec = mes_fec + dato;
                    if (mes_fec > 12)
                    {
                        while (mes_fec > 12)
                        {
                            año_fec = año_fec + 1;
                            mes_fec = mes_fec - 12;
                        }
                    }
                    dias = dias - (30 * dato);
                }
                if (dias > 0)
                {
                    if (30 - dia_fec < dias)
                    {
                        mes_fec = mes_fec + 1;
                        if (mes_fec > 12)
                        {
                            año_fec = año_fec + 1;
                            mes_fec = 1;
                        }
                        dia_fec = dias - (30 - dia_fec);
                    }
                    else
                    {
                        dia_fec = dia_fec + dias;
                    }
                }
                if (mes_fec == 2 && (dia_fec > dias_febrero && dia_fec <= 30))
                    dia_fec = dias_febrero;
                if (mes_fec == 2 && (dia_fec > 30 && dia_fec <= 59))
                    dia_fec = dia_fec - (30 - dias_febrero);
                if (mes_fec == 4 || mes_fec == 6 || mes_fec == 9 || mes_fec == 11)
                    if (dia_fec == 31)
                        dia_fec = 30;
                // Validar mes de febrero
                if (año_fec % 4 == 0)
                    dias_febrero = 29;
                else
                    dias_febrero = 28;
                if (mes_fec == 2 && (dia_fec > dias_febrero && dia_fec <= 30))
                    dia_fec = dias_febrero;
                // Crear la fecha
                fecha = new DateTime(año_fec, mes_fec, dia_fec, 0, 0, 0);
            }
            else
            {
                fecha = fecha.AddDays(dias);
            }
            return fecha;
        }

        public Int32? FecDifDia(DateTime fecha_ini, DateTime fecha_fin, int tipo_cal)
        {
            // Inicializando las variables
            Int32  dias = 0;
            Int32  ano_ini = 0;
            Int32  ano_fin = 0;
            Int32  mes_ini = 0;
            Int32  mes_fin = 0;
            Int32  dia_ini = 0;
            Int32  dia_fin = 0;
            // Verificando datos nulos
            if (fecha_ini == null || fecha_fin == null)
                return null;
            // Verificando que la fecha inicial no sea superior a la fecha final
            if (fecha_ini > fecha_fin)
                return null;
            // Calcular la diferencia según el tipo de calendario
            if (tipo_cal == 1)              // Días comerciales
            {
                ano_ini = fecha_ini.Year;
                ano_fin = fecha_fin.Year;
                mes_ini = fecha_ini.Month;
                mes_fin = fecha_fin.Month;
                dia_ini = fecha_ini.Day;
                if (dia_ini > 30 || (dia_ini >= 28 && mes_ini == 2))
                    dia_ini = 30;
                dia_fin = fecha_fin.Day;
                if (dia_fin > 30 || (dia_fin >= 28 && mes_fin == 2))
                {
                    dia_fin = 30;
                    if (dia_ini >= 28 && (mes_ini == 2 || mes_fin == 2))
                        dia_ini = 30;
                }
                dias = (ano_fin - ano_ini)*360;
                dias = dias + (mes_fin - mes_ini)*30;
                dias = dias + (dia_fin - dia_ini);
            }
            else if (tipo_cal == 2)         // Días calendario
                dias = Convert.ToInt32(fecha_fin - fecha_ini);
            else
                return null;          
  
            return dias;          
        }

    }

}
