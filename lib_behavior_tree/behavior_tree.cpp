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

BlackBoard* BaseNode::GetBlackboard()
{
	if (!m_pTree)
		return NULL;
	return m_pTree->GetBlackboard();
}

bool BaseNode::LoadProto( const BehaviorPB::Node* pProto )
{
	if (pProto->type() != GetType())
	{
		assert(false);
		return false;
	}
#ifdef _DEBUG
	const BehaviorPB::Node_Editor* pEditor = &pProto->editor();
	m_sName = pEditor->name();
	m_sDesc = pEditor->desc();
#endif
	return true;
}

void BaseNode::SetRunning()
{
	GetBlackboard()->SetRunning(this);
}

//-------------------------------------------------------------------------
bool NonLeafNode::LoadProto( const BehaviorPB::Node* pProto )
{
	if (!BaseNode::LoadProto(pProto))
		return false;

	InitChildsList(pProto->nodes_size());
	for (int i = 0; i < pProto->nodes_size(); ++ i)
	{
		const BehaviorPB::Node* pChildProto = &pProto->nodes(i);
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

// AI编辑器负责生成树
// bool NonLeafNode::DumpProto( BehaviorPB::Node* pProto )
// {
// 	BaseNode::DumpProto(pProto);
// 
// 	pProto->clear_nodes();
// 	for (PtrList::iterator it = m_vChilds.begin(); it != m_vChilds.end(); ++ it)
// 	{
// 		BehaviorPB::Node* pChildProto = pProto->add_nodes();
// 		if (!(*it)->DumpProto(pChildProto))
// 		{
// 			pProto->clear_nodes();
// 			return false;
// 		}
// 	}
// 
// 	return true;
// }


//-------------------------------------------------------------------------
bool LinkNode::LoadProto( const BehaviorPB::Node* pProto )
{
	if (!LeafNode::LoadProto(pProto))
		return false;

	if (!pProto->has_link())
		return false;

	m_pProto = &pProto->link();
	return SetLinkTree(m_pProto->sub_tree_name());
}

//-------------------------------------------------------------------------

bool DecoratorRandNode::LoadProto( const BehaviorPB::Node* pProto )
{
	if (!DecoratorNode::LoadProto(pProto))
		return false;

	if (!pProto->has_d_rand())
		return false;

	m_pProto = &pProto->d_rand();
	m_rand.reset_seed(m_rand.RANDOM_int(-0xfffffff, 0xfffffff, m_pProto->r_idx()));
	PRINTF("rand seed = %d\n", m_rand.m_base_seed);

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
	case NodeType_DecoratorRand:
		return new DecoratorRandNode();
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
	if (m_pBlackboard->GetRunning())
	{
		NodeExecState nRet = m_pBlackboard->GetRunning()->Execute();
		if (nRet != NodeExec_Running)
			m_pBlackboard->SetRunning(NULL);
		return nRet;
	}

	if (m_pRoot->PreExecute())
		return m_pRoot->Execute();
	return NodeExec_Fail;
}

bool Tree::IsValid()
{
	return true;
}

// AI编辑器负责生成树
// bool Tree::DumpFile( const char* szFile )
// {
// 	BehaviorPB::Node* pProto = new BehaviorPB::Node;
// 
// 	pProto->set_name(m_sName);
// 	if (m_pRoot)
// 	{
// 		NonLeafNode::BehaviorPB::Node* pRootProto = pProto->mutable_root();
// 		m_pRoot->DumpProto(pRootProto);
// 	}
// 
// 	std::fstream output(szFile, ios::out | ios::trunc | ios::binary);
// 	if (!pProto->SerializeToOstream(&output))
// 		return false;
// 
// 	delete pProto;
// 	TreeProtoFactory::Register(m_sName, szFile);
// 	return true;
// }

bool Tree::LoadFile( BehaviorPB::Tree* pProto )
{
	m_sName = pProto->name();

	const BehaviorPB::Node* pRootProto = &pProto->root();
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

	return true;
}
//-------------------------------------------------------------------------

TreeFactory::~TreeFactory()
{
	for (TreeProtoMap::iterator it = m_mapTree.begin(); it != m_mapTree.end(); ++ it)
	{
		TreeCache& item = (it->second);
		SafeDelete(item.pProto);
	}
	m_mapTree.clear();
}

Tree* TreeFactory::CreateTree( TreeName sName, Tree* pParentTree /*= NULL*/, BlackBoard* pBlackboard /*= NULL*/ )
{
	TreeProtoMap::iterator it = m_mapTree.find(sName);
	if (it == m_mapTree.end())
		return NULL;
	
	if (pBlackboard == NULL && pParentTree != NULL)
		pBlackboard = pParentTree->GetBlackboard();

	TreeCache& item = (it->second);
	Tree* pTree = new Tree(pBlackboard);
	if (!pTree->LoadFile(item.pProto))
		SafeDelete(pTree);
	
	return pTree;
}

void TreeFactory::RegisterTree( String sPath )
{
	//GOOGLE_PROTOBUF_VERIFY_VERSION;

	BehaviorPB::Tree* pProto = new BehaviorPB::Tree;
	std::fstream input(sPath.c_str(), ios::in | ios::binary);
	if (!pProto->ParseFromIstream(&input))
	{
		SafeDelete(pProto);
		return;
	}

	//pProto->PrintDebugString(); // debug output

	TreeProtoMap::iterator it = m_mapTree.find(pProto->name());
	if (it != m_mapTree.end())
	{
		SafeDelete(pProto);
		return;
	}

	TreeCache item;
	item.sTreeName = pProto->name();
	item.sFilePath = sPath;
	item.pProto = pProto;
	m_mapTree[pProto->name()] = item;
}

//-------------------------------------------------------------------------
void ChalkInkPtr::SetOwnerRef( ChalkInkRef* pChalkRef )
{
	if (m_pChalkRef)
		m_pChalkRef->SubRef(this);

	if (pChalkRef)
		pChalkRef->AddRef(this);
}

bool ChalkInkPtr::IsNull() const
{
	return m_pChalkRef == NULL || !m_pChalkRef->IsInBlackBoard();
}

};