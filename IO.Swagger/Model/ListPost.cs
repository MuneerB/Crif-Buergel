/* 
 * Newsletter2Go-API (https://api.newsletter2go.com)
 *
 * <h5>JSON first</h5><br/>Our REST API exchanges data in the JSON data format. Every parameter you pass (with a few exceptions e.g. when you upload files) should therefore be formatted in JSON and our API will always return results in JSON as well.<br/><br/><h5>Very RESTful</h5><br/>Our API follows a very RESTful approach.<br/>Most importantly, we implemented the following four request methods for CRUD operations:<br/><br/>POST - Create a new record<br/>GET - Retrieve / read records without changing anything<br/>PATCH - Update an existing record<br/>DELETE - Delete one or more records<br/><br/><h5>HTTP Status codes</h5><br/>We also follow the most common HTTP status codes when outputting the API response:<br/><br/><b>2xx - Successful calls</b><br/>200 - Success<br/>201 - Created<br/><br/><b>4xx - Error codes</b><br/>400 - API error - inspect the body for detailed information about what went wrong. The most common error is \"code\":1062, which means, that a unique constraint was hit. For example if the name of the group is already in use.<br/>401 - Not authenticated - invalid access_token. Check if the access_token has expired or is incorrect.<br/>403 - Access denied<br/>404 - Call not available or no records found<br/><br/><b>5xx - API error - please contact support</b><br/><br/><h5>Response format</h5><br/>The API always returns data in the following format<br/><br/><code>{<br/>&nbsp;&nbsp;\"info\": {<br/>&nbsp;&nbsp;&nbsp;&nbsp;\"count\": 0<br/>&nbsp;&nbsp;},<br/>&nbsp;&nbsp;\"value\": [<br/>&nbsp;&nbsp;&nbsp;{<br/>&nbsp;&nbsp;&nbsp;&nbsp;\"key\": \"value\"<br/>&nbsp;&nbsp;&nbsp;}<br/>&nbsp;&nbsp;]<br/>}</code><br/><br/><h5>Recurring GET parameters</h5><br/><ul><li>_filter - a complex filter for filtering the result set based on <a target=\"blank\" href=\"https://tools.ietf.org/html/draft-nottingham-atompub-fiql-00\">FIQL</a>.</li><li>_limit - the maximum number records returned.</li><li>_offset - pagination for the result-set</li><li>_expand - submit true to get all default fields of the resource</li><li>_fields - submit a comma-separated list of case-sensetive field-names to get the values of these fields in the response. You can use this the get values of custom attribute of recipients for example.</li></ul><br/><br/><h5>Filter language</h5><br/>The filter language for filtering results is based on <a target=\"blank\" href=\"https://tools.ietf.org/html/draft-nottingham-atompub-fiql-00\">FIQL</a>.<br/>With the only restriction, that plain values must be surrounded by \". For example first_name==\"Max\"<br/>The following operators are supported<ul><li>Equals - <b>==</b></li><li>Not equals - <b>=ne=</b></li><li>Greater than - <b>=gt=</b></li><li>Greater than equals - <b>=ge=</b></li><li>Lower than - <b>=lt=</b></li><li>Lower than equals - <b>=le=</b></li><li>Like - <b>=like=</b> (in combination with % you are able to search for <i>starts with</i>, <i>ends with</i>, <i>contains</i>. For example first_name=like=\"%Max%\")</li><li>Not like - <b>=nlike=</b></li><li>Logical and - <b>;</b></li><li>Logical or - <b>,</b></li></ul><br/><br/><h5>How to make Calls?</h5><br/>After you authenticated and received a valid access_token, you have to pass it in every consecutive call. Use the Authorization header for that purpose as follows:<br/><code>xhr.setRequestHeader(\"Authorization\", \"Bearer \" + your_access_token);</code><br/><br/>Every call takes additional parameters that can be used to modify the request. These parameters should be passed as JSON<br/><code>var params = {<br/>&nbsp;&nbsp;\"key\"= \"value\"<br/>}<br/>xhr.send(JSON.stringify(params));</code><br/><br/><h5>Sending transactional emails</h5><br/>It is very important to understand the following concept in order for you to take full advantage of our powerful personalization features and our detailed reports when sending transactional emails.<br/><br/>First, you will have to create a new mailing. We recommend that you create that mailing through our UI in order to take full advantage of our powerful newsletter builder. This way, we will automatically create cross-client optimized and responsive HTML. Yet another advantage lies in the possibility for other users (e.g. the marketing team) to change the layout or the content of the mailing without having to contact the developer (you).<br/><br/>Of course, it is also possible to create a mailing through the API. When doing so, you can also take advantage of our cross-client optimized responsive HTML by passing us JSON or YAML according to the Newsletter2Go Markup Language.<br/>No matter how you create the mailing, try to create *one* reusable template. You can customize individual emails by inserting placeholders for personalized fields such as name, products or other information that will be filled through an API call when sending.<br/><br/>By only creating one template, you can take advantage of our full reporting since all emails will be treated part of a \"campaign\". When you change that template, we will create a new version of the mailing in the background and you will be able to see the difference in performance in the reports. This is particularily useful when you are trying to test and optimize different versions of transactional emails such as a sign up email.<br/><br/>After creating a mailing, you will have access to its ID. Use that ID to actually send the email in the next step.<br/><br/>When sending an email, you have to pass the newsletter ID and information about the recipient. Either pass the recipient ID or pass all the recipient's data (including the e-mail-address) as JSON.<br/><br/>If you only pass the recipient ID, we will use his or her data from your list to personalize the mailing. If you pass full recipient data as JSON, we will try to merge the data with existing data from your list.<br/><br/>You can also pass additional data such as product data which is not saved in your list. Just make sure that the placeholders in the source of the mailing correspond to the parameters that you are passing.<br/>This way you can also create for-loops which can be useful if you pass different amounts of data for each recipient (e.g. a purchase confirmation where you want to list all the products that were just bought).
 *
 * OpenAPI spec version: 1.0.0
 * Contact: support@newsletter2go.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IO.Swagger.Model
{
    /// <summary>
    /// ListPost
    /// </summary>
    [DataContract]
    public partial class ListPost :  IEquatable<ListPost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListPost" /> class.
        /// </summary>
        /// <param name="Name">the name of the list.</param>
        /// <param name="UsesEconda">uses econda tracking or not.</param>
        /// <param name="UsesGoogleanalytics">uses google-analytics tracking or not.</param>
        /// <param name="HasOpentracking">uses open-tracking tracking or not.</param>
        /// <param name="HasClicktracking">uses click-tracking tracking or not.</param>
        /// <param name="HasConversiontracking">uses conversion-tracking tracking or not.</param>
        /// <param name="Imprint">the link to the imprint for this list, which can be used in mailings.</param>
        /// <param name="HeaderFromEmail">the email which is used as the from field in a newsletter.</param>
        /// <param name="HeaderFromName">the name which is used as the from field in a newsletter.</param>
        /// <param name="HeaderReplyEmail">the email which is used as the reply field in a newsletter.</param>
        /// <param name="HeaderReplyName">the name which is used as the reply field in a newsletter.</param>
        /// <param name="TrackingUrl">a custom tracking url which is used instead of our default one.</param>
        public ListPost(string Name = null, bool? UsesEconda = null, bool? UsesGoogleanalytics = null, bool? HasOpentracking = null, bool? HasClicktracking = null, bool? HasConversiontracking = null, string Imprint = null, string HeaderFromEmail = null, string HeaderFromName = null, string HeaderReplyEmail = null, string HeaderReplyName = null, string TrackingUrl = null)
        {
            this.Name = Name;
            this.UsesEconda = UsesEconda;
            this.UsesGoogleanalytics = UsesGoogleanalytics;
            this.HasOpentracking = HasOpentracking;
            this.HasClicktracking = HasClicktracking;
            this.HasConversiontracking = HasConversiontracking;
            this.Imprint = Imprint;
            this.HeaderFromEmail = HeaderFromEmail;
            this.HeaderFromName = HeaderFromName;
            this.HeaderReplyEmail = HeaderReplyEmail;
            this.HeaderReplyName = HeaderReplyName;
            this.TrackingUrl = TrackingUrl;
        }
        
        /// <summary>
        /// the name of the list
        /// </summary>
        /// <value>the name of the list</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }
        /// <summary>
        /// uses econda tracking or not
        /// </summary>
        /// <value>uses econda tracking or not</value>
        [DataMember(Name="uses_econda", EmitDefaultValue=false)]
        public bool? UsesEconda { get; set; }
        /// <summary>
        /// uses google-analytics tracking or not
        /// </summary>
        /// <value>uses google-analytics tracking or not</value>
        [DataMember(Name="uses_googleanalytics", EmitDefaultValue=false)]
        public bool? UsesGoogleanalytics { get; set; }
        /// <summary>
        /// uses open-tracking tracking or not
        /// </summary>
        /// <value>uses open-tracking tracking or not</value>
        [DataMember(Name="has_opentracking", EmitDefaultValue=false)]
        public bool? HasOpentracking { get; set; }
        /// <summary>
        /// uses click-tracking tracking or not
        /// </summary>
        /// <value>uses click-tracking tracking or not</value>
        [DataMember(Name="has_clicktracking", EmitDefaultValue=false)]
        public bool? HasClicktracking { get; set; }
        /// <summary>
        /// uses conversion-tracking tracking or not
        /// </summary>
        /// <value>uses conversion-tracking tracking or not</value>
        [DataMember(Name="has_conversiontracking", EmitDefaultValue=false)]
        public bool? HasConversiontracking { get; set; }
        /// <summary>
        /// the link to the imprint for this list, which can be used in mailings
        /// </summary>
        /// <value>the link to the imprint for this list, which can be used in mailings</value>
        [DataMember(Name="imprint", EmitDefaultValue=false)]
        public string Imprint { get; set; }
        /// <summary>
        /// the email which is used as the from field in a newsletter
        /// </summary>
        /// <value>the email which is used as the from field in a newsletter</value>
        [DataMember(Name="header_from_email", EmitDefaultValue=false)]
        public string HeaderFromEmail { get; set; }
        /// <summary>
        /// the name which is used as the from field in a newsletter
        /// </summary>
        /// <value>the name which is used as the from field in a newsletter</value>
        [DataMember(Name="header_from_name", EmitDefaultValue=false)]
        public string HeaderFromName { get; set; }
        /// <summary>
        /// the email which is used as the reply field in a newsletter
        /// </summary>
        /// <value>the email which is used as the reply field in a newsletter</value>
        [DataMember(Name="header_reply_email", EmitDefaultValue=false)]
        public string HeaderReplyEmail { get; set; }
        /// <summary>
        /// the name which is used as the reply field in a newsletter
        /// </summary>
        /// <value>the name which is used as the reply field in a newsletter</value>
        [DataMember(Name="header_reply_name", EmitDefaultValue=false)]
        public string HeaderReplyName { get; set; }
        /// <summary>
        /// a custom tracking url which is used instead of our default one
        /// </summary>
        /// <value>a custom tracking url which is used instead of our default one</value>
        [DataMember(Name="tracking_url", EmitDefaultValue=false)]
        public string TrackingUrl { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ListPost {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  UsesEconda: ").Append(UsesEconda).Append("\n");
            sb.Append("  UsesGoogleanalytics: ").Append(UsesGoogleanalytics).Append("\n");
            sb.Append("  HasOpentracking: ").Append(HasOpentracking).Append("\n");
            sb.Append("  HasClicktracking: ").Append(HasClicktracking).Append("\n");
            sb.Append("  HasConversiontracking: ").Append(HasConversiontracking).Append("\n");
            sb.Append("  Imprint: ").Append(Imprint).Append("\n");
            sb.Append("  HeaderFromEmail: ").Append(HeaderFromEmail).Append("\n");
            sb.Append("  HeaderFromName: ").Append(HeaderFromName).Append("\n");
            sb.Append("  HeaderReplyEmail: ").Append(HeaderReplyEmail).Append("\n");
            sb.Append("  HeaderReplyName: ").Append(HeaderReplyName).Append("\n");
            sb.Append("  TrackingUrl: ").Append(TrackingUrl).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as ListPost);
        }

        /// <summary>
        /// Returns true if ListPost instances are equal
        /// </summary>
        /// <param name="other">Instance of ListPost to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ListPost other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) && 
                (
                    this.UsesEconda == other.UsesEconda ||
                    this.UsesEconda != null &&
                    this.UsesEconda.Equals(other.UsesEconda)
                ) && 
                (
                    this.UsesGoogleanalytics == other.UsesGoogleanalytics ||
                    this.UsesGoogleanalytics != null &&
                    this.UsesGoogleanalytics.Equals(other.UsesGoogleanalytics)
                ) && 
                (
                    this.HasOpentracking == other.HasOpentracking ||
                    this.HasOpentracking != null &&
                    this.HasOpentracking.Equals(other.HasOpentracking)
                ) && 
                (
                    this.HasClicktracking == other.HasClicktracking ||
                    this.HasClicktracking != null &&
                    this.HasClicktracking.Equals(other.HasClicktracking)
                ) && 
                (
                    this.HasConversiontracking == other.HasConversiontracking ||
                    this.HasConversiontracking != null &&
                    this.HasConversiontracking.Equals(other.HasConversiontracking)
                ) && 
                (
                    this.Imprint == other.Imprint ||
                    this.Imprint != null &&
                    this.Imprint.Equals(other.Imprint)
                ) && 
                (
                    this.HeaderFromEmail == other.HeaderFromEmail ||
                    this.HeaderFromEmail != null &&
                    this.HeaderFromEmail.Equals(other.HeaderFromEmail)
                ) && 
                (
                    this.HeaderFromName == other.HeaderFromName ||
                    this.HeaderFromName != null &&
                    this.HeaderFromName.Equals(other.HeaderFromName)
                ) && 
                (
                    this.HeaderReplyEmail == other.HeaderReplyEmail ||
                    this.HeaderReplyEmail != null &&
                    this.HeaderReplyEmail.Equals(other.HeaderReplyEmail)
                ) && 
                (
                    this.HeaderReplyName == other.HeaderReplyName ||
                    this.HeaderReplyName != null &&
                    this.HeaderReplyName.Equals(other.HeaderReplyName)
                ) && 
                (
                    this.TrackingUrl == other.TrackingUrl ||
                    this.TrackingUrl != null &&
                    this.TrackingUrl.Equals(other.TrackingUrl)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
                if (this.UsesEconda != null)
                    hash = hash * 59 + this.UsesEconda.GetHashCode();
                if (this.UsesGoogleanalytics != null)
                    hash = hash * 59 + this.UsesGoogleanalytics.GetHashCode();
                if (this.HasOpentracking != null)
                    hash = hash * 59 + this.HasOpentracking.GetHashCode();
                if (this.HasClicktracking != null)
                    hash = hash * 59 + this.HasClicktracking.GetHashCode();
                if (this.HasConversiontracking != null)
                    hash = hash * 59 + this.HasConversiontracking.GetHashCode();
                if (this.Imprint != null)
                    hash = hash * 59 + this.Imprint.GetHashCode();
                if (this.HeaderFromEmail != null)
                    hash = hash * 59 + this.HeaderFromEmail.GetHashCode();
                if (this.HeaderFromName != null)
                    hash = hash * 59 + this.HeaderFromName.GetHashCode();
                if (this.HeaderReplyEmail != null)
                    hash = hash * 59 + this.HeaderReplyEmail.GetHashCode();
                if (this.HeaderReplyName != null)
                    hash = hash * 59 + this.HeaderReplyName.GetHashCode();
                if (this.TrackingUrl != null)
                    hash = hash * 59 + this.TrackingUrl.GetHashCode();
                return hash;
            }
        }
    }

}
