using CarRentalApp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalApp_MVC.Repository;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.Validators;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalApp_MVC.Controllers
{
    [Authorize(Policy = "RequireAdminOrManager")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private ClientModelValidator _validator;
        private IMapper _mapper;
        public ClientsController(IClientService clientSrevice, ClientModelValidator validator, IMapper mapper)
        {
            _clientService = clientSrevice?? throw new ArgumentNullException(nameof(clientSrevice));
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var clients = _clientService.GetAllClients();

            var model = _mapper.Map<List<Client>>(clients);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddClient()
        {
            return View(new Client());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient(Client model)
        {
            var result = _validator.Validate(model);
            if(!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                var client = _mapper.Map<Client>(model);

                _clientService.AddClient(client);
                _clientService.Save();
                return RedirectToAction("Index", "Clients");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditClient(int ClientId)
        {
            var client = _clientService.GetClientById(ClientId);

            var model = _mapper.Map<Client>(client);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditClient(Client model)
        {
            var result = _validator.Validate(model);
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            if (ModelState.IsValid)
            {
                var client = _mapper.Map<Client>(model);

                _clientService.UpdateClient(client);
                _clientService.Save();
                return RedirectToAction("Index", "Clients");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteClient(int ClientID)
        {
            var client = _clientService.GetClientById(ClientID);

            var model = _mapper.Map<Client>(client);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int ClientID)
        {
            _clientService.DeleteClient(ClientID);
            _clientService.Save();
            return RedirectToAction("Index", "Clients");
        }

    }
}
