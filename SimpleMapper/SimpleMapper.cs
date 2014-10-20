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



        public static void InitOtherFields<T>(object obj, T defaultValue)
        {
            #region Set property values using reflections
            string[] invalidPropName = { "ID", "Year", "Quarter", "DealerID", "StatusID", "DateCreated", "LastModified" };
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (var prop in props)
            {
                #region MyRegion
                if (!invalidPropName.Contains(prop.Name))
                {
                    #region MyRegion
                    Type tProp = prop.PropertyType;
                    if (tProp.IsGenericType && tProp.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        tProp = new NullableConverter(prop.PropertyType).UnderlyingType;
                    }
                    prop.SetValue(obj, Convert.ChangeType(defaultValue, tProp), null);
                    #endregion
                }
                #endregion
            }
            #endregion
        }




    }
}
