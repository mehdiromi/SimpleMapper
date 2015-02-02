using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper
{
    public class SimpleMapper
    {

        //Version 1: Using Generics and reflections
        public static TDestination Map(TSource source)
            where TDestination: class

        {
            TDestination result = Activator.CreateInstance();
            IEnumerable props = typeof(TDestination).GetProperties();
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

      
    
        //Version 2: Using system.Object
        private static void Map(object obj1, object obj2)
        {
            #region Set property values using reflections
            Type type = obj1.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (var prop in props)
            {
                #region
                Type tProp = prop.PropertyType;
                //Nullable properties have to be treated differently, since we 
                //  use their underlying property to set the value in the object
                if (tProp.IsGenericType
                    && tProp.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    //if it's null, just set the value from the reserved word null, and return
                    if (obj2.GetType().GetProperty(prop.Name).GetValue(obj2) == null)
                    {
                        prop.SetValue(obj1, null, null);
                        return;
                    }
                    //Get the underlying type property instead of the nullable generic
                    tProp = new NullableConverter(prop.PropertyType).UnderlyingType;
                }
                //use the converter to get the correct value
                prop.SetValue(obj1, Convert.ChangeType(obj2.GetType().GetProperty(prop.Name).GetValue(obj2), tProp), null);
                #endregion
            }
            #endregion
        }



    }
}
