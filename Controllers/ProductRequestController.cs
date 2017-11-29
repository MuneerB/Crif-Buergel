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

namespace CrifAustria.Controllers
{
    public class ProductRequestController : SurfaceController
    {
        Node currentpage = uQuery.GetCurrentNode();
        public ActionResult Index()
        {
            return PartialView("~/Views/Partials/Forms/productRequestForm.cshtml", new ProductRequestModel());
        }
        public ActionResult ProductRequestSend(ProductRequestModel ProductRequest)
        {
            //TryValidateModel(Model);

            //Check if the dat posted is valid (All required's & email set in email field)
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated                
                return CurrentUmbracoPage();
            }
            //Node currentpage = uQuery.GetCurrentNode();
            Node ProductHome = currentpage.GetAncestorByPathLevel(3);
            using (CrifAustriaEFContainer objDB = new CrifAustriaEFContainer())
            {
                try
                {
                    ProductRequest objContact = new ProductRequest();
                    objContact.First_Name = ProductRequest.FirstName;
                    objContact.Last_Name = ProductRequest.LastName;
                    objContact.Email = ProductRequest.Email;
                    objContact.Company = ProductRequest.Company;
                    objContact.Industry = ((String.IsNullOrEmpty(ProductRequest.Industry)) ? String.Empty :
                        ((Umbraco.TypedContent(ProductRequest.Industry) != null) ? Umbraco.TypedContent(ProductRequest.Industry).Name : ProductRequest.Industry));
                    objContact.JobTitle = ((String.IsNullOrEmpty(ProductRequest.JobTitle)) ? String.Empty : ProductRequest.JobTitle);
                    objContact.Telephone = ((String.IsNullOrEmpty(ProductRequest.PhoneNumber)) ? String.Empty : ProductRequest.PhoneNumber);
                    objContact.Agreement = ProductRequest.Agreement.ToString();
                    objContact.Newsletter = (ProductRequest.Newsletter) ? "Y" : "N";
                    objContact.Culture = CultureInfo.CurrentCulture.Name;
                    objContact.Culture = CultureInfo.CurrentCulture.Name;
                    objContact.ProductName = currentpage.Name;
                    objContact.CreatedDate = DateTime.Now;
                    objContact.ModifiedDate = DateTime.Now;
                    objDB.ProductRequests.Add(objContact);
                    objDB.SaveChanges();

                    if (ProductRequest.Newsletter)
                    {
                        //Dictionary<string, string> dictFormValuesNewsletter = new Dictionary<string, string>();
                        //dictFormValuesNewsletter.Add("First Name", Model.FirstName);
                        //dictFormValuesNewsletter.Add("Last Name", Model.LastName);
                        //dictFormValuesNewsletter.Add("Email", Model.Email);
                        //dictFormValuesNewsletter.Add("Job Title", Model.JobTitle);
                        //dictFormValuesNewsletter.Add("Telephone Number", "");
                        //dictFormValuesNewsletter.Add("company", Model.Company);
                        //dictFormValuesNewsletter.Add("Industry", (String.IsNullOrEmpty(Model.Industry)) ? String.Empty : Model.Industry);
                        //dictFormValuesNewsletter.Add("Country", list.ConvertTwoLetterNameFullName(Model.Country));
                        //dictFormValuesNewsletter.Add("linkedinbio", ((!String.IsNullOrEmpty(Model.linkedinProfileUrl)) ? Model.linkedinProfileUrl : String.Empty));

                        //strFormGUID = System.Configuration.ConfigurationManager.AppSettings["hubspotnewsletterformId"].ToString();

                        //bool blnRetNewsletter = Post_To_HubSpot_SubmitFormsAPI(intPortalID, strFormGUID, dictFormValuesNewsletter, strHubSpotUTK, strIpAddress, strPageTitle, strPageURL, ref strError);
                        //if (blnRetNewsletter)
                        //{
                        //    try
                        //    {
                        //        ccf_NewsLetter objContactNewletter = new ccf_NewsLetter();
                        //        objContactNewletter.First_Name = Model.FirstName;
                        //        objContactNewletter.Last_Name = Model.LastName;
                        //        objContactNewletter.Email = Model.Email;
                        //        objContactNewletter.Telephone = "";
                        //        objContactNewletter.Company = Model.Company;
                        //        objContactNewletter.JobTitle = Model.JobTitle;
                        //        objContactNewletter.Industry = ((String.IsNullOrEmpty(Model.Industry)) ? String.Empty : Model.Industry);
                        //        objContactNewletter.Country = list.ConvertTwoLetterNameFullName(Model.Country);
                        //        objContactNewletter.Agreement = Model.Agreement.ToString();
                        //        objContactNewletter.Culture = CultureInfo.CurrentCulture.Name;
                        //        objContactNewletter.CreatedDate = DateTime.Now;
                        //        objContactNewletter.ModifiedDate = DateTime.Now;
                        //        objDB.ccf_NewsLetter.Add(objContactNewletter);
                        //        objDB.SaveChanges();
                        //    }
                        //    catch (DbEntityValidationException ex)
                        //    {
                        //        StringBuilder sb = new StringBuilder();

                        //        foreach (var failure in ex.EntityValidationErrors)
                        //        {
                        //            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        //            foreach (var error in failure.ValidationErrors)
                        //            {
                        //                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        //                sb.AppendLine();
                        //            }
                        //        }

                        //        throw new DbEntityValidationException(
                        //            "Entity Validation Failed - errors follow:\n" +
                        //            sb.ToString(), ex
                        //        ); // Add the original exception as the innerException
                        //    }
                        //}
                        //else
                        //{
                        //    return CurrentUmbracoPage();
                        //}
                    }
                    string toEmailAddress = ""
                            , bccvalue = ""
                            , emailSubject = ""
                            , emailbody = ""
                            , replyMesssage = ""
                            , replyemailSubject = "";
                    //string message = null;
                    if (ProductHome.GetProperty("sendNotificationTo") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("sendNotificationTo").Value))
                    {
                        toEmailAddress = ProductHome.GetProperty("sendNotificationTo").Value;
                    }
                    if (ProductHome.GetProperty("bCCRecipients") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("bCCRecipients").Value))
                    {
                        bccvalue = ProductHome.GetProperty("bCCRecipients").Value.Trim();
                    }

