using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyCRMProject.DAL;
using MyCRMProject.Entities;
using PagedList;

namespace MyCRMProject.Controllers
{
    public class ClientController : Controller
    {
        private readonly MyCrmContext _db = new MyCrmContext();

        // GET: Client
        public ViewResult Index(string sortOrder, string currentFilter,string searchString,int? page)
        {
            ViewBag.CurrenSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var clients = from s in _db.Clients
                           select s;
            switch (sortOrder)
            {
                case "Name desc":
                    clients = clients.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    clients = clients.OrderBy(s => s.DateOfBirth);
                    break;
                case "Date desc":
                    clients = clients.OrderByDescending(s => s.DateOfBirth);
                    break;
                default:
                    clients = clients.OrderBy(s => s.FirstName);
                    break;
            }
            const int pageSize = 5; 
            int pageNumber = (page ?? 1); 
            return View(clients.ToPagedList(pageNumber, pageSize));
            //return View(clients.ToList());
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = _db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,DateOfBirth")] Client client)
        {
            if (!ModelState.IsValid) return View(client);
            _db.Clients.Add(client);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = _db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,DateOfBirth")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(client).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                ModelState.AddModelError("",dex.Message);
            }
            return View(client);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
