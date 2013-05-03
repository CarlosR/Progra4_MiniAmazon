using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;
using System.Collections.Generic;

namespace MiniAmazon.Web.Controllers
{
    public class SalesController : BootstrapBaseController
    {

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public SalesController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }


        public ActionResult Index()
        {
            IEnumerable<Sale> model = _repository.Query<Sale>(x => x == x);
            return View(model);

        }

        public ActionResult Create()
        {
            return View(new SalesModel());
        }

        [HttpPost]
        public ActionResult Create(SalesModel salesModel)
        {
            var category = _repository.First<Category>(x => x.Id == salesModel.CategoryId);

            var sale = _mappingEngine.Map<SalesModel, Sale>(salesModel);
            sale.Category = category;
            sale.CreateDateTime = System.DateTime.Now;
            _repository.Create<Sale>(sale);
            
            var account = _repository.First<Account>(x => x.Id == salesModel.AccountId);
            account.AddSale(sale);
            _repository.Update(account);

            return RedirectToAction("Index", "Sales");
        }

        public ActionResult Edit(int id)
        {
            var editing = _repository.GetById<Sale>(id);
            var model = Mapper.Map<Sale, SalesModel>(editing);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(SalesModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var category = _repository.First<Category>(x => x.Id == model.CategoryId);
                var sale = _mappingEngine.Map<SalesModel, Sale>(model);
                sale.Category = category;

                sale.CreateDateTime = System.DateTime.Now;
                _repository.Update(sale);
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

        public ActionResult Details(int id)
        {
            var model = _repository.GetById<Sale>(id);

            return View(model);
        }

    }
}