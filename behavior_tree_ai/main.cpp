#include <stdio.h>
#include <string.h>
#include <windows.h>

#include "behavior_tree.h"
#include "behavior_composite_node.h"
#include "behavior_condition_node.h"
#include "behavior_decorator_node.h"
#include "behavior_action_node.h"

LARGE_INTEGER nFreq;
LARGE_INTEGER nBeginTime;
LARGE_INTEGER nEndTime;
double ctime, mps;
const int N = 1000;

using namespace BehaviorTree;

// enum
// {
// 	NodeType_UserTypeStart = NodeType_UserCustom,
// 	NodeTypeDef(PrintfDecoratorCounter),
// 	NodeTypeDef(PrintfCondtion),
// 	NodeTypeDef(PrintfAction),
// };


class PrintfDecoratorCounter : public DecoratorCounterNode
{
public:
	NodeFactoryDef(PrintfDecoratorCounter);

	PrintfDecoratorCounter()
	{
		SetLimit(10);
	}

	virtual NodeExecState Execute()
	{
		NodeExecState ret = DecoratorCounterNode::Execute();
		if (ret != NodeExec_Fail)
		{
			GetBlackboard()->WriteValue("limit", ChalkInk(m_nLimit));
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

void test_tree_w()
{
	BehaviorTree::BlackBoard board;
	BehaviorTree::Tree tree(&board);

	board.WriteValue("tick", ChalkInk((unsigned int)nFreq.QuadPart));

	tree.SetRoot(new PrintfDecoratorCounter());
	tree.SetBlackboard(&board);
	tree.Name() = "test_tree";

	SequenceNode* node0 = new SequenceNode();
	tree.GetRoot()->AddChild(node0);
	node0->AddChild(new PrintfCondtion());
	node0->AddChild(new PrintfAction());

	for (int i = 0; i < 30; i ++)
	{
		printf("i = %d\n", i);
		int ret = tree.Process();
		printf("ret = %d\n", ret);
	}

	tree.DumpFile("test.bt");
}

void test_tree_r()
{
	BehaviorTree::BlackBoard board;
	BehaviorTree::Tree tree(&board);

	tree.LoadFile("export.bt");

	board.WriteValue("tick", ChalkInk((unsigned int)nFreq.QuadPart));

	for (int i = 0; i < 30; i ++)
	{
		printf("i = %d\n", i);
		int ret = tree.Process();
		printf("ret = %d\n", ret);
	}
}

void test_tree2()
{
	BehaviorTree::BlackBoard board;
	BehaviorTree::Tree* ptree = TreeProtoFactory::CreateInstance("test_tree2");
	BehaviorTree::Tree& tree = *ptree;

	tree.Clear();

	board.WriteValue("tick", ChalkInk((unsigned int)nFreq.QuadPart));

	tree.SetRoot(new PrintfAction());
	tree.SetBlackboard(&board);
	tree.Name() = "test_tree2";

	for (int i = 0; i < 30; i ++)
	{
		printf("i = %d\n", i);
		int ret = tree.Process();
		printf("ret = %d\n", ret);
	}

	tree.DumpFile("test2.bt");
}

int main()
{
	PrintfDecoratorCounter::Register();
	PrintfCondtion::Register();
	PrintfAction::Register();

	TreeProtoFactory::Register("test_tree", "test.bt");
	TreeProtoFactory::Register("test_tree2", "test2.bt");


	QueryPerformanceFrequency(&nFreq);

	QueryPerformanceCounter(&nBeginTime); 
	{
		test_tree_r();
	}
	QueryPerformanceCounter(&nEndTime);

	ctime = (double)(1.0*nEndTime.QuadPart-nBeginTime.QuadPart)/(double)nFreq.QuadPart;
	mps = 1.0*N/ctime/1024.0/1024.0;
	printf("%s cost %lf s, %lf MI/s\n", "test_FastDelegate", ctime, mps);

	return 0;
}