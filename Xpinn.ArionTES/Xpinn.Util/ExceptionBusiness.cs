/*
 * Desarrollado por Roman Albarracin
 * romanalbarracin@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Xpinn.Util
{
    public class ExceptionBusiness : ApplicationException
    {
        public String ubicacionExcepcion = "";

        public ExceptionBusiness()
        {
            ubicacionExcepcion = "";
        }

        public ExceptionBusiness(String pMensaje)
            : base(pMensaje)
        {
            ubicacionExcepcion = "";
        }

        public ExceptionBusiness(String pMensaje, Exception pError)
            : base(pMensaje, pError)
        {
            ubicacionExcepcion = "";
        }
    }

    public class ExcepcionBusiness
    {
        public ExcepcionBusiness()
        {
        }

        public void Throw(String pClase, String pMetodo, Exception e)
        {
            Type Nuevaclase;
            Nuevaclase = e.GetType();
            String Nombre = Nuevaclase.Name;

            switch (Nombre)
            {
                case ("SoapException"):
                    string error = e.Message;
                    Int32 indiceSplit = 0;
                    indiceSplit = error.IndexOf("--->");
                    error = error.Substring(indiceSplit);
                    indiceSplit = error.IndexOf(" at ");

                    if (indiceSplit == -1)
                        indiceSplit = error.IndexOf(" en ");

                    error = error.Substring(0, indiceSplit);
                    indiceSplit = error.IndexOf(":");
                    error = error.Substring(indiceSplit + 1).Trim();
                    throw new ExceptionBusiness(error);
                case ("ExceptionBusiness"):
                    ExceptionBusiness ex = new ExceptionBusiness();
                    ex = (ExceptionBusiness)e;
                    ex.ubicacionExcepcion = "Error en " + pClase + "." + pMetodo;
                    throw new ExceptionBusiness(ex.Message);
                case ("FormatException"):
                    throw new ExceptionBusiness(e.Message);
                case ("OutOfMemoryException"):
                    throw new ExceptionBusiness(e.Message);
                case ("SqlException"):
                    if (e.Message.Contains("duplicate key"))
                        throw new ExceptionBusiness("Registro duplicado, verifique por favor.");
                    else if (e.Message.Contains("DELETE statement conflicted with COLUMN REFERENCE constraint "))
                        throw new ExceptionBusiness("La eliminación no fue realizada, este registro contiene elementos que dependen de él");
                    else if (e.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint "))
                        throw new ExceptionBusiness("La eliminación no fue realizada, este registro contiene elementos que dependen de él");
                    else if (e.Message.Contains("The DELETE statement conflicted with the SAME TABLE REFERENCE constraint "))
                        throw new ExceptionBusiness("La eliminación no fue realizada, este registro contiene elementos que dependen de él");
                    else
                        throw new Exception("Error en " + pClase + "." + pMetodo + ": [" + Nombre + "] " + e.Message);
                case ("OracleException"):
                    if (e.Message.Contains("ORA-00001"))
                        throw new ExceptionBusiness("Registro duplicado, verifique por favor, " + e.Message);
                    else if (e.Message.Contains("ORA-20101"))
                        throw new Exception(e.Message);
                    else
                        throw new Exception("Error en " + pClase + "." + pMetodo + ": [" + Nombre + "] " + e.Message);
                case ("Exception"):
                    if (e.Message.Contains(".Consultar"))
                        if (e.Message.Contains("no encontrado."))
                            throw new ExceptionBusiness("El registro no fue encontrado en el sistema, verifique por favor.");
                        else
                            throw new Exception("Error en " + pClase + "." + pMetodo + ": [" + Nombre + "] " + e.Message);
                    else
                       throw new Exception("Error en " + pClase + "." + pMetodo + ": [" + Nombre + "] " + e.Message);
                default:
                    throw new Exception("Error en " + pClase + "." + pMetodo + ": [" + Nombre + "] " + e.Message);
            }
        }
    }
}