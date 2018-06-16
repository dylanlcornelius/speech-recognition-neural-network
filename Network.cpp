#include "Network.h"
#include "Matrix.h"
#include <vector>

Network::Network(){}

Network::~Network(){}

//Trains the network for a given set of inputs
void Network::Train(Matrix &inputs, Matrix &expected, int &trainingIterations) {
	//rows = n, cols = 1
	Inputs = inputs;

	Weights1 = Matrix(HIDDEN_COUNT, Inputs.rows);
	Bias1 = Matrix(HIDDEN_COUNT, 1);
	Activation1 = Matrix(HIDDEN_COUNT, 1);
	Hidden = Matrix(HIDDEN_COUNT, 1);

	Weights1 = Matrix(OUTPUT_COUNT, HIDDEN_COUNT);
	Bias2 = Matrix(OUTPUT_COUNT, 1);
	Activation2 = Matrix(OUTPUT_COUNT, 1);
	Outputs = Matrix(OUTPUT_COUNT, 1);

	for (int i = 0; i < trainingIterations; i++) {
		Initialization();
		Feedforward();
		Backpropagation(expected);
	}

	IsTrained = true;
}

//Runs the network with a given set of inputs
void Network::Run(Matrix &inputs) {
	if (IsTrained) {
		Feedforward();
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

	//Matrix z4 = Hidden2.Dot(Weights3).Add(Bias3).ApplyFunction(HyperbolicDerivative);
	//dBias3 = Outputs.Subtract(expected).Multiply(z4);

	//Matrix z3 = Feedforward(Hidden, Weights2, Bias2).ApplyFunction(HyperbolicDerivative);
	dBias2 = Outputs.Subtract(expected).Multiply(Activation2.ApplyFunction(HyperbolicDerivative));
	dWeights2 = dWeights2.Add(Hidden.Transpose().Dot(dBias2));

	//Matrix z2 = Feedforward(Inputs, Weights1, Bias1).ApplyFunction(HyperbolicDerivative);
	dBias1 = dBias2.Dot(Weights2.Transpose().Multiply(Activation1.ApplyFunction(HyperbolicDerivative)));
	dWeights1 = dWeights1.Add(Inputs.Transpose().Dot(dBias1));

	Weights1 = Weights1.Subtract(dBias1.MultiplyScalar(LEARNING_RATE));
	Weights2 = Weights2.Subtract(dBias2.MultiplyScalar(LEARNING_RATE));

	//Weights2 = CalculateError(Weights2, Outputs);
	//Weights1 = CalculateError(Weights1, Hidden);

	//double mse = MSE(ideal, Outputs);
}

/*
double Network::MSE(Matrix &ideal, Matrix &actual) {
	return ideal.Subtract(actual).Power(2).Sum() / ideal.rows;
}
*/

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