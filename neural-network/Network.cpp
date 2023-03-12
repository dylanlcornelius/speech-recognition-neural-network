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
#include <algorithm>

Network::Network(){
	Momentum = 0;
	epochMSEprev = 0;
}

Network::~Network(){}

extern "C" _declspec(dllexport) Network* CreateNetwork() {
	return new Network();
}

extern "C" _declspec(dllexport) std::vector<Matrix>* CreateMatrixVector(double* data, int rows, int columns) {
	std::vector<Matrix>* matrixList = new std::vector<Matrix>;

	for (int i = 0; i < rows; i++) {
		Matrix in = Matrix(1, columns);
		for (int j = 0; j < columns; j++) {
			in.matrix[0][j] = data[(i * columns) + j];
		}
		matrixList->push_back(in);
	}

	return matrixList;
}

extern "C" _declspec(dllexport) void Init(Network* network, int inputCount, int hiddenCount, int hiddenCount2, int outputCount, char* weightsPath) {
	network->Initialization(inputCount, hiddenCount, hiddenCount2, outputCount);
	network->ReadWeights(weightsPath);
}

extern "C" _declspec(dllexport) double Train(Network* network, std::vector<Matrix>* inputs, std::vector<Matrix>* expected, double learningRate, double momentum) {
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

	Matrix result = network->Run(matrix);

	double* output = new double[result.columns];
	for (int i = 0; i < result.columns; i++) {
		output[i] = result.matrix[0][i];
	}

	return output;
}

//Trains the network for a given set of inputs
double Network::Train(std::vector<Matrix> inputs, std::vector<Matrix> expected, double learningRate, double momentum) {
	if (Momentum == 0) {
		Momentum = momentum;
	}
	double epochMSE = 0;

	std::vector<int> indices(inputs.size());
	for (int i = 0; i < inputs.size(); i++) {
		indices[i] = i;
	}
	std::random_shuffle(indices.begin(), indices.end());

	for (int i = 0; i < indices.size(); i++) {
		Inputs = Matrix(inputs[indices[i]]);
		Matrix Expected = Matrix(expected[indices[i]]);

		Feedforward();
		Backpropagation(Expected);

		epochMSE += MSE(Expected).Sum();

		
		if (i % (int)ceil(indices.size()/3) == 0) {
			SGD(learningRate, momentum);
		}
		
	}
	
	SGD(learningRate, momentum);

	epochMSE = epochMSE / indices.size() * 100;

	epochMSEprev = epochMSE;

	return epochMSE;
}

//Runs the network with a given set of inputs
Matrix Network::Run(Matrix &inputs) {
	Inputs = inputs;

	Feedforward();

	return Outputs.Step();
}

//Randomize the starting weights of the network
void Network::Initialization(int inputCount, int hiddenCount, int hiddenCount2, int outputCount) {
	Weights1 = Matrix(inputCount, hiddenCount);
	dWeights1 = Matrix(inputCount, hiddenCount);
	dWeights1prev = Matrix(inputCount, hiddenCount);
	Bias1 = Matrix(std::vector<std::vector<double> >(1, std::vector<double>(hiddenCount, 1.0)));
	dBias1 = Matrix(1, hiddenCount);
	Activation1 = Matrix(1, hiddenCount);
	Hidden = Matrix(1, hiddenCount);

	Weights2 = Matrix(hiddenCount, hiddenCount2);
	dWeights2 = Matrix(hiddenCount, hiddenCount2);
	dWeights2prev = Matrix(hiddenCount, hiddenCount2);
	Bias2 = Matrix(std::vector<std::vector<double> >(1, std::vector<double>(hiddenCount2, 1.0)));
	dBias2 = Matrix(1, hiddenCount2);
	Activation2 = Matrix(1, hiddenCount2);
	Hidden2 = Matrix(1, hiddenCount2);

	Weights3 = Matrix(hiddenCount2, outputCount);
	dWeights3 = Matrix(hiddenCount2, outputCount);
	dWeights3prev = Matrix(hiddenCount2, outputCount);
	Bias3 = Matrix(std::vector<std::vector<double> >(1, std::vector<double>(outputCount, 1.0)));
	dBias3 = Matrix(1, outputCount);
	Activation3 = Matrix(1, outputCount);
	Outputs = Matrix(1, outputCount);

	std::srand(time(NULL));
	Weights1 = Weights1.ApplyRandomize();
	Weights2 = Weights2.ApplyRandomize();
	Weights3 = Weights3.ApplyRandomize();
}

//Calculate the outputs of the network for the given inputs
void Network::Feedforward() {
	Activation1 = Inputs.Dot(Weights1) + Bias1;
	Hidden = Activation1.ApplySigmoid();

	Activation2 = Hidden.Dot(Weights2) + Bias2;
	Hidden2 = Activation2.ApplySigmoid();

	Activation3 = Hidden2.Dot(Weights3) + Bias3;
	Outputs = Activation3.ApplySoftmax();
}

//Calculate the gradient descents for the network weights.
void Network::Backpropagation(Matrix &expected) {
	dBias3 = (Outputs - expected) * Activation3.ApplySigmoidP();
	dWeights3 = (Hidden2.Transpose().Dot(dBias3));

	dBias2 = dBias3.Dot(Weights3.Transpose()) * (Activation2.ApplySigmoidP());
	dWeights2 = (Hidden.Transpose().Dot(dBias2));

	dBias1 = dBias2.Dot(Weights2.Transpose()) * (Activation1.ApplySigmoidP());
	dWeights1 = (Inputs.Transpose().Dot(dBias1));
}

//Update weights with calculated gradient descents
void Network::SGD(double &learningRate, double &momentum) {
	dWeights1prev = dWeights1prev.MultiplyScalar(momentum) + dWeights1.MultiplyScalar(learningRate);
	Weights1 = Weights1 - dWeights1prev;
	Bias1 = Bias1 - (dBias1.MultiplyScalar(learningRate));
	
	dWeights2prev = dWeights2prev.MultiplyScalar(momentum) + dWeights2.MultiplyScalar(learningRate);
	Weights2 = Weights2 - dWeights2prev;
	Bias2 = Bias2 - (dBias2.MultiplyScalar(learningRate));

	dWeights3prev = dWeights3prev.MultiplyScalar(momentum) + dWeights3.MultiplyScalar(learningRate);
	Weights3 = Weights3 - dWeights3prev;
	Bias3 = Bias3 - (dBias3.MultiplyScalar(learningRate));
}

//Calculating global error of the network
Matrix Network::MSE(Matrix &expected) {
	return ((Outputs - expected) * (Outputs - expected));
}

#pragma region SAVE/LOAD WEIGHTS

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

	file.close();

	return i;
}

void Network::ReadWeights(char* weightsPath) {
	std::fstream file;
	file.open(weightsPath, std::fstream::in | std::fstream::out | std::fstream::app);

	ReadSection(file, &Weights1);
	ReadSection(file, &Bias1);
	ReadSection(file, &Weights2);
	ReadSection(file, &Bias2);
	ReadSection(file, &Weights3);
	ReadSection(file, &Bias3);

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
	WriteSection(file, &Weights3);
	WriteSection(file, &Bias3);

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

#pragma endregion