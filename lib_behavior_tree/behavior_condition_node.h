/**
 * @File: 		behavior_condition_node.h
 * @Created:	2013/03/01
 * @Author:		Huang.WisKey
 * @E-Mail:		sir.huangwei[at]gmail.com
 * @Brief:		
 *
 * May you do good and not evil.
 * May you find forgiveness for yourself and forgive others.
 * May you share freely, never taking more than you give.
 */

#pragma once

#ifndef __BEHAVIOR_CONDITION_NODE_H__
#define __BEHAVIOR_CONDITION_NODE_H__

#include "behavior_tree.h"

namespace BehaviorTree
{

/**
 * 
 */
class ConditionNode : public Node
{
public:
	ConditionNode()						{}
	virtual ~ConditionNode()			{}

	virtual Type		GetType()		{return NodeType_Condition;}
	virtual TypeStr		GetTypeStr()	{return "Condition";}


	/**
	 * @brief ConditionNode Execute
	 *
	 * virtual public 
	 * Conditions check that certain actor or game world states hold true. 
	 * If a sequence node has a condition as one of its children then the failing of the condition 
	 * will prevent the following nodes from being traversed during the update. 
	 * When placed below a concurrent node, 
	 * conditions become a kind of invariant check that prevents its sibling nodes from running if a necessary state becomes invalid.
	 * @return 		ExecState
	 */
	virtual ExecState	Execute()		{return NodeExec_Success;}

protected:

};

};

#endif /* __BEHAVIOR_CONDITION_NODE_H__ */
