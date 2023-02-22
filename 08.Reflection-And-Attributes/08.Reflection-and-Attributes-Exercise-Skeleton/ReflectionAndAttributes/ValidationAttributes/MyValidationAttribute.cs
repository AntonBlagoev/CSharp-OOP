namespace ValidationAttributes
{
    using System;
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class MyValidationAttribute : Attribute
    {
        public abstract bool IsValid(object obj);
    }
    public class MyRangeAttribute : MyValidationAttribute
    {
        private readonly int min;
        private readonly int max;
        public MyRangeAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
        public override bool IsValid(object obj)
        {
            int currValue = (int)obj;
            return currValue >= min && currValue <= max;
        }
    }
    public class MyRequiredAttribute : MyValidationAttribute
    {
        public override bool IsValid(object obj)
        => obj != null;
    }
}