#include <iostream>
#include <fstream>
#include <sstream>
#include <string>

#include "behavior_tree.h"
#include "behavior_composite_node.h"
#include "behavior_decorator_node.h"
#include "behavior_condition_node.h"
#include "behavior_action_node.h"
#include "behavior_link_node.h"

namespace BehaviorTree
{

//-------------------------------------------------------------------------
void BaseNode::SetParent( NonLeafNode* pParent )
{
	m_pParent = pParent;
	if (m_pParent)
		m_pTree = ((BaseNode*)m_pParent)->m_pTree;
}

BlackBoard* BaseNode::GetBlackboard()
{
	if (!m_pTree)
		return NULL;
	return m_pTree->GetBlackboard();
}

bool BaseNode::LoadProto( const Proto* pProto )
{
	if (pProto->type() != GetType())
	{
		assert(false);
		return false;
	}
	return true;
}

bool BaseNode::DumpProto( Proto* pProto )
{
	pProto->set_type(GetType());
	return true;
}

//-------------------------------------------------------------------------
bool NonLeafNode::LoadProto( const Proto* pProto )
{
	if (!BaseNode::LoadProto(pProto))
		return false;

	m_vChilds.clear();
	for (int i = 0; i < pProto->nodes_size(); ++ i)
	{
		const Proto* pChildProto = &pProto->nodes(i);
		BaseNode* pChild = NodeFactory::CreateInstance(pChildProto->type());
		AddChild(pChild);
		if (!pChild->LoadProto(pChildProto))
		{
			ClearChild();
			return false;
		}
	}

	return true;
}

bool NonLeafNode::DumpProto( Proto* pProto )
{
	BaseNode::DumpProto(pProto);

	pProto->clear_nodes();
	for (PtrList::iterator it = m_vChilds.begin(); it != m_vChilds.end(); ++ it)
	{
		Proto* pChildProto = pProto->add_nodes();
		if (!(*it)->DumpProto(pChildProto))
		{
			pProto->clear_nodes();
			return false;
		}
	}

	return true;
}

//-------------------------------------------------------------------------
bool ParallelNode::LoadProto( const Proto* pProto )
{
	if (!NonLeafNode::LoadProto(pProto))
		return false;
	
	if (!pProto->has_parallel())
		return false;

	const BehaviorPB::Parallel* paral = &pProto->parallel();
	m_nPolicy = (ParallelPolicy)paral->policy();

	return true;
}

bool ParallelNode::DumpProto( Proto* pProto )
{
	if (!NonLeafNode::DumpProto(pProto))
		return false;

	BehaviorPB::Parallel* paral = pProto->mutable_parallel();

	paral->set_policy(m_nPolicy);
	return true;
}
//-------------------------------------------------------------------------
bool DecoratorLoopNode::LoadProto( const Proto* pProto )
{
	if (!DecoratorNode::LoadProto(pProto))
		return false;

	if (!pProto->has_d_loop())
		return false;

	const BehaviorPB::DecoratorLoop* loop = &pProto->d_loop();
	m_nLoop = loop->loop_cnt();

	return true;
}

bool DecoratorLoopNode::DumpProto( Proto* pProto )
{
	if (!DecoratorNode::DumpProto(pProto))
		return false;

	BehaviorPB::DecoratorLoop* loop = pProto->mutable_d_loop();

	loop->set_loop_cnt(m_nLoop);
	return true;
}

//-------------------------------------------------------------------------
bool DecoratorCounterNode::LoadProto( const Proto* pProto )
{
	if (!DecoratorNode::LoadProto(pProto))
		return false;

	if (!pProto->has_d_counter())
		return false;

	const BehaviorPB::DecoratorCounter* counter = &pProto->d_counter();
	m_nLimit = counter->limit_cnt();

	return true;
}

bool DecoratorCounterNode::DumpProto( Proto* pProto )
{
	if (!DecoratorNode::DumpProto(pProto))
		return false;

	BehaviorPB::DecoratorCounter* counter = pProto->mutable_d_counter();

	counter->set_limit_cnt(m_nLimit);
	return true;
}
//-------------------------------------------------------------------------
bool LinkNode::LoadProto( const Proto* pProto )
{
	if (!LeafNode::LoadProto(pProto))
		return false;

	if (!pProto->has_link())
		return false;

	const BehaviorPB::Link* link = &pProto->link();
	SetLinkTree(link->sub_tree_name());

	return true;
}

bool LinkNode::DumpProto( Proto* pProto )
{
	if (!LeafNode::DumpProto(pProto))
		return false;

	BehaviorPB::Link* link = pProto->mutable_link();

	link->set_sub_tree_name(m_sSubTreeName);
	return true;
}

//-------------------------------------------------------------------------
bool DecoratorTimerNode::LoadProto( const Proto* pProto )
{
	if (!LeafNode::LoadProto(pProto))
		return false;

	if (!pProto->has_d_timer())
		return false;

	const BehaviorPB::DecoratorTimer* d_timer = &pProto->d_timer();
	SetElpase(d_timer->elpase());

	return true;
}

bool DecoratorTimerNode::DumpProto( Proto* pProto )
{
	if (!LeafNode::DumpProto(pProto))
		return false;

	BehaviorPB::DecoratorTimer* d_timer = pProto->mutable_d_timer();

	d_timer->set_elpase(m_nElpase);
	return true;
}

//-------------------------------------------------------------------------

NodeFactory::NodeClassMap NodeFactory::ms_mapNodes;

BaseNode* NodeFactory::CreateInstance( int nType )
{
	switch (nType)
	{
	case NodeType_Null:
		assert(false);
		return NULL;
	case NodeType_Selector:
		return new SelectorNode();
	case NodeType_Sequence:
		return new SequenceNode();
	case NodeType_Parallel:
		return new ParallelNode();
	case NodeType_Action:
		return new ActionNode();
	case NodeType_Condition:
		return new ConditionNode();
	case NodeType_Link:
		return new LinkNode();
	case NodeType_Decorator:
		assert(false);
		return NULL;
	case NodeType_DecoratorNot:
		return new DecoratorNotNode();
	case NodeType_DecoratorLoop:
		return new DecoratorLoopNode();
	case NodeType_DecoratorCounter:
		return new DecoratorCounterNode();
	case NodeType_DecoratorTimer:
		return new DecoratorTimerNode();
	}

	NodeClassMap::iterator it = ms_mapNodes.find(nType);
	if (it == ms_mapNodes.end())
	{
		assert(false);
		return NULL;
	}

	return (it->second)();
}

void NodeFactory::Register( int nType, const CreateCallback& cbCreate )
{
	ms_mapNodes[nType] = cbCreate;
}

//-------------------------------------------------------------------------

Tree::Tree()
:	m_pRoot(NULL), m_pBlackboard(NULL)
{
}

Tree::Tree( BlackBoard* pBlackboard )
:	m_pRoot(NULL), m_pBlackboard(NULL)
{
	SetBlackboard(pBlackboard);
}

Tree::~Tree()
{
	Clear();
	m_pBlackboard = NULL;
}

void Tree::Clear()
{
	if (m_pRoot)
	{
		m_pRoot->ClearChild();
		SafeDelete(m_pRoot);
	}
	m_sName.clear();
}

NodeExecState Tree::Process()
{
	if (m_pBlackboard->m_pRunningNode)
	{
		NodeExecState nRet = m_pBlackboard->m_pRunningNode->Execute();
		if (nRet != NodeExec_Running)
			m_pBlackboard->m_pRunningNode = NULL;
		return nRet;
	}

	return m_pRoot->Execute();
}

bool Tree::IsValid()
{
	return true;
}

bool Tree::DumpFile( const char* szFile )
{
	Proto* pProto = new Proto;

	pProto->set_name(m_sName);
	if (m_pRoot)
	{
		NonLeafNode::Proto* pRootProto = pProto->mutable_root();
		m_pRoot->DumpProto(pRootProto);
	}

	std::fstream output(szFile, ios::out | ios::trunc | ios::binary);
	if (!pProto->SerializeToOstream(&output))
		return false;

	delete pProto;
	TreeProtoFactory::Register(m_sName, szFile);
	return true;
}

bool Tree::LoadFile( const char* szFile )
{
	//GOOGLE_PROTOBUF_VERIFY_VERSION;

	Proto* pProto = new Proto;

	std::fstream input(szFile, ios::in | ios::binary);
	if (!pProto->ParseFromIstream(&input))
		return false;

	pProto->PrintDebugString(); // debug output

	m_sName = pProto->name();

	const NonLeafNode::Proto* pRootProto = &pProto->root();
	BaseNode* pNode = NodeFactory::CreateInstance(pRootProto->type());
	if (NULL == pNode)
	{
		SafeDelete(pNode);
		return false;
	}

	SetRoot(pNode);
	if (!pNode->LoadProto(pRootProto))
	{
		SafeDelete(pNode);
		return false;
	}

	delete pProto;
	TreeProtoFactory::Register(m_sName, szFile);
	return true;
}

//-------------------------------------------------------------------------

TreeProtoFactory::TreeProtoMap TreeProtoFactory::ms_mapTree;


Tree* TreeProtoFactory::CreateInstance( TreeName sName, Tree* pParentTree /*= NULL*/, BlackBoard* pBlackboard /*= NULL*/ )
{
	TreeProtoMap::iterator it = ms_mapTree.find(sName);
	if (it == ms_mapTree.end())
		return NULL;
	
	if (pBlackboard == NULL && pParentTree != NULL)
		pBlackboard = pParentTree->GetBlackboard();

	Tree* pTree = new Tree(pBlackboard);
	if (!pTree->LoadFile(it->second.c_str()))
		SafeDelete(pTree);
	
	return pTree;
}

void TreeProtoFactory::Register( TreeName sName, ProtoPath sPath )
{
	ms_mapTree[sName] = sPath;
}

};