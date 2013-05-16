/**
 * @File: 		behavior_link_node.h
 * @Created:	2013/05/14
 * @Author:		Huang.WisKey
 * @E-Mail:		sir.huangwei[at]gmail.com
 * @Brief:		
 *
 * May you do good and not evil.
 * May you find forgiveness for yourself and forgive others.
 * May you share freely, never taking more than you give.
 */

#pragma once

#ifndef __BEHAVIOR_LINK_NODE_H__
#define __BEHAVIOR_LINK_NODE_H__


#include "behavior_tree.h"

namespace BehaviorTree
{

/**
 * 
 */
class LinkNode : public LeafNode
{
public:
	LinkNode()
		: m_pSubTree(NULL)
	{}

	virtual ~LinkNode()				{}

	virtual int GetType()			{return NodeType_Link;}

	void SetLinkTree(String sTreeName)
	{
		m_sSubTreeName = sTreeName;
		SafeDelete(m_pSubTree);
		m_pSubTree = TreeProtoFactory::CreateInstance(sTreeName, m_pTree);
	}

	/**
	 * @brief LinkNode Execute
	 *
	 * virtual public 
	 * A link node holds a link to the root of another BT.When a link node is executed it will execute the linked
	 * BT and wait for a response. If the linked BT is successfully executed, it will respond with success,
	 * otherwise it will respond with failure. This introduces modularity and re-usability of behaviors.
	 * @return 		NodeExecState
	 */
	virtual NodeExecState Execute()	
	{
		if (m_pSubTree)
			return m_pSubTree->Process();
		return NodeExec_Fail;
	}

	virtual bool LoadProto(const Proto* pProto);
	virtual bool DumpProto(Proto* pProto);

protected:
	String		m_sSubTreeName;
	Tree*		m_pSubTree;
};

}
#endif /* __BEHAVIOR_LINK_NODE_H__ */

