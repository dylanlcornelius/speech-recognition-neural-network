#pragma once

#include "Matrix.h"
#include <list>

class Network
{
public:
	Network();

	Matrix Inputs;

	void Initialization(int inputCount, int hiddenCount, int hiddenCount2, int outputCount);
	int ReadSize(char* weightsPath);
	void ReadWeights(char* weightsPath);
	void WriteWeights(char* weightsPath);
	double Train(std::vector<Matrix> inputs, std::vector<Matrix> expected, double learningRate, double momentum);
	Matrix Run(Matrix &inputs);

	~Network();

private:
	double Momentum;
	double epochMSEprev;

	Matrix Weights1;
	Matrix Bias1;
	Matrix Activation1;
	Matrix Updates1;
	Matrix Hidden;

	Matrix Weights2;
	Matrix Bias2;
	Matrix Activation2;
	Matrix Hidden2;

	Matrix Weights3;
	Matrix Bias3;
	Matrix Activation3;
	Matrix Outputs;

	Matrix dWeights1;
	Matrix dWeights1prev;
	Matrix dWeights2;
	Matrix dWeights2prev;
	Matrix dWeights3;
	Matrix dWeights3prev;
	Matrix dBias1;
	Matrix dBias2;
	Matrix dBias3;

	void ReadSection(std::fstream &file, Matrix *matrix);
	void WriteSection(std::fstream &file, Matrix *matrix);

	void Feedforward();
	void Backpropagation(Matrix &expected);
	void SGD(double &learningRate, double &momentum);
	Matrix MSE(Matrix &expected);
};

