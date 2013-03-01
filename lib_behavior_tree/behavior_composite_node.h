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

/**
 * 
 */
class SelectorNode : public Node
{
public:
	SelectorNode()						{}
	virtual ~SelectorNode()				{}

	virtual Type		GetType()		{return NodeType_Selector;}
	virtual TypeStr		GetTypeStr()	{return "Selector";}


	/**
	 * @brief SelectorNode Execute
	 *
	 * virtual public 
	 * A selector starts running and ticks the first child (from left to right). 
	 * if a child returns success then the selector will return success also. 
	 * if a selector returns Failure the selector will move on to the next child and return running.
	 * if a selector reaches the end of its child then it return failure and the next time it will start at the first child.
	 * @return 		ExecState
	 */
	virtual ExecState Execute()
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			ExecState nRet = *it->Execute();
			switch (nRet)
			{
			case NodeExec_Success:
			case NodeExec_Running:
				return nRet;
			}
		}
		return NodeExec_Fail;
	}

protected:

};

/**
 * 
 */
class SequenceNode : public Node
{
public:
	SequenceNode()						{}
	virtual ~SequenceNode()				{}

	virtual Type		GetType()		{return NodeType_Sequence;}
	virtual TypeStr		GetTypeStr()	{return "Sequence";}

	/**
	 * @brief SequenceNode Execute
	 *
	 * virtual public 
	 * A sequence start running and ticks the first child (from left to right). 
	 * if the child return success the sequence continues its execution and ticks the next a child can also return running. 
	 * return running will cause the sequence to return running and the next time the sequence is called the child that was running is called.
	 * If all child would return success the sequence executes. When the sequence finishes the return result is success.
	 * if a child fails then the sequence stops and also returns failure and the next time the sequence will start with the first child.
	 * @return 		ExecState
	 */
	virtual ExecState Execute()
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			ExecState nRet = *it->Execute();
			switch (nRet)
			{
			case NodeExec_Fail:
			case NodeExec_Running:
				return nRet;
			}
		}
		return NodeExec_Success;
	}

protected:

};


/**
 * 
 */

enum ParallelPolicy
{
	ParallelPolicy_FailOnAll = 0,
	ParallelPolicy_SuccOnAll,
};

class ParallelNode : public Node
{
public:
	ParallelNode() 
		: m_nPolicy(ParallelPolicy_FailOnAll) 
										{}
	virtual ~ParallelNode()				{}

	virtual Type		GetType()		{return NodeType_Sequence;}
	virtual TypeStr		GetTypeStr()	{return "Parallel";}

	/**
	 * @brief ParallelNode Execute
	 *
	 * virtual public 
	 * A parrallel component will tick all it¡¯s childs the same time, 
	 * unlike the sequence and selector (those will tick its child one by one).
	 * @return 		ExecState
	 */
	virtual ExecState Execute()
	{
		int nRetCnt[NodeExec_Total] = {0};
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			++ nRetCnt[*it->Execute()];
		}

		switch (m_nPolicy)
		{
		case ParallelPolicy_FailOnAll:
			return nRetCnt[NodeExec_Success] == 0;
		case ParallelPolicy_SuccOnAll:
			return nRetCnt[NodeExec_Fail] == 0;
		}
		return NodeExec_Fail;
	}

	/**
	 * @brief SetPolicy
	 *
	 * public 
	 * @param 		nPolicy [in]
	 */
	void SetPolicy(ParallelPolicy nPolicy)	{m_nPolicy = nPolicy;}

protected:
	ParallelPolicy		m_nPolicy;
};

};

#endif /* __BEHAVIOR_COMPOSITE_NODE_H__ */
