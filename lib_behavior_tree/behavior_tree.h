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
	typedef int						Type;
	typedef const char*				TypeStr;
	typedef Node*					Ptr;
	typedef std::vector <Ptr>		PtrVec;

public:
	Node() : m_pParent(NULL)					{}
	Node(Ptr pParent) : m_pParent(pParent)		{}
	virtual ~Node()								{}

	Ptr					GetParent()				{return m_pParent;}

	virtual void		AddChild(Ptr pChild);
	virtual void		EraseChild(Ptr pChild);

	virtual Type		GetType() = 0;
	virtual TypeStr		GetTypeStr() = 0;
	virtual bool		Process() = 0;

protected:
	Ptr					m_pParent;
	PtrVec				m_vChilds;
};

/**
 * behavior tree class
 */
class Tree
{
public:
	Tree();
	~Tree();

	void				Init();
	void				Clear();
	bool				Process();
	bool				IsValid();

	bool				DumpFile(const char* szFile);
	bool				LoadFile(const char* szFile);

protected:
	Node::Ptr			m_pRoot;
};

};

#endif /* __BEHAVIOR_TREE_H__ */
