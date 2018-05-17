using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using YCF.Common;
using YCF.Model;

namespace YCF.DAL
{
    /// <summary>
    /// DAL的基类，DAL可继承此类，封装有基础的增删改查
    /// lrp
    /// 2016-11-01
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DALBase<T> where T : class
    {
        protected OracleEntities db = null;
        protected readonly string connString = CommonHelper.EFConnentionString;
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns>受影响的行数</returns>
        public virtual int Add(T entity)
        {
            using (db = new OracleEntities(connString))
            {
                db.Set<T>().Add(entity);
                db.Configuration.ValidateOnSaveEnabled = false;

                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 添加一个集合的数据
        /// </summary>
        /// <param name="list">要添加的实体集合</param>
        /// <returns>受影响的行数</returns>
        public virtual int Add(IList<T> list)
        {
            using (db = new OracleEntities(connString))
            {
                db.Set<T>().AddRange(list);
                db.Configuration.ValidateOnSaveEnabled = false;

                try
                {
                    return db.SaveChanges();

                }
                catch (Exception e)
                {

                    throw e;
                }

            }
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns>当前添加的实体</returns>
        public virtual T AddAndReturnCurrent(T entity)
        {
            using (db = new OracleEntities(connString))
            {
                var current = db.Set<T>().Add(entity);
                db.Configuration.ValidateOnSaveEnabled = false;

                db.SaveChanges();

                return current;
            }
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <param name="propNames">需要修改的属性的属性名，不填则代表所有属性</param>
        /// <returns>受影响的行数</returns>
        public virtual int Update(T entity, params string[] propNames)
        {
            using (db = new OracleEntities(connString))
            {
                var entry = db.Entry(entity);

                if (propNames != null && propNames.Length > 0)
                {
                    entry.State = System.Data.Entity.EntityState.Unchanged;

                    foreach (string propName in propNames)
                    {
                        if (propName.ToLower() != "id")
                        {
                            entry.Property(propName).IsModified = true;
                        }
                    }
                }
                else
                {
                    entry.State = System.Data.Entity.EntityState.Modified;
                }

                db.Configuration.ValidateOnSaveEnabled = false;
                return db.SaveChanges();
            }

        }

        /// <summary>
        /// 修改多条数据
        /// </summary>
        /// <param name="entity">要修改的实体集合</param>
        /// <param name="propNames">需要修改的属性的属性名，不填则代表所有属性</param>
        /// <returns>受影响的行数</returns>
        public virtual int Update(IList<T> list, params string[] propNames)
        {
            using (db = new OracleEntities(connString))
            {
                foreach (var entity in list)
                {
                    var entry = db.Entry(entity);

                    if (propNames != null && propNames.Length > 0)
                    {
                        entry.State = EntityState.Unchanged;

                        foreach (string propName in propNames)
                        {
                            if (propName.ToLower() != "id")
                            {
                                entry.Property(propName).IsModified = true;
                            }
                        }
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }

                db.Configuration.ValidateOnSaveEnabled = false;
                return db.SaveChanges();
            }

        }

        /// <summary>
        /// 批量修改满足满足条件的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereLambda"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual int Update(T entity, Expression<Func<T, bool>> whereLambda, params string[] propertyNames)
        {
            using (db = new OracleEntities(connString))
            {
                List<T> list = db.Set<T>().Where(whereLambda).ToList();

                Type t = typeof(T);

                List<PropertyInfo> propertyInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

                Dictionary<string, PropertyInfo> dicPropertys = new Dictionary<string, PropertyInfo>();

                propertyInfos.ForEach(p =>
                {
                    if (propertyNames.Contains(p.Name))
                    {
                        dicPropertys.Add(p.Name, p);
                    }
                });

                if (propertyNames != null && propertyNames.Length > 0)
                {
                    foreach (var propertyName in propertyNames)
                    {
                        if (dicPropertys.ContainsKey(propertyName))
                        {
                            PropertyInfo propInfo = dicPropertys[propertyName];
                            object newValue = propInfo.GetValue(entity, null);
                            foreach (T item in list)
                            {
                                propInfo.SetValue(item, newValue, null);
                                db.Entry(item).Property(propInfo.Name).IsModified = true;
                            }

                        }
                    }
                }
                else
                {
                    foreach (var property in propertyInfos)
                    {
                        if (property.Name.ToLower() != "id")
                        {
                            object newValue = property.GetValue(entity, null);
                            foreach (T item in list)
                            {
                                property.SetValue(item, newValue, null);
                                db.Entry(item).Property(property.Name).IsModified = true;
                            }
                        }
                    }
                }

                db.Configuration.ValidateOnSaveEnabled = false;



                return db.SaveChanges();
            }

        }

        /// <summary>
        /// 通过主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>受影响的行数</returns>
        public virtual int Delete(object id)
        {
            using (db = new OracleEntities(connString))
            {
                try
                {
                    var entity = db.Set<T>().Find(id);
                    if (entity != null)
                    {
                        db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;

                        return db.SaveChanges();
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }


            }

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity">需要删除的实体</param>
        /// <returns>受影响的行数</returns>
        public virtual int Delete(T entity)
        {
            using (db = new OracleEntities(connString))
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;

                return db.SaveChanges();
            }

        }

        /// <summary>
        /// 删除满足条件的所有实体
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns>受影响的行数</returns>
        public virtual int Delete(Expression<Func<T, bool>> whereLambda)
        {
            using (db = new OracleEntities(connString))
            {
                List<T> list = db.Set<T>().Where(whereLambda).ToList();

                list.ForEach(t =>
                {
                    db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                });

                return db.SaveChanges();
            }

        }



        /// <summary>
        /// 获取总的数据条数
        /// </summary>
        /// <returns>数据条数</returns>
        public virtual int Count()
        {
            using (db = new OracleEntities(connString))
            {
                return db.Set<T>().Count();
            }
        }

        /// <summary>
        /// 获取满足条件的数据条数
        /// </summary>
        /// <param name="whereCount">条件</param>
        /// <returns>数据条数</returns>
        public virtual int Count(Expression<Func<T, bool>> whereCount)
        {
            using (db = new OracleEntities(connString))
            {
                return db.Set<T>().Count(whereCount);
            }
        }

        /// <summary>
        /// 获取指定字段的最大值
        /// </summary>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="selector">需要取的字段</param>
        /// <returns></returns>
        public virtual TSelector Max<TSelector>(Expression<Func<T, TSelector>> selector)
        {
            using (db = new OracleEntities(connString))
            {
                return db.Set<T>().Max(selector);
            }
        }

        /// <summary>
        /// 获取指定字段的最大值
        /// </summary>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="selector">需要取的字段</param>
        /// <returns></returns>
        public virtual TSelector Max<TSelector>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TSelector>> selector)
        {
            using (db = new OracleEntities(connString))
            {
                return db.Set<T>().Where(whereLambda).Max(selector);
            }
        }

        /// <summary>
        /// 获取指定字段的最小值
        /// </summary>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="selector">需要取的字段</param>
        /// <returns></returns>
        public virtual TSelector Min<TSelector>(Expression<Func<T, TSelector>> selector)
        {
            using (db = new OracleEntities(connString))
            {
                return db.Set<T>().Min(selector);
            }
        }

        /// <summary>
        /// 获取指定字段的最小值
        /// </summary>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="selector">需要取的字段</param>
        /// <returns></returns>
        public virtual TSelector Min<TSelector>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TSelector>> selector)
        {
            using (db = new OracleEntities(connString))
            {
                return db.Set<T>().Where(whereLambda).Min(selector);
            }
        }

        /// <summary>
        /// 通过主键查找实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体对象</returns>
        public virtual T Get(object id)
        {
            using (db = new OracleEntities(connString))
            {
                return db.Set<T>().Find(id);
            }
        }

        /// <summary>
        /// 查找满足条件的第一个实体，未找到则返回null
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns>实体对象</returns>
        public virtual T Get(Expression<Func<T, bool>> whereLambda)
        {
            using (db = new OracleEntities(connString))
            {
                try
                {
                    return db.Set<T>().Where(whereLambda).AsNoTracking().FirstOrDefault();

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        /// <summary>
        /// 查找满足条件的第一个实体的指定字段数据
        /// </summary>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="selector">指定字段对象</param>
        /// <returns>指定字段的数据对象</returns>
        public virtual TSelector Get<TSelector>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TSelector>> selector)
        {
            using (db = new OracleEntities(connString))
            {
                try
                {
                    return db.Set<T>().Where(whereLambda).AsNoTracking().Select(selector).FirstOrDefault();

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        /// <summary>
        /// 通过条件和排序查找满足条件的第一个实体，未找到则返回null
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>实体对象</returns>
        public virtual T Get<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderLambda).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).AsNoTracking().FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// 通过条件和排序查找满足条件的第一个实体的指定字段数据
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>指定字段的数据对象</returns>
        public virtual TSelector Get<TKey, TSelector>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, Expression<Func<T, TSelector>> selector, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderLambda).AsNoTracking().Select(selector).FirstOrDefault();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).AsNoTracking().Select(selector).FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// 通过条件和多个排序查找满足条件的第一个实体，未找到则返回null
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>实体对象</returns>
        public virtual T Get<TKey>(Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }

                }

                return queryable.AsNoTracking().FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过条件和多个排序查找满足条件的第一个实体的指定字段数据
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>指定字段的数据对象</returns>
        public virtual TSelector Get<TKey, TSelector>(Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, Expression<Func<T, TSelector>> selector, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }

                }

                return queryable.AsNoTracking().Select(selector).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns>实体集合</returns>
        public virtual IList<T> GetList()
        {

            try
            {
                using (db = new OracleEntities(connString))
                {
                    return db.Set<T>().AsNoTracking().ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 获取指定字段的数据集合
        /// </summary>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="selector">指定字段对象</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual IList<TSelector> GetList<TSelector>(Expression<Func<T, TSelector>> selector)
        {
            try
            {
                using (db = new OracleEntities(connString))
                {
                    return db.Set<T>().AsNoTracking().Select(selector).ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 获取满足条件的实体集合
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns>实体集合</returns>
        public virtual IList<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            using (db = new OracleEntities(connString))
            {
                try
                {
                    return db.Set<T>().Where(whereLambda).AsNoTracking().ToList();

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        /// <summary>
        /// 获取满足条件的指定字段的数据集合
        /// </summary>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="selector">指定字段对象</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual IList<TSelector> GetList<TSelector>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TSelector>> selector)
        {
            using (db = new OracleEntities(connString))
            {
                try
                {
                    return db.Set<T>().Where(whereLambda).AsNoTracking().Select(selector).ToList();

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        /// <summary>
        /// 获取满足条件并指定排序的实体集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>实体集合</returns>
        public virtual IList<T> GetList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderLambda).AsNoTracking().ToList();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).AsNoTracking().ToList();
                }
            }
        }

        /// <summary>
        /// 获取满足条件并指定排序，指定字段的数据集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual IList<TSelector> GetList<TKey, TSelector>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, Expression<Func<T, TSelector>> selector, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderLambda).AsNoTracking().Select(selector).ToList();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).AsNoTracking().Select(selector).ToList();
                }
            }
        }

        /// <summary>
        /// 获取满足条件并指定排序，指定条数的实体集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="top">条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>实体集合</returns>
        public virtual IList<T> GetList<TKey>(int top, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderLambda).Take(top).AsNoTracking().ToList();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Take(top).AsNoTracking().ToList();
                }
            }
        }

        /// <summary>
        /// 获取满足条件并指定排序，指定条数,指定字段的数据集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="top">条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambda">排序</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual IList<TSelector> GetList<TKey, TSelector>(int top, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, Expression<Func<T, TSelector>> selector, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderLambda).Take(top).AsNoTracking().Select(selector).ToList();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Take(top).AsNoTracking().Select(selector).ToList();
                }
            }
        }

        /// <summary>
        /// 通过条件和多个排序查找满足条件的实体集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>实体集合</returns>
        public virtual IList<T> GetList<TKey>(Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// 通过条件和多个排序查找满足条件,指定字段的数据集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual IList<TSelector> GetList<TKey, TSelector>(Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, Expression<Func<T, TSelector>> selector, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.AsNoTracking().Select(selector).ToList();
            }
        }

