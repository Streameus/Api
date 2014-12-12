using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Streameus.Enums
{
    /// <summary>
    ///     This class contains all the extensions methods for enums
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Get display value if existing.
        /// </summary>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumVal)
        {
            FieldInfo fi = enumVal.GetType().GetField(enumVal.ToString());
            DisplayAttribute[] attributes = (DisplayAttribute[]) fi.GetCustomAttributes(typeof (DisplayAttribute), true);
            if (attributes.Length > 0)
                return attributes[0].GetName();
            return enumVal.ToString();


//
//            var enumType = enumVal.GetType();
//            var customAtributesGen = enumType.GetCustomAttributes(typeof (DisplayAttribute), false);
//            var customAtributes = customAtributesGen.Cast<DisplayAttribute>();
//            var attr = customAtributes.SingleOrDefault();
//
//
//            return (attr != null) ? attr.Name : enumVal.ToString();
        }
    }
}