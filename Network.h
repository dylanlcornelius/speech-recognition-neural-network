#pragma once

#include "Matrix.h"

class Network
{
public:
	Network();

	void Train(Matrix &inputs, int &trainingIterations);
	void Run(Matrix &inputs);

	~Network();

private:
	double const LEARNING_RATE = -0.1;

	Matrix Inputs;
	Matrix Weights1;
	Matrix Bias1;
	Matrix Hidden;
	Matrix Weights2;
	Matrix Bias2;
	Matrix Outputs;

	Matrix dBias2;
	Matrix dBias1;
	Matrix dWeights2;
	Matrix dWeights1;

	//Temporary assertion while weights are not stored.
	bool IsTrained;

	void Initialization();
	Matrix Initialize();
	void Propagation(Matrix &inputs);
	Matrix Propagate(Matrix &inputs, Matrix &weights, Matrix &bias);
	void ErrorCalculation(Matrix &expected);
	Matrix UpdateWeights(Matrix &weights, Matrix &gradients);
	//double MSE(Matrix &weights, Matrix &outputs);
};

