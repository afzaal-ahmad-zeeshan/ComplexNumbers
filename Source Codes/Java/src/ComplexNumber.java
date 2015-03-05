/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author AfzaalAhmad
 * This class is intended for complex numbers of mathematics.
 */
public class ComplexNumber {
    // Members
    private boolean isPolar;
    
    private double real;
    private double imaginary;
    
    private double angle;
    private double radius;
    
    // Operations
    ComplexNumber add(ComplexNumber a) {
        ComplexNumber number = new ComplexNumber();
        
        number.real = this.real + a.real;
        number.imaginary = this.imaginary + a.imaginary;
        number.isPolar = false;
        return number;
    }
    
    ComplexNumber subtract(ComplexNumber a) {
        ComplexNumber number = new ComplexNumber();
        
        number.real = this.real - a.real;
        number.imaginary = this.imaginary - a.imaginary;
        number.isPolar = false;
        return number;
    }
    
    // Might be a polar that needs to multiply
    ComplexNumber multiply(ComplexNumber a) {
        ComplexNumber number = new ComplexNumber();
        
        if(!this.isPolar && !a.isPolar) {
            number.isPolar = false;
            number.real = (this.real * a.real) - (this.imaginary * a.imaginary);
            number.imaginary = (this.real * a.imaginary) + (this.imaginary * a.real);
        } else {
            number.isPolar = true;
            number.radius = this.radius * a.radius;
            number.angle = this.angle + a.angle;
        }
        return number;
    }
    
    ComplexNumber divide(ComplexNumber a) {
        ComplexNumber number = new ComplexNumber();
        
        if(!this.isPolar && !a.isPolar) {
            number.isPolar = false;
            number.real = (this.real * a.real) + (this.imaginary * a.imaginary)
                    / (a.real * a.real) + (a.imaginary * a.imaginary);
            number.imaginary = (this.imaginary * a.real) - (this.real * a.imaginary)
                    / (a.real * a.real) + (a.imaginary * a.imaginary);
        } else {
            number.isPolar = true;
            number.radius = this.radius / a.radius;
            number.angle = this.angle - a.angle;
        }
        return number;
    }
    
    boolean equals(ComplexNumber a) {
        if (this.isPolar && a.isPolar) {
            // If both are polar
            if (this.angle == a.angle && this.radius == a.radius) {
                return true;
            }
            else {
                return false;
            }
        }
        else if(this.isPolar && !a.isPolar) {
            return false;
        }
        else if (!this.isPolar && a.isPolar) {
            return false;
        }
        else {
            // Check whether their values are same
            if (this.real == a.real && this.imaginary == a.imaginary) {
                return true;
            }
            else {
                return false;
            }
        }
    }
    
    ComplexNumber negate() {
        ComplexNumber number = new ComplexNumber();
        
        if(!this.isPolar) {
            number.real = -this.real;
            number.imaginary = -this.imaginary;
            number.isPolar = false;
        }
        return number;
    }
    
    // Functions
    ComplexNumber asPolar() {
        ComplexNumber number = new ComplexNumber();
        if(this.isPolar) {
            // Number is already polar
            return this;
        } else {
            number.isPolar = true;
            number.angle = Math.atan((this.imaginary / this.real));
            number.radius = Math.sqrt(
                    (this.imaginary * this.imaginary) + (this.real * this.real)
            );
        }
        return number;
    }
    
    ComplexNumber asRectangular() {
        ComplexNumber number = new ComplexNumber();
        
        if(!this.isPolar) {
            return this;
        } else {
            number.isPolar = false;
            number.real = this.radius * Math.cos(this.angle);
            number.real = this.radius * Math.sin(this.angle);
        }
        return number;
    }
    
    ComplexNumber getLogarithm() {
        ComplexNumber number = new ComplexNumber();
        
        number.real = Math.log(this.radius);
        number.imaginary = this.angle;
        number.isPolar = false;
        return number;
    }
    
    ComplexNumber getConjugate() {
        ComplexNumber number = new ComplexNumber();
        
        if(this.isPolar) {
            number.angle = -this.angle;
            number.isPolar = true;
            number.radius = this.radius;
        } else {
            number.real = this.real;
            number.imaginary = -this.imaginary;
            number.isPolar = false;
        }
        return number;
    }
    
    ComplexNumber getReciprocal() {
        ComplexNumber number = new ComplexNumber();
        
        if(!this.isPolar) {
            number.real = this.real / 
                    (this.real * this.real) + (this.imaginary * this.imaginary);
            number.real = -this.imaginary / 
                    (this.real * this.real) + (this.imaginary * this.imaginary);
        }
        return number;
    }
    
    @Override
    public String toString() {
        String str = "";
        
        if(this.isPolar) {
            // Polar notation
            if(this.angle > 0) {
                str = this.radius + "(cos" + this.angle + " + isin" + this.angle + ")";
            } else {
                str = this.radius + "(cos" + this.angle + " - isin" + -this.angle + ")";
            }
        } else {
            // Cartesian notation
            if(this.imaginary > 0) {
                str = this.real + " + i" + this.imaginary;
            } else {
                str = this.real + " + -" + -this.imaginary;
            }
        }
        return str;
    }
    
    // Constructors
    public ComplexNumber() {
        isPolar = false;
        real = 0;
        imaginary = 0;
    }
    
    public ComplexNumber(boolean isPolar, double firstVal, double secondVal) {
        this.isPolar = isPolar;
        if(isPolar) {
            // Polar
            this.radius = firstVal;
            this.angle = secondVal;
        } else {
            this.real = firstVal;
            this.imaginary = secondVal;
        }
    }
}
