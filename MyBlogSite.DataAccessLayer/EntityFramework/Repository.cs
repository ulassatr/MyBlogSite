﻿using MyBlogSite.DataAccessLayer;
using MyBlogSite.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase where T : class
    {
       
        private DbSet<T> _objectSet;
        public Repository()
        {
           
            //class yenilendiğinde verilen tipten objectset oluştur
            _objectSet = db.Set<T>();
        }
        public List<T> List()
        {
           return _objectSet.ToList();
        }
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }
        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }
        public int Insert(T obj)
        {
            //Set le hangi tipteyse bul ekle sonra insert et.
            _objectSet.Add(obj);

            if(obj is EntitiyBase)
            {
                EntitiyBase o = obj as EntitiyBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUserName = "system";

            }
            return Save();
        }
        public int Update(T obj)
        {
            if (obj is EntitiyBase)
            {
                EntitiyBase o = obj as EntitiyBase;         
                               
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUserName = "system";

            }
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }
        public int Save()
        {
            return db.SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

    }
}
