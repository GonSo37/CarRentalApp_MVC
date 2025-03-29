using CarRentalApp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;

namespace CarRentalApp_MVC.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientSrevice)
        {
            _clientService = clientSrevice?? throw new ArgumentNullException(nameof(clientSrevice));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _clientService.GetAllClients();
            return View(model);
        }


        public IActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient(Client model)
        {
            if (ModelState.IsValid)
            {
                _clientService.AddClient(model);
                _clientService.Save();
                return RedirectToAction("Index", "Clients");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditClient(int ClientId)
        {
            Client model = _clientService.GetClientById(ClientId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditClient(Client model)
        {
            if (ModelState.IsValid)
            {
                _clientService.UpdateClient(model);
                _clientService.Save();
                return RedirectToAction("Index", "Clients");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteClient(int ClientID)
        {
            Client model = _clientService.GetClientById(ClientID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int ClientID)
        {
            _clientService.DeleteClient(ClientID);
            _clientService.Save();
            return RedirectToAction("Index", "Clients");
        }

    }
}
