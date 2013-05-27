#include <stdio.h>
#include <string.h>
#include <windows.h>

#include "behavior_tree.h"
#include "behavior_composite_node.h"
#include "behavior_condition_node.h"
#include "behavior_decorator_node.h"
#include "behavior_action_node.h"
#include "BaseFunc.h"

LARGE_INTEGER nFreq;
LARGE_INTEGER nBeginTime;
LARGE_INTEGER nEndTime;
double c_time, mps;

using namespace BehaviorTree;

class PrintfDecoratorCounter : public DecoratorCounterNode
{
public:
	NodeFactoryDef(PrintfDecoratorCounter);

	virtual NodeExecState Execute()
	{
		NodeExecState ret = DecoratorCounterNode::Execute();
		if (ret != NodeExec_Fail)
		{
			AuxBBoard()["limit"] = m_pProto->limit_cnt();
			AuxBBoard()["cnt"] = m_nCount;
		}
		return ret;
	}
};

class PrintfCondtion : public ConditionNode
{
public:
	NodeFactoryDef(PrintfCondtion);

	virtual NodeExecState	Execute()
	{
		if (AuxBBoard()["tick"].Get<int>() > 0)
			srand(AuxBBoard()["tick"].Get<int>());

		if (rand() % 2)
		{
			printf("rand ok\n");
			return NodeExec_Success;
		}
		return NodeExec_Fail;
	}
};

class PrintfAction : public ActionNode
{
public:
	NodeFactoryDef(PrintfAction);

	virtual NodeExecState	Execute()
	{
		printf("hello, cnt %d\n", (int)AuxBBoard()["cnt"]);
		return NodeExec_Success;
	}

};

const int N = 100000;
const int SR = 123;

void test_tree_r()
{
	BehaviorTree::BlackBoard board;
	BehaviorTree::Tree* tree = BehaviorTree::TreeFactory::GetInstance()->CreateTree("test_tree_r", NULL, &board);

	board["tick"] = (unsigned int)nFreq.QuadPart;

	for (int i = 0; i < 30; i ++)
	{
		printf("i = %d\n", i);
		int ret = tree->Process();
		printf("ret = %d\n", ret);
	}

	delete tree;
}

//////////////////////////////////////////////////////////////////////////
#define TEST_OPT
#define PRINT //printf

class CntTestAction : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction);

	CntTestAction()
	{
// 		p_j = NULL;
// 		p_m = NULL;
	}

	virtual NodeExecState Execute()
	{
#ifdef TEST_OPT
		if (p_j.IsNull())
			p_j = GetBlackboard()->LookupValue("j");
		int j = p_j->Get<int>();
		if (p_m.IsNull())
			p_m = GetBlackboard()->WriteValue("m", 0);
		p_m->Set(j%3);
		PRINT("j = %d, m = %d\n", j, j%3);
#else
		AuxBBoard()["m"] = (int)AuxBBoard()["j"] % 3;
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInkPtr p_j;
	ChalkInkPtr p_m;
};

class CntTestAction0 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction0);

	CntTestAction0()
	{
// 		p_cnt = NULL;
// 		p_ret = NULL;
// 		p_m = NULL;
// 		p_j = NULL;
	}

	virtual NodeExecState Execute()
	{
		int m;
		int cnt;
		int ret;
		int j;

#ifdef TEST_OPT
		if (p_cnt.IsNull())
			p_cnt = GetBlackboard()->LookupValue("cnt");
		if (p_ret.IsNull())
			p_ret = GetBlackboard()->LookupValue("ret");
		if (p_m.IsNull())
			p_m = GetBlackboard()->LookupValue("m");
		if (p_j.IsNull())
			p_j = GetBlackboard()->LookupValue("j");

		m = p_m->Get<int>();
		cnt = p_cnt->Get<int>();
		ret = p_ret->Get<int>();
		j = p_j->Get<int>();

		if (m != 0)
			return NodeExec_Fail;
		cnt ++;
		if (ret < j*m*100)
			cnt ++;
		p_cnt->Set(cnt);

#else
		m = AuxBBoard()["m"];
		cnt = AuxBBoard()["cnt"];
		ret = AuxBBoard()["ret"];
		j = AuxBBoard()["j"];

		if (m != 0)
			return NodeExec_Fail;
		cnt ++;
		if (ret < j*m*100)
			cnt ++;
		
		AuxBBoard()["cnt"] = cnt;
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInkPtr p_j;
	ChalkInkPtr p_cnt;
	ChalkInkPtr p_ret;
	ChalkInkPtr p_m;
};

