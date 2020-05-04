using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    public class StringHelper
    {
        public string QuitarAndYColocarWhereEnFiltroQuery(string filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string newFilter = filtro;

                // Si la Query esta llena la ordeno de manera que no explote por tener un "and" al iniciar
                if (filtro.StartsWith(" and "))
                {
                    newFilter = filtro.Remove(0, 4).Insert(0, " WHERE ");
                }

                return newFilter;
            }

            return filtro;
        }

        public string DesformatearNumerosEnteros(string numeroConFormato)
        {
            string numeroDesformateado = DesformatearNumerosDecimales(numeroConFormato).Replace("$", "").Replace(" ", "");
            int index = numeroDesformateado.IndexOf(",");

            if (index < 0)
            {
                index = numeroDesformateado.Length;
            }

            numeroDesformateado = numeroDesformateado.Substring(0, index);

            return numeroDesformateado;
        }

        public string FormatearNumerosComoCurrency(string stringTextBox)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stringTextBox))
                {
                    return " ";
                }
                else if (stringTextBox.Trim().StartsWith("0"))
                {
                    return "$ " + stringTextBox.Trim();
                }
                else if (stringTextBox.Contains("$"))
                {
                    return stringTextBox.Trim();
                }
                else
                {
                    return string.Format("$ {0:#,##0.00}", Convert.ToDecimal(stringTextBox.Trim().Replace(".", "")));
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Pasaste una letra al metodo (FormatearNumerosComoCurrency) que solo formatea numeros");
            }
        }

        public string FormatearNumerosComoCurrency(int numero)
        {
            return FormatearNumerosComoCurrency(numero.ToString());
        }


        public string FormatearNumerosComoCurrency(long numero)
        {
            return FormatearNumerosComoCurrency(numero.ToString());
        }


        public string FormatearNumerosComoCurrency(decimal numero)
        {
            return FormatearNumerosComoCurrency(numero.ToString());
        }


        public string FormatearNumerosComoCurrency(double numero)
        {
            return FormatearNumerosComoCurrency(numero.ToString());
        }


        public string FormatearNumerosComoCurrency(float numero)
        {
            return FormatearNumerosComoCurrency(numero.ToString());
        }


        public string DesformatearNumerosDecimales(string numeroConFormato)
        {
            return numeroConFormato.Replace(".", "").Replace("$", "").Trim();
        }

        public string FormatearNumerosComoMilesSinDecimales(string stringTextBox)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stringTextBox))
                {
                    return " ";
                }
                else if (stringTextBox.Trim().StartsWith("0"))
                {
                    return stringTextBox.Trim();
                }

                if (stringTextBox.Contains("$"))
                {
                    stringTextBox.Replace("$", "").Trim();
                }

                return string.Format("{0:#,##0}", Convert.ToDecimal(stringTextBox.Trim().Replace(".", "")));
            }
            catch (Exception)
            {
                throw new ArgumentException("Pasaste una letra al metodo (FormatearNumerosComoMilesSinDecimales) que solo formatea numeros");
            }
        }

        public string FormatearNumerosComoMilesSinDecimales(int numero)
        {
            return FormatearNumerosComoMilesSinDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesSinDecimales(long numero)
        {
            return FormatearNumerosComoMilesSinDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesSinDecimales(decimal numero)
        {
            return FormatearNumerosComoMilesSinDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesSinDecimales(double numero)
        {
            return FormatearNumerosComoMilesSinDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesSinDecimales(float numero)
        {
            return FormatearNumerosComoMilesSinDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesConDecimales(string stringTextBox)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stringTextBox))
                {
                    return " ";
                }
                else if (stringTextBox.Trim().StartsWith("0"))
                {
                    return stringTextBox.Trim();
                }

                if (stringTextBox.Contains("$"))
                {
                    stringTextBox.Replace("$", "").Trim();
                }

                return string.Format("{0:#,##0.00}", Convert.ToDecimal(stringTextBox.Trim().Replace(".", "")));
            }
            catch (Exception)
            {
                throw new ArgumentException("Pasaste una letra al metodo (FormatearNumerosComoMilesConDecimales) que solo formatea numeros");
            }
        }

        public string FormatearNumerosComoMilesConDecimales(int numero)
        {
            return FormatearNumerosComoMilesConDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesConDecimales(long numero)
        {
            return FormatearNumerosComoMilesConDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesConDecimales(decimal numero)
        {
            return FormatearNumerosComoMilesConDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesConDecimales(double numero)
        {
            return FormatearNumerosComoMilesConDecimales(numero.ToString());
        }

        public string FormatearNumerosComoMilesConDecimales(float numero)
        {
            return FormatearNumerosComoMilesConDecimales(numero.ToString());
        }
    }
}
