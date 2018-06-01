#include "Matrix.h"

#include <assert.h>

Matrix::Matrix(){}


Matrix::~Matrix(){}

Matrix::Matrix(int rows, int columns) {
	this->rows = rows;
	this->columns = columns;
}

Matrix::Matrix(std::vector<std::vector<double> > const &array) {
	//checking for valid inputs
	assert(array.size() != 0);
	this->rows = array.size();
	this->columns = array[0].size();
	this->array = array;
}

Matrix Matrix::add(Matrix const &m) {
	//checking for valid inputs
	assert(rows == m.rows && columns == m.columns);

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++) {

		}
	}
}

Matrix Matrix::subtract(Matrix const &m) {
	assert(rows == m.rows && columns == m.columns);


}

Matrix Matrix::multiplyScalar(double const &scalar)
{
	Matrix result(rows, columns);
	
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++) {
			result.array[i][j] = array[i][j] * scalar;
		}
	}

	return Matrix();
}
