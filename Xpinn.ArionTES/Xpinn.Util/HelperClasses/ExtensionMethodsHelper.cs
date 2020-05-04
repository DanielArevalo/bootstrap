using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Xpinn.Util
{
    public static class ExtensionMethodsHelper
    {
        // Sustitutos para el operador nameof() de C# 6.0 que no se puede tener aca por no tener 4.0
        public static String nameof<T, TT>(this Expression<Func<T, TT>> accessor)
        {
            return nameof(accessor.Body);
        }

        public static String nameof<T>(this Expression<Func<T>> accessor)
        {
            return nameof(accessor.Body);
        }

        public static String nameof<T, TT>(this T obj, Expression<Func<T, TT>> propertyAccessor)
        {
            return nameof(propertyAccessor.Body);
        }

        static String nameof(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = expression as MemberExpression;
                if (memberExpression == null)
                    return null;
                return memberExpression.Member.Name;
            }
            return null;
        }

        public static int ObtenerIndiceMasAltoEntreDosListas(IList listaUno, IList listaDos)
        {
            int indexUno = listaUno.Count;
            int indexDos = listaDos.Count;

            if (indexUno >= indexDos)
            {
                return indexUno;
            }
            else
            {
                return indexDos;
            }
        }

        // Extension Method para usar un foreach improvisado en cualquier colleccion :D
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null) throw new ArgumentNullException("collection nulo!.");
            if (action == null) throw new ArgumentNullException("action nulo!.");

            foreach (T item in collection)
                action(item);
        }


        // Extension Method para crear un DataTable de una coleccion de objetos, 
        // CUIDADO EL ORDEN DE LAS COLUMNAS SERA EL ORDEN EL CUAL FUERON DEFINIDAS LAS PROPIEDADES
        // Si quieres que solo añada unas columnas en especifico, pasa un array de string con el nombre de ellas (No importa el case)
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string[] columnaQueQuiero = null, string dataTableName = "DataTable")
        {
            if (collection == null) throw new ArgumentNullException("collection nulo!.");

            DataTable dataTable = new DataTable(dataTableName);
            Type typeObject = typeof(T);
            List<PropertyInfo> propertyInfoList = typeObject.GetProperties().Where(x => !x.IsEnumerableNonString()).ToList();

            if (columnaQueQuiero != null && columnaQueQuiero.Count () > 0)
            {
                propertyInfoList = propertyInfoList.Where(x => x.Name.VerifyIfStringIsInCollection(columnaQueQuiero)).ToList();
            }

            //Inspect the properties and create the columns in the DataTable
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                Type columnType = propertyInfo.PropertyType;
                if ((columnType.IsGenericType))
                {
                    columnType = columnType.GetGenericArguments()[0];
                }

                dataTable.Columns.Add(propertyInfo.Name, columnType);
            }

            //Populate the data table
            foreach (T item in collection)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow.BeginEdit();

                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    if (propertyInfo.GetValue(item, null) != null)
                    {
                        dataRow[propertyInfo.Name] = propertyInfo.GetValue(item, null);
                    }
                }

                dataRow.EndEdit();
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }


        // Crear un DataSet de una coleccion de objetos, 
        // CUIDADO EL ORDEN DE LAS COLUMNAS SERA EL ORDEN EL CUAL FUERON DEFINIDAS LAS PROPIEDADES
        // Si quieres que solo añada unas columnas en especifico, o un orden en especifico, pasa un array de string con el nombre de ellas (No importa el case)
        public static DataSet ToDataSet<T>(this IEnumerable<T> collection, string[] columnaQueQuiero = null, string dataSetName = "DataSet", string dataTableName = "DataTable")
        {
            if (collection == null) throw new ArgumentNullException("collection nulo!.");

            DataSet dataSet = new DataSet(dataSetName);

            DataTable dataTable = ToDataTable(collection, columnaQueQuiero, dataTableName);

            //Add table to dataset
            dataSet.Tables.Add(dataTable);

            return dataSet;
        }

        // Ver si un string esta exactamente igual en un IEnumerable<string>
        public static bool VerifyIfStringIsInCollection(this string source, IEnumerable<string> collection, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (source == null) throw new ArgumentNullException("source nulo!.");
            if (collection == null) throw new ArgumentNullException("collection nulo!.");

            bool esIgual = false;
            foreach (var item in collection)
            {
                esIgual = source.Equals(item, comp);

                if (esIgual) break;
            }

            return esIgual;
        }


        // Verifica si un string esta en otro string teniendo la opcion de eliminar la comparacion del el case
        public static bool Contains(this string source, string stringToCheck, StringComparison comp)
        {
            if (source == null) throw new ArgumentNullException("source nulo!.");
            if (stringToCheck == null) throw new ArgumentNullException("stringToCheck nulo!.");

            return source.IndexOf(stringToCheck, comp) >= 0;
        }


        // Verifica si un string esta dentro de un array de string teniendo la opcion de eliminar la comparacion del el case
        // Valida si mi source existe en el array (OJO no significa que sea exactamente igual el string, solo significa que esta adentro)
        public static bool Contains(this string source, string[] stringArrayToCheck, StringComparison comp = StringComparison.InvariantCulture)
        {
            if (source == null) throw new ArgumentNullException("source nulo!.");
            if (stringArrayToCheck == null) throw new ArgumentNullException("stringToCheck nulo!.");

            foreach (var item in stringArrayToCheck)
            {
                if (source.Contains(item)) return true;
            }

            return false;
        }


        // Verifica si un array de string contiene un string teniendo la opcion de eliminar la comparacion del case
        // Valida si en mi sourceArray existe el string (OJO no significa que sea exactamente igual el string, solo significa que esta adentro)
        public static bool Contains(this string[] source, string stringToCheck, StringComparison comp = StringComparison.InvariantCulture)
        {
            if (source == null) throw new ArgumentNullException("source nulo!.");
            if (stringToCheck == null) throw new ArgumentNullException("stringToCheck nulo!.");

            foreach (var item in source)
            {
                if (item.Contains(stringToCheck, comp)) return true;
            }

            return false;
        }

        // Verifica si un PropertyInfo es una Collecion, pero no afecta al string que es un IEnumrable
        public static bool IsEnumerableNonString(this PropertyInfo propertyInfo)
        {
            return propertyInfo != null && propertyInfo.PropertyType.IsEnumerableNonString();
        }

        // Verifica si un object es una Collecion, pero no afecta al string que es un IEnumrable
        public static bool IsEnumerableNonString(this object instance)
        {
            return instance != null && instance.GetType().IsEnumerableNonString();
        }

        // Verifica si un Type es una Collecion, pero no afecta al string que es un IEnumrable
        public static bool IsEnumerableNonString(this Type type)
        {
            if (type == null || type == typeof(string))
                return false;
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        // Convertir un string en un Enum
        // Ejemplo =>     TipoReporteCartera tipoReporte = ddlConsultar.SelectedValue.ToEnum<TipoReporteCartera>();
        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue = default(TEnum))
            where TEnum : struct, IConvertible
        {
            if (value == null) throw new ArgumentNullException("Source no puede ser nulo!.");

            TEnum result;
            return Enum.TryParse(value, true, out result) ? result : defaultValue;
        }

        // Extension Method para convertir un numero en un Enum
        // Ejemplo =>  int aww = 1   TipoReporteCartera tipo = aww.ToEnum<TipoReporteCartera, int>();
        public static TEnum ToEnum<TEnum>(this long value, TEnum defaultValue = default(TEnum))
            where TEnum : struct, IConvertible
        {
            TEnum tipo = value.ToString().ToEnum<TEnum>();
            return tipo;
        }

        public static TEnum ToEnum<TEnum>(this int value, TEnum defaultValue = default(TEnum))
        where TEnum : struct, IConvertible
        {
            TEnum tipo = value.ToString().ToEnum<TEnum>();
            return tipo;
        }

        public static TEnum ToEnum<TEnum>(this int? value, TEnum defaultValue = default(TEnum))
        where TEnum : struct, IConvertible
        {
            if (!value.HasValue) throw new ArgumentNullException("value es nulo!.");            

            TEnum tipo = value.ToString().ToEnum<TEnum>();
            return tipo;
        }

        public static TEnum ToEnum<TEnum>(this long? value, TEnum defaultValue = default(TEnum))
        where TEnum : struct, IConvertible
        {
            if (!value.HasValue) throw new ArgumentNullException("value es nulo!.");

            TEnum tipo = value.ToString().ToEnum<TEnum>();
            return tipo;
        }
    }
}
