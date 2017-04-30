﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;
using System.Data.Entity;

namespace MobileMart.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MobileMartEntities _context;
        public ProductRepository()
        {
            _context = new MobileMartEntities();
        }
        public Product GetProductByID(int? productID)
        {
            return _context.Products.FirstOrDefault(s => s.ProductID == productID);
        }
        public void delete(int id)
        {
            var delete = _context.Products.Where(s => s.ProductID == id).FirstOrDefault();
            _context.Products.Remove(delete);
            _context.SaveChanges();
        }

        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetProduct(int? shopID)
        {
            return _context.Products.Where(w=>w.Supplier.Shop.ShopID == shopID).ToList();
        }

        public void insert(Product entity)
        {
                _context.Products.Add(entity);
                _context.SaveChanges();
        }

        public void update(Product entity)
        { 
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public int InsertAndGetID(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
            return entity.ProductID;
        }
    }
}
