using System;
using System.Text;

namespace StartUp
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get => this.length;
            private set
            {
                if (this.IsValid(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BoxParameterCannotBeZeroOrNegative), nameof(this.length));
                }
                this.length = value;
            }
        }
        public double Width
        {
            get => this.width;
            private set
            {
                if (this.IsValid(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BoxParameterCannotBeZeroOrNegative), nameof(this.width));
                }
                this.width = value;
            }
        }
        public double Height
        {
            get => this.height;
            private set
            {
                if (this.IsValid(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BoxParameterCannotBeZeroOrNegative), nameof(this.height));
                }
                this.height = value;
            }
        }
        private bool IsValid(double value) => value <= 0;   
        public double SurfaceArea()
            => (2 * this.Length * this.Width) + LateralSurfaceArea();

        public double LateralSurfaceArea()
            => (2 * this.Length * this.Height) + (2 * this.Width * this.Height);

        public double Volume()
            => this.Length * this.Height * this.Width;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine($"Surface Area - {this.SurfaceArea():f2}")
                .AppendLine($"Lateral Surface Area - {this.LateralSurfaceArea():f2}")
                .AppendLine($"Volume - {this.Volume():f2}");

            return sb.ToString().TrimEnd();


        }

    }
}
