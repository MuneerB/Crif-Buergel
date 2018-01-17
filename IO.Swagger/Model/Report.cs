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
    /// Report
    /// </summary>
    [DataContract]
    public partial class Report :  IEquatable<Report>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Report" /> class.
        /// </summary>
        /// <param name="NewsletterId">the id of the newsletter.</param>
        /// <param name="Date">the date of the aggregation.</param>
        /// <param name="MailCount">the number of recipients reached on this day.</param>
        /// <param name="Opens">the number of opens on this day.</param>
        /// <param name="UniqueOpens">the number of unique opens on this day.</param>
        /// <param name="Clicks">the number of clicks on this day.</param>
        /// <param name="UniqueClicks">the number of unique clicks on this day.</param>
        /// <param name="Conversions">the number of conversions on this day.</param>
        /// <param name="SumConversions">the amount recipients spent on this day.</param>
        /// <param name="Unsubscribes">the number of unsubscribes on this day.</param>
        /// <param name="HardBounces">the number of hard_bounces on this day.</param>
        /// <param name="SoftBounces">the number of soft_bounces on this day.</param>
        /// <param name="SpamBounces">the number of spam_bounces on this day.</param>
        public Report(string NewsletterId = null, DateTime? Date = null, int? MailCount = null, int? Opens = null, int? UniqueOpens = null, int? Clicks = null, int? UniqueClicks = null, int? Conversions = null, int? SumConversions = null, int? Unsubscribes = null, int? HardBounces = null, int? SoftBounces = null, int? SpamBounces = null)
        {
            this.NewsletterId = NewsletterId;
            this.Date = Date;
            this.MailCount = MailCount;
            this.Opens = Opens;
            this.UniqueOpens = UniqueOpens;
            this.Clicks = Clicks;
            this.UniqueClicks = UniqueClicks;
            this.Conversions = Conversions;
            this.SumConversions = SumConversions;
            this.Unsubscribes = Unsubscribes;
            this.HardBounces = HardBounces;
            this.SoftBounces = SoftBounces;
            this.SpamBounces = SpamBounces;
        }
        
        /// <summary>
        /// the id of the newsletter
        /// </summary>
        /// <value>the id of the newsletter</value>
        [DataMember(Name="newsletter_id", EmitDefaultValue=false)]
        public string NewsletterId { get; set; }
        /// <summary>
        /// the date of the aggregation
        /// </summary>
        /// <value>the date of the aggregation</value>
        [DataMember(Name="date", EmitDefaultValue=false)]
        public DateTime? Date { get; set; }
        /// <summary>
        /// the number of recipients reached on this day
        /// </summary>
        /// <value>the number of recipients reached on this day</value>
        [DataMember(Name="mail_count", EmitDefaultValue=false)]
        public int? MailCount { get; set; }
        /// <summary>
        /// the number of opens on this day
        /// </summary>
        /// <value>the number of opens on this day</value>
        [DataMember(Name="opens", EmitDefaultValue=false)]
        public int? Opens { get; set; }
        /// <summary>
        /// the number of unique opens on this day
        /// </summary>
        /// <value>the number of unique opens on this day</value>
        [DataMember(Name="unique_opens", EmitDefaultValue=false)]
        public int? UniqueOpens { get; set; }
        /// <summary>
        /// the number of clicks on this day
        /// </summary>
        /// <value>the number of clicks on this day</value>
        [DataMember(Name="clicks", EmitDefaultValue=false)]
        public int? Clicks { get; set; }
        /// <summary>
        /// the number of unique clicks on this day
        /// </summary>
        /// <value>the number of unique clicks on this day</value>
        [DataMember(Name="unique_clicks", EmitDefaultValue=false)]
        public int? UniqueClicks { get; set; }
        /// <summary>
        /// the number of conversions on this day
        /// </summary>
        /// <value>the number of conversions on this day</value>
        [DataMember(Name="conversions", EmitDefaultValue=false)]
        public int? Conversions { get; set; }
        /// <summary>
        /// the amount recipients spent on this day
        /// </summary>
        /// <value>the amount recipients spent on this day</value>
        [DataMember(Name="sum_conversions", EmitDefaultValue=false)]
        public int? SumConversions { get; set; }
        /// <summary>
        /// the number of unsubscribes on this day
        /// </summary>
        /// <value>the number of unsubscribes on this day</value>
        [DataMember(Name="unsubscribes", EmitDefaultValue=false)]
        public int? Unsubscribes { get; set; }
        /// <summary>
        /// the number of hard_bounces on this day
        /// </summary>
        /// <value>the number of hard_bounces on this day</value>
        [DataMember(Name="hard_bounces", EmitDefaultValue=false)]
        public int? HardBounces { get; set; }
        /// <summary>
        /// the number of soft_bounces on this day
        /// </summary>
        /// <value>the number of soft_bounces on this day</value>
        [DataMember(Name="soft_bounces", EmitDefaultValue=false)]
        public int? SoftBounces { get; set; }
        /// <summary>
        /// the number of spam_bounces on this day
        /// </summary>
        /// <value>the number of spam_bounces on this day</value>
        [DataMember(Name="spam_bounces", EmitDefaultValue=false)]
        public int? SpamBounces { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Report {\n");
            sb.Append("  NewsletterId: ").Append(NewsletterId).Append("\n");
            sb.Append("  Date: ").Append(Date).Append("\n");
            sb.Append("  MailCount: ").Append(MailCount).Append("\n");
            sb.Append("  Opens: ").Append(Opens).Append("\n");
            sb.Append("  UniqueOpens: ").Append(UniqueOpens).Append("\n");
            sb.Append("  Clicks: ").Append(Clicks).Append("\n");
            sb.Append("  UniqueClicks: ").Append(UniqueClicks).Append("\n");
            sb.Append("  Conversions: ").Append(Conversions).Append("\n");
            sb.Append("  SumConversions: ").Append(SumConversions).Append("\n");
            sb.Append("  Unsubscribes: ").Append(Unsubscribes).Append("\n");
            sb.Append("  HardBounces: ").Append(HardBounces).Append("\n");
            sb.Append("  SoftBounces: ").Append(SoftBounces).Append("\n");
            sb.Append("  SpamBounces: ").Append(SpamBounces).Append("\n");
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
            return this.Equals(obj as Report);
        }

        /// <summary>
        /// Returns true if Report instances are equal
        /// </summary>
        /// <param name="other">Instance of Report to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Report other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.NewsletterId == other.NewsletterId ||
                    this.NewsletterId != null &&
                    this.NewsletterId.Equals(other.NewsletterId)
                ) && 
                (
                    this.Date == other.Date ||
                    this.Date != null &&
                    this.Date.Equals(other.Date)
                ) && 
                (
                    this.MailCount == other.MailCount ||
                    this.MailCount != null &&
                    this.MailCount.Equals(other.MailCount)
                ) && 
                (
                    this.Opens == other.Opens ||
                    this.Opens != null &&
                    this.Opens.Equals(other.Opens)
                ) && 
                (
                    this.UniqueOpens == other.UniqueOpens ||
                    this.UniqueOpens != null &&
                    this.UniqueOpens.Equals(other.UniqueOpens)
                ) && 
                (
                    this.Clicks == other.Clicks ||
                    this.Clicks != null &&
                    this.Clicks.Equals(other.Clicks)
                ) && 
                (
                    this.UniqueClicks == other.UniqueClicks ||
                    this.UniqueClicks != null &&
                    this.UniqueClicks.Equals(other.UniqueClicks)
                ) && 
                (
                    this.Conversions == other.Conversions ||
                    this.Conversions != null &&
                    this.Conversions.Equals(other.Conversions)
                ) && 
                (
                    this.SumConversions == other.SumConversions ||
                    this.SumConversions != null &&
                    this.SumConversions.Equals(other.SumConversions)
                ) && 
                (
                    this.Unsubscribes == other.Unsubscribes ||
                    this.Unsubscribes != null &&
                    this.Unsubscribes.Equals(other.Unsubscribes)
                ) && 
                (
                    this.HardBounces == other.HardBounces ||
                    this.HardBounces != null &&
                    this.HardBounces.Equals(other.HardBounces)
                ) && 
                (
                    this.SoftBounces == other.SoftBounces ||
                    this.SoftBounces != null &&
                    this.SoftBounces.Equals(other.SoftBounces)
                ) && 
                (
                    this.SpamBounces == other.SpamBounces ||
                    this.SpamBounces != null &&
                    this.SpamBounces.Equals(other.SpamBounces)
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
                if (this.NewsletterId != null)
                    hash = hash * 59 + this.NewsletterId.GetHashCode();
                if (this.Date != null)
                    hash = hash * 59 + this.Date.GetHashCode();
                if (this.MailCount != null)
                    hash = hash * 59 + this.MailCount.GetHashCode();
                if (this.Opens != null)
                    hash = hash * 59 + this.Opens.GetHashCode();
                if (this.UniqueOpens != null)
                    hash = hash * 59 + this.UniqueOpens.GetHashCode();
                if (this.Clicks != null)
                    hash = hash * 59 + this.Clicks.GetHashCode();
                if (this.UniqueClicks != null)
                    hash = hash * 59 + this.UniqueClicks.GetHashCode();
                if (this.Conversions != null)
                    hash = hash * 59 + this.Conversions.GetHashCode();
                if (this.SumConversions != null)
                    hash = hash * 59 + this.SumConversions.GetHashCode();
                if (this.Unsubscribes != null)
                    hash = hash * 59 + this.Unsubscribes.GetHashCode();
                if (this.HardBounces != null)
                    hash = hash * 59 + this.HardBounces.GetHashCode();
                if (this.SoftBounces != null)
                    hash = hash * 59 + this.SoftBounces.GetHashCode();
                if (this.SpamBounces != null)
                    hash = hash * 59 + this.SpamBounces.GetHashCode();
                return hash;
            }
        }
    }

}
