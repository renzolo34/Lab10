using Demo09.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business;
using Entity;
using System.Xml.Schema;

namespace Demo09.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: InvoiceController
        public ActionResult Index()
        {
            BInvoice bInvoice = new BInvoice();
            List<Invoice> invoicesEntity = bInvoice.GetInvoiceActives();
            List<InvoiceModel> invoices = invoicesEntity.Select(x => new InvoiceModel
            {
                invoice_id = x.invoice_id,
                customer_id = x.customer_id,
                date = x.date,
                total = x.total,
                igv = (x.total / 100) * 18,
            }).ToList();

            return View(invoices);
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceModel invoiceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newInvoice = new Invoice
                    {
                        customer_id = invoiceModel.customer_id,
                        date = invoiceModel.date,
                        total = invoiceModel.total,
                    };


                    BInvoice bInvoice = new BInvoice();
                    bInvoice.CreateInvoice(newInvoice);

                    return RedirectToAction(nameof(Index));
                }

                return View(invoiceModel);
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(invoiceModel);
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvoiceController/Edit/5
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

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int id)
        {
            BInvoice bInvoice = new BInvoice();
            Invoice invoice = bInvoice.GetInvoiceActives().Where(x => x.invoice_id == id).FirstOrDefault();

            InvoiceModel model = new InvoiceModel
            {
                invoice_id = invoice.invoice_id,
                customer_id = invoice.customer_id,
                total = invoice.total,

            };

            //el modelo por id
            return View(model);
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                BInvoice bInvoice = new BInvoice();
                bInvoice.DeactivateInvoice(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
