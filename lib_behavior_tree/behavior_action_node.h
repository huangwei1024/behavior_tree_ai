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
class ActionNode : public LeafNode
{
public:
	ActionNode()						{}
	virtual ~ActionNode()				{}

	virtual int				GetType()	{return NodeType_Action;}

	/**
	 * @brief ActionNode Execute
	 *
	 * virtual public 
	 * Actions which finally implement an actors or game world state changes, 
	 * for example to plan a path and move on it, to sense for the nearest enemies. 
	 * Actions will typically coordinate and call into different game systems. 
	 * They might run for one simulation tick ¨C one frame ¨C or might need to be ticked for multiple frames to finish their work.
	 * @return 		NodeExecState
	 */
	virtual NodeExecState	Execute()	{return NodeExec_Success;}

protected:

};


};

#endif /* __BEHAVIOR_ACTION_NODE_H__ */
