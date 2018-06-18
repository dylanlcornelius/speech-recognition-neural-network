#include "Main.h"

#include "Network.h"
#include "Matrix.h"
#include <vector>
#include <list>

Main::Main(){}
Main::~Main(){}

int main(int argc, char *argb[]) {
	
	Matrix i1 = { std::vector<std::vector<double> >(1, std::vector<double>({0, 0})) };
	Matrix i2 = { std::vector<std::vector<double> >(1, std::vector<double>({1, 0})) };
	Matrix i3 = { std::vector<std::vector<double> >(1, std::vector<double>({0, 1})) };
	Matrix i4 = { std::vector<std::vector<double> >(1, std::vector<double>({1, 1})) };
	std::list<Matrix> inputs = {i1, i2, i3, i4};

	std::vector<double> e1 = { 0 };
	std::vector<double> e2 = { 1 };
	std::vector<double> e3 = { 1 };
	std::vector<double> e4 = { 0 };
	Matrix e = { std::vector<std::vector<double> > { {e1, e2, e3, e4}} };

	int ti = 10;

	Network xor;

	xor.Train(inputs, e, ti);


	Matrix r1 = { std::vector<std::vector<double> >(1, std::vector<double>({ 1, 0 })) };

	xor.Run(r1);
}