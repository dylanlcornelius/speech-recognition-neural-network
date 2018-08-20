#include "Network.h"

#include "Matrix.h"
#include <vector>
#include <list>
#include <iostream>
#include <ctime>

Network::Network(){}

Network::~Network(){}

extern "C" _declspec(dllexport) Network* CreateNetwork() {
	return new Network();
}

extern "C" _declspec(dllexport) std::list<Matrix>* CreateInputs(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4) {
	//std::list<Matrix>* inputs;
	//inputs->push_back({ std::vector<std::vector<double> >(1, std::vector<double>({ x1, arg2 })) });

	Matrix i1 = { std::vector<std::vector<double> >(1, std::vector<double>({ x1, y1 })) };
	Matrix i2 = { std::vector<std::vector<double> >(1, std::vector<double>({ x2, y2 })) };
	Matrix i3 = { std::vector<std::vector<double> >(1, std::vector<double>({ x3, y3 })) };
	Matrix i4 = { std::vector<std::vector<double> >(1, std::vector<double>({ x4, y4 })) };
	std::list<Matrix>* inputs = new std::list<Matrix> { i1, i2, i3, i4 };

	return inputs;
}

extern "C" _declspec(dllexport) std::list<Matrix>* CreateExpected(double x1, double x2, double x3, double x4) {
	//std::list<Matrix>* expected;
	//expected->push_back({ std::vector<std::vector<double> >(1, std::vector<double>({ arg1 })) });

	Matrix e1 = { std::vector<std::vector<double> >(1, std::vector<double>({ x1 })) };
	Matrix e2 = { std::vector<std::vector<double> >(1, std::vector<double>({ x2 })) };
	Matrix e3 = { std::vector<std::vector<double> >(1, std::vector<double>({ x3 })) };
	Matrix e4 = { std::vector<std::vector<double> >(1, std::vector<double>({ x4 })) };
	std::list<Matrix>* expected = new std::list<Matrix> { e1, e2, e3, e4 };

	return expected;
}

extern "C" _declspec(dllexport) void Train(Network* network, std::list<Matrix>* &inputs, std::list<Matrix>* &expected, int &hiddenCount, int &trainingIterations, double &learningRate) {
	network->Train(inputs, expected, hiddenCount, trainingIterations, learningRate);
}

extern "C" _declspec(dllexport)  void Run(Network* network, Matrix &inputs) {
	network->Run(inputs);
}

extern "C" _declspec(dllexport) int HelloWorld() {
	//std::cout << "Hello World!" << std::endl;
	return 117;
}

//Trains the network for a given set of inputs
void Network::Train(std::list<Matrix> &inputs, std::list<Matrix> &expected, int &hiddenCount, int &trainingIterations, double &learningRate) {
	//rows = different inputs, cols = 1 input
	Weights1 =		Matrix(inputs.front().columns, hiddenCount);
	dWeights1 =		Matrix(inputs.front().columns, hiddenCount);
	Bias1 = Matrix(std::vector<std::vector<double> >(1, std::vector<double>(hiddenCount, 1.0)));
	dBias1 =		Matrix(1, hiddenCount);
	Activation1 =	Matrix(1, hiddenCount);
	Hidden =		Matrix(1, hiddenCount);

	Weights2 =		Matrix(hiddenCount, OUTPUT_COUNT);
	dWeights2 =		Matrix(hiddenCount, OUTPUT_COUNT);
	Bias2 = Matrix(std::vector<std::vector<double> >(1, std::vector<double>(OUTPUT_COUNT, 1.0)));
	dBias2 =		Matrix(1, OUTPUT_COUNT);
	Activation2 =	Matrix(1, OUTPUT_COUNT);
	Outputs =		Matrix(1, OUTPUT_COUNT);

	Initialization();

	//per batch
	for (int i = 0; i < trainingIterations; i++) {
		//per input
		double mse = 0;
		auto inputsA = inputs.begin();
		auto expectedA = expected.begin();
		while (inputsA != inputs.end()) {
			Inputs = Matrix(inputsA->matrix);
			Matrix Expected = Matrix(expectedA->matrix);

			Feedforward();
			Backpropagation(Expected);

			mse += MSE(Expected) * MSE(Expected);

			++inputsA;
			++expectedA;
		}

		SGD(learningRate);

		mse = mse / expected.size() * 100;

		PrintBatch(i, mse);

		if (mse < 0.0001)
			break;
	}

	IsTrained = true;
}

//Runs the network with a given set of inputs
void Network::Run(Matrix &inputs) {
	Inputs = inputs;

	if (IsTrained) {
		Feedforward();
		PrintTest(inputs);
	}
}

//Randomize the starting weights of the network
void Network::Initialization() {
	std::srand(time(NULL));
	Weights1 = Weights1.ApplyRandomize();
	Weights2 = Weights2.ApplyRandomize();
}

//Calculate the outputs of the network for the given inputs
void Network::Feedforward() {
	Activation1 = Inputs.Dot(Weights1) + Bias1;
	Hidden = Activation1.ApplySigmoid();

	Activation2 = Hidden.Dot(Weights2) + Bias2;
	Outputs = Activation2.ApplySigmoid();
}

//Calculate the gradient descents for the network weights.
void Network::Backpropagation(Matrix &expected) {
	dBias2 = (Outputs - expected) * Activation2.ApplySigmoidP();
	dWeights2 = dWeights2 + (Hidden.Transpose().Dot(dBias2));

	dBias1 = dBias2.Dot(Weights2.Transpose() * Activation1.ApplySigmoidP());
	dWeights1 = dWeights1 + (Inputs.Transpose().Dot(dBias1));
}

//Update weights with calculated gradient descents
void Network::SGD(double &learningRate) {
	Weights1 = Weights1 - (dWeights1.MultiplyScalar(learningRate));
	Bias1 = Bias1 - (dBias1.MultiplyScalar(learningRate));
	Weights2 = Weights2 - (dWeights2.MultiplyScalar(learningRate));
	Bias2 = Bias2 - (dBias2.MultiplyScalar(learningRate));
}

double Network::MSE(Matrix &expected) {
	return (expected - Outputs).Sum();
}

#pragma region PRINTING FUNCTIONS

void Network::PrintResults(Matrix &expected, int &i) {
	std::cout << "Iteration: " << i+1 << " ";
	std::cout << "Error: " << MSE(expected) << std::endl;
	std::cout << "Input: ";
	Inputs.PrintMatrix();
	std::cout << ", Output: ";
	Outputs.PrintMatrix();
	std::cout << ", Expected: ";
	expected.PrintMatrix();
	std::cout << std::endl;
}

void Network::PrintBatch(int &i, double &mse) {
	std::cout << "Iteration: " << i + 1 << " ";
	std::cout << "Error: " << mse << std::endl;
}

void Network::PrintTest(Matrix &inputs) {
	std::cout << "test: ";
	inputs.PrintMatrix();
	std::cout << "result: ";
	Outputs.Step().PrintMatrix();
	std::cout << std::endl;
}

#pragma endregion 