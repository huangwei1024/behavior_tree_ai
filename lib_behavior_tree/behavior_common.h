/**
 * @File: 		behavior_common.h
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

#ifndef __BEHAVIOR_COMMON_H__
#define __BEHAVIOR_COMMON_H__

#include <assert.h>

#include <set>
#include <map>
#include <list>
#include <string>
#include <vector>
#include <algorithm>
#include <functional>

//-------------------------------------------------------------------------

#define SafeDelete(p)			{if (p) {delete (p); (p) = NULL;}}
#define SafeDeleteArray(p)		{if (p) {delete [] (p); (p) = NULL;}}

//-------------------------------------------------------------------------
// Node Register Macro Sample 
// 
// enum {
//		NodeType_UserTypeStart = NodeType_UserCustom,
// 		NodeTypeDef(class1),
// };
// 
// class class1 : public ConditionNode {
// public:
// 		NodeFactoryDef(class1);
// };
// 
// int main() {
//		class1::Register();
// }
//

/**
 * NodeTypeDef macro
 *
 * use in global enum define
 */
#define NodeTypeDef(cls) \
	NodeType_##cls

/**
 * NodeFactoryDef macro
 *
 * use in every derived node class
 */
#define NodeFactoryDef(cls) \
	static BehaviorTree::BaseNode* CreateInstance() {return new cls;} \
	static void Register() {BehaviorTree::NodeFactory::Register(NodeType_##cls, \
	BehaviorTree::NodeFactory::CreateCallback(&cls::CreateInstance));} \
	virtual int	GetType() {return NodeType_##cls;}

//-------------------------------------------------------------------------

namespace BehaviorTree
{

class BaseNode;
class NonLeafNode;
class Tree;
class BlackBoard;
class NodeFactory;
class TreeProtoFactory;

typedef std::string		String;
typedef BaseNode		LeafNode;

enum NodeExecState
{
	NodeExec_Fail = 0,
	NodeExec_Success,
	NodeExec_Running,

	NodeExec_Total
};

enum NodeType
{
	NodeType_Null = 0,
	NodeType_Selector,
	NodeType_Sequence,
	NodeType_Parallel,
	NodeType_Action,
	NodeType_Condition,
	NodeType_Link,
	NodeType_Decorator,
	NodeType_DecoratorNot,
	NodeType_DecoratorLoop,
	NodeType_DecoratorCounter,
	NodeType_DecoratorTime,

	NodeType_UserCustom = 100,
};

};

#endif /* __BEHAVIOR_COMMON_H__ */
