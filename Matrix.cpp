#include "Matrix.h"

#include <assert.h>
#include <iostream>

Matrix::Matrix(){}

Matrix::~Matrix(){}

//Set number of rows and colums
Matrix::Matrix(int rows, int columns) {
	this->rows = rows;
	this->columns = columns;
	this->matrix = std::vector<std::vector<double> >(rows, std::vector<double>(columns, 0.0));
}

//Initialize Matrix
Matrix::Matrix(std::vector<std::vector<double> > const &matrix) {
	//checking for valid inputs
	assert(matrix.size() != 0);

	this->rows = matrix.size();
	this->columns = matrix[0].size();
	this->matrix = matrix;
}

//Matrix addition
Matrix Matrix::Add(Matrix const &matrix2) {
	//checking for valid inputs
	assert(rows == matrix2.rows && columns == matrix2.columns);

	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] + matrix2.matrix[i][j];

	return result;
}

//Matrix subtraction
Matrix Matrix::Subtract(Matrix const &matrix2) {
	assert(rows == matrix2.rows && columns == matrix2.columns);

	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] - matrix2.matrix[i][j];

	return result;
}

//Matrix Hadamard multiplication
Matrix Matrix::Multiply(Matrix const &matrix2) {
	assert(rows == matrix2.rows && columns == matrix2.columns);

	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] * matrix2.matrix[i][j];

	return result;
}

//Matrix multiplication
Matrix Matrix::Dot(Matrix const &matrix2) {
	assert(columns == matrix2.rows);

	Matrix result(rows, matrix2.columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < matrix2.columns; j++)
			result.matrix[i][j] = DotProduct(matrix2, i, j);

	return result;
}

//Vector dot product
int Matrix::DotProduct(Matrix const &matrix2, int &m1Row, int &m2Col) {

	int result;

	for (int i = 0; i < rows; i++)
		result += matrix[m1Row][i] * matrix2.matrix[i][m2Col];

	return result;
}

//Matrix scalar multiplication
Matrix Matrix::MultiplyScalar(double const &scalar)
{
	Matrix result(rows, columns);
	
	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] * scalar;

	//return Matrix()?
	return result;
}

//Matrix exponential function
Matrix Matrix::Power(int const exponent) {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			for (int k = 0; k < exponent; k++)
				result.matrix[i][j] *= matrix[i][j];

	return result;
}

//Matrix summation
double Matrix::Sum() {
	double sum = 0;

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			sum += matrix[i][j];

	return sum;
}

/*
Matrix Matrix::Divide(int const divisor) {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] / divisor;

	return result;
}
*/

//Matrix transposition
Matrix Matrix::Transpose() {
	Matrix result(columns, rows);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[j][i] = matrix[i][j];

	return result;
}

//Matrix application of a given function to every element
Matrix Matrix::ApplyFunction(double (*function)(double)) const {
	Matrix result(columns, rows);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = (*function)(matrix[i][j]);

	return result;
}

void Matrix::PrintMatrix() {
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++)
			std::cout << matrix[i][j];
		std::cout << std::endl;
	}
			
}