#include <stdio.h>
#include <string.h>
#include <windows.h>

LARGE_INTEGER nFreq;
LARGE_INTEGER nBeginTime;
LARGE_INTEGER nEndTime;
double time, mps;
const int N = 1000000;
const int T = -12345678;

int main()
{
	QueryPerformanceFrequency(&nFreq);

	char buf[256];

	QueryPerformanceCounter(&nBeginTime); 
	{
		for (int i = 0; i < N; ++ i)
			sprintf(buf, "%d", T);
	}
	QueryPerformanceCounter(&nEndTime);
	printf("%s\n", buf);

	time = (double)(1.0*nEndTime.QuadPart-nBeginTime.QuadPart)/(double)nFreq.QuadPart;
	mps = 1.0*N/time/1024.0/1024.0;
	printf("%s cost %lf s, %lf MI/s\n", "sprintf", time, mps);

	//////////////////////////////////////////////////////////////////////////

	QueryPerformanceCounter(&nBeginTime); 
	{
		for (int i = 0; i < N; ++ i)
			sprintf_s(buf, 256, "%d", T);
	}
	QueryPerformanceCounter(&nEndTime);
	printf("%s\n", buf);

	time = (double)(1.0*nEndTime.QuadPart-nBeginTime.QuadPart)/(double)nFreq.QuadPart;
	mps = 1.0*N/time/1024.0/1024.0;
	printf("%s cost %lf s, %lf MI/s\n", "sprintf_s", time, mps);

	//////////////////////////////////////////////////////////////////////////

	QueryPerformanceCounter(&nBeginTime); 
	{
		for (int i = 0; i < N; ++ i)
			_itoa_s(T, buf, 256, 10);
	}
	QueryPerformanceCounter(&nEndTime);
	printf("%s\n", buf);

	time = (double)(1.0*nEndTime.QuadPart-nBeginTime.QuadPart)/(double)nFreq.QuadPart;
	mps = 1.0*N/time/1024.0/1024.0;
	printf("%s cost %lf s, %lf MI/s\n", "_itoa_s", time, mps);

	return 0;
}