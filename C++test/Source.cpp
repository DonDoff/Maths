#include <iostream>

class Matrix
{
public:
	Matrix();
	~Matrix();
	Matrix(int r, int c);

private:

};

Matrix::Matrix()
{
}

Matrix::~Matrix()
{
}

Matrix::Matrix(int r, int c) {

}

class Vector : Matrix
{
public:
	Vector();
	~Vector();
	Vector(int s);

private:

};

Vector::Vector()
{
}

Vector::~Vector()
{
}

Vector::Vector(int s) {

}

int main() {
	Matrix m(2, 2);
	Vector v(2, 2);
	std::cout << "Hallo" << std::endl;
}