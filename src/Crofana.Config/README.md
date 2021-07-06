# Crofana.Config

## 概述

Crofana.Config模块提供一组只读的缓存接口，常用于对大量数据进行频繁读取操作的场景，如游戏配置。缓存以类名和一个长整形字段ID唯一标识一条数据，在初始化时从数据源加载数据，并进行反序列化和依赖注入操作，以强类型的形式缓存数据对象，同时这些数据对象之间拥有正确的引用关系。类名在反序列化后将转化为类对象，因此用户在查询一个对象时只需传入类对象和ID即可。缓存有一些可选功能将在下文介绍。此模块提供一个默认的缓存实现，使用基于内存的缓存，从Excel表格（xlsx格式）中读取数据，提供条件查询、元数据和自定义反序列化支持。

## 功能

### 基础功能

此模块中的缓存称为DataContext，其基础功能声明在IDataContext接口中，包括**加载数据源**和**单点查询**。

### 可选功能

#### 条件查询（IConditionalQueryDataContext、IConditionalQueryMetadataContext）

#### 元数据（IMetadataContext)

#### 自定义反序列化（ICustomParserDataContext）

#### 继承（IHierarchicalDataContext）

#### 别名（IAliasableDataContext）

#### 迭代（IEnumerable<IConfig\>）