        /// <summary>
        /// 获取满足条件并指定多个排序，指定条数的实体集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="top">条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>实体集合</returns>
        public virtual IList<T> GetList<TKey>(int top, Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.Take(top).AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// 获取满足条件并指定多个排序，指定条数,指定字段的数据集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="top">条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual IList<TSelector> GetList<TKey, TSelector>(int top, Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, Expression<Func<T, TSelector>> selector, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.Take(top).AsNoTracking().Select(selector).ToList();
            }
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderByLambda">排序</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>分页实体集合</returns>
        public virtual List<T> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderByLambda, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
                }
            }
        }

        /// <summary>
        /// 分页查询指定字段的数据集合
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderByLambda">排序</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual List<TSelector> GetPagedList<TKey, TSelector>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderByLambda, Expression<Func<T, TSelector>> selector, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                if (isAsc)
                {
                    return db.Set<T>().Where(whereLambda).OrderBy(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().Select(selector).ToList();
                }
                else
                {
                    return db.Set<T>().Where(whereLambda).OrderByDescending(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().Select(selector).ToList();
                }
            }
        }

        /// <summary>
        /// 分页查询，并输出总条数
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="rowCount">总条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderByLambda">排序</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>分页实体集合</returns>
        public virtual List<T> GetPagedList<TKey>(int pageIndex, int pageSize, ref int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderByLambda, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                rowCount = db.Set<T>().Where(whereLambda).Count();
                if (isAsc)
                {
                    return db.Set<T>().OrderBy(orderByLambda).Where(whereLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
                }
                else
                {
                    return db.Set<T>().OrderByDescending(orderByLambda).Where(whereLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
                }
            }
        }

        /// <summary>
        /// 分页查询指定字段的数据集合，并输出总条数
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="rowCount">总条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderByLambda">排序</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAsc">是否顺序，默认值为true</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual List<TSelector> GetPagedList<TKey, TSelector>(int pageIndex, int pageSize, ref int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderByLambda, Expression<Func<T, TSelector>> selector, bool isAsc = true)
        {
            using (db = new OracleEntities(connString))
            {
                rowCount = db.Set<T>().Where(whereLambda).Count();
                if (isAsc)
                {
                    return db.Set<T>().OrderBy(orderByLambda).Where(whereLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().Select(selector).ToList();
                }
                else
                {
                    return db.Set<T>().OrderByDescending(orderByLambda).Where(whereLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().Select(selector).ToList();
                }
            }
        }

        /// <summary>
        /// 多字段排序的分页查询
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>分页实体集合</returns>
        public virtual List<T> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            var type = typeof(TKey);
            var tkey = type.Assembly.CreateInstance(type.FullName);

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// 多字段排序的指定字段的分页查询
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual List<TSelector> GetPagedList<TKey, TSelector>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, Expression<Func<T, TSelector>> selector, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            var type = typeof(TKey);
            var tkey = type.Assembly.CreateInstance(type.FullName);

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().Select(selector).ToList();
            }
        }


        /// <summary>
        /// 分页查询，并输出总条数
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="rowCount">总条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderByLambda">排序集合</param>
        /// <param name="isAsc">是否顺序集合</param>
        /// <returns>分页实体集合</returns>
        public virtual List<T> GetPagedList<TKey>(int pageIndex, int pageSize, ref int rowCount, Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);
                rowCount = db.Set<T>().Where(whereLambda).Count();

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// 多字段排序的指定字段的分页查询
        /// </summary>
        /// <typeparam name="TKey">实体对象的属性</typeparam>
        /// <typeparam name="TSelector">指定字段对象类型</typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="rowCount">总条数</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderLambdas">排序集合</param>
        /// <param name="selector">指定字段对象</param>
        /// <param name="isAscs">是否顺序集合</param>
        /// <returns>指定字段的数据集合</returns>
        public virtual List<TSelector> GetPagedList<TKey, TSelector>(int pageIndex, int pageSize, ref int rowCount, Expression<Func<T, bool>> whereLambda, IList<Expression<Func<T, TKey>>> orderLambdas, Expression<Func<T, TSelector>> selector, IList<bool> isAscs)
        {
            if (orderLambdas != null)
            {
                if (isAscs == null || orderLambdas.Count != isAscs.Count)
                {
                    throw new Exception("\"排序\"的数量必须和\"是否顺序\"的数量一致");
                }

            }

            bool isFirstOrder = true;

            using (db = new OracleEntities(connString))
            {
                var queryable = db.Set<T>().Where(whereLambda);
                rowCount = db.Set<T>().Where(whereLambda).Count();

                for (int i = 0; i < orderLambdas.Count; i++)
                {
                    var orderLambda = orderLambdas[i];
                    var isAsc = isAscs[i];

                    if (isFirstOrder)
                    {
                        if (isAsc)
                        {
                            queryable = queryable.OrderBy(orderLambda);
                        }
                        else
                        {
                            queryable = queryable.OrderByDescending(orderLambda);
                        }

                        isFirstOrder = false;
                    }
                    else
                    {
                        if (isAsc)
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenBy(orderLambda);
                        }
                        else
                        {
                            queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(orderLambda);
                        }
                    }
                }

                return queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().Select(selector).ToList();
            }
        }
    }
}
