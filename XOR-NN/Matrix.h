#pragma once

#include <vector>

class Matrix
{
public:
	Matrix();
	Matrix(int height, int width);
	Matrix(std::vector<std::vector<double> > const &array);

	Matrix add(Matrix const &m);
	Matrix subtract(Matrix const &m);
	Matrix multiply(Matrix const &m);
	Matrix multiplyScalar(double const &value);
	Matrix dotProduct(Matrix const &m);
	Matrix divide(Matrix const &m);
	Matrix transpose();
	Matrix applyFunction();
	
	~Matrix();

private:
	std::vector<std::vector<double> > array;
	int rows;
	int columns;
};

