using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using InventorySystem.Models;

namespace InventorySystem.Controllers
{
    public class ProductsController : ApiController
    {
        private InventorySystemEntities db = new InventorySystemEntities();

        // GET: api/Products
       
        public List<P_getAllProducts_Result> GetProducts()
        {
            return db.P_getAllProducts().ToList();
        }
        public List<P_getAllProduct_by_category_Result> GetProducts(string categoryName)
        {
            return db.P_getAllProduct_by_category(categoryName).ToList();
        }
        

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Product_id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Products.Add(product);
                db.SaveChanges();
            }catch(ArgumentNullException ex)
            {
                return BadRequest("values are null");
            }
            

            return CreatedAtRoute("DefaultApi", new { id = product.Product_id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }
        //delete product by Name
        public IHttpActionResult DeleteProduct(string name)
        {
            var products = db.Products.Where(x => x.Name == name);
            if (products.Any())
            {
                db.Products.RemoveRange(products);
                db.SaveChanges();

                return Ok("product is removed");
            }else
            {
                return BadRequest("product with this name not found ");
            }
           

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Product_id == id) > 0;
        }
    }
}