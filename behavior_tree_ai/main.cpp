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
const int N = 1000;

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
			GetBlackboard()->WriteValue("limit", ChalkInk(m_pProto->limit_cnt()));
			GetBlackboard()->WriteValue("cnt", ChalkInk(m_nCount));
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
		ChalkInk& val = GetBlackboard()->LookupValue("tick");
		if (val.Get<int>() > 0)
			srand(val.Get<int>());
		val.SetEmpty();

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
		printf("hello, cnt %d\n", GetBlackboard()->LookupValue("cnt").Get<int>());
		return NodeExec_Success;
	}

};

const int SR = 1234;

void test_tree_r()
{
	BehaviorTree::BlackBoard board;
	BehaviorTree::Tree* tree = BehaviorTree::TreeFactory::GetInstance()->CreateTree("test_tree_r", NULL, &board);

	board.WriteValue("tick", ChalkInk((unsigned int)nFreq.QuadPart));

	for (int i = 0; i < 30; i ++)
	{
		printf("i = %d\n", i);
		int ret = tree->Process();
		printf("ret = %d\n", ret);
	}
}

//////////////////////////////////////////////////////////////////////////
#define TEST_OPT
class CntTestAction : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction);

	CntTestAction()
	{
		p_j = NULL;
		p_m = NULL;
	}

	virtual NodeExecState Execute()
	{
#ifdef TEST_OPT
		if (!p_j)
			p_j = &GetBlackboard()->LookupValue("j");
		int j = p_j->Get<int>();

		GetBlackboard()->WriteValue("m", ChalkInk(j%3));
		if (!p_m)
			p_m = &GetBlackboard()->WriteValue("m", ChalkInk(j%3));
#else
		GetBlackboard()->WriteValue("m", ChalkInk(GetBlackboard()->LookupValue("j").Get<int>() % 3));
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInk* p_j;
	ChalkInk* p_m;
};

class CntTestAction0 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction0);

	CntTestAction0()
	{
		p_cnt = NULL;
		p_ret = NULL;
		p_m = NULL;
	}

	virtual NodeExecState Execute()
	{
		int m;
		int cnt;
		int ret;

#ifdef TEST_OPT
		if (!p_cnt)
			p_cnt = &GetBlackboard()->LookupValue("cnt");
		if (!p_ret)
			p_ret = &GetBlackboard()->LookupValue("ret");
		if (!p_m)
			p_m = &GetBlackboard()->LookupValue("m");

		m = p_m->Get<int>();
		cnt = p_cnt->Get<int>();
		ret = p_ret->Get<int>();

		if (m != 0)
			return NodeExec_Fail;
		cnt ++;
		if (ret < 1000)
			cnt ++;
		p_cnt->Set(cnt);

#else
		m = GetBlackboard()->LookupValue("m").Get<int>();
		cnt = GetBlackboard()->LookupValue("cnt").Get<int>();
		ret = GetBlackboard()->LookupValue("ret").Get<int>();

		if (m != 0)
			return NodeExec_Fail;
		cnt ++;
		if (ret < 1000)
			cnt ++;
		
		GetBlackboard()->WriteValue("cnt", cnt);
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInk* p_cnt;
	ChalkInk* p_ret;
	ChalkInk* p_m;
};

class CntTestAction1 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction1);

	CntTestAction1()
	{
		p_cnt = NULL;
		p_ret = NULL;
		p_m = NULL;
	}

	virtual NodeExecState Execute()
	{
		int m;
		int cnt;
		int ret;

#ifdef TEST_OPT
		if (!p_cnt)
			p_cnt = &GetBlackboard()->LookupValue("cnt");
		if (!p_ret)
			p_ret = &GetBlackboard()->LookupValue("ret");
		if (!p_m)
			p_m = &GetBlackboard()->LookupValue("m");

		m = p_m->Get<int>();
		cnt = p_cnt->Get<int>();
		ret = p_ret->Get<int>();

		if (m != 1)
			return NodeExec_Fail;
		cnt --;
		if (ret > 1000)
			cnt --;

		p_cnt->Set(cnt);

#else
		m = GetBlackboard()->LookupValue("m").Get<int>();
		cnt = GetBlackboard()->LookupValue("cnt").Get<int>();
		ret = GetBlackboard()->LookupValue("ret").Get<int>();

		if (m != 1)
			return NodeExec_Fail;
		cnt --;
		if (ret > 1000)
			cnt --;

		GetBlackboard()->WriteValue("cnt", cnt);
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInk* p_cnt;
	ChalkInk* p_ret;
	ChalkInk* p_m;
};

