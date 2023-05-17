using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
	[Authorize]
	public class ViolationController : Controller
	{
		// GET: Violations
		public ActionResult Index()
		{
			return View();
		}

		// GET: Violations/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Violations/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Violations/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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

		// GET: Violations/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Violations/Edit/5
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

		// GET: Violations/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Violations/Delete/5
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
