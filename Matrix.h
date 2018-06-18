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
	std::vector<std::vector<double> > matrix;

	Matrix Add(Matrix const &matrix2);
	Matrix Subtract(Matrix const &matrix2);
	Matrix Multiply(Matrix const &matrix2);
	Matrix Dot(Matrix const &matrix2);
	Matrix MultiplyScalar(double const &scalar);
	Matrix Power(int const exponent);
	double Sum();
	//Matrix Divide(int const divisor);
	Matrix Transpose();
	void PrintMatrix();

	Matrix ApplyRandomize();
	double Randomize(double x);
	Matrix ApplyHyperbolic();
	double HyperbolicTangent(double x);
	Matrix ApplyHyperbolicP();
	double HyperbolicDerivative(double x);
	Matrix ApplySigmoid();
	double Sigmoid(double x);
	Matrix ApplySigmoidP();
	double SigmoidDerivative(double x);

	~Matrix();

private:
	int DotProduct(Matrix const &matrix2, int &m1Row, int &m2Col);
};

