
namespace ValidationAttributes
{
    using System;
    using System.Linq;
    using System.Reflection;
    public class Validator
    {
        public static bool IsValid(object obj)
        {
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType
                .GetProperties()
                .Where(t => t.GetCustomAttributes(typeof(MyValidationAttribute)).Any())
                .ToArray();

            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj);
                bool isValid = property
                    .GetCustomAttribute<MyValidationAttribute>()
                    .IsValid(value); 

                if (!isValid)
                {
                    return false;
                }
            }
            return true;
        }
    }
}