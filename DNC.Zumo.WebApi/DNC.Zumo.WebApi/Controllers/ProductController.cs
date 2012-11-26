using DNC.Zumo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DNC.Zumo.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private IRepository<Product> _repo;

        public ProductController() : this(new ZumoRepository<Product>())
        {}

        public ProductController(IRepository<Product> repo)
        {
            _repo = repo;
        }

        public IEnumerable<Product> Get()
        {
            return _repo.GetAll();
        }

        public Product Get(int id)
        {
            return _repo.Get(id);
        }

        public void Post(Product product)
        {
            if (ModelState.IsValid)
                _repo.Add(product);
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public void Put(Product product)
        {
            if (ModelState.IsValid)
                _repo.Update(product);
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}