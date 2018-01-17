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
    public class SuccessStoriesRequestController : SurfaceController
    {
        Node currentpage = uQuery.GetCurrentNode();
        public ActionResult Index()
        {
            return PartialView("~/Views/Partials/Forms/successStoriesRequestForm.cshtml", new SuccessRequestModel());
        }
        public ActionResult SuccessRequestSend(SuccessRequestModel ProductRequest)
        {
            //TryValidateModel(Model);

            //Check if the dat posted is valid (All required's & email set in email field)
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated                
                return CurrentUmbracoPage();
            }
            
            Node SuccessHome = currentpage.GetAncestorByPathLevel(2);
            using (CrifAustriaEFContainer objDB = new CrifAustriaEFContainer())
            {
                try
                {
                    SuccessStoryRequest objContact = new SuccessStoryRequest();
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
                    objDB.SuccessStoryRequests.Add(objContact);
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
                    if (SuccessHome.GetProperty("sendNotificationTo") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("sendNotificationTo").Value))
                    {
                        toEmailAddress = SuccessHome.GetProperty("sendNotificationTo").Value;
                    }
                    if (SuccessHome.GetProperty("bCCRecipients") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("bCCRecipients").Value))
                    {
                        bccvalue = SuccessHome.GetProperty("bCCRecipients").Value.Trim();
                    }

                    if (SuccessHome.GetProperty("eMailSubject") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("eMailSubject").Value))
                    {
                        emailSubject = SuccessHome.GetProperty("eMailSubject").Value;
                        emailSubject = ReplacePlaceholders(emailSubject, ProductRequest);
                    }
                    else
                    {
                        emailSubject = "New email product  request - " + DateTime.Now.Date.ToString("dd MMM yyyy");
                    }

                    if (SuccessHome.GetProperty("notificationBody") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("notificationBody").Value))
                    {
                        emailbody = SuccessHome.GetProperty("notificationBody").Value;
                    }
                    else
                    {
                        emailbody = @"<p>Hi,</p>
                            <p>There is a new email from the crif Austria Success Stories request page.</p>
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
                    if (SuccessHome.GetProperty("fromEmail") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("fromEmail").Value))
                    {
                        var fromEmail = SuccessHome.GetProperty("fromEmail").Value;
                        Email.sendEmail(fromEmail,toEmailAddress,emailSubject, emailbody,"",true,"", bccvalue);
                    }

                    if (SuccessHome.GetProperty("replyToUser") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("replyToUser").Value) && SuccessHome.GetProperty("replyToUser").Value == "1")
                    {
                        if (SuccessHome.GetProperty("replyEmailSubject") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("replyEmailSubject").Value))
                        {
                            replyemailSubject = SuccessHome.GetProperty("replyEmailSubject").Value;
                            replyemailSubject = ReplacePlaceholders(replyemailSubject, ProductRequest);
                        }
                        else
                        {
                            replyemailSubject = "Thank you for contacting us.";
                        }

                        //Reply Senders Body
                        if (SuccessHome.GetProperty("bodyContent") != null && !string.IsNullOrEmpty(SuccessHome.GetProperty("bodyContent").Value))
                        {
                            replyMesssage = SuccessHome.GetProperty("bodyContent").Value.Trim();
                            replyMesssage = ReplacePlaceholders(replyMesssage, ProductRequest);

                        }
                        if (!string.IsNullOrEmpty(ProductRequest.Email))
                        {
                            var fromEmail = SuccessHome.GetProperty("fromEmail").Value;
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
        public string ReplacePlaceholders(string mailContent, SuccessRequestModel productRequest)
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
            var productUrl = Common.GetHyperLink(currentpage.GetFullNiceUrl(), currentpage.Name);
            mailContent = mailContent.Replace(firstnamePlaceholder, productRequest.FirstName)
                                           .Replace(lastnamePlaceholder, productRequest.LastName)
                                           .Replace(jobtitlePlaceholder, productRequest.JobTitle)
                                           .Replace(companyPlaceholder, productRequest.Company)
                                           .Replace(emailPlaceholder, productRequest.Email)
                                           .Replace(industryPlaceholder, ((String.IsNullOrEmpty(productRequest.Industry)) ? String.Empty :
                        ((Umbraco.TypedContent(productRequest.Industry) != null) ? Umbraco.TypedContent(productRequest.Industry).Name : productRequest.Industry)))
                                            .Replace(telephonePlaceholder, productRequest.PhoneNumber)
                                            .Replace(productPlaceholder, productUrl)
                                           .Replace(datePlaceholder, date);
            return mailContent;

        }
    }
}
