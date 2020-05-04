using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    public class DateTimeHelper
    {
        public DateTime PrimerDiaDelAño(DateTime fechaActual)
        {
            return new DateTime(fechaActual.Year, 1, 1);
        }

        public DateTime UltimoDiaDelAño(DateTime fechaActual)
        {
            return new DateTime(fechaActual.Year, 12, 31);
        }

        public DateTime PrimerDiaDelMes(DateTime fechaActual)
        {
            return new DateTime(fechaActual.Year, fechaActual.Month, 1);
        }

        public DateTime UltimoDiaDelMes(DateTime fechaActual)
        {
            DateTime primerDia = PrimerDiaDelMes(fechaActual);
            return primerDia.AddMonths(1).AddDays(-1);
        }

        // Diferencia en meses
        public long DiferenciaEntreDosFechas(DateTime fechaMayor, DateTime fechaMenor)
        {
            return Convert.ToInt64(Math.Ceiling(fechaMayor.Subtract(fechaMenor).Days / (365.25 / 12)));
        }

        // Diferencia en años
        public long DiferenciaEntreDosFechasAños(DateTime fechaMayor, DateTime fechaMenor)
        {
            return fechaMayor.Year - fechaMenor.Year;
        }

        // Diferencia en dias
        public long DiferenciaEntreDosFechasDias(DateTime fechaMayor, DateTime fechaMenor)
        {
            return Convert.ToInt64(Math.Ceiling((fechaMayor - fechaMenor).TotalDays));
        }

        public DateTime SumarDiasSegunTipoCalendario(DateTime fecha, int dias, int tipo_cal)
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
                        año_fec = año_fec + 1;
                        mes_fec = mes_fec - 12;
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

        public DateTime RestarDiasSegunTipoCalendario(DateTime fecha, int dias, int tipo_cal)
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
                        año_fec = año_fec + 1;
                        mes_fec = mes_fec - 12;
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
                fecha = fecha.AddDays(-dias);
            }
            return fecha;
        }

    }
}
