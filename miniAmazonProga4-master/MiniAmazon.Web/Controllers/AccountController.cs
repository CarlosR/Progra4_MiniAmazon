using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;
using System.Collections.Generic;

namespace MiniAmazon.Web.Controllers
{
    public class AccountController : BootstrapBaseController
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public AccountController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }

        public ActionResult SignIn()
        {
            return View(new AccountSignInModel());
        }

        [HttpPost]
        public ActionResult SignIn(AccountSignInModel accountSignInModel)
        {
            var account =
                _repository.First<Account>(
                    x => x.Email == accountSignInModel.Email && x.Password == accountSignInModel.Password);

            if (account != null)
            {
                return RedirectToAction("Index");
            }
            return View(accountSignInModel);
        }


        public ActionResult Index()
        {
            IEnumerable<Account> model = _repository.Query<Account>(x => x == x);
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new AccountInputModel());
        }

        [HttpPost]
        public ActionResult Create(AccountInputModel accountInputModel)
        {

            var account = _mappingEngine.Map<AccountInputModel, Account>(accountInputModel);
            _repository.Create<Account>(account);

            return RedirectToAction("Index", "Sales");
        }

        public ActionResult Edit(int id)
        {
            var editing = _repository.GetById<Account>(id);
            var model = Mapper.Map<Account,AccountInputModel>(editing);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(AccountInputModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var account = _mappingEngine.Map<AccountInputModel, Account>(model);
                _repository.Update(account);
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

        public ActionResult Details(int id)
        {
            var model = _repository.GetById<Account>(id);

            return View(model);
        }
    }
}


































































































































