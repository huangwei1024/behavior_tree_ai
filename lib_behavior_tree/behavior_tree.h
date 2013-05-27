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
#include "callback.hpp"


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
	void SetParent(NonLeafNode* pParent)
	{
		m_pParent = pParent;
		if (m_pParent)
			m_pTree = ((BaseNode*)m_pParent)->m_pTree;
	}

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

	/**
	 * @brief AuxBBoard
	 *
	 * public 
	 * auxiliary for GetBlackboard
	 * @return 		Blackboard&
	 */
	BlackBoard& AuxBBoard()		{return *GetBlackboard();}

	/**
	 * @brief SetRunning
	 *
	 * public 
	 * I running
	 */
	void SetRunning();

	virtual int				GetType() = 0;
	virtual bool			PreExecute() {return true;}
	virtual NodeExecState	Execute() = 0;

	virtual void			ClearChild() {}
	virtual void			AddChild(BaseNode* pChild) {assert(false);}

	/**
	 * @brief LoadProto
	 *
	 * virtual public 
	 * @param 		pProto [in]		protobuf pointer
	 */
	virtual bool			LoadProto(const BehaviorPB::Node* pProto);

protected:
	Tree*					m_pTree;
	NonLeafNode*			m_pParent;
#ifdef _DEBUG
	String					m_sName;
	String					m_sDesc;
#endif
};

/**
 * behavior non-leaf node class
 */
class NonLeafNode : public BaseNode
{
public:
	NonLeafNode()
		: m_nChilds(0), m_nChildsSize(0), m_pChilds(NULL)
	{}

	virtual ~NonLeafNode()
	{
		ClearChild();
		SafeDelete(m_pChilds);
		m_nChilds = 0;
		m_nChildsSize = 0;
	}

	void InitChildsList(int nSize)
	{
		ClearChild();
		if (nSize > m_nChildsSize)
		{
			SafeDelete(m_pChilds);
			m_pChilds = new BaseNode* [nSize];
			m_nChildsSize = nSize;
		}
	}

	virtual void ClearChild()
	{
		for (int i = 0; i < m_nChilds; ++ i)
		{
			NonLeafNode* pNonLeadNode = dynamic_cast<NonLeafNode*> (m_pChilds[i]);
			if (pNonLeadNode)
				pNonLeadNode->ClearChild();
			SafeDelete(m_pChilds[i]);
		}
		m_nChilds = 0;
	}

	virtual void AddChild(BaseNode* pChild)
	{
		pChild->SetParent(this);
		m_pChilds[m_nChilds ++] = pChild;
		assert(m_nChilds <= m_nChildsSize);
	}


	virtual bool PreExecute()
	{
		for (int i = 0; i < m_nChilds; ++ i)
		{
			if (!m_pChilds[i]->PreExecute())
				return false;
		}
		return true;
	}

	virtual bool LoadProto(const BehaviorPB::Node* pProto);

protected:
	BaseNode**		m_pChilds;
	int				m_nChildsSize;
	int				m_nChilds;
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
		Type type;
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

