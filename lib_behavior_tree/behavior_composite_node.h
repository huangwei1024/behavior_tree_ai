/**
 * @File: 		behavior_composite_node.h
 * @Created:	2013/02/28
 * @Author:		Huang.WisKey
 * @E-Mail:		sir.huangwei[at]gmail.com
 * @Brief:		
 *
 * May you do good and not evil.
 * May you find forgiveness for yourself and forgive others.
 * May you share freely, never taking more than you give.
 */

#pragma once

#ifndef __BEHAVIOR_COMPOSITE_NODE_H__
#define __BEHAVIOR_COMPOSITE_NODE_H__

#include "behavior_tree.h"

namespace BehaviorTree
{

class SelectorNode : public Node
{
public:
	SelectorNode();
	virtual ~SelectorNode();

	virtual Type		GetType()		{return NodeType_Selector;}
	virtual TypeStr		GetTypeStr()	{return "Selector";}
	virtual bool		Process();

protected:

};

};

#endif /* __BEHAVIOR_COMPOSITE_NODE_H__ */
