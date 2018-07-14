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

	Matrix operator+(Matrix const &matrix2);
	Matrix operator-(Matrix &matrix2);
	Matrix operator*(Matrix const &matrix2);
	Matrix Dot(Matrix const &matrix2);
	Matrix MultiplyScalar(double const &scalar);
	Matrix operator^(int const exponent);
	double Sum();
	//Matrix Divide(int const divisor);
	Matrix Transpose();
	Matrix Step();
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
	double DotProduct(Matrix const &matrix2, int &m1Row, int &m2Col);
};

