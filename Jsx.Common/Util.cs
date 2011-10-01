using System;
using System.ComponentModel;

namespace Jsx
{
	public static class Util
	{
		/// <summary>
		/// I believe this converts an object to any type, if it's able.
		/// </summary>
		/// <param name="value">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <returns>
		/// A <see cref="http://blogs.msdn.com/b/jongallant/archive/2006/06/19/637023.aspx"/>
		/// </returns>
		public static T ConvertTo<T>(object value)
		{
			bool isNullable = false;
	        // check for value = null, thx alex       
	
	        Type t = typeof(T);
	
	        // do we have a nullable type?
	        if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
	        {
	            NullableConverter nc = new NullableConverter(t);
	            t = nc.UnderlyingType;
				isNullable = true;
	        }
	
	        if (t.IsEnum) // if enum use parse
	            return (T)Enum.Parse(t, value.ToString(), false);
	        else
	        {
	            // if we have a custom type converter then use it
	            TypeConverter td = TypeDescriptor.GetConverter(t);
				if(isNullable && value == null){
					return (T)value;
				}
	            else if (td.CanConvertFrom(value.GetType()))
	            {
	                return (T)td.ConvertFrom(value);
	            }
	            else // otherwise use the changetype
	                return (T)Convert.ChangeType(value, t);
	        }
		}
	}
}