class CntTestAction2 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction2);

	CntTestAction2()
	{
		p_m = NULL;
		p_ret = NULL;
		p_absret = NULL;
	}

	virtual NodeExecState Execute()
	{
		int m;
		int ret;

#ifdef TEST_OPT
		if (!p_m)
			p_m = &GetBlackboard()->LookupValue("m");
		if (!p_ret)
			p_ret = &GetBlackboard()->LookupValue("ret");
		if (!p_absret)
			p_absret = &GetBlackboard()->WriteValue("absret", 0);

		m = p_m->Get<int>();
		ret = p_ret->Get<int>();
		if (m != 2)
			return NodeExec_Fail;

		p_absret->Set(abs(ret));

#else
		m = GetBlackboard()->LookupValue("m").Get<int>();
		ret = GetBlackboard()->LookupValue("ret").Get<int>();

		if (m != 2)
			return NodeExec_Fail;

		GetBlackboard()->WriteValue("absret", abs(ret));
#endif
		return NodeExec_Success;
	}

protected:
	ChalkInk* p_m;
	ChalkInk* p_ret;
	ChalkInk* p_absret;
};

class CntTestAction2_2 : public ActionNode
{
public:
	NodeFactoryDef(CntTestAction2_2);

	CntTestAction2_2()
	{
		p_cnt = NULL;
		p_rand2 = NULL;
	}

	virtual NodeExecState Execute()
	{
		int cnt;
		int rand2;

#ifdef TEST_OPT
		if (!p_cnt)
			p_cnt = &GetBlackboard()->LookupValue("cnt");
		if (!p_rand2)
			p_rand2 = &GetBlackboard()->LookupValue("rand2");

		cnt = p_cnt->Get<int>() + p_rand2->Get<int>();
		p_cnt->Set(cnt);

#else
		cnt = GetBlackboard()->LookupValue("cnt").Get<int>();
		rand2 = GetBlackboard()->LookupValue("rand2").Get<int>();

		cnt += rand2;
		GetBlackboard()->WriteValue("cnt", cnt);
#endif
		
		if (cnt % 5 == 0)
			return NodeExec_Fail;
		return NodeExec_Success;
	}

protected:
	ChalkInk* p_cnt;
	ChalkInk* p_rand2;
};

//////////////////////////////////////////////////////////////////////////
void test_tree()
{
	BehaviorTree::BlackBoard board;
	BehaviorTree::Tree* tree = BehaviorTree::TreeFactory::GetInstance()->CreateTree("test_tree", NULL, &board);

	board.WriteValue("ret", BehaviorTree::ChalkInk(0));
	srand(SR);

	for (int i = 0; i < N; ++ i)
	{
		board.WriteValue("cnt", 0);
		tree->Process();
	}

	printf("%d\n", board.LookupValue("ret").Get<int>());
}

void test_non_tree()
{
	int ret = 0;
	srand(SR);

	G_RANDOM_MGR m_rand0;
	G_RANDOM_MGR m_rand1;
	G_RANDOM_MGR m_rand2;

	m_rand0.reset_seed(0);
	m_rand1.reset_seed(0);
	m_rand2.reset_seed(0);

	for (int i = 0; i < N; ++ i)
	{
		int cnt = 0;
		if (m_rand0.RANDOM_int(0, 1))
		{
			int loop = m_rand1.RANDOM_int(0, 8);
			for (int j = 0; j < loop; j ++)
			{
				int m = j % 3;
				if (0 == m)
				{
					cnt ++;
					if (ret < 1000)
						cnt ++;
				}
				else if (1 == m)
				{
					cnt --;
					if (ret > 1000)
						cnt --;
				}
				else
				{
					for (int k = 0; k < abs(ret); k ++)
					{
						cnt += m_rand2.RANDOM_int(0, 4);
						if (cnt % 5 == 0)
							break;
					}
				}
			}
		}
		ret += cnt;
	}
	printf("%d\n", ret);
}

int main()
{
	PrintfDecoratorCounter::Register();
	PrintfCondtion::Register();
	PrintfAction::Register();

	TreeFactory::GetInstance()->RegisterTree("export.bt");

	QueryPerformanceFrequency(&nFreq);

	QueryPerformanceCounter(&nBeginTime); 
	{
		//test_non_tree();
		//test_tree_r();
		test_tree();
	}
	QueryPerformanceCounter(&nEndTime);

	c_time = (double)(1.0*nEndTime.QuadPart-nBeginTime.QuadPart)/(double)nFreq.QuadPart;
	mps = 1.0*N/c_time/1024.0/1024.0;
	printf("%s cost %lf s, %lf MI/s\n", "test_FastDelegate", c_time, mps);

	return 0;
}