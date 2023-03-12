#include "Matrix.h"

#include <assert.h>
#include <iostream>
#include <Windows.h>

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
	assert(matrix.size() != 0);

	this->rows = matrix.size();
	this->columns = matrix[0].size();
	this->matrix = matrix;
}

//Matrix addition
Matrix Matrix::operator+(Matrix const &matrix2) {
	assert(rows == matrix2.rows && columns == matrix2.columns);

	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] + matrix2.matrix[i][j];

	return result;
}

//Matrix subtraction
Matrix Matrix::operator-(Matrix &matrix2) {
	assert(rows == matrix2.rows && columns == matrix2.columns);

	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] - matrix2.matrix[i][j];

	return result;
}

//Matrix Hadamard multiplication
Matrix Matrix::operator*(Matrix const &matrix2) {
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
double Matrix::DotProduct(Matrix const &matrix2, int &m1Row, int &m2Col) {
	double result = 0;

	for (int i = 0; i < columns; i++)
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

	return result;
}

//Matrix exponential function
Matrix Matrix::operator^(int const exponent) {
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

//Matrix transposition
Matrix Matrix::Transpose() {
	Matrix result(columns, rows);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[j][i] = matrix[i][j];

	return result;
}

//Round outputs near 1 or 0 to exactly 1 or 0
Matrix Matrix::Step() {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++) {
			if (matrix[i][j] < 0.01)
				result.matrix[i][j] = 0;
			else if (matrix[i][j] > 0.99)
				result.matrix[i][j] = 1;
			else
				result.matrix[i][j] = matrix[i][j];
		}
		
	return result;
}

#pragma region FUNCTIONS

//Matrix application of a given function to every element
Matrix Matrix::ApplyRandomize() {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++) {
			result.matrix[i][j] = Randomize(matrix[i][j]);
		}
	}

	return result;
}

//Randomize initial weights
double Matrix::Randomize(double x) {
	double r = (double)(rand() % 10000 + 1) / 10000 - 0.5;
	if (rand() % 2 == 0)
		r = -r;
	return r;
}

//Matrix application of a given function to every element
Matrix Matrix::ApplyHyperbolic() {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = HyperbolicTangent(matrix[i][j]);

	return result;
}

//Hyperbolic Tangent Function
double Matrix::HyperbolicTangent(double x) {
	return (1 - exp(2 * x)) / (1 + exp(-(2 * x)));
}

//Matrix application of a given function to every element
Matrix Matrix::ApplyHyperbolicP() {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = HyperbolicDerivative(matrix[i][j]);

	return result;
}

//Hyperbolic Tangent Function Derivative
double Matrix::HyperbolicDerivative(double x) {
	return (1 - pow(HyperbolicTangent(x), 2));
}

//Matrix application of a given function to every element
Matrix Matrix::ApplySigmoid() {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = Sigmoid(matrix[i][j]);

	return result;
}

//Sigmoid Function
double Matrix::Sigmoid(double x) {
	return 1 / (1 + exp(-x));
}

//Matrix application of a given function to every element
Matrix Matrix::ApplySigmoidP() {
	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = SigmoidDerivative(matrix[i][j]);

	return result;
}

//Sigmoid Derivative Function
double Matrix::SigmoidDerivative(double x) {
	if (x < 0.0000001) {
		x = 0.00001;
	}
	return (exp(-x) / pow((1 + exp(-x)), 2));
}

//Softmax Function
Matrix Matrix::ApplySoftmax() {
	Matrix result(rows, columns);

	double sum = 0.00001;

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++) {
			sum += exp(matrix[i][j]);
		}
	}

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++) {
			result.matrix[i][j] = exp(matrix[i][j]) / sum;
		}
	}

	return result;
}

//Softmax Derivative Function (Doesn't work)
Matrix Matrix::ApplySoftmaxP() {
	assert(rows == 1);
	Matrix result(columns, columns);

	for (int i = 0; i < columns; i++) {
		for (int j = 0; j < columns; j++) {
			if (i == j) {
				result.matrix[i][j] = matrix[0][j] * (1 - matrix[0][j]);
			}
			else {
				result.matrix[i][j] = -matrix[0][j] * matrix[0][i];
			}
		}
	}

	return result;
}

#pragma endregion