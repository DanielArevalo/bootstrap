using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Xpinn.Util
{
    public class ReflectionHelper
    {
        public string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        public bool IsPropertyExist(dynamic settings, params string[] names)
        {
            bool sinError = true;

            foreach (var name in names)
            {
                if (settings.GetType().GetProperty(name) == null)
                {
                    sinError = false;
                    return sinError;
                }
            }

            return sinError;
        }

        public object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
