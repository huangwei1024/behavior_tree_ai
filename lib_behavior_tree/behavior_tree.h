/**
 * @File: 		behavior_tree.h
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

#ifndef __BEHAVIOR_TREE_H__
#define __BEHAVIOR_TREE_H__

#include "behavior_common.h"

namespace BehaviorTree
{

/**
 * behavior base node class
 */
class Node
{
public:
	typedef NodeType				Type;
	typedef const char*				TypeStr;
	typedef NodeExecState			ExecState;
	typedef Node*					Ptr;
	typedef std::list <Ptr>			PtrList;

public:
	Node() : m_pParent(NULL)					{}
	Node(Ptr pParent) : m_pParent(pParent)		{}
	virtual ~Node()								{}

	/**
	 * @brief GetParent
	 *
	 * public 
	 * @return 		BehaviorTree::Node::Ptr
	 */
	Ptr GetParent()								{return m_pParent;}

	/**
	 * @brief ClearChild
	 *
	 * public 
	 */
	void ClearChild()
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			it->ClearChild();
		}
		m_vChilds.clear();
	}

	/**
	 * @brief AddChild
	 *
	 * public 
	 * @param 		pChild [in]
	 */
	void AddChild(Ptr pChild)
	{
		m_vChilds.push_back(pChild);
	}

	/**
	 * @brief EraseChild
	 *
	 * public 
	 * @param 		pChild [in]
	 */
	void EraseChild(Ptr pChild)
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			if (pChild == *it)
			{
				m_vChilds.erase(it);
				break;
			}
		}
	}

	/**
	 * @brief SwapChild
	 *
	 * public 
	 * @param 		pChild1 [in]
	 * @param 		pChild2 [in]
	 */
	void SwapChild(Ptr pChild1, Ptr pChild2)
	{
		PtrList::iterator it, it2, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(), it2 = itEnd; it != itEnd; ++ it)
		{
			if (pChild1 == *it || pChild2 == *it)
			{
				if (it2 != itEnd)
				{
					std::swap(it, it2);
					break;
				}
				it2 = it;
			}
		}
	}

	virtual Type		GetType() = 0;
	virtual TypeStr		GetTypeStr() = 0;
	virtual ExecState	Execute() = 0;

protected:
	Ptr					m_pParent;
	PtrList				m_vChilds;
};


/**
 * behavior param data class
 */
class ParamData
{
public:
	typedef ParamData*				Ptr;

public:
	ParamData();
	virtual ~ParamData();

	virtual	void		SetParam() = 0;
	virtual				GetParam() = 0;
};


/**
 * behavior blackboard class
 */
class Blackboard
{
public:
	typedef Blackboard*				Ptr;

public:
	Blackboard();
	virtual ~Blackboard();

	void				Clear();

protected:
	Tree::Ptr			m_pTree;
	ParamData::Ptr		m_pInput;
	ParamData::Ptr		m_pOutput;
};


/**
 * behavior tree class
 */
class Tree
{
public:
	typedef Tree*					Ptr;

public:
	Tree(Blackboard::Ptr pBlackboard);
	~Tree();

	void				Init(Node::Ptr pRoot);
	void				Clear();
	NodeExecState		Process(ParamData::Ptr pInput, ParamData::Ptr pOutput);
	bool				IsValid();

	bool				DumpFile(const char* szFile);
	bool				LoadFile(const char* szFile);

protected:
	Node::Ptr			m_pRoot;
	Blackboard::Ptr		m_pBlackboard;
};

};

#endif /* __BEHAVIOR_TREE_H__ */