	template <class T>
	operator T()
	{
		return Get<T>();
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
		return NULL;
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
 * behavior chalk ink pointer class
 * 通过ChalkInkPtr实现安全的ChalkInk指针访问
 */
class ChalkInkPtr
{
	friend ChalkInkRef;
public:
	ChalkInkPtr()
	{
		m_pChalk = NULL;
		m_pChalkRef = NULL;
	}

	ChalkInkPtr(ChalkInkRef* pChalkRef)
	{
		m_pChalk = NULL;
		m_pChalkRef = NULL;
		SetOwnerRef(pChalkRef);
	}

	ChalkInkPtr(const ChalkInkPtr& oPtr)
	{
		m_pChalk = NULL;
		m_pChalkRef = NULL;
		SetOwnerRef(oPtr.m_pChalkRef);
	}

	~ChalkInkPtr()
	{
		SetOwnerRef(NULL);
	}

	ChalkInkPtr& operator= (const ChalkInkPtr& oPtr)
	{
		SetOwnerRef(oPtr.m_pChalkRef);
		return *this;
	}

	ChalkInk* operator-> () const
	{
		return m_pChalk;
	}

	operator bool () const
	{
		return !IsNull();
	}

	bool operator! () const
	{
		return IsNull();
	}

	void SetNull()
	{
		SetOwnerRef(NULL);
		m_pChalk = NULL;
		m_pChalkRef = NULL;
	}

	bool IsNull() const;	
	void SetOwnerRef(ChalkInkRef* pChalkRef);

protected:
	ChalkInk*			m_pChalk;
	ChalkInkRef*		m_pChalkRef;
};


/**
 * behavior chalk ink ref class
 * 保证外部ChalkInkPtr访问时，不会出现野指针
 */
class ChalkInkRef
{
public:
	ChalkInkRef(bool bIn = false)
	{
		m_nRef = 1;
		m_bInBlackBoard = bIn;
	}

	~ChalkInkRef()
	{
		assert(m_nRef == 0);
		assert(!m_bInBlackBoard);
	}

	ChalkInk& GetChalk()
	{
		return m_oVal;
	}

	void AddRef(ChalkInkPtr* pPtr)
	{
		pPtr->m_pChalk = &m_oVal;
		pPtr->m_pChalkRef = this;
		++ m_nRef;
	}

	void SubRef(ChalkInkPtr* pPtr)
	{
		if (pPtr)
		{
			pPtr->m_pChalk = NULL;
			pPtr->m_pChalkRef = NULL;
		}
		if (-- m_nRef == 0)
			delete this;
	}

	void InBlackBoard(bool bIn)
	{
		m_bInBlackBoard = bIn;
	}

	bool IsInBlackBoard()
	{
		return m_bInBlackBoard;
	}

protected:
	ChalkInk	m_oVal;
	int			m_nRef;
	bool		m_bInBlackBoard;
};


/**
 * behavior blackboard class
 */
class BlackBoard
{
public:
	friend Tree;
	typedef String								ChalkName;
	typedef std::map <ChalkName, ChalkInkRef*>	ChalkMap;

public:
	BlackBoard()
		: m_pRunningNode(NULL)
	{}

	virtual ~BlackBoard()
	{
		Clear();
	}

	void Clear()
	{
		ChalkMap::iterator it, it_end = m_mapChalks.end();
		for (it = m_mapChalks.begin(); it != it_end; ++ it)
		{
			ChalkInkRef* pRef = it->second;
			pRef->InBlackBoard(false);
			pRef->SubRef(NULL);
		}
		m_mapChalks.clear();
		m_pRunningNode = NULL;
	}

	ChalkInk& operator [] (const ChalkName& sKey)
	{
		typedef std::pair<ChalkMap::iterator, bool> PairItB;
		PairItB prItB = m_mapChalks.insert(std::make_pair(sKey, new ChalkInkRef(true)));
		ChalkInkRef* pRef = prItB.first->second;
		return pRef->GetChalk();
	}

	ChalkInkPtr WriteValue(const ChalkName& sKey, const ChalkInk& oVal)
	{
		typedef std::pair<ChalkMap::iterator, bool> PairItB;
		PairItB prItB = m_mapChalks.insert(std::make_pair(sKey, new ChalkInkRef(true)));
		ChalkInkRef* pRef = prItB.first->second;
		pRef->GetChalk() = oVal;
		return ChalkInkPtr(pRef);
	}

	ChalkInkPtr LookupValue(const ChalkName& sKey)
	{
		ChalkMap::iterator it = m_mapChalks.find(sKey);
		if (it == m_mapChalks.end())
			return ChalkInkPtr();
		return ChalkInkPtr(it->second);
	}

	void EraseValue(const ChalkName& sKey)
	{
		ChalkMap::iterator it = m_mapChalks.find(sKey);
		if (it == m_mapChalks.end())
			return;

		ChalkInkRef* pRef = it->second;
		pRef->InBlackBoard(false);
		pRef->SubRef(NULL);
		m_mapChalks.erase(it);
	}

	BaseNode* GetRunning()
	{
		return m_pRunningNode;
	}

	void SetRunning(BaseNode* pRunning)
	{
		m_pRunningNode = pRunning;
	}

protected:
	// running state
	BaseNode*			m_pRunningNode;
	ChalkMap			m_mapChalks;
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
	}

	bool LoadFile(BehaviorPB::Tree* pProto);

protected:
	String				m_sName;
	BaseNode*			m_pRoot;
	BlackBoard*			m_pBlackboard;
};


/**
 * behavior tree protobuf factory
 */
class TreeFactory
{
public:
	struct TreeCache;
	typedef String							TreeName;
	typedef std::map <TreeName, TreeCache>	TreeProtoMap;

	struct TreeCache
	{
		String				sTreeName;
		String				sFilePath;
		BehaviorPB::Tree*	pProto;
	};

public:
	virtual ~TreeFactory();

	static TreeFactory*			GetInstance()	{static TreeFactory ins; return &ins;}
	Tree*						CreateTree(TreeName sName, Tree* pParentTree = NULL, BlackBoard* pBlackboard = NULL);
	void						RegisterTree(String sPath);

protected:
	TreeFactory()			{}

protected:
	TreeProtoMap	m_mapTree;
};

};

#endif /* __BEHAVIOR_TREE_H__ */
