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

// conversion from '__w64 int' to 'int', possible loss of data
#pragma warning(disable : 4244)
// conversion from 'size_t' to 'int', possible loss of data
#pragma warning(disable : 4267)
// pointer truncation from 'void *' to 'int'
#pragma warning(disable : 4311)

#include "behavior_common.h"
#include "callback.hpp"

namespace BehaviorPB
{
class Tree;
class Node;
};


namespace BehaviorTree
{

/**
 * behavior base node class
 */
class BaseNode
{
public:
	friend Tree;
	typedef const char*					TypeStr;
	typedef BehaviorPB::Node			Proto;

public:
	BaseNode()
		: m_pParent(NULL), m_pTree(NULL)
	{}

	virtual ~BaseNode()
	{
		m_pParent = NULL;
		m_pTree = NULL;
	}

	/**
	 * @brief SetParent
	 *
	 * public 
	 */
	void SetParent(NonLeafNode* pParent);

	/**
	 * @brief GetParent
	 *
	 * public 
	 * @return 		BehaviorTree::Node*
	 */
	NonLeafNode* GetParent()
	{
		return m_pParent;
	}


	/**
	 * @brief GetBlackboard
	 *
	 * public 
	 * Blackboard belong to Tree
	 * @return 		Blackboard*
	 */
	BlackBoard* GetBlackboard();

	virtual void ClearChild() {}
	virtual void AddChild(BaseNode* pChild) {assert(false);}
	virtual void DeleteChild(BaseNode* pChild) {assert(false);}
	virtual void SwapChild(BaseNode* pChild1, BaseNode* pChild2) {assert(false);}

	/**
	 * @brief LoadProto
	 *
	 * virtual public 
	 * @param 		pProto [in]		protobuf pointer
	 */
	virtual bool			LoadProto(const Proto* pProto);

	/**
	 * @brief DumpProto
	 *
	 * virtual public 
	 * @param 		pProto [out]	protobuf pointer
	 */
	virtual bool			DumpProto(Proto* pProto);

	virtual int				GetType() = 0;
	virtual NodeExecState	Execute() = 0;

protected:
	Tree*					m_pTree;
	NonLeafNode*			m_pParent;
};

/**
 * behavior non-leaf node class
 */
class NonLeafNode : public BaseNode
{
public:
	typedef std::list <BaseNode*>	PtrList;

public:
	NonLeafNode()
	{}

	virtual ~NonLeafNode()
	{
		ClearChild();
	}

	virtual void ClearChild()
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			(*it)->ClearChild();
			delete (*it);
		}
		m_vChilds.clear();
	}

	virtual void AddChild(BaseNode* pChild)
	{
		pChild->SetParent(this);
		m_vChilds.push_back(pChild);
	}

	virtual void DeleteChild(BaseNode* pChild)
	{
		PtrList::iterator it, itEnd = m_vChilds.end();
		for (it = m_vChilds.begin(); it != itEnd; ++ it)
		{
			if (pChild == *it)
			{
				SafeDelete(*it);
				m_vChilds.erase(it);
				break;
			}
		}
	}

	virtual void SwapChild(BaseNode* pChild1, BaseNode* pChild2)
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

	virtual bool	LoadProto(const Proto* pProto);
	virtual bool	DumpProto(Proto* pProto);

protected:
	PtrList			m_vChilds;
};

/**
 * behavior node factory
 */
class NodeFactory
{
public:
	typedef cb::Callback0 <BaseNode*>		CreateCallback;
	typedef std::map <int, CreateCallback>	NodeClassMap;

public:
	static BaseNode*	CreateInstance(int nType);
	static void			Register(int nType, const CreateCallback& cbCreate);

protected:
	static NodeClassMap	ms_mapNodes;
};

/**
 * behavior chalk ink class
 */
class ChalkInk
{
public:
	enum Type
	{
		Type_Null = 0,
		Type_Bool,
		Type_Int,
		Type_UInt,
		Type_Float,
		Type_String,
		Type_Pointer,
	};

	struct Content
	{
		char type;
		union
		{
			bool				b;
			int					n;
			unsigned int		un;
			float				f;
			void*				ptr;
		};
		std::string				str;
	};

public:
	ChalkInk()
	{
		m_oVal.type = Type_Null;
	}

	template <class T>
	ChalkInk(T v)
	{
		Set(v);
	}

#define SetFunc(ctype, var, etype) \
	void Set(ctype v) \
	{ \
		m_oVal.type = etype; \
		m_oVal.var = v; \
	}
	
