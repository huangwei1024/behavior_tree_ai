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

namespace BehaviorTree
{

class Node;
class Tree;

enum NodeExecState
{
	NodeExec_Success = 0,
	NodeExec_Fail,
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
	NodeType_Decorator,
};

};

#endif /* __BEHAVIOR_COMMON_H__ */
