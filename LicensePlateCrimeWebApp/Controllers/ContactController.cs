using LicensePlateCrimeWebApp.Helpers;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using LicensePlateCrimeWebApp.Repository;
using LicensePlateCrimeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace LicensePlateCrimeWebApp.Controllers
{
    public class ContactController : Controller
    {
		private readonly IContactRepository _contactRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ContactController(IContactRepository contactRepository, IHttpContextAccessor httpContextAccessor)
		{
			_contactRepository = contactRepository;
			_httpContextAccessor = httpContextAccessor;
		}
		// GET: Contact
		public async Task<ActionResult> Index()
		{
			return View();
		}

		// GET: Contact/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Contact/Create
		public ActionResult Create()
		{
			var createcontactModel = new Contact();
			return View(createcontactModel);
		}

		// POST: Contact/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Contact createcontactModel)
		{
			if (ModelState.IsValid)
			{
			
        var contact = new Contact(createcontactModel.Name, createcontactModel.Email, createcontactModel.Telephone, createcontactModel.Subject, createcontactModel.Message);
        var id = await _contactRepository.AddAsync(contact);
        contact.Id = id;
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError("", "Mesaj gönderme sırasında hata!!! Tekrar deneyiniz.");
				return View("Index", createcontactModel);
			}
		}

		// GET: Contact/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Contact/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Contact/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Contact/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

	}
}
