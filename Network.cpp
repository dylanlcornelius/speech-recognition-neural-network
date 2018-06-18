#include "Network.h"

#include "Matrix.h"
#include <vector>
#include <list>
#include <iostream>

Network::Network(){}

Network::~Network(){}

//Trains the network for a given set of inputs
void Network::Train(std::list<Matrix> &inputs, Matrix &expected, int &trainingIterations) {
	//rows = different inputs, cols = 1 input
	Weights1 =		Matrix(inputs.front().rows, HIDDEN_COUNT);
	Bias1 =			Matrix(1, HIDDEN_COUNT);
	Activation1 =	Matrix(1, HIDDEN_COUNT);
	Hidden =		Matrix(1, HIDDEN_COUNT);

	Weights1 =		Matrix(HIDDEN_COUNT, OUTPUT_COUNT);
	Bias2 =			Matrix(1, OUTPUT_COUNT);
	Activation2 =	Matrix(1, OUTPUT_COUNT);
	Outputs =		Matrix(1, OUTPUT_COUNT);

	Initialization();

	//per batch
	for (int i = 0; i < trainingIterations; i++) {
		//per input
		for (std::list<Matrix>::const_iterator input = inputs.begin(); input != inputs.end();  input++) {
			Inputs = Matrix(input->matrix);

			Feedforward();
			Backpropagation(expected);

			PrintResults(expected);
		}
		Weights1 = Weights1.Subtract(dWeights1.MultiplyScalar(LEARNING_RATE));
		Bias1 = Bias1.Subtract(dBias1.MultiplyScalar(LEARNING_RATE));
		Weights2 = Weights2.Subtract(dWeights2.MultiplyScalar(LEARNING_RATE));
		Bias2 = Bias2.Subtract(dBias2.MultiplyScalar(LEARNING_RATE));
	}

	IsTrained = true;
}

//Runs the network with a given set of inputs
void Network::Run(Matrix &inputs) {
	if (IsTrained) {
		Feedforward();
		Outputs.PrintMatrix();
	}
}

//Randomize the starting weights of the network
void Network::Initialization() {
	Weights1 = Weights1.ApplyRandomize();
	Weights2 = Weights2.ApplyRandomize();
}

//Calculate the outputs of the network for the given inputs
void Network::Feedforward() {
	Activation1 = Inputs.Dot(Weights1).Add(Bias1);
	Hidden = Activation1.ApplySigmoid();

	Activation2 = Hidden.Dot(Weights2).Add(Bias2);
	Outputs = Activation2.ApplySigmoid();
}

//Calculate the gradient descents for the network weights.
void Network::Backpropagation(Matrix &expected) {
	dBias2 = Outputs.Subtract(expected).Multiply(Activation2.ApplySigmoidP());
	dWeights2 = dWeights2.Add(Hidden.Transpose().Dot(dBias2));

	dBias1 = dBias2.Dot(Weights2.Transpose().Multiply(Activation1.ApplySigmoidP()));
	dWeights1 = dWeights1.Add(Inputs.Transpose().Dot(dBias1));
}

double Network::MSE(Matrix & expected) {
	return expected.Subtract(Outputs).Sum() / expected.columns;
}

void Network::PrintResults(Matrix &expected) {
	std::cout << "Error: " << MSE(expected) << std::endl;
	std::cout << "Input: ";
	Inputs.PrintMatrix();
	std::cout << ", Output: ";
	Outputs.PrintMatrix();
	std::cout << ", Expected: ";
	expected.PrintMatrix();
	std::cout << std::endl;
}