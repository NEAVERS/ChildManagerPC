using YCF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;

namespace YCF.BLL
{
    public class BLLBase<T, D>
        where T : class, new()
        where D : DALBase<T>, new()
    {
        TempDateLogic dg = new TempDateLogic();
        protected D dal = (D)typeof(D).GetConstructor(Type.EmptyTypes).Invoke(null);

        protected List<Expression<Func<T, object>>> orders;
        protected List<bool> isAscs;
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns>是否成功</returns>
        public virtual bool Add(T entity)
        {
            return dal.Add(entity) > 0;
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns>当前添加的实体</returns>
        public virtual T AddAndReturnCurrent(T entity)
        {
            return dal.AddAndReturnCurrent(entity);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <param name="propNames">需要修改的属性的属性名，不填则代表所有属性</param>
        /// <returns>是否成功</returns>
        public virtual bool Update(T entity, params string[] propNames)
        {
            return dal.Update(entity, propNames) > 0;
        }

        /// <summary>
        /// 通过主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>受影响的行数</returns>
        public virtual bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }


        public virtual bool Delete(decimal id)
        {
            return dal.Delete(id) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity">需要删除的实体</param>
        /// <returns>受影响的行数</returns>
        public virtual bool Delete(T entity)
        {
            return dal.Delete(entity) > 0;
        }

        /// <summary>
        /// 通过主键查找实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体对象</returns>
        public virtual T Get(int id)
        {
            return dal.Get(id);
        }
        public virtual T Get(decimal id)
        {
            return dal.Get(id);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns>实体集合</returns>
        public virtual IList<T> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// 获取指定字段的最大值
        /// </summary>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="selector">需要取的字段</param>
        /// <returns></returns>
        public virtual TSelector Max<TSelector>(Expression<Func<T, TSelector>> selector)
        {
            return dal.Max(selector);
        }

        public DataSet getDataSet(string tempstr)
        {
            return dg.getDataSet(tempstr);
        }
    }
}
