#include "Network.h"
#include "Matrix.h"
#include <vector>

Network::Network(){}

Network::~Network(){}

//Trains the network for a given set of inputs
void Network::Train(Matrix &inputs, int &trainingIterations) {

	for (int i = 0; i < trainingIterations; i++) {
		Initialization();
		Propagation(inputs);
		//ErrorCalculation();
	}

	IsTrained = true;
}

//Runs the network with a given set of inputs
void Network::Run(Matrix &inputs) {
	if (IsTrained) {
		Propagation(inputs);
	}
}

void Network::Initialization() {
	Weights1 = Initialize();
	Weights2 = Initialize();
}

Matrix Network::Initialize() {

}

void Network::Propagation(Matrix &inputs) {
	Hidden = Propagate(inputs, Weights1, Bias1);
	Outputs = Propagate(Hidden, Weights2, Bias2);
}

Matrix Network::Propagate(Matrix &inputs, Matrix &weights, Matrix &bias) {
	return inputs.Multiply(weights).Add(bias).ApplyFunction(HyperbolicTangent);;
}

//Hyperbolic Tangent Function
double HyperbolicTangent(double x) {
	return (exp(2 * x) - 1) / (exp(2 * x) + 1);
}

//Hyperbolic Tangent Function Derivative
double HyperbolicDerivative(double x) {
	return (1 - pow(HyperbolicTangent(x), 2));
}


void Network::ErrorCalculation(Matrix &expected) {

	//Batch Training

	//dBias2 = dBias2.Add(Outputs.Subtract(expected).Product(Hidden.Multiply(Weights2).Add(Bias2).ApplyFunction(HyperbolicDerivative)));
	//dBias1 = dBias1.Add(dBias2.Multiply(Weights2.Transpose().Product(Inputs.Multiply(Weights1).Add(Bias1).ApplyFunction(HyperbolicDerivative))));
	//dWeights2 = dWeights2.Add(Hidden.Transpose().Multiply(dBias2));
	//dWeights1 = dWeights1.Add(Inputs.Transpose().Multiply(dBias1));

	//Weights1 = UpdateWeights();

	//Weights2 = CalculateError(Weights2, Outputs);
	//Weights1 = CalculateError(Weights1, Hidden);

	///double mse = MSE(ideal, Outputs);
}

Matrix Network::UpdateWeights(Matrix &weights, Matrix &gradients) {
	return weights.Subtract(gradients.MultiplyScalar(LEARNING_RATE));
}

/*
double Network::MSE(Matrix &ideal, Matrix &actual) {
	return ideal.Subtract(actual).Power(2).Sum() / ideal.rows;
}
*/