using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper
{
    public static class SimpleMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)            
        {
            TDestination result = Activator.CreateInstance<TDestination>();
            IEnumerable<PropertyInfo> props = typeof(TDestination).GetProperties();
            foreach (var prop in props)
            {
                Type tProp = prop.PropertyType;
                if (typeof(TSource).GetProperty(prop.Name) != null)
                {
                    prop.SetValue(result, Convert.ChangeType(source.GetType().GetProperty(prop.Name).GetValue(source), tProp), null);
                }
            }
            return result;
        }

      
    }
}
