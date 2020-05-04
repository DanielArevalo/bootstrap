using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;

namespace Xpinn.Asesores.Data
{
    public class PlanoDiligenciaData
    {
        private String Mensaje = "";

        public String Mensaje1
        {
            get { return Mensaje; }
            set { Mensaje = value; }
        }

        private int Numregistro = 0;

        public int Numregistro1
        {
            get { return Numregistro; }
            set { Numregistro = value; }
        }
     
        
        private String NUMERO_RADICACION = "";
        

        public String NUMERO_RADICACION1
        {
            get { return NUMERO_RADICACION; }
            set { NUMERO_RADICACION = value; }
        }
        private DateTime FECHA_DILIGENCIA;

        public DateTime FECHA_DILIGENCIA1
        {
            get { return FECHA_DILIGENCIA; }
            set { FECHA_DILIGENCIA = value; }
        }
        private Int64 TIPO_DILIGENCIA = 0;

        public Int64 TIPO_DILIGENCIA1
        {
            get { return TIPO_DILIGENCIA; }
            set { TIPO_DILIGENCIA = value; }
        }
        private String ATENDIO = "";

        public String ATENDIO1
        {
            get { return ATENDIO; }
            set { ATENDIO = value; }
        }
        private String RESPUESTA = "";

        public String RESPUESTA1
        {
            get { return RESPUESTA; }
            set { RESPUESTA = value; }
        }
       
        private DateTime FECHA_ACUERDO;

        public DateTime FECHA_ACUERDO1
        {
            get { return FECHA_ACUERDO; }
            set { FECHA_ACUERDO = value; }
        }
        private Int64 VALOR_ACUERDO = 0;

        public Int64 VALOR_ACUERDO1
        {
            get { return VALOR_ACUERDO; }
            set { VALOR_ACUERDO = value; }
        }
       
        private String OBSERVACION = "";

        public String OBSERVACION1
        {
            get { return OBSERVACION; }
            set { OBSERVACION = value; }
        }
       
        
    }
}
