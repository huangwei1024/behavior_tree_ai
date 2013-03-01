/**
 * @File: 		behavior_action_node.h
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

#ifndef __BEHAVIOR_ACTION_NODE_H__
#define __BEHAVIOR_ACTION_NODE_H__

#include "behavior_tree.h"

namespace BehaviorTree
{

/**
 * 
 */
class ActionNode : public Node
{
public:
	ActionNode()						{}
	virtual ~ActionNode()				{}

	virtual Type		GetType()		{return NodeType_Action;}
	virtual TypeStr		GetTypeStr()	{return "Action";}


	/**
	 * @brief ActionNode Execute
	 *
	 * virtual public 
	 * Some detailed comment.
	 * @return 		ExecState
	 */
	virtual ExecState	Execute()		{return NodeExec_Success;}

protected:

};


};

#endif /* __BEHAVIOR_ACTION_NODE_H__ */
