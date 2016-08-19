// DiskVolumesExplorer.Native.Tester.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "DiskVolumesManager.h"

using namespace DiskVolumesExplorer::Core::Native;

#include <iostream>
using namespace std;

class A
{
public:
	A() { cout << "A()" << endl; }
	~A() { cout << "~A()" << endl; }
};

class B
{
public:
	B() { cout << "B()" << endl; throw exception(); }
	~B() { cout << "~B()" << endl; }

	void M() { cout << "M()" << endl; };
};

class Aggregate
{
public:
	static A a;
	static B b;

	Aggregate() { cout << "Aggregate()" << endl; }
	~Aggregate() { cout << "~Aggregate()" << endl; }
};


int main()
{
	try
	{
		A a;
		B b;
	}
	catch (...)
	{
		cout << "catch" << endl;
	}


	//DiskVolumesManager diskVolumesManager;

	//diskVolumesManager.Connect();

    return 0;
}