class CntTestAction1 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction1);

	CntTestAction1()
	{
// 		p_cnt = NULL;
// 		p_ret = NULL;
// 		p_m = NULL;
// 		p_j = NULL;
	}

	virtual NodeExecState Execute()
	{
		int m;
		int j;
		int cnt;
		int ret;

#ifdef TEST_OPT
		if (p_cnt.IsNull())
			p_cnt = GetBlackboard()->LookupValue("cnt");
		if (p_ret.IsNull())
			p_ret = GetBlackboard()->LookupValue("ret");
		if (p_m.IsNull())
			p_m = GetBlackboard()->LookupValue("m");
		if (p_j.IsNull())
			p_j = GetBlackboard()->LookupValue("j");

		m = p_m->Get<int>();
		j = p_j->Get<int>();
		cnt = p_cnt->Get<int>();
		ret = p_ret->Get<int>();

		if (m != 1)
			return NodeExec_Fail;
		cnt --;
		if (ret > j*m*100)
			cnt --;

		p_cnt->Set(cnt);

#else
		m = AuxBBoard()["m"];
		j = AuxBBoard()["j"];
		cnt = AuxBBoard()["cnt"];
		ret = AuxBBoard()["ret"];

		if (m != 1)
			return NodeExec_Fail;
		cnt --;
		if (ret > j*m*100)
			cnt --;

		AuxBBoard()["cnt"] = cnt;
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInkPtr p_cnt;
	ChalkInkPtr p_ret;
	ChalkInkPtr p_m;
	ChalkInkPtr p_j;
};

class CntTestAction2 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction2);

	CntTestAction2()
	{
// 		p_m = NULL;
// 		p_ret = NULL;
// 		p_absret = NULL;
	}

	virtual NodeExecState Execute()
	{
		int m;
		int ret;

#ifdef TEST_OPT
		if (p_m.IsNull())
			p_m = GetBlackboard()->LookupValue("m");
		if (p_ret.IsNull())
			p_ret = GetBlackboard()->LookupValue("ret");
		if (p_absret.IsNull())
			p_absret = GetBlackboard()->WriteValue("absret", 0);

		m = p_m->Get<int>();
		ret = p_ret->Get<int>();
		if (m != 2)
			return NodeExec_Fail;

		p_absret->Set(abs(ret));
		PRINT("abs_ret = %d\n", abs(ret));

#else
		m = AuxBBoard()["m"];
		ret = AuxBBoard()["ret"];

		if (m != 2)
			return NodeExec_Fail;

		AuxBBoard()["absret"] = abs(ret);
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInkPtr p_m;
	ChalkInkPtr p_ret;
	ChalkInkPtr p_absret;
};

class CntTestAction2_2 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction2_2);

	CntTestAction2_2()
	{
// 		p_cnt = NULL;
// 		p_rand2 = NULL;
	}

	virtual NodeExecState Execute()
	{
		int cnt;
		int rand2;

#ifdef TEST_OPT
		if (p_cnt.IsNull())
			p_cnt = GetBlackboard()->LookupValue("cnt");
		if (p_rand2.IsNull())
			p_rand2 = GetBlackboard()->LookupValue("rand2");

		PRINT("rand2 = %d\n", p_rand2->Get<int>());
		cnt = p_cnt->Get<int>() + p_rand2->Get<int>();
		p_cnt->Set(cnt);

#else
		cnt = AuxBBoard()["cnt"];
		rand2 = AuxBBoard()["rand2"];

		cnt += rand2;
		AuxBBoard()["cnt"] = cnt;
#endif
		
		if (cnt % 5 == 0)
			return NodeExec_Fail;
		return NodeExec_Success;
	}

protected:
	ChalkInkPtr p_cnt;
	ChalkInkPtr p_rand2;
};

