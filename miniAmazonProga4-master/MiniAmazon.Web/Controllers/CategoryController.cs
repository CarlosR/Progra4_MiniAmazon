using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;
using System.Collections.Generic;

namespace MiniAmazon.Web.Controllers
{
    public class CategoryController : BootstrapBaseController
    {

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public CategoryController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }


        public ActionResult Index()
        {
            IEnumerable<Category> model = _repository.Query<Category>(x => x == x);
            return View(model);

        }

        public ActionResult Create()
        {
            return View(new CategoryModel());
        }

        [HttpPost]
        public ActionResult Create(CategoryModel categoryModel)
        {

            var category = _mappingEngine.Map<CategoryModel, Category>(categoryModel);
            _repository.Create<Category>(category);

            return RedirectToAction("Index", "Category");
        }

        public ActionResult Edit(int id)
        {
            var editing = _repository.GetById<Category>(id);
            var model = Mapper.Map<Category, CategoryModel>(editing);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var category = _mappingEngine.Map<CategoryModel, Category>(model);
                _repository.Update(category);
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

        public ActionResult Details(int id)
        {
            var model = _repository.GetById<Category>(id);

            return View(model);
        }

    }
}
