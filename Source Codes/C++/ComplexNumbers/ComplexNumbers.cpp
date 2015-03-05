// ComplexNumbers.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "ComplexNumber.h"
#include <string>
#include <iostream>

int _tmain(int argc, _TCHAR* argv[])
{
	ComplexNumber number = ComplexNumber::ComplexNumber(false, 2, 4);
	ComplexNumber number1 = ComplexNumber::ComplexNumber(true, 2, 4);
	std::cout << number.toString() << std::endl;
	std::cout << number1.toString() << std::endl;
	number = -number;
	std::cout << number.toString();
	system("pause");
}