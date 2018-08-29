#pragma once

#include "Matrix.h"
#include <list>

/*
extern "C" {
	void Train(Network &network, std::list<Matrix> &inputs, std::list<Matrix> &expected, int &hiddenCount, int &trainingIterations, double &learningRate);
	void Run(Network &network, Matrix &inputs);
}
*/

class Network
{
public:
	Network();

	void Initialization(std::list<Matrix> inputs, int hiddenCount, int outputCount);
	void ReadWeights(char* weightsPath);
	void WriteWeights(char* weightsPath);
	//void Train(int hiddenCount, int epochs, double learningRate);
	double Train(std::list<Matrix> inputs, std::list<Matrix> expected, double learningRate);
	//double* Train(std::list<Matrix> inputs, std::list<Matrix> expected, int hiddenCount, int epochs, double learningRate);
	void Run(Matrix &inputs);

	~Network();

private:
	double epochMSE;

	Matrix Inputs;

	Matrix Weights1;
	Matrix Bias1;
	Matrix Activation1;
	Matrix Updates1;
	Matrix Hidden;

	Matrix Weights2;
	Matrix Bias2;
	Matrix Activation2;
	Matrix Outputs;

	Matrix dWeights1;
	Matrix dWeights2;
	Matrix dBias1;
	Matrix dBias2;


	//Temporary assertion while weights are not stored.
	bool IsTrained;

	void ReadSection(std::fstream &file, Matrix *matrix);
	void WriteSection(std::fstream &file, Matrix *matrix);

	void Feedforward();
	void Backpropagation(Matrix &expected);
	void SGD(double &learningRate);
	double MSE(Matrix &expected);
	void PrintResults(Matrix &expected, int &i);
	void PrintBatch(int &i, double &mse);
	void PrintTest(Matrix &inputs);
};

