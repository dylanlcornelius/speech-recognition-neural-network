#include "Network.h"

#include "Matrix.h"
#include <vector>
#include <list>
#include <iostream>
#include <ctime>
#include <sstream>
#include <cstdio>
#include <Windows.h>
#include <fstream>

Network::Network(){}

Network::~Network(){}

extern "C" _declspec(dllexport) Network* CreateNetwork() {
	return new Network();
}

extern "C" _declspec(dllexport) std::list<Matrix>* CreateMatrixList(double* data, int rows, int columns) {
	std::list<Matrix>* matrixList = new std::list<Matrix>;

	for (int i = 0; i < rows; i++) {
		Matrix in = Matrix(1, columns);
		for (int j = 0; j < columns; j++) {
			in.matrix[0][j] = data[(i * columns) + j];
		}
		matrixList->push_back(in);
	}

	return matrixList;
}

extern "C" _declspec(dllexport) void Init(Network* network, int inputCount, int hiddenCount, int outputCount, char* weightsPath) {
	network->Initialization(inputCount, hiddenCount, outputCount);
	network->ReadWeights(weightsPath);
}

extern "C" _declspec(dllexport) double Train(Network* network, std::list<Matrix>* inputs, std::list<Matrix>* expected, double learningRate, double momentum) {
	return network->Train(*inputs, *expected, learningRate, momentum);
}

extern "C" _declspec(dllexport) void Export(Network* network, char* weightsPath) {
	network->WriteWeights(weightsPath);
}

extern "C" _declspec(dllexport) int GetInputSize(Network* network, char* weightsPath) {
	return network->ReadSize(weightsPath);
}

extern "C" _declspec(dllexport)  double* Run(Network* network, double* data, int columns) {
	Matrix matrix = Matrix(1, columns);
	for (int i = 0; i < columns; i++) {
		matrix.matrix[0][i] = data[i];
	}

	//AllocConsole();
	//freopen("CONOUT$", "w", stdout);

	//std::cout << matrix.columns << std::endl;

	Matrix result = network->Run(matrix);

	double* output = new double[result.columns];
	for (int i = 0; i < result.columns; i++) {
		output[i] = result.matrix[0][i];
	}

	return output;
}

//Trains the network for a given set of inputs
double Network::Train(std::list<Matrix> inputs, std::list<Matrix> expected, double learningRate, double momentum) {

	double epochMSE = 0;

	//per input
	auto inputsA = inputs.begin();
	auto expectedA = expected.begin();
	while (inputsA != inputs.end()) {
		Inputs = Matrix(inputsA->matrix);
		Matrix Expected = Matrix(expectedA->matrix);

		Feedforward();
		Backpropagation(Expected);

		epochMSE += MSE(Expected).Sum();

		++inputsA;
		++expectedA;
	}

	SGD(learningRate, momentum);

	epochMSE = epochMSE / (expected.size() * expected.front().columns) * 100;

	if (epochMSE < 0.0001 && !IsTrained)
		IsTrained = true;

	return epochMSE;
}

/*
//Trains the network for a given set of inputs
double*  Network::Train(std::list<Matrix> inputs, std::list<Matrix> expected, int hiddenCount, int epochs, double learningRate) {

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

	double* epochMSE = new double[epochs];

	Initialization();

	//per batch
	for (int i = 0; i < epochs; i++) {
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
		epochMSE[i] = mse;

		//if (mse < 0.0001)
		//	break;
	}

	IsTrained = true;

	return epochMSE;
}
*/

//Runs the network with a given set of inputs
Matrix Network::Run(Matrix &inputs) {
	Inputs = inputs;

	if (IsTrained) {
		Feedforward();
		//PrintTest(inputs);
	}

	return Outputs.Step();
}

int Network::ReadSize(char* weightsPath) {
	std::fstream file;
	file.open(weightsPath, std::fstream::in);

	std::string v;
	int i = 0;
	while (std::getline(file, v, ',')) {
		if (v[0] == ':') {
			break;
		}
		if (v[0] == '/') {
			i++;
		}
	}

	return i;
}

void Network::ReadWeights(char* weightsPath) {
	std::fstream file;
	file.open(weightsPath, std::fstream::in | std::fstream::out | std::fstream::app);
	//AllocConsole();
	//freopen("CONOUT$", "w", stdout);

	ReadSection(file, &Weights1);
	ReadSection(file, &Bias1);
	ReadSection(file, &Weights2);
	ReadSection(file, &Bias2);

	file.close();
}

