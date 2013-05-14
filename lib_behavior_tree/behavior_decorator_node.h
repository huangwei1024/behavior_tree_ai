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
class DecoratorNode : public NonLeafNode
{
public:

	virtual int GetType()	{return NodeType_Decorator;}

	virtual void AddChild(BaseNode* pChild)
	{
		if (!m_vChilds.empty())
		{
			assert(false);
			return;
		}

		NonLeafNode::AddChild(pChild);
	}

	virtual void SwapChild(BaseNode* pChild1, BaseNode* pChild2)
	{
		assert(false);
	}

	/**
	 * @brief DecoratorNode Execute
	 *
	 * virtual public 
	 * Decorator nodes typically have only one child and are used to enforce a certain return state 
	 * or to implement timers to restrict how often the child will run in a given amount 
	 * of time or how often it can be executed without a pause.
	 * @return 		NodeExecState
	 */
	virtual NodeExecState Execute()
	{
		if (m_vChilds.empty())
			return NodeExec_Fail;

		assert(m_vChilds.size() == 1);
		return _Decorate();
	}

protected:
	virtual NodeExecState	_Decorate() = 0;
};

/**
 * 
 */
class DecoratorNotNode : public DecoratorNode
{
public:
	virtual int GetType()	{return NodeType_DecoratorNot;}

protected:
	/**
	 * @brief DecoratorNotNode _Decorate
	 *
	 * virtual protected 
	 * Logic-Not, !exec;
	 * @return 		NodeExecState
	 */
	virtual NodeExecState _Decorate()
	{
		NodeExecState nOld = m_vChilds.front()->Execute();
		switch (nOld)
		{
		case NodeExec_Fail:
			return NodeExec_Success;
		case NodeExec_Success:
			return NodeExec_Fail;
		case NodeExec_Running:
			return NodeExec_Running;
		}
		assert(false);
		return NodeExec_Fail;
	}
};

/**
 * 
 */
class DecoratorLoopNode : public DecoratorNode
{
public:
	DecoratorLoopNode() : m_nLoop(0)	{}
	virtual ~DecoratorLoopNode()		{}

	virtual int	GetType()				{return NodeType_DecoratorLoop;}

	void SetLoop(int nLoop)				{m_nLoop = nLoop;}

	virtual bool LoadProto(const Proto* pProto);
	virtual bool DumpProto(Proto* pProto);

protected:
	/**
	 * @brief DecoratorLoopNode _Decorate
	 *
	 * virtual protected 
	 * Logic-Loop, for (loop) exec;
	 * @return 		NodeExecState		last state
	 */
	virtual NodeExecState _Decorate()
	{
		NodeExecState nOld;
		for (int i = 0; i < m_nLoop; ++ i)
			nOld = m_vChilds.front()->Execute();
		return nOld;
	}

protected:
	int					m_nLoop;
};

/**
 * 
 */
class DecoratorCounterNode : public DecoratorNode
{
public:
	DecoratorCounterNode() 
		: m_nCount(0), m_nLimit(1)		{}
	virtual ~DecoratorCounterNode()		{}

	virtual int GetType()				{return NodeType_DecoratorCounter;}

	void SetLimit(int nLimit)			{m_nLimit = nLimit;}
	void ClearCount()					{m_nCount = 0;}

	virtual bool LoadProto(const Proto* pProto);
	virtual bool DumpProto(Proto* pProto);

protected:

	/**
	 * @brief DecoratorLoopNode _Decorate
	 *
	 * virtual protected 
	 * Logic-Loop, for (loop) exec;
	 * @return 		NodeExecState		last state
	 * @TODO if last Execute is running, will result false limit
	 */
	virtual NodeExecState _Decorate()
	{
		// TODO
		if (m_nCount >= m_nLimit)
			return NodeExec_Fail;

		NodeExecState nRet = m_vChilds.front()->Execute();
		if (nRet == NodeExec_Success)
			++ m_nCount;
		return nRet;
	}

protected:
	int					m_nLimit;
	int					m_nCount;
};


};

#endif /* __BEHAVIOR_DECORATOR_NODE_H__ */
