#ifndef Calculate
#define Calculate

extern "C"
{
	__declspec(dllexport)int Sum(int a, int b)
	{
		return a + b;
	}
	__declspec(dllexport) int Multiply(int number1, int number2)
	{
		int result = number1 * number2;

		return result;
	}
	__declspec(dllexport) double divide(int number1, int number2)
	{
		double result = 0.0;
		if (number2 != 0)
		 result = number1 / number2;
		return result;
	}
}

#endif

