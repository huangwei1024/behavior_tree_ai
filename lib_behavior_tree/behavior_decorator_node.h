/**
 * @File: 		behavior_decorator_node.h
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

#ifndef __BEHAVIOR_DECORATOR_NODE_H__
#define __BEHAVIOR_DECORATOR_NODE_H__

#include "behavior_tree.h"

namespace BehaviorTree
{

/**
 * 
 */
class DecoratorNode : public Node
{
public:
	DecoratorNode()						{}
	virtual ~DecoratorNode()			{}

	virtual Type		GetType()		{return NodeType_Decorator;}
	virtual TypeStr		GetTypeStr()	{return "Decorator";}


	/**
	 * @brief DecoratorNode Execute
	 *
	 * virtual public 
	 * Decorator nodes typically have only one child and are used to enforce a certain return state 
	 * or to implement timers to restrict how often the child will run in a given amount 
	 * of time or how often it can be executed without a pause.
	 * @return 		ExecState
	 */
	virtual ExecState Execute()
	{
		if (m_vChilds.empty())
			return NodeExec_Fail;

		assert(m_vChilds.size() == 1);
		return _Decorate(m_vChilds[0]->Execute());
	}

protected:
	virtual ExecState	_Decorate(ExecState nOld) = 0;
};

};

#endif /* __BEHAVIOR_DECORATOR_NODE_H__ */
