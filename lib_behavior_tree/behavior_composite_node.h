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
 * SelectorNode
 * 
 */
class SelectorNode : public NonLeafNode
{
public:
	SelectorNode()						{}
	virtual ~SelectorNode()				{}

	virtual int GetType()				{return NodeType_Selector;}

	/**
	 * @brief SelectorNode Execute
	 *
	 * virtual public 
	 * A selector starts running and ticks the first child (from left to right). 
	 * if a child returns success then the selector will return success also. 
	 * if a selector returns Failure the selector will move on to the next child and return running.
	 * if a selector reaches the end of its child then it return failure and the next time it will start at the first child.
	 * @return 		NodeExecState
	 */
	virtual NodeExecState Execute()
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			NodeExecState nRet = (*it)->Execute();
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
class SequenceNode : public NonLeafNode
{
public:
	SequenceNode()						{}
	virtual ~SequenceNode()				{}

	virtual int				GetType()	{return NodeType_Sequence;}

	/**
	 * @brief SequenceNode Execute
	 *
	 * virtual public 
	 * A sequence start running and ticks the first child (from left to right). 
	 * if the child return success the sequence continues its execution and ticks the next a child can also return running. 
	 * return running will cause the sequence to return running and the next time the sequence is called the child that was running is called.
	 * If all child would return success the sequence executes. When the sequence finishes the return result is success.
	 * if a child fails then the sequence stops and also returns failure and the next time the sequence will start with the first child.
	 * @return 		NodeExecState
	 */
	virtual NodeExecState Execute()
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			NodeExecState nRet = (*it)->Execute();
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
 * ParallelPolicy_FailOnAll
 *	全部fail，返回fail，否则succ
 * ParallelPolicy_SuccOnAll
 *	全部succ，返回succ，否则fail
 */

enum ParallelPolicy
{
	ParallelPolicy_FailOnAll = 0,
	ParallelPolicy_SuccOnAll,
};

/**
 * 
 */
class ParallelNode : public NonLeafNode
{
public:
	ParallelNode() 
		: m_nPolicy(ParallelPolicy_FailOnAll) 
										{}
	virtual ~ParallelNode()				{}

	virtual int				GetType()	{return NodeType_Parallel;}

	/**
	 * @brief ParallelNode Execute
	 *
	 * virtual public 
	 * A parallel component will tick all it’s childs the same time, 
	 * unlike the sequence and selector (those will tick its child one by one).
	 * @return 		NodeExecState
	 */
	virtual NodeExecState Execute()
	{
		int nRetCnt[NodeExec_Total] = {0};
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			++ nRetCnt[(*it)->Execute()];
		}

		switch (m_nPolicy)
		{
		case ParallelPolicy_FailOnAll:
			return (nRetCnt[NodeExec_Success] == 0) ? NodeExec_Fail : NodeExec_Success;
		case ParallelPolicy_SuccOnAll:
			return (nRetCnt[NodeExec_Fail] == 0) ? NodeExec_Success : NodeExec_Fail;
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

	virtual bool LoadProto(const Proto* pProto);
	virtual bool DumpProto(Proto* pProto);

protected:
	ParallelPolicy		m_nPolicy;
};


};

#endif /* __BEHAVIOR_COMPOSITE_NODE_H__ */
