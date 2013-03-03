#include "behavior_tree.h"
#include "behavior_composite_node.h"

namespace BehaviorTree
{

Tree::Tree( Blackboard::Ptr pBlackboard )
:	m_pRoot(NULL), m_pBlackboard(pBlackboard)
{
	m_pBlackboard->m_pTree = this;
}

Tree::~Tree()
{
	SafeDelete(m_pRoot);
}

void Tree::Init( Node::Ptr pRoot )
{
	assert(m_pRoot == NULL);
	m_pRoot = pRoot;
}

void Tree::Clear()
{
	if (m_pRoot)
		m_pRoot->ClearChild();
	if (m_pBlackboard)
		m_pBlackboard->Clear();
}

NodeExecState Tree::Process( ParamData::Ptr pInput, ParamData::Ptr pOutput )
{
	m_pBlackboard->m_pInput = pInput;
	m_pBlackboard->m_pOutput = pOutput;

	return m_pRoot->Execute();
}

bool Tree::IsValid()
{
	return true;
}

bool Tree::DumpFile( const char* szFile )
{

}

bool Tree::LoadFile( const char* szFile )
{

}


};