//////////////////////////////////////////////////////////////////////////
void test_tree()
{
	BehaviorTree::BlackBoard board;
	BehaviorTree::Tree* tree = BehaviorTree::TreeFactory::GetInstance()->CreateTree("test_tree", NULL, &board);

// 	board["test111"] = 123;
// 	board["test111"] = 526;
// 	int a = (int)board["test111"];
// 	int b = (int)board["test1112"];
// 	char* szb = (char*)board["test1112"];
// 	std::string sb = (std::string)board["test1112"];

	int ret = 0;
	srand(SR);

	board["ret"] = 0;
	for (int i = 0; i < N; ++ i)
	{
		PRINT("-------\ni = %d\n", i);
		board["cnt"] = 0;
		tree->Process();
		int cnt = (int)board["cnt"];
		ret += cnt;
		board["ret"] = ret;
		PRINT("ret = %d, cnt = %d\n", ret, cnt);
	}

	printf("%d\n", ret);
	delete tree;
}

void test_non_tree()
{
	int ret = 0;
	srand(SR);

	G_RANDOM_MGR m_rand0;
	G_RANDOM_MGR m_rand1;
	G_RANDOM_MGR m_rand2;

	m_rand0.reset_seed(m_rand0.RANDOM_int(-0xfffffff, 0xfffffff, 0));
	m_rand1.reset_seed(m_rand1.RANDOM_int(-0xfffffff, 0xfffffff, 0));
	m_rand2.reset_seed(m_rand2.RANDOM_int(-0xfffffff, 0xfffffff, 0));

	PRINT("rand0 seed = %d\n", m_rand0.m_base_seed);
	PRINT("rand1 seed = %d\n", m_rand1.m_base_seed);
	PRINT("rand2 seed = %d\n", m_rand2.m_base_seed);

	for (int i = 0; i < N; ++ i)
	{
		PRINT("-------\ni = %d\n", i);
		int cnt = 0;
		int rand0 = m_rand0.RANDOM_int(0, 3);
		PRINT("rand0 = %d\n", rand0);
		if (rand0 ==1 || rand0==2)
		{
			int loop = m_rand1.RANDOM_int(0, 5);
			PRINT("rand1 = %d\n", loop);
			for (int j = 0; j < loop; j ++)
			{
				int m = j % 3;
				PRINT("j = %d, m = %d\n", j, m);
				if (0 == m)
				{
					cnt ++;
					if (ret < j*m*100)
						cnt ++;
				}
				else if (1 == m)
				{
					cnt --;
					if (ret > j*m*100)
						cnt --;
				}
				else
				{
					PRINT("abs_ret = %d\n", abs(ret));
					for (int k = 0; k < abs(ret); k ++)
					{
						int rand2 = m_rand2.RANDOM_int(0, 10);
						PRINT("rand2 = %d\n", rand2);
						cnt += rand2;
						if (cnt % 5 == 0)
							break;
					}
				}
				PRINT("cnt = %d\n", cnt);
			}
		}
		ret += cnt;
		PRINT("ret = %d, cnt = %d\n", ret, cnt);
	}
	printf("%d\n", ret);
}

int main()
{
	PrintfDecoratorCounter::Register();
	PrintfCondtion::Register();
	PrintfAction::Register();
	CntTestAction::Register();
	CntTestAction0::Register();
	CntTestAction1::Register();
	CntTestAction2::Register();
	CntTestAction2_2::Register();

	TreeFactory::GetInstance()->RegisterTree("export.bt");
	TreeFactory::GetInstance()->RegisterTree("export2.bt");

	QueryPerformanceFrequency(&nFreq);

	QueryPerformanceCounter(&nBeginTime); 
	{
		test_non_tree();
	}
	QueryPerformanceCounter(&nEndTime);

	c_time = (double)(1.0*nEndTime.QuadPart-nBeginTime.QuadPart)/(double)nFreq.QuadPart;
	mps = 1.0*N/c_time/1024.0/1024.0;
	printf("%s cost %lf s, %lf MI/s\n", "test_non_tree", c_time, mps);
	double c_time1 = c_time;

	//////////////////////////////////////////////////////////////////////////
	QueryPerformanceCounter(&nBeginTime); 
	{
		test_tree();
	}
	QueryPerformanceCounter(&nEndTime);

	c_time = (double)(1.0*nEndTime.QuadPart-nBeginTime.QuadPart)/(double)nFreq.QuadPart;
	mps = 1.0*N/c_time/1024.0/1024.0;
	printf("%s cost %lf s, %lf MI/s\n", "test_tree", c_time, mps);

	printf("%lf\n", c_time/c_time1);
	//system("pause");
	return 0;
}