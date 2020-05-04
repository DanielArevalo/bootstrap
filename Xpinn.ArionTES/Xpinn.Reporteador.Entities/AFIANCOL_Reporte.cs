using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Reporteador.Entities
{
    public class AFIANCOL_Reporte
    {
        public DateTime FechaHistorico { get; set; }
        public string Identificacion  { get; set; }
        public string NombreApellidos { get; set; }
        public long NumeroRadicacion { get; set; }
        public DateTime FechaDesembolso { get; set; }
        public int Plazo { get; set; }
        public int ValorDesembolsado { get; set; }
        public int  SaldoCredito { get; set; }
        public int ValorAportes { get; set; }
        public decimal TasaAfiancol { get; set; }
        public int SaldoInsoluto { get; set; }
        public int Remuneracion  { get; set; }
        public decimal Iva { get; set; }
        public int Total { get; set; }
        public int Validar { get; set; }
    }

    public class FechaCorte
    {
        public DateTime Fecha { get; set; }
    }
}



