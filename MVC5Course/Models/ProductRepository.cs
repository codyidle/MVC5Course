using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }

        public override void Delete(Product entity)
        {
            entity.IsDelete = true;
        }

        public IQueryable<Product> All(bool isAll = false)
        {
            if (isAll)
                return base.All();
            else
                return this.All();
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}