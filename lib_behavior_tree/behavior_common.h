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

// conversion from '__w64 int' to 'int', possible loss of data
#pragma warning(disable : 4244)
// conversion from 'size_t' to 'int', possible loss of data
#pragma warning(disable : 4267)
// pointer truncation from 'void *' to 'int'
#pragma warning(disable : 4311)

#include "protobuf/BehaviorPB.pb.h"

//-------------------------------------------------------------------------

#define SafeDelete(p)			{if (p) {delete (p); (p) = NULL;}}
#define SafeDeleteArray(p)		{if (p) {delete [] (p); (p) = NULL;}}

//-------------------------------------------------------------------------
// Node Register Macro Sample 
// 
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
 * class register func
 */
#define NodeFactoryDef(cls) \
	static BehaviorTree::BaseNode* CreateInstance() {return new cls();} \
	static void Register() {BehaviorTree::NodeFactory::Register(BehaviorPB::NodeType_##cls, \
	BehaviorTree::NodeFactory::CreateCallback(&cls::CreateInstance));} \
	virtual int	GetType() {return BehaviorPB::NodeType_##cls;}

/**
 * NodeLoadProtoDef macro
 *
 * use in every derived node class
 * class protobuf load func
 */
#define NodeLoadProtoDef(supercls, pbcls, pbname) \
public: \
virtual bool LoadProto( const BehaviorPB::Node* pProto ) \
{ \
	if (!supercls::LoadProto(pProto)) \
		return false; \
	if (!pProto->has_##pbname()) \
		return false; \
	m_pProto = &pProto->##pbname(); \
	return true; \
} \
protected: \
const BehaviorPB::pbcls* m_pProto;

/**
 * PRINTF macro
 *
 * switch to log print
 */
#define PRINTF //printf

//-------------------------------------------------------------------------
namespace BehaviorPB
{
	class Tree;
	class Node;
};

using BehaviorPB::NodeType;
using BehaviorPB::NodeType_Null;
using BehaviorPB::NodeType_Selector;
using BehaviorPB::NodeType_Sequence;
using BehaviorPB::NodeType_Parallel;
using BehaviorPB::NodeType_Action;
using BehaviorPB::NodeType_Condition;
using BehaviorPB::NodeType_Link;
using BehaviorPB::NodeType_Decorator;
using BehaviorPB::NodeType_DecoratorNot;
using BehaviorPB::NodeType_DecoratorLoop;
using BehaviorPB::NodeType_DecoratorCounter;
using BehaviorPB::NodeType_DecoratorTimer;
using BehaviorPB::NodeType_DecoratorRand;

using BehaviorPB::ParallelPolicy;
using BehaviorPB::ParallelPolicy_FailOnAll;
using BehaviorPB::ParallelPolicy_SuccOnAll;
using BehaviorPB::ParallelPolicy_FailAlways;
using BehaviorPB::ParallelPolicy_SuccAlways;

namespace BehaviorTree
{

class BaseNode;
class NonLeafNode;
class Tree;
class ChalkInk;
class ChalkInkPtr;
class ChalkInkRef;
class BlackBoard;
class NodeFactory;
class TreeFactory;

typedef std::string					String;
typedef BaseNode					LeafNode;
typedef std::list <ChalkInkPtr*>	ChalkPtrList;

enum NodeExecState
{
	NodeExec_Fail = 0,
	NodeExec_Success,
	NodeExec_Running,

	NodeExec_Total
};

};

#endif /* __BEHAVIOR_COMMON_H__ */
