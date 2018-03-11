using System;
using System.Net.Mail;
using System.Web.Mvc;
using HFFUmbraco.Models;
using Umbraco.Web.Mvc;

namespace HFFUmbraco.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        public ActionResult RenderContactForm()
        {
            return PartialView("~/Views/Partials/ContactFormSurface/ContactForm.cshtml", new ContactFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleContactForm(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            MailMessage email = new MailMessage(model.Email, "tom@tintinet.co.uk");
            email.Subject = "Contact Form Request";
            email.Body = model.Message;
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["IsSuccessful"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}