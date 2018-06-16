#include "Network.h"

#include "Matrix.h"
#include <vector>
#include <list>

Network::Network(){}

Network::~Network(){}

//Trains the network for a given set of inputs
void Network::Train(std::list<Matrix> &inputs, Matrix &expected, int &trainingIterations) {
	Weights1 = Matrix(HIDDEN_COUNT, inputs.front().rows);
	Bias1 = Matrix(HIDDEN_COUNT, 1);
	Activation1 = Matrix(HIDDEN_COUNT, 1);
	Hidden = Matrix(HIDDEN_COUNT, 1);

	Weights1 = Matrix(OUTPUT_COUNT, HIDDEN_COUNT);
	Bias2 = Matrix(OUTPUT_COUNT, 1);
	Activation2 = Matrix(OUTPUT_COUNT, 1);
	Outputs = Matrix(OUTPUT_COUNT, 1);

	Initialization();

	//per batch
	for (int i = 0; i < trainingIterations; i++) {

		//per input
		for (std::list<Matrix>::const_iterator input = inputs.begin; input != inputs.end();  input++) {
			Inputs = Matrix(input->rows, 1);

			Feedforward();
			Backpropagation(expected);
		}
		Weights1 = Weights1.Subtract(dWeights1.MultiplyScalar(LEARNING_RATE));
		Bias1 = Bias1.Subtract(dBias1.MultiplyScalar(LEARNING_RATE));
		Weights2 = Weights2.Subtract(dWeights2.MultiplyScalar(LEARNING_RATE));
		Bias2 = Bias2.Subtract(dBias2.MultiplyScalar(LEARNING_RATE));
	
		//print global errors, output, and expected?
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

void Network::Initialization() {
	Weights1 = Weights1.ApplyFunction(Randomize);
	Weights2 = Weights2.ApplyFunction(Randomize);
}

void Network::Feedforward() {
	Activation1 = Inputs.Dot(Weights1).Add(Bias1);
	Hidden = Activation1.ApplyFunction(HyperbolicTangent);

	Activation2 = Hidden.Dot(Weights2).Add(Bias2);
	Outputs = Activation2.ApplyFunction(HyperbolicTangent);
}

void Network::Backpropagation(Matrix &expected) {
	dBias2 = Outputs.Subtract(expected).Multiply(Activation2.ApplyFunction(HyperbolicDerivative));
	dWeights2 = dWeights2.Add(Hidden.Transpose().Dot(dBias2));

	dBias1 = dBias2.Dot(Weights2.Transpose().Multiply(Activation1.ApplyFunction(HyperbolicDerivative)));
	dWeights1 = dWeights1.Add(Inputs.Transpose().Dot(dBias1));
}

#pragma region FUNCTIONS

//Randomize initial weights
double Randomize(double x) {
	double r = (double)(rand() % 10000 + 1) / 10000 - 0.5;
	if (rand() % 2 == 0)
		r = -r;
	return r;
}

//Hyperbolic Tangent Function
double HyperbolicTangent(double x) {
	return (exp(2 * x) - 1) / (exp(2 * x) + 1);
}

//Hyperbolic Tangent Function Derivative
double HyperbolicDerivative(double x) {
	return (1 - pow(HyperbolicTangent(x), 2));
}

#pragma endregion