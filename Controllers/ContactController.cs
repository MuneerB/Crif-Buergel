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
using Umbraco.Web;
using System.IO;
using Fluentx.Mvc;
//using Phases.UmbracoUtils;

namespace CrifAustria.Controllers
{
    public class ContactController : SurfaceController
    {
        public ActionResult ContactUs()
        {
            return PartialView("~/Views/Partials/Forms/contactForm.cshtml", new ContactModel());
        }
        public ActionResult ContactRequestSend(ContactModel Model)
        {
            //TryValidateModel(Model);

            //Check if the dat posted is valid (All required's & email set in email field)
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated                
                return CurrentUmbracoPage();
            }
            Node currentpage = Node.GetCurrent();
            using (CrifAustriaEFContainer objDB = new CrifAustriaEFContainer())
            {
                try
                {
                    contactUs objContact = new contactUs();
                    objContact.firstName = Model.FirstName;
                    objContact.lastName = Model.LastName;
                    objContact.email = Model.Email;
                    objContact.company = Model.Company;
                    objContact.bestWayToReach = Model.BestWayToReach;
                    objContact.message = Model.Message;
                    objContact.industry = ((String.IsNullOrEmpty(Model.Industry)) ? String.Empty :
                        ((Umbraco.TypedContent(Model.Industry) != null) ? Umbraco.TypedContent(Model.Industry).Name : Model.Industry));
                    objContact.jobTitle = ((String.IsNullOrEmpty(Model.JobTitle)) ? String.Empty : Model.JobTitle);
                    objContact.telephone = Model.PhoneNumber;
                    objContact.agreement = Model.Agreement.ToString();
                    objContact.newsletter = (Model.Newsletter) ? "Y" : "N";
                    objContact.culture = CultureInfo.CurrentCulture.Name;
                    objContact.createdDate = DateTime.Now;
                    objContact.modifiedDate = DateTime.Now;
                    objDB.contactUs.Add(objContact);
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
                                objContactNewletter.jobTitle = ((String.IsNullOrEmpty(Model.JobTitle)) ? String.Empty : Model.JobTitle);
                                objContactNewletter.industry = ((String.IsNullOrEmpty(Model.Industry)) ? String.Empty :
                        ((Umbraco.TypedContent(Model.Industry) != null) ? Umbraco.TypedContent(Model.Industry).Name : Model.Industry));
                                objContact.bestWayToReach = ((String.IsNullOrEmpty(Model.BestWayToReach)) ? String.Empty : Model.BestWayToReach);
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
                        emailbody = currentpage.GetProperty("notificationBody").Value.Trim();
                    }
                    else
                    {
                        emailbody = @"<p>Hi,</p>
                            <p>There is a new email from the crif Austria Contact Us page.</p>
                            <p>Please see the details below.</p>
                                <p>First Name : [FIRSTNAME]</p>
                                <p>Last Name :  [LASTNAME]</p>
                                <p>Email : [EMAIL]</p>
                                <p>Telephone:[TELEPHONE]</p>
                                <p>Industry :[INDUSTRY]</p>
                                <p>Company : [COMPANY]</p>
                                <p>Job Title : [JOBTITLE]</p>
                                <p>Best Way To Reach : [BESTWAYTOREACH]</p>
                                <p>Message : [MESSAGE]</p>
                                <p>Date :[DATE]</p>
                                <p> </p>
                                <p> </p>";
                    }
                    emailbody = ReplacePlaceholders(emailbody, Model);
                    if (currentpage.GetProperty("fromEmail") != null && !string.IsNullOrEmpty(currentpage.GetProperty("fromEmail").Value))
                    {
                        var fromEmail = currentpage.GetProperty("fromEmail").Value;
                        Email.sendEmail(fromEmail, toEmailAddress, emailSubject, emailbody, "" , true, "", bccvalue);
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
        public string ReplacePlaceholders(string mailContent, ContactModel productRequest)
        {

            string firstnamePlaceholder = "[FIRSTNAME]",
                    lastnamePlaceholder = "[LASTNAME]",
                    companyPlaceholder = "[COMPANY]",
                    emailPlaceholder = "[EMAIL]",
                    industryPlaceholder = "[INDUSTRY]",
                    jobtitlePlaceholder = "[JOBTITLE]",
                    telephonePlaceholder = "[TELEPHONE]",
                    messagePlaceholder = "[MESSAGE]",
                    bestWayToReachPlaceholder = "[BESTWAYTOREACH]",
                    datePlaceholder = "[DATE]",
                    date = DateTime.Now.Date.ToString("dd MMM yyyy");

            mailContent = mailContent.Replace(firstnamePlaceholder, productRequest.FirstName)
                                           .Replace(lastnamePlaceholder, productRequest.LastName)
                                           .Replace(jobtitlePlaceholder, productRequest.JobTitle)
                                           .Replace(companyPlaceholder, productRequest.Company)
                                           .Replace(emailPlaceholder, productRequest.Email)
                                           .Replace(industryPlaceholder, ((String.IsNullOrEmpty(productRequest.Industry)) ? String.Empty :
                        ((Umbraco.TypedContent(productRequest.Industry) != null) ? Umbraco.TypedContent(productRequest.Industry).Name : productRequest.Industry)))
                                            .Replace(telephonePlaceholder, productRequest.PhoneNumber)
                                            .Replace(messagePlaceholder, productRequest.Message)
                                            .Replace(bestWayToReachPlaceholder, productRequest.BestWayToReach)
                                           .Replace(datePlaceholder, date);
            return mailContent;

        }

        public ActionResult Login(LoginModel Model)
        {
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated                
                return CurrentUmbracoPage();
            }
            //var currentpage = Node.GetCurrent();
            //var homeNode = Umbraco.TypedContent(currentpage.Id).AncestorOrSelf(1);
            //Utility.ToBase64Url(Cryptography.Encrypt(Model.UserName, ConfigurationManager.AppSettings["EncryptionPassPhrase"].ToString()));
            //String encodedUserName = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Model.UserName));
            //String encodedPassword = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Model.PassWord));
            //var redirectUri = string.Format(
            //"https://www.crif-online.at/sb/checkperson?auth.id={0}&auth.pw={1}",
            //Model.UserName,
            //Model.PassWord);
            ////var redirectUri = "https://www.crif-online.at/sb/checkperson";
            //WebRequest request =
            //WebRequest.Create(redirectUri);
            //request.Method = "POST";
            //string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Model.UserName + ":" + Model.PassWord));

            //request.Headers.Add("Authorization", "Basic " + svcCredentials);
            //string url = @"https://www.crif-online.at/sb/checkperson";
            //WebRequest request = WebRequest.Create(url);
            //request.Credentials = GetCredential(Model);
            //request.PreAuthenticate = true;
            //request.Headers["Authorization"] = "Basic " + (Model.UserName + ":" + Model.PassWord);
            //request.Headers.Add("Authorization", Model.UserName + ":" + Model.PassWord);
            //request.Headers.Add("auth.id", Model.UserName);
            //request.Headers.Add("auth.pw", Model.PassWord);
            //request.Credentials = new NetworkCredential(Model.UserName, Model.PassWord);
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.Headers.Add("Access-Control-Allow-Origin", "*");
            //Stream dataStream = request.GetRequestStream();

            //string postData = string.Empty;
            //byte[] postArray = Encoding.ASCII.GetBytes(postData);
            //dataStream.Write(postArray, 0, postArray.Length);
            //dataStream.Flush();
            //dataStream.Close();

            //var response = (HttpWebResponse)request.GetResponse();
            //if ((int)response.StatusCode == 200)
            //{
            //    return Redirect("https://www.crif-online.at/sb/login");
            //}
            //else
            //{
            //    return CurrentUmbracoPage();
            //}
            //var url = "http://...."

        //var request = (HttpWebRequest)WebRequest.Create("https://www.crif-online.at/sb/checkperson");
        //request.ContentType = "application/x-www-form-urlencoded";
        //request.Method = "POST";
        //request.Headers.Add("auth.id", Model.UserName);
        //request.Headers.Add("auth.pw", Model.PassWord);
        //request.CookieContainer = new CookieContainer();

        //var response = (HttpWebResponse)request.GetResponse();

        //foreach (Cookie cook in response.Cookies)
        //{
        //    Response.Cookies.Add(new HttpCookie(cook.Name, cook.Value) { Expires = cook.Expires, HttpOnly = cook.HttpOnly, Domain = cook.Domain });
        //}
            //string responsestr = Post_To_External_SubmitFormsAPI(Model);
            string strEndpointURL = string.Format("https://www.crif-online.at/sb/checkperson");
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("auth.id", Model.UserName);
            postData.Add("auth.pw", Model.PassWord);
            return this.RedirectAndPost(strEndpointURL, postData);
        }
        private string Post_To_External_SubmitFormsAPI(LoginModel Model)
        {

            // Build Endpoint URL
            string strEndpointURL = string.Format("https://www.crif-online.at/sb/checkperson");
            

            // Setup HS Context Object
            //Dictionary<string, string> hsContext = new Dictionary<string, string>();
            //hsContext.Add("hutk", strHubSpotUTK);
            //hsContext.Add("ipAddress", strIpAddress);
            //hsContext.Add("pageUrl", strPageURL);
            //hsContext.Add("pageName", strPageTitle);

            //// Serialize HS Context to JSON (string)
            //System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string strHubSpotContextJSON = json.Serialize(hsContext);

            //// Create string with post data
            //string strPostData = "";

            //// Add dictionary values
            //foreach (var d in dictFormValues)
            //{
            //    strPostData += d.Key + "=" + Server.UrlEncode(d.Value.ToString()) + "&";
            //}

            //// Append HS Context JSON
            //strPostData += "hs_context=" + Server.UrlEncode(strHubSpotContextJSON);

            // Create web request object
            System.Net.HttpWebRequest r = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strEndpointURL);
            //if (System.Configuration.ConfigurationManager.AppSettings["proxyServerAddress"].ToString() != "" && System.Configuration.ConfigurationManager.AppSettings["proxyServerPort"].ToString() != "")
            //{
            //    string strprxyaddr = System.Configuration.ConfigurationManager.AppSettings["proxyServerAddress"].ToString();
            //    string strprxyport = System.Configuration.ConfigurationManager.AppSettings["proxyServerPort"].ToString();
            //    WebProxy myproxy = new WebProxy("" + strprxyaddr + ":" + strprxyport + "", false);
            //    r.Proxy = myproxy;
            //}
            // Set headers for POST
            r.Method = "POST";
            r.AllowAutoRedirect=true;
            r.ContentType = "application/x-www-form-urlencoded";
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Model.UserName + ":" + Model.PassWord));

            r.Headers.Add("authorization", "Basic " + svcCredentials);
            //r.Headers.Add("auth.id", Model.UserName);
            //r.Headers.Add("auth.pw", Model.PassWord);
            //r.ContentLength = strPostData.Length;
            r.KeepAlive = false;

            // POST data to endpoint
            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(r.GetRequestStream()))
            //{
            //    try
            //    {
            //        sw.Write(strPostData);

            //    }
            //    catch (Exception ex)
            //    {
            //        // POST Request Failed
            //        strMessage = ex.Message;
            //        return false;
            //    }
            //}
            //System.IO.Stream dataStream = r.GetResponse().GetResponseStream();
            //System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
            // Read the content.
            //string responseFromServer = reader.ReadToEnd();
            //TempData["IsSuccessful"] = responseFromServer;

             return r.GetResponse().ResponseUri.ToString();//POST Succeeded

        }
        private CredentialCache GetCredential(LoginModel Model)
        {
            string url = @"https://www.crif-online.at/sb/checkperson";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(Model.UserName, Model.PassWord));
            return credentialCache;
        }
	}
}