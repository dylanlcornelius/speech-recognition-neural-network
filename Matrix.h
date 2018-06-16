#pragma once

#include <vector>

class Matrix
{
public:
	Matrix();
	Matrix(int rows, int columns);
	Matrix(std::vector<std::vector<double> > const &matrix);

	int rows;
	int columns;

	Matrix Add(Matrix const &matrix2);
	Matrix Subtract(Matrix const &matrix2);
	Matrix Multiply(Matrix const &matrix2);
	Matrix Dot(Matrix const &matrix2);
	int DotProduct(Matrix const &matrix2, int &m1Row, int &m2Col);
	Matrix MultiplyScalar(double const &scalar);
	Matrix Power(int const exponent);
	double Sum();
	//Matrix Divide(int const divisor);
	Matrix Transpose();
	Matrix ApplyFunction(double(*function)(double)) const;

	~Matrix();

private:
	std::vector<std::vector<double> > matrix;
};

