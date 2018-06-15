#pragma once

#include <vector>

class Matrix
{
public:
	Matrix();
	Matrix(int height, int width);
	Matrix(std::vector<std::vector<double> > const &matrix);

	Matrix add(Matrix const &matrix2);
	Matrix subtract(Matrix const &matrix2);
	Matrix multiply(Matrix const &matrix2);
	int dotProduct(Matrix const &matrix2, int &m1Row, int &m2Col);
	Matrix multiplyScalar(double const &scalar);
	//Matrix divide(Matrix const &matrix2);
	Matrix transpose();
	//Matrix applyFunction();
	
	~Matrix();

private:
	std::vector<std::vector<double> > matrix;
	int rows;
	int columns;
};

