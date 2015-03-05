#include <math.h>
#include <string>
#include <sstream>

#pragma once

class ComplexNumber
{
	// Properties that a ComplexNumber can have
private:
	bool isPolar;

	// For a non-polar complex number
	int real;
	int imaginary;

	// For a rectangular complex number
	int radius;
	int angle;

public:

	// Functions that can be triggered over a ComplexNumber
	// For conversions
	ComplexNumber asPolar() {
		// Logic to convert the rectangular into polar form
		// Then return it
		ComplexNumber number;

		if (!isPolar) {
			// Get the radius
			number.radius = (sqrt(pow((double)real, 2) + pow((double)imaginary, 2)));
			number.angle = atan((double)(imaginary / real));
		}
		else {
			number.radius = this->radius;
			number.angle = this->angle;
		}

		number.isPolar = true;
		return number;
	}

	ComplexNumber asRectangular() {
		// Logic to convert the polar form into rectangular
		// Then return it
		ComplexNumber number;

		if (isPolar) {
			number.real = radius * (cos((double)angle));
			number.imaginary = radius * (sin((double)angle));
		}
		else {
			number.real = this->real;
			number.imaginary = this->imaginary;
		}

		number.isPolar = false;
		return number;
	}

	// Operators
	ComplexNumber operator + (ComplexNumber a) {
		// Return the sum of the operator
		ComplexNumber number;
		// Check if the numbers are equal; non-polar
		if (!this->isPolar && !a.isPolar) {
			// Run the logic
			number.real = this->real + a.real;
			number.imaginary = this->imaginary + a.imaginary;
			number.isPolar = false;
		}

		return number;
	}

	ComplexNumber operator - (ComplexNumber a) {
		// Return the answer of the operator
		ComplexNumber number, me;
		me.isPolar = this->isPolar;

		if (!me.isPolar && !a.isPolar) {
			// Both numbers are not polar; subtraction can continue

			number.real = this->real - a.real;
			number.imaginary = this->imaginary - a.imaginary;
			number.isPolar = false;
		}

		return number;
	}

	ComplexNumber operator - () {
		ComplexNumber number;
		if (!this->isPolar){
			// Number is cartesian
			number.real = -(this->real);
			number.imaginary = -(this->imaginary);
			number.isPolar = false;
		}
		return number;
	}

	ComplexNumber operator * (ComplexNumber a) {
		// Return the answer of the operator
		ComplexNumber number, me;
		// Check if the numbers are same type
		me.isPolar = this->isPolar;

		if (me.isPolar && a.isPolar) {
			// Both are polar form
			me.radius = this->radius;
			me.angle = this->angle;

			number.radius = me.radius * a.radius;
			number.angle = me.angle + a.angle;
			number.isPolar = true;
		}
		else if (!me.isPolar && !a.isPolar) {
			// Both are not polar; they're rectangular
			me.real = this->real;
			me.imaginary = this->imaginary;

			number.real = (this->real * a.real) - (this->imaginary * a.imaginary);
			number.imaginary = (this->real * a.imaginary) + (this->imaginary * a.real);
			number.isPolar = false;
		}

		return number;
	}

	ComplexNumber operator / (ComplexNumber a) {
		// Return the answer of the operator
		ComplexNumber number, me;
		// Check if the numbers are polar
		me.isPolar = this->isPolar;
		if (me.isPolar && a.isPolar) {
			// Both are polar numbers
			me.angle = this->angle;
			me.radius = this->radius;

			number.radius = me.radius / a.radius;
			number.angle = me.angle - a.angle;
			number.isPolar = true;
		}
		else if(!me.isPolar && !a.isPolar) {
			// Both are rect;
			number.real = (this->real * a.real) + (this->imaginary * a.imaginary) /
				((a.real * a.real) + (a.imaginary * a.imaginary));

			number.imaginary = (this->real * a.real) - (this->imaginary * a.imaginary) /
				((a.real * a.real) + (a.imaginary * a.imaginary));
			number.isPolar = false;
		}

		return number;
	}

	bool operator == (ComplexNumber a) {
		if (this->isPolar && a.isPolar) {
			// If both are polar
			if (this->angle == a.angle && this->radius == a.radius) {
				return true;
			}
			else {
				return false;
			}
		}
		else if(this->isPolar && !a.isPolar) {
			return false;
		}
		else if (!this->isPolar && a.isPolar) {
			return false;
		}
		else {
			// Check whether their values are same
			if (this->real == a.real && this->imaginary == a.imaginary) {
				return true;
			}
			else {
				return false;
			}
		}
	}

	//Properties of the ComplexNumbers
	// Set some checks for polar and non-polar numbers

	int getReal() {
		if (!isPolar) {
			return real;
		}
		else {
			return NULL;
		}
	}

	int getImaginary() {
		if (!isPolar) {
			return imaginary;
		}
		else {
			return NULL;
		}
	}

	// Formula for logarithm ln(z) = ln(|z|) + i(arctan"theta")
	ComplexNumber getLogarithm() {

		ComplexNumber number;
		number.real = log(this->getMagnitude());
		number.imaginary = this->getArgument();
		number.isPolar = false;
		return number;
	}

	ComplexNumber getConjugate() {
		// Get the conjugate to this ComplexNumber
		ComplexNumber number;
		if (this->isPolar) {
			// Number is polar, change the angle
			number.radius = this->radius;
			number.angle = -this->angle;
			number.isPolar = false;
		}
		else {
			// Change the sign
			number.real = this->real;
			number.imaginary = -this->imaginary;
			number.isPolar = false;
		}

		return number;
	}

	long getMagnitude() {
		if (!this->isPolar) {
			return sqrt((this->real * this->real) + (this->imaginary * this->imaginary));
		}
		else {
			return this->radius;
		}
	}

	double getArgument() { 
		if (!this->isPolar) {
			return (atan(this->imaginary / this->real));
		}
		else {
			return this->angle;
		}
	}

	ComplexNumber getReciprocal() {
		// Create a new object
		ComplexNumber number;
		// Run the logic
		number.real = (this->real) / (this->real * this->real + this->imaginary * this->imaginary);
		// Minus sign shows that there is a conjugate of the complex number.
		number.imaginary = -(this->imaginary) / (this->real * this->real + this->imaginary * this->imaginary);

		number.isPolar = false;
		return number;
	}

	std::string toString() {

		// Check whether the complex number is a polar or non-polar number
		std::stringstream stream;
		if (isPolar) {
			// Polar, write in the form of cos, sin and radius
			if (this->angle > 0) {
				// Angle is positive
				stream << this->radius << "(cos" << this->angle << " + isin" << this->angle << ")";
			}
			else {
				stream << this->radius << "(cos" << this->angle << " - isin" << -(this->angle) << ")";
			}
		}
		else {
			if (this->imaginary > 0) {
				// imaginary is positive
				stream << this->real << " + i" << this->imaginary;
			}
			else {
				stream << this->real << " - i" << -(this->imaginary);
			}
		}

		return stream.str();
	}

	// Constructors and Destructors of the objects
public:
	// Simple constructor
	ComplexNumber() {
		this->real = 0;
		this->imaginary = 0;
		this->isPolar = false;
	};

	// Constructor with a few values
	ComplexNumber(bool isPolar, int firstVal, int secondVal){
		ComplexNumber::isPolar = isPolar;
		if (!isPolar) {
			// The complex number is a polar number
			real = firstVal;
			imaginary = secondVal;
		}
		else {
			// The complex number is not a polar
			radius = firstVal;
			angle = secondVal;
		}
	};

	// A destructor for the ComplexNumber class object. 
	~ComplexNumber() {

	};
};

