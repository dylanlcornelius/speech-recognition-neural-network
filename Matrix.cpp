#include "Matrix.h"

#include <assert.h>

Matrix::Matrix(){}


Matrix::~Matrix(){}

//Set number of rows and colums
Matrix::Matrix(int rows, int columns) {
	this->rows = rows;
	this->columns = columns;
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
Matrix Matrix::add(Matrix const &matrix2) {
	//checking for valid inputs
	assert(rows == matrix2.rows && columns == matrix2.columns);

	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] + matrix2.matrix[i][j];

	return result;
}

//Matrix subtraction
Matrix Matrix::subtract(Matrix const &matrix2) {
	assert(rows == matrix2.rows && columns == matrix2.columns);

	Matrix result(rows, columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] - matrix2.matrix[i][j];

	return result;
}

//Matrix multiplication
Matrix Matrix::multiply(Matrix const &matrix2) {
	assert(columns == matrix2.rows);

	Matrix result(rows, matrix2.columns);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < matrix2.columns; j++)
			result.matrix[i][j] = dotProduct(matrix2, i, j);

	return result;
}

//Vector dot product
int Matrix::dotProduct(Matrix const &matrix2, int &m1Row, int &m2Col) {

	int result;

	for (int i = 0; i < rows; i++)
		result += matrix[m1Row][i] * matrix2.matrix[i][m2Col];

	return result;
}

//Matrix scalar multiplication
Matrix Matrix::multiplyScalar(double const &scalar)
{
	Matrix result(rows, columns);
	
	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[i][j] = matrix[i][j] * scalar;

	//return Matrix()?
	return result;
}

//Matrix transposition
Matrix Matrix::transpose() {
	Matrix result(columns, rows);

	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result.matrix[j][i] = matrix[i][j];

	return result;
}
