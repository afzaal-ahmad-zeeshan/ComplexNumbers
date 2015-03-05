using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers_CSharp
{
    class ComplexNumber
    {
        // Members of the class
        private bool isPolar;

        private double real;
        private double imaginary;

        private double radius;
        private double angle;

        // Member calling
        double GetReal()
        {
            return real;
        }

        void SetReal(double real)
        {
            this.real = real;
        }

        double GetImaginary()
        {
            return imaginary;
        }

        void SetImaginary(double i)
        {
            imaginary = i;
        }

        double GetRadius()
        {
            if (this.isPolar)
            {
                return radius;
            }
            else
            {
                return ((this.real * this.real) + (this.imaginary * this.imaginary));
            }
        }

        void SetRadius(double r)
        {
            radius = r;
        }

        double GetAngle()
        {
            if (this.isPolar)
            {
                return angle;
            }
            else
            {
                return (Math.Atan(this.imaginary/this.real));
            }
        }

        void SetAngle(double a)
        {
            if (this.isPolar)
            {
                angle = a;
            }
        }

        // Functions
        string ToString()
        {
            string str = "";

            if (this.isPolar)
            {
                // Polar
                if (this.angle > 0)
                {
                    // Positive angle
                    str = this.radius + "(cos" + this.angle + " + isin" + this.angle + ")";
                }
                else
                {
                    str = this.radius + "(cos" + this.angle + " - isin" + -this.angle + ")";
                }
            }
            else
            {
                // Rect
                if (this.imaginary > 0)
                {
                    // Positive imaginary
                    str = this.real + " + i" + this.imaginary;
                }
                else
                {
                    str = this.real + " - i" + -this.imaginary;
                }
            }
            return str;
        }

        ComplexNumber AsPolar()
        {
            ComplexNumber number = new ComplexNumber();
            if (this.isPolar)
            {
                return this;
            }
            else
            {
                number = new ComplexNumber(true, this.GetRadius(), this.GetAngle());
            }

            return number;
        }

        ComplexNumber AsCartesian()
        {
            ComplexNumber number = new ComplexNumber();
            if (!this.isPolar)
            {
                return this;
            }
            else
            {
                number = new ComplexNumber(false, 
                    this.GetRadius() * Math.Cos(this.GetAngle()), 
                    this.GetRadius() * Math.Sin(this.GetAngle()));
            }

            return number;
        }

        ComplexNumber Logarithm()
        {
            ComplexNumber number = new ComplexNumber();
            number.real = Math.Log(this.GetRadius());
            number.imaginary = this.GetAngle();
            number.isPolar = false;
            return number;
        }

        ComplexNumber Conjugate()
        {
            ComplexNumber number = new ComplexNumber();

            if (this.isPolar)
            {
                // The number is polar
                number.radius = this.radius;
                number.angle = -this.angle;
                number.isPolar = true;
            }
            else
            {
                number.real = this.real;
                number.imaginary = -this.imaginary;
                number.isPolar = false;
            }

            return number;
        }

        ComplexNumber Reciprocal()
        {
            ComplexNumber number = new ComplexNumber();

            number.real = (this.real) / (this.real * this.real + this.imaginary * this.imaginary);
            // Minus sign shows that there is a conjugate of the complex number.
            number.imaginary = -(this.imaginary) / (this.real * this.real + this.imaginary * this.imaginary);

            number.isPolar = false;
            return number;
        }

        // Operator overloading
        static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) {
            // Add then up
            if (!a.isPolar == !b.isPolar)
            {
                return new ComplexNumber(a.isPolar, a.real + b.real, a.imaginary + b.imaginary);
            }
            else
            {
                // Return exception
                throw new NotImplementedException();
            }
        }
        
        static ComplexNumber operator -(ComplexNumber a)
        {
            if (!a.isPolar)
            {
                return new ComplexNumber(a.isPolar, -a.radius, a.angle);
            }
            else
            {
                return new ComplexNumber(a.isPolar, -a.real, -a.imaginary);
            }
        }

        static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            if (!a.isPolar && !b.isPolar)
            {
                return new ComplexNumber(a.isPolar, a.real - b.real, a.imaginary - b.imaginary);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            ComplexNumber number = new ComplexNumber();
            if (a.isPolar && b.isPolar)
            {
                number.radius = a.radius * b.radius;
                number.angle = a.angle + b.angle;
                number.isPolar = true;
            }
            else if (!b.isPolar && !b.isPolar)
            {
                // Both are not polar; they're rectangular
                number.real = (a.real * b.real) - (a.imaginary * b.imaginary);
                number.imaginary = (a.real * b.imaginary) + (a.imaginary * b.real);
                number.isPolar = false;
            }
            return number;
        }

        static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            ComplexNumber number = new ComplexNumber();

            if (a.isPolar && b.isPolar)
            {
                // Both are polar numbers
                number.radius = a.radius / b.radius;
                number.angle = a.angle - b.angle;
                number.isPolar = true;
            }
            else if (!a.isPolar && !b.isPolar)
            {
                // Both are rect;
                number.real = (a.real * b.real) + (a.imaginary * b.imaginary) /
                    ((b.real * b.real) + (b.imaginary * b.imaginary));

                number.imaginary = (a.real * b.real) - (a.imaginary * b.imaginary) /
                    ((b.real * b.real) + (b.imaginary * b.imaginary));
                number.isPolar = false;
            }
            return number;
        }

        static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (b.isPolar && a.isPolar)
            {
                // If both are polar
                if (b.angle == a.angle && b.radius == a.radius)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (b.isPolar && !a.isPolar)
            {
                return false;
            }
            else if (!b.isPolar && a.isPolar)
            {
                return false;
            }
            else
            {
                // Check whether their values are same
                if (b.real == a.real && b.imaginary == a.imaginary)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Constructors
        public ComplexNumber()
        {
            // Default
            isPolar = false;
            real = 0;
            imaginary = 0;
        }

        public ComplexNumber(bool isPolar, double firstVal, double secondVal)
        {
            // Custom 
            this.isPolar = isPolar;
            if (this.isPolar)
            {
                // Polar
                radius = firstVal;
                angle = secondVal;
            }
            else
            {
                real = firstVal;
                imaginary = secondVal;
            }
        }
    }
}
