using CrifAustria.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using umbraco;
using System.Linq;
using umbraco.NodeFactory;
using System.Globalization;
using System;
using System.Data.Entity.Validation;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Net;
using CrifAustria.Utils;
using RJP.MultiUrlPicker.Models;

namespace CrifAustria.Controllers
{
    public class SheduleConsultationController : SurfaceController
    {
        //
        // GET: /SheduleConsultation/
        public ActionResult SheduleConsultation()
        {
            return PartialView("~/Views/Partials/Forms/sheduleConsultationForm.cshtml", new SheduleConsultationModel());
        }
        public ActionResult sheduleConsultationRequestSend(SheduleConsultationModel Model)
        {
            //TryValidateModel(Model);

            //Check if the dat posted is valid (All required's & email set in email field)
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated                
                return CurrentUmbracoPage();
            }
            Node currentpage = uQuery.GetCurrentNode();
            using (CrifAustriaEFContainer objDB = new CrifAustriaEFContainer())
            {
                try
                {
                    sheduleConsultation objContact = new sheduleConsultation();
                    objContact.firstName = Model.FirstName;
                    objContact.lastName = Model.LastName;
                    objContact.email = Model.Email;
                    objContact.company = Model.Company;
                    objContact.industry = ((String.IsNullOrEmpty(Model.Industry)) ? String.Empty : 
                        ((Umbraco.TypedContent(Model.Industry) != null) ? Umbraco.TypedContent(Model.Industry).Name : Model.Industry));
                    objContact.jobTitle = ((String.IsNullOrEmpty(Model.JobTitle)) ? String.Empty : Model.JobTitle);
                    objContact.bestWayToReach = ((String.IsNullOrEmpty(Model.BestWayToReach)) ? String.Empty : Model.BestWayToReach);
                    objContact.howDidYouHearAbout = ((String.IsNullOrEmpty(Model.MediaSource)) ? String.Empty : Model.MediaSource);
                    objContact.telephone = Model.PhoneNumber;
                    objContact.message = Model.Message;
                    objContact.agreement = Model.Agreement.ToString();
                    objContact.newsletter = (Model.Newsletter) ? "Y" : "N";
                    objContact.culture = CultureInfo.CurrentCulture.Name;
                    objContact.createdDate = DateTime.Now;
                    objContact.modifiedDate = DateTime.Now;
                    objDB.sheduleConsultations.Add(objContact);
                    objDB.SaveChanges();

                    if (Model.Newsletter)
                    {
                        try
                        {
                            newsletterSubscription objContactNewletter = new newsletterSubscription();
                            objContactNewletter.firstName = Model.FirstName;
                            objContactNewletter.lastName = Model.LastName;
                            objContactNewletter.email = Model.Email;
                            objContactNewletter.company = Model.Company;
                            objContactNewletter.jobTitle = Model.JobTitle;
                            objContactNewletter.industry = ((String.IsNullOrEmpty(Model.Industry)) ? String.Empty :
                        ((Umbraco.TypedContent(Model.Industry) != null) ? Umbraco.TypedContent(Model.Industry).Name : Model.Industry));
                            objContactNewletter.agreement = Model.Agreement.ToString();
                            objContactNewletter.culture = CultureInfo.CurrentCulture.Name;
                            objContactNewletter.createdDate = DateTime.Now;
                            objContactNewletter.modifiedDate = DateTime.Now;
                            objDB.newsletterSubscriptions.Add(objContactNewletter);
                            objDB.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            StringBuilder sb = new StringBuilder();

                            foreach (var failure in ex.EntityValidationErrors)
                            {
                                sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                                foreach (var error in failure.ValidationErrors)
                                {
                                    sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                                    sb.AppendLine();
                                }
                            }

                            throw new DbEntityValidationException(
                                "Entity Validation Failed - errors follow:\n" +
                                sb.ToString(), ex
                            ); // Add the original exception as the innerException
                        }
                    }
                    string toEmailAddress = ""
                            , bccvalue = ""
                            , emailSubject = ""
                            , emailbody = ""
                            , replyMesssage = ""
                            , replyemailSubject = "";
                    //string message = null;
                    if (currentpage.GetProperty("sendNotificationTo") != null && !string.IsNullOrEmpty(currentpage.GetProperty("sendNotificationTo").Value))
                    {
                        toEmailAddress = currentpage.GetProperty("sendNotificationTo").Value;
                    }
                    if (currentpage.GetProperty("bCCRecipients") != null && !string.IsNullOrEmpty(currentpage.GetProperty("bCCRecipients").Value))
                    {
                        bccvalue = currentpage.GetProperty("bCCRecipients").Value.Trim();
                    }

                    if (currentpage.GetProperty("eMailSubject") != null && !string.IsNullOrEmpty(currentpage.GetProperty("eMailSubject").Value))
                    {
                        emailSubject = currentpage.GetProperty("eMailSubject").Value;
                        emailSubject = ReplacePlaceholders(emailSubject, Model);
                    }
                    else
                    {
                        emailSubject = "New email product  request - " + DateTime.Now.Date.ToString("dd MMM yyyy");
                    }

                    if (currentpage.GetProperty("notificationBody") != null && !string.IsNullOrEmpty(currentpage.GetProperty("notificationBody").Value))
                    {
                        emailbody = currentpage.GetProperty("notificationBody").Value;
                    }
                    else
                    {
                        emailbody = @"<p>Hi,</p>
                            <p>There is a new email from the crif Austria Shedule Consultation request page.</p>
                            <p>Please see the details below.</p>
                                <p>First Name : [FIRSTNAME]</p>
                                <p>Last Name :  [LASTNAME]</p>
                                <p>Email : [EMAIL]</p>
                                <p>Telephone:[TELEPHONE]</p>
                                <p>Industry :[INDUSTRY]</p>
                                <p>Company : [COMPANY]</p>
                                <p>Job Title : [JOBTITLE]</p>
                                <p>Best Way To Reach : [BESTWAYTOREACH]</p>
                                <p>How did you hear about us:[MEDIASOURCE]</p>
                                <p>Message : [MESSAGE]</p>
                                <p>Date :[DATE]</p>
                                <p> </p>
                                <p> </p>";
                    }
                    emailbody = ReplacePlaceholders(emailbody, Model);
                    if (currentpage.GetProperty("fromEmail") != null && !string.IsNullOrEmpty(currentpage.GetProperty("fromEmail").Value))
                    {
                        var fromEmail = currentpage.GetProperty("fromEmail").Value;
                        Email.sendEmail(fromEmail, toEmailAddress, emailSubject, emailbody, "", true, "", bccvalue);
                    }

                    if (currentpage.GetProperty("replyToUser") != null && !string.IsNullOrEmpty(currentpage.GetProperty("replyToUser").Value) && currentpage.GetProperty("replyToUser").Value == "1")
                    {
                        if (currentpage.GetProperty("replyEmailSubject") != null && !string.IsNullOrEmpty(currentpage.GetProperty("replyEmailSubject").Value))
                        {
                            replyemailSubject = currentpage.GetProperty("replyEmailSubject").Value;
                            replyemailSubject = ReplacePlaceholders(replyemailSubject, Model);
                        }
                        else
                        {
                            replyemailSubject = "Thank you for contacting us.";
                        }

                        //Reply Senders Body
                        if (currentpage.GetProperty("bodyContent") != null && !string.IsNullOrEmpty(currentpage.GetProperty("bodyContent").Value))
                        {
                            replyMesssage = currentpage.GetProperty("bodyContent").Value.Trim();
                            replyMesssage = ReplacePlaceholders(replyMesssage, Model);

                        }
                        if (!string.IsNullOrEmpty(Model.Email))
                        {
                            var fromEmail = currentpage.GetProperty("fromEmail").Value;
                            Email.sendEmail(fromEmail, Model.Email, replyemailSubject, replyMesssage, "", true, "", "");
                        }
                    }

                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                    ); // Add the original exception as the innerException
                }
            }
            var redirectthankyouPage = (currentpage.GetDescendantNodesByType("thankYou").Count() > 0 ? currentpage.GetDescendantNodesByType("thankYou").FirstOrDefault().Url : currentpage.GetAncestorByPathLevel(1).Url);
            return Redirect(redirectthankyouPage);
        }
        public string ReplacePlaceholders(string mailContent, SheduleConsultationModel productRequest)
        {

            string firstnamePlaceholder = "[FIRSTNAME]",
                    lastnamePlaceholder = "[LASTNAME]",
                    companyPlaceholder = "[COMPANY]",
                    emailPlaceholder = "[EMAIL]",
                    industryPlaceholder = "[INDUSTRY]",
                    jobtitlePlaceholder = "[JOBTITLE]",
                    telephonePlaceholder = "[TELEPHONE]",
                    bestWayToReachPlaceholder = "[BESTWAYTOREACH]",
                    MediaSourcePlaceholder = "[MEDIASOURCE]",
                    datePlaceholder = "[DATE]",
                    messagePlaceholder = "[MESSAGE]",
                    date = DateTime.Now.Date.ToString("dd MMM yyyy");

            mailContent = mailContent.Replace(firstnamePlaceholder, productRequest.FirstName)
                                           .Replace(lastnamePlaceholder, productRequest.LastName)
                                           .Replace(jobtitlePlaceholder, productRequest.JobTitle)
                                           .Replace(companyPlaceholder, productRequest.Company)
                                           .Replace(emailPlaceholder, productRequest.Email)
                                           .Replace(industryPlaceholder, ((String.IsNullOrEmpty(productRequest.Industry)) ? String.Empty :
                                            ((Umbraco.TypedContent(productRequest.Industry) != null) ? Umbraco.TypedContent(productRequest.Industry).Name : productRequest.Industry)))
                                            .Replace(telephonePlaceholder, productRequest.PhoneNumber)
                                            .Replace(bestWayToReachPlaceholder, productRequest.BestWayToReach)
                                            .Replace(MediaSourcePlaceholder, productRequest.MediaSource)
                                             .Replace(messagePlaceholder, productRequest.Message)
                                            .Replace(datePlaceholder, date);
            return mailContent;

        }
	}
}