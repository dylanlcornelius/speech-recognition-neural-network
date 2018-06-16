#pragma once

#include "Matrix.h"

class Network
{
public:
	Network();

	void Train(Matrix &inputs, Matrix &expected, int &trainingIterations);
	void Run(Matrix &inputs);

	~Network();

private:
	int const HIDDEN_COUNT = 2;
	int const OUTPUT_COUNT = 1;
	double const LEARNING_RATE = -0.1;

	Matrix Inputs;

	Matrix Weights1;
	Matrix Bias1;
	Matrix Activation1;
	Matrix Hidden;

	Matrix Weights2;
	Matrix Bias2;
	Matrix Activation2;
	Matrix Outputs;

	Matrix dBias2;
	Matrix dBias1;
	Matrix dWeights2;
	Matrix dWeights1;

	//Temporary assertion while weights are not stored.
	bool IsTrained;

	void Initialization();
	void Feedforward();
	void Backpropagation(Matrix &expected);
	//double MSE(Matrix &weights, Matrix &outputs);
};