                    if (ProductHome.GetProperty("eMailSubject") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("eMailSubject").Value))
                    {
                        emailSubject = ProductHome.GetProperty("eMailSubject").Value;
                        emailSubject = ReplacePlaceholders(emailSubject, ProductRequest);
                    }
                    else
                    {
                        emailSubject = "New email product  request - " + DateTime.Now.Date.ToString("dd MMM yyyy");
                    }

                    if (ProductHome.GetProperty("notificationBody") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("notificationBody").Value))
                    {
                        emailbody = ProductHome.GetProperty("notificationBody").Value;
                    }
                    else
                    {
                        emailbody = @"<p>Hi,</p>
                            <p>There is a new email from the crif Austria product request page.</p>
                            <p>Please see the details below.</p>
                                <p>First Name : [FIRSTNAME]</p>
                                <p>Last Name :  [LASTNAME]</p>
                                <p>Email : [EMAIL]</p>
                                <p>Telephone:[TELEPHONE]</p>
                                <p>Industry :[INDUSTRY]</p>
                                <p>Company : [COMPANY]</p>
                                <p>Job Title : [JOBTITLE]</p>
                                <p>Product : [PRODUCT]</p>
                                <p>Date :[DATE]</p>
                                <p> </p>
                                <p> </p>";
                    }
                    emailbody = ReplacePlaceholders(emailbody, ProductRequest);
                    if (ProductHome.GetProperty("fromEmail") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("fromEmail").Value))
                    {
                        var fromEmail = ProductHome.GetProperty("fromEmail").Value;
                        Email.sendEmail(fromEmail,toEmailAddress,emailSubject, emailbody,"",true,"", bccvalue);
                    }

                    if (ProductHome.GetProperty("replyToUser") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("replyToUser").Value) && ProductHome.GetProperty("replyToUser").Value == "1")
                    {
                        if (ProductHome.GetProperty("replyEmailSubject") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("replyEmailSubject").Value))
                        {
                            replyemailSubject = ProductHome.GetProperty("replyEmailSubject").Value;
                            replyemailSubject = ReplacePlaceholders(replyemailSubject, ProductRequest);
                        }
                        else
                        {
                            replyemailSubject = "Thank you for contacting us.";
                        }

                        //Reply Senders Body
                        if (ProductHome.GetProperty("bodyContent") != null && !string.IsNullOrEmpty(ProductHome.GetProperty("bodyContent").Value))
                        {
                            replyMesssage = ProductHome.GetProperty("bodyContent").Value.Trim();
                            replyMesssage = ReplacePlaceholders(replyMesssage, ProductRequest);

                        }
                        if (!string.IsNullOrEmpty(ProductRequest.Email))
                        {
                            var fromEmail = ProductHome.GetProperty("fromEmail").Value;
                            Email.sendEmail(fromEmail,ProductRequest.Email,replyemailSubject, replyMesssage,"",true,"", "");
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

            return Redirect(currentpage.GetAncestorByPathLevel(1).GetDescendantNodesByType("thankYou").Where(x => x.Level == 2).First().Url + "?id=" + @currentpage.Id);
        }
        public string ReplacePlaceholders(string mailContent, ProductRequestModel productRequest)
        {

            string firstnamePlaceholder = "[FIRSTNAME]",
                    lastnamePlaceholder = "[LASTNAME]",
                    companyPlaceholder = "[COMPANY]",
                    emailPlaceholder = "[EMAIL]",
                    industryPlaceholder = "[INDUSTRY]",
                    jobtitlePlaceholder = "[JOBTITLE]",
                    telephonePlaceholder = "[TELEPHONE]",
                    productPlaceholder = "[PRODUCT]",
                    datePlaceholder = "[DATE]",
                    date = DateTime.Now.Date.ToString("dd MMM yyyy");
            //int defaultPort = Request.IsSecureConnection ? 443 : 80;
            //var productUrl = Common.GetHyperLink(Common.GetDomainUrl(currentpage.Id, defaultPort), currentpage.Name);
            var productUrl = Common.GetHyperLink(currentpage.GetFullNiceUrl(), currentpage.Name);
            mailContent = mailContent.Replace(firstnamePlaceholder, productRequest.FirstName)
                                           .Replace(lastnamePlaceholder, productRequest.LastName)
                                           .Replace(jobtitlePlaceholder, productRequest.JobTitle)
                                           .Replace(companyPlaceholder, productRequest.Company)
                                           .Replace(emailPlaceholder, productRequest.Email)
                                           .Replace(industryPlaceholder, ((String.IsNullOrEmpty(productRequest.Industry)) ? String.Empty :
                        ((Umbraco.TypedContent(productRequest.Industry) != null) ? Umbraco.TypedContent(productRequest.Industry).Name : productRequest.Industry)))
                                            .Replace(telephonePlaceholder, productRequest.PhoneNumber)
                                            .Replace(productPlaceholder , productUrl)
                                           .Replace(datePlaceholder, date);
            return mailContent;

        }
    }
}