	SetFunc(bool,			b,		Type_Bool);
	SetFunc(int,			n,		Type_Int);
	SetFunc(unsigned int,	un,		Type_UInt);
	SetFunc(float,			f,		Type_Float);
	SetFunc(void*,			ptr,	Type_Pointer);
	SetFunc(int*,			ptr,	Type_Pointer);
	SetFunc(float*,			ptr,	Type_Pointer);
	SetFunc(unsigned char*,	ptr,	Type_Pointer);

	void Set(char* v)
	{
		m_oVal.type = Type_String;
		m_oVal.str = v;
	}

	void Set(const std::string& v)
	{
		m_oVal.type = Type_String;
		m_oVal.str = v;
	}

	template <class R>
	R Get()
	{
		switch (m_oVal.type)
		{
		case Type_Null:
			return 0;
		case Type_Bool:
			return m_oVal.b;
		case Type_Int:
			return m_oVal.n;
		case Type_UInt:
			return m_oVal.un;
		case Type_Float:
			return m_oVal.f;
		case Type_Pointer:
			return (R)m_oVal.ptr;
		}
		return 0;
	}

	template <>
	char* Get()
	{
		switch (m_oVal.type)
		{
		case Type_String:
			return (char*)m_oVal.str.c_str();
		case Type_Pointer:
			return (char*)m_oVal.ptr;
		}
		return "";
	}

	template <>
	std::string Get()
	{
		switch (m_oVal.type)
		{
		case Type_String:
			return m_oVal.str;
		case Type_Pointer:
			return (char*)m_oVal.ptr;
		}
		return "";
	}

	bool IsEmpty()
	{
		return m_oVal.type == Type_Null;
	}

	void SetEmpty()
	{
		m_oVal.type = Type_Null;
		m_oVal.str.clear();
	}

protected:
	Content		m_oVal;
};

/**
 * behavior blackboard class
 */
class BlackBoard
{
public:
	friend Tree;
	typedef String								ChalkName;
	typedef std::map <ChalkName, ChalkInk>		ChalkMap;

public:
	BlackBoard()
		: m_pTree(NULL), m_pRunningNode(NULL)
	{}

	virtual ~BlackBoard()
	{}

	void Clear()
	{
		m_valNull.SetEmpty();
		m_mapChalks.clear();
		m_pTree = NULL;
		m_pRunningNode = NULL;
	}

	void WriteValue(const ChalkName& sKey, const ChalkInk& oVal)
	{
		m_mapChalks[sKey] = oVal;
	}

	ChalkInk& LookupValue(const ChalkName& sKey)
	{
		ChalkMap::iterator it = m_mapChalks.find(sKey);
		if (it == m_mapChalks.end())
		{
			m_valNull.SetEmpty();
			return m_valNull;
		}
		return it->second;
	}

	void EraseValue(const ChalkName& sKey)
	{
		m_mapChalks.erase(sKey);
	}


protected:
	Tree*				m_pTree;
	BaseNode*			m_pRunningNode;
	ChalkMap			m_mapChalks;
	ChalkInk			m_valNull;
};


/**
 * behavior tree class
 *
 * Node生命周期内部管理
 * Blackboard只是内部使用，不管理
 */
class Tree
{
public:
	typedef BehaviorPB::Tree	Proto;

public:
	Tree();
	Tree(BlackBoard* pBlackboard);
	~Tree();

	void				Init();
	void				Clear();
	NodeExecState		Process();
	bool				IsValid();
	BaseNode*			GetRoot()			{return m_pRoot;}
	BlackBoard*			GetBlackboard()		{return m_pBlackboard;}
	String&				Name()				{return m_sName;}

	void SetRoot(BaseNode* pRoot)
	{
		if (m_pRoot != pRoot)
			SafeDelete(m_pRoot);
		m_pRoot = pRoot;
		m_pRoot->m_pTree = this;
		m_pRoot->m_pParent = NULL;
	}

	void SetBlackboard(BlackBoard* pBlackboard)
	{
		m_pBlackboard = pBlackboard;
		m_pBlackboard->m_pTree = this;
	}

	bool				DumpFile(const char* szFile);
	bool				LoadFile(const char* szFile);

protected:
	String				m_sName;
	BaseNode*			m_pRoot;
	BlackBoard*			m_pBlackboard;
};


/**
 * behavior tree protobuf factory
 */
class TreeProtoFactory
{
public:
	typedef String							TreeName;
	typedef String							ProtoPath;
	typedef std::map <TreeName, ProtoPath>	TreeProtoMap;

public:
	static Tree*		CreateInstance(TreeName sName);
	static void			Register(TreeName sName, ProtoPath sPath);

protected:
	static TreeProtoMap	ms_mapTree;
};

};

#endif /* __BEHAVIOR_TREE_H__ */