void Network::ReadSection(std::fstream &file, Matrix *matrix) {
	std::string v;
	int i = 0;
	int j = 0;
	while (std::getline(file, v, ',')) {
		if (v[0] == ':') {
			break;
		}
		if (v[0] == '/') {
			j = 0;
			i++;
			continue;
		}
		
		std::istringstream s(v);
		double x;
		(s >> x);
		matrix->matrix[i][j] = x;
		j++;
	}
}

void Network::WriteWeights(char* weightsPath) {
	std::fstream file;
	remove(weightsPath);
	file.open(weightsPath, std::fstream::in | std::fstream::out | std::fstream::app);

	WriteSection(file, &Weights1);
	WriteSection(file, &Bias1);
	WriteSection(file, &Weights2);
	WriteSection(file, &Bias2);

	file.close();
}

void Network::WriteSection(std::fstream &file, Matrix *matrix) {
	for (int i = 0; i < matrix->rows; i++) {
		for (int j = 0; j < matrix->columns; j++) {
			file << matrix->matrix[i][j] << ',';
		}
		file << "/,";
	}
	file << ":,";
}

//Randomize the starting weights of the network
void Network::Initialization(int inputCount, int hiddenCount, int outputCount) {
	Weights1 = Matrix(inputCount, hiddenCount);
	dWeights1 = Matrix(inputCount, hiddenCount);
	dWeights1prev = Matrix(inputCount, hiddenCount);
	Bias1 = Matrix(std::vector<std::vector<double> >(1, std::vector<double>(hiddenCount, 1.0)));
	dBias1 = Matrix(1, hiddenCount);
	Activation1 = Matrix(1, hiddenCount);
	Hidden = Matrix(1, hiddenCount);

	Weights2 = Matrix(hiddenCount, outputCount);
	dWeights2 = Matrix(hiddenCount, outputCount);
	dWeights2prev = Matrix(hiddenCount, outputCount);
	Bias2 = Matrix(std::vector<std::vector<double> >(1, std::vector<double>(outputCount, 1.0)));
	dBias2 = Matrix(1, outputCount);
	Activation2 = Matrix(1, outputCount);
	Outputs = Matrix(1, outputCount);

	std::srand(time(NULL));
	Weights1 = Weights1.ApplyRandomize();
	Weights2 = Weights2.ApplyRandomize();
}

//Calculate the outputs of the network for the given inputs
void Network::Feedforward() {
	Activation1 = Inputs.Dot(Weights1) + Bias1;
	Hidden = Activation1.ApplySigmoid();
	//Hidden = Activation1.ApplyHyperbolic();

	Activation2 = Hidden.Dot(Weights2) + Bias2;
	Outputs = Activation2.ApplySigmoid();
	//Outputs = Activation2.ApplyHyperbolic();
}

//Calculate the gradient descents for the network weights.
void Network::Backpropagation(Matrix &expected) {
	dBias2 = (Outputs - expected) * Activation2.ApplySigmoidP(); //- (y - y)
	//dBias2 = (Outputs - expected) * Activation2.ApplyHyperbolicP();
	dWeights2 = (Hidden.Transpose().Dot(dBias2));

	//dBias1 = dBias2.Dot(Weights2.Transpose() * Activation1.ApplySigmoidP());
	dBias1 = dBias2.Dot(Weights2.Transpose()) * Activation1.ApplySigmoidP();
	//dBias1 = dBias2.Dot(Weights2.Transpose()) * Activation1.ApplyHyperbolicP();
	dWeights1 = (Inputs.Transpose().Dot(dBias1));
}

//Update weights with calculated gradient descents
void Network::SGD(double &learningRate, double &momentum) {
	dWeights1prev = dWeights1prev.MultiplyScalar(momentum) + dWeights1.MultiplyScalar(learningRate);
	Weights1 = Weights1 - dWeights1prev;
	Bias1 = Bias1 - (dBias1.MultiplyScalar(learningRate));
	
	dWeights2prev = dWeights2prev.MultiplyScalar(0.5) + dWeights2.MultiplyScalar(learningRate);
	Weights2 = Weights2 - dWeights2prev;
	Bias2 = Bias2 - (dBias2.MultiplyScalar(learningRate));
}

Matrix Network::MSE(Matrix &expected) {
	return ((Outputs - expected) * (Outputs - expected));
}

#pragma region PRINTING FUNCTIONS

void Network::PrintResults(Matrix &expected, int &i) {
	std::cout << "Iteration: " << i+1 << " ";
	//std::cout << "Error: " << MSE(expected) << std::endl;
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