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
#include "windows.h"
#include "Mmsystem.h"
#include "BaseFunc.h"

namespace BehaviorTree
{

/**
 * DecoratorNode
 * 装饰节点基类
 */
class DecoratorNode : public NonLeafNode
{
public:

	virtual int GetType()	{return NodeType_Decorator;}

	virtual void AddChild(BaseNode* pChild)
	{
		if (m_nChilds > 0)
		{
			assert(false);
			return;
		}

		NonLeafNode::AddChild(pChild);
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
		if (m_nChilds == 0)
			return NodeExec_Fail;

		assert(m_nChilds == 1);
		return _Decorate();
	}

protected:
	virtual NodeExecState	_Decorate() = 0;
};

/**
 * DecoratorNotNode
 * 装饰节点
 * 取反
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
		NodeExecState nRet = m_pChilds[0]->Execute();
		switch (nRet)
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
 * DecoratorLoopNode
 * 装饰节点
 * 循环
 */
class DecoratorLoopNode : public DecoratorNode
{
	NodeLoadProtoDef(DecoratorNode, DecoratorLoop, d_loop);

public:
	DecoratorLoopNode() 
		: m_pProto(NULL)				{}
	virtual ~DecoratorLoopNode()		{}

	virtual int	GetType()				{return NodeType_DecoratorLoop;}

protected:

	virtual bool PreExecute()
	{
		if (!DecoratorNode::PreExecute())
			return false;

		if (!m_pProto->bb_loop_key().empty() && m_pLoopKey.IsNull())
			m_pLoopKey = GetBlackboard()->WriteValue(m_pProto->bb_loop_key(), 0);
		
		if (!m_pProto->bb_i().empty() && m_pIKey.IsNull())
			m_pIKey = GetBlackboard()->WriteValue(m_pProto->bb_i(), 0);

		return true;
	}

	/**
	 * @brief DecoratorLoopNode _Decorate
	 *
	 * virtual protected 
	 * Logic-Loop, for (loop) exec;
	 * @return 		NodeExecState		last state
	 */
	virtual NodeExecState _Decorate()
	{
		NodeExecState nRet = NodeExec_Success;
		int loop_cnt = m_pProto->loop_cnt();
		if (loop_cnt == 0 && !m_pLoopKey.IsNull())
			loop_cnt = m_pLoopKey->Get<int>();

		for (int i = 0; i < loop_cnt; ++ i)
		{
			if (!m_pIKey.IsNull())
				m_pIKey->Set(i);
			nRet = m_pChilds[0]->Execute();
			if (nRet != NodeExec_Success)
				break;
		}
		return nRet;
	}

protected:
	ChalkInkPtr		m_pLoopKey;
	ChalkInkPtr		m_pIKey;
};

/**
 * DecoratorCounterNode
 * 装饰节点
 * 计数器
 */
class DecoratorCounterNode : public DecoratorNode
{
	NodeLoadProtoDef(DecoratorNode, DecoratorCounter, d_counter);

public:
	DecoratorCounterNode() 
		: m_pProto(NULL), m_nCount(0)	{}
	virtual ~DecoratorCounterNode()		{}

	virtual int GetType()				{return NodeType_DecoratorCounter;}

	void ClearCount()					{m_nCount = 0;}

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
		if (m_nCount >= m_pProto->limit_cnt())
			return NodeExec_Fail;

		NodeExecState nRet = m_pChilds[0]->Execute();
		if (nRet == NodeExec_Success)
			++ m_nCount;
		return nRet;
	}

protected:
	int										m_nCount;
};



/**
 * DecoratorTimerNode
 * 装饰节点
 * 定时器
 */
class DecoratorTimerNode : public DecoratorNode
{
	NodeLoadProtoDef(DecoratorNode, DecoratorTimer, d_timer);

public:
	DecoratorTimerNode() 
		: m_pProto(NULL), m_nStart(0), m_bPassStart(false)
	{}
	virtual ~DecoratorTimerNode()		{}

	virtual int GetType()				{return NodeType_DecoratorTimer;}

	void ResetTimer()					{m_nStart = 0;}

protected:

	/**
	 * @brief DecoratorTimerNode _Decorate
	 *
	 * virtual protected 
	 * Logic-Timer, on_timer exec;
	 * @return 		NodeExecState		last state
	 */
	virtual NodeExecState _Decorate()
	{
		bool bRun = false;
		int nTimer = 0;
		if (m_bPassStart)
			nTimer = m_pProto->elpase();
		else
		{
			nTimer = m_pProto->start();
			if (nTimer == 0)
				m_bPassStart = bRun = true;
		}

		if (m_nStart > 0)
		{
			if ((int)::timeGetTime() - m_nStart >= nTimer)
			{
				m_nStart = 0;
				m_bPassStart = bRun = true;
			}
		}
		
		if (!bRun)
			return NodeExec_Fail;

		NodeExecState nRet = m_pChilds[0]->Execute();
		if (nRet == NodeExec_Success)
			m_nStart = ::timeGetTime();
		return nRet;
	}

protected:
	int										m_nStart;
	bool									m_bPassStart;
};


/**
 * DecoratorRandNode
 * 装饰节点
 * 随机
 */
class DecoratorRandNode : public DecoratorNode
{
public:
	DecoratorRandNode() 
		: m_pProto(NULL)				{}
	virtual ~DecoratorRandNode()		{}

	virtual int GetType()				{return NodeType_DecoratorRand;}

	virtual bool LoadProto(const BehaviorPB::Node* pProto);

protected:

	virtual bool PreExecute()
	{
		if (!DecoratorNode::PreExecute())
			return false;

		if (!m_pProto->bb_rnd().empty() && m_pRndKey.IsNull())
			m_pRndKey = GetBlackboard()->WriteValue(m_pProto->bb_rnd(), 0);
		return true;
	}

	/**
	 * @brief DecoratorRandNode _Decorate
	 *
	 * virtual protected 
	 * Logic-Rand, on_rand exec;
	 * @return 		NodeExecState		last state
	 */
	virtual NodeExecState _Decorate()
	{
		int nRandom = m_rand.RANDOM_int(m_pProto->r_begin(), m_pProto->r_end());
		PRINTF("rand = %d (%d, %d)\n", nRandom, m_pProto->r_begin(), m_pProto->r_end());
		if (m_pProto->choose_arr_size() > 0)
		{
			bool bHit = false;
			for (int i = 0; i < m_pProto->choose_arr_size(); ++ i)
				if (nRandom == m_pProto->choose_arr(i))
				{
					bHit = true;
					break;
				}
			if (!bHit)
				return NodeExec_Fail;
		}
		if (!m_pRndKey.IsNull())
			m_pRndKey->Set(nRandom);

		return m_pChilds[0]->Execute();
	}

protected:
	const BehaviorPB::DecoratorRand*		m_pProto;
	G_RANDOM_MGR							m_rand;
	ChalkInkPtr								m_pRndKey;

};

};

#endif /* __BEHAVIOR_DECORATOR_NODE_H__ */
