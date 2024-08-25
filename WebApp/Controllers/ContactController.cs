using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult Index()
    {
      return View();
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
  }
}
