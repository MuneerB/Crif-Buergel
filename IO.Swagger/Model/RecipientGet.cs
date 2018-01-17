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
    /// RecipientGet
    /// </summary>
    [DataContract]
    public partial class RecipientGet :  IEquatable<RecipientGet>
    {
        /// <summary>
        /// m for male and f for female
        /// </summary>
        /// <value>m for male and f for female</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum GenderEnum
        {
            
            /// <summary>
            /// Enum M for "m"
            /// </summary>
            [EnumMember(Value = "m")]
            M,
            
            /// <summary>
            /// Enum F for "f"
            /// </summary>
            [EnumMember(Value = "f")]
            F
        }

        /// <summary>
        /// the last useragent device used for opening a newsletter
        /// </summary>
        /// <value>the last useragent device used for opening a newsletter</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatisticLastUseragentDeviceEnum
        {
            
            /// <summary>
            /// Enum Desktop for "desktop"
            /// </summary>
            [EnumMember(Value = "desktop")]
            Desktop,
            
            /// <summary>
            /// Enum Mobile for "mobile"
            /// </summary>
            [EnumMember(Value = "mobile")]
            Mobile,
            
            /// <summary>
            /// Enum Tablet for "tablet"
            /// </summary>
            [EnumMember(Value = "tablet")]
            Tablet
        }

        /// <summary>
        /// the last useragent family used for opening a newsletter
        /// </summary>
        /// <value>the last useragent family used for opening a newsletter</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatisticLastUseragentFamilyEnum
        {
            
            /// <summary>
            /// Enum LotusNotes for "lotus_notes"
            /// </summary>
            [EnumMember(Value = "lotus_notes")]
            LotusNotes,
            
            /// <summary>
            /// Enum AppleMail for "apple_mail"
            /// </summary>
            [EnumMember(Value = "apple_mail")]
            AppleMail,
            
            /// <summary>
            /// Enum Thunderbird for "thunderbird"
            /// </summary>
            [EnumMember(Value = "thunderbird")]
            Thunderbird,
            
            /// <summary>
            /// Enum WindowsLiveMail for "windows_live_mail"
            /// </summary>
            [EnumMember(Value = "windows_live_mail")]
            WindowsLiveMail,
            
            /// <summary>
            /// Enum Outlook for "outlook"
            /// </summary>
            [EnumMember(Value = "outlook")]
            Outlook,
            
            /// <summary>
            /// Enum Ios for "iOS"
            /// </summary>
            [EnumMember(Value = "iOS")]
            Ios,
            
            /// <summary>
            /// Enum Android for "Android"
            /// </summary>
            [EnumMember(Value = "Android")]
            Android,
            
            /// <summary>
            /// Enum Windows for "Windows"
            /// </summary>
            [EnumMember(Value = "Windows")]
            Windows,
            
            /// <summary>
            /// Enum Else for "else"
            /// </summary>
            [EnumMember(Value = "else")]
            Else
        }

        /// <summary>
        /// m for male and f for female
        /// </summary>
        /// <value>m for male and f for female</value>
        [DataMember(Name="gender", EmitDefaultValue=false)]
        public GenderEnum? Gender { get; set; }
        /// <summary>
        /// the last useragent device used for opening a newsletter
        /// </summary>
        /// <value>the last useragent device used for opening a newsletter</value>
        [DataMember(Name="statistic_last_useragent_device", EmitDefaultValue=false)]
        public StatisticLastUseragentDeviceEnum? StatisticLastUseragentDevice { get; set; }
        /// <summary>
        /// the last useragent family used for opening a newsletter
        /// </summary>
        /// <value>the last useragent family used for opening a newsletter</value>
        [DataMember(Name="statistic_last_useragent_family", EmitDefaultValue=false)]
        public StatisticLastUseragentFamilyEnum? StatisticLastUseragentFamily { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipientGet" /> class.
        /// </summary>
        /// <param name="Id">the id of the recipient.</param>
        /// <param name="Email">the email of the recipient.</param>
        /// <param name="Phone">the phone number of the recipient.</param>
        /// <param name="Gender">m for male and f for female.</param>
        /// <param name="FirstName">the first name of the recipient.</param>
        /// <param name="LastName">the last name of the recipient.</param>
        /// <param name="IsUnsubscribed">weither the recipient is unsubscribed or not.</param>
        /// <param name="IsBlacklisted">weither the recipient is blacklisted or not.</param>
        /// <param name="CustomAttribute_">every custom attribute can be submitted.</param>
        /// <param name="Rating">Indicating the quality of the recipient. Between 0 and 5.</param>
        /// <param name="StatisticMailsReceived">how many newsletters the recipient has received.</param>
        /// <param name="StatisticMailsOpened">how many newsletters the recipient has opened.</param>
        /// <param name="StatisticMailsUniqueOpened">how many newsletters the recipient has uniqly opened.</param>
        /// <param name="StatisticMailsClicked">how often the recipient has clicked on links in newsletters.</param>
        /// <param name="StatisticConversions">how often the recipient has converted.</param>
        /// <param name="StatisticConversionsSum">how much the recipient has spent.</param>
        /// <param name="StatisticLastMailAction">when the recipient has had the last interaction with a newsletter.</param>
        /// <param name="StatisticLastMailReceived">when the recipient has had received the last newsletter.</param>
        /// <param name="StatisticLastMailOpened">when the recipient has had opened the last newsletter.</param>
        /// <param name="StatisticLastMailClicked">when the recipient has had clicked the last newsletter.</param>
        /// <param name="StatisticLastMailConverted">when the recipient has had converted the last time.</param>
        /// <param name="StatisticLastUseragentDevice">the last useragent device used for opening a newsletter.</param>
        /// <param name="StatisticLastUseragentFamily">the last useragent family used for opening a newsletter.</param>
        /// <param name="StatisticLastLatitude">the last latitude where the recipient has opened a newsletter.</param>
        /// <param name="StatisticLastLongitude">the last longitude where the recipient has opened a newsletter.</param>
        /// <param name="StatisticLastBlacklistList">when the recipient was blacklisted the last time.</param>
        /// <param name="StatisticLastUnsubscribeList">when the recipient was unsubscribed the last time.</param>
        /// <param name="StatisticLastBlacklistListReason">the reason the recipient got blacklisted the last time.</param>
        /// <param name="StatisticLastUnsubscribeListReason">the reason the recipient got unsubscribed the last time.</param>
        public RecipientGet(string Id = null, string Email = null, string Phone = null, GenderEnum? Gender = null, string FirstName = null, string LastName = null, bool? IsUnsubscribed = null, bool? IsBlacklisted = null, string CustomAttribute_ = null, int? Rating = null, int? StatisticMailsReceived = null, int? StatisticMailsOpened = null, int? StatisticMailsUniqueOpened = null, int? StatisticMailsClicked = null, int? StatisticConversions = null, float? StatisticConversionsSum = null, DateTime? StatisticLastMailAction = null, DateTime? StatisticLastMailReceived = null, DateTime? StatisticLastMailOpened = null, DateTime? StatisticLastMailClicked = null, DateTime? StatisticLastMailConverted = null, StatisticLastUseragentDeviceEnum? StatisticLastUseragentDevice = null, StatisticLastUseragentFamilyEnum? StatisticLastUseragentFamily = null, double? StatisticLastLatitude = null, double? StatisticLastLongitude = null, DateTime? StatisticLastBlacklistList = null, DateTime? StatisticLastUnsubscribeList = null, string StatisticLastBlacklistListReason = null, string StatisticLastUnsubscribeListReason = null)
        {
            this.Id = Id;
            this.Email = Email;
            this.Phone = Phone;
            this.Gender = Gender;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.IsUnsubscribed = IsUnsubscribed;
            this.IsBlacklisted = IsBlacklisted;
            this.CustomAttribute_ = CustomAttribute_;
            this.Rating = Rating;
            this.StatisticMailsReceived = StatisticMailsReceived;
            this.StatisticMailsOpened = StatisticMailsOpened;
            this.StatisticMailsUniqueOpened = StatisticMailsUniqueOpened;
            this.StatisticMailsClicked = StatisticMailsClicked;
            this.StatisticConversions = StatisticConversions;
            this.StatisticConversionsSum = StatisticConversionsSum;
            this.StatisticLastMailAction = StatisticLastMailAction;
            this.StatisticLastMailReceived = StatisticLastMailReceived;
            this.StatisticLastMailOpened = StatisticLastMailOpened;
            this.StatisticLastMailClicked = StatisticLastMailClicked;
            this.StatisticLastMailConverted = StatisticLastMailConverted;
            this.StatisticLastUseragentDevice = StatisticLastUseragentDevice;
            this.StatisticLastUseragentFamily = StatisticLastUseragentFamily;
            this.StatisticLastLatitude = StatisticLastLatitude;
            this.StatisticLastLongitude = StatisticLastLongitude;
            this.StatisticLastBlacklistList = StatisticLastBlacklistList;
            this.StatisticLastUnsubscribeList = StatisticLastUnsubscribeList;
            this.StatisticLastBlacklistListReason = StatisticLastBlacklistListReason;
            this.StatisticLastUnsubscribeListReason = StatisticLastUnsubscribeListReason;
        }
        
        /// <summary>
        /// the id of the recipient
        /// </summary>
        /// <value>the id of the recipient</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }
        /// <summary>
        /// the email of the recipient
        /// </summary>
        /// <value>the email of the recipient</value>
        [DataMember(Name="email", EmitDefaultValue=false)]
        public string Email { get; set; }
        /// <summary>
        /// the phone number of the recipient
        /// </summary>
        /// <value>the phone number of the recipient</value>
        [DataMember(Name="phone", EmitDefaultValue=false)]
        public string Phone { get; set; }
        /// <summary>
        /// the first name of the recipient
        /// </summary>
        /// <value>the first name of the recipient</value>
        [DataMember(Name="first_name", EmitDefaultValue=false)]
        public string FirstName { get; set; }
        /// <summary>
        /// the last name of the recipient
        /// </summary>
        /// <value>the last name of the recipient</value>
        [DataMember(Name="last_name", EmitDefaultValue=false)]
        public string LastName { get; set; }
        /// <summary>
        /// weither the recipient is unsubscribed or not
        /// </summary>
        /// <value>weither the recipient is unsubscribed or not</value>
        [DataMember(Name="is_unsubscribed", EmitDefaultValue=false)]
        public bool? IsUnsubscribed { get; set; }
        /// <summary>
        /// weither the recipient is blacklisted or not
        /// </summary>
        /// <value>weither the recipient is blacklisted or not</value>
        [DataMember(Name="is_blacklisted", EmitDefaultValue=false)]
        public bool? IsBlacklisted { get; set; }
        /// <summary>
        /// every custom attribute can be submitted
        /// </summary>
        /// <value>every custom attribute can be submitted</value>
        [DataMember(Name="_custom_attribute_", EmitDefaultValue=false)]
        public string CustomAttribute_ { get; set; }
        /// <summary>
        /// Indicating the quality of the recipient. Between 0 and 5
        /// </summary>
        /// <value>Indicating the quality of the recipient. Between 0 and 5</value>
        [DataMember(Name="rating", EmitDefaultValue=false)]
        public int? Rating { get; set; }
        /// <summary>
        /// how many newsletters the recipient has received
        /// </summary>
        /// <value>how many newsletters the recipient has received</value>
        [DataMember(Name="statistic_mails_received", EmitDefaultValue=false)]
        public int? StatisticMailsReceived { get; set; }
        /// <summary>
        /// how many newsletters the recipient has opened
        /// </summary>
        /// <value>how many newsletters the recipient has opened</value>
        [DataMember(Name="statistic_mails_opened", EmitDefaultValue=false)]
        public int? StatisticMailsOpened { get; set; }
        /// <summary>
        /// how many newsletters the recipient has uniqly opened
        /// </summary>
        /// <value>how many newsletters the recipient has uniqly opened</value>
        [DataMember(Name="statistic_mails_unique_opened", EmitDefaultValue=false)]
        public int? StatisticMailsUniqueOpened { get; set; }
        /// <summary>
        /// how often the recipient has clicked on links in newsletters
        /// </summary>
        /// <value>how often the recipient has clicked on links in newsletters</value>
        [DataMember(Name="statistic_mails_clicked", EmitDefaultValue=false)]
        public int? StatisticMailsClicked { get; set; }
        /// <summary>
        /// how often the recipient has converted
        /// </summary>
        /// <value>how often the recipient has converted</value>
        [DataMember(Name="statistic_conversions", EmitDefaultValue=false)]
        public int? StatisticConversions { get; set; }
        /// <summary>
        /// how much the recipient has spent
        /// </summary>
        /// <value>how much the recipient has spent</value>
        [DataMember(Name="statistic_conversions_sum", EmitDefaultValue=false)]
        public float? StatisticConversionsSum { get; set; }
        /// <summary>
        /// when the recipient has had the last interaction with a newsletter
        /// </summary>
        /// <value>when the recipient has had the last interaction with a newsletter</value>
        [DataMember(Name="statistic_last_mail_action", EmitDefaultValue=false)]
        public DateTime? StatisticLastMailAction { get; set; }
        /// <summary>
        /// when the recipient has had received the last newsletter
        /// </summary>
        /// <value>when the recipient has had received the last newsletter</value>
        [DataMember(Name="statistic_last_mail_received", EmitDefaultValue=false)]
        public DateTime? StatisticLastMailReceived { get; set; }
        /// <summary>
        /// when the recipient has had opened the last newsletter
        /// </summary>
        /// <value>when the recipient has had opened the last newsletter</value>
        [DataMember(Name="statistic_last_mail_opened", EmitDefaultValue=false)]
        public DateTime? StatisticLastMailOpened { get; set; }
        /// <summary>
        /// when the recipient has had clicked the last newsletter
        /// </summary>
        /// <value>when the recipient has had clicked the last newsletter</value>
        [DataMember(Name="statistic_last_mail_clicked", EmitDefaultValue=false)]
        public DateTime? StatisticLastMailClicked { get; set; }
        /// <summary>
        /// when the recipient has had converted the last time
        /// </summary>
        /// <value>when the recipient has had converted the last time</value>
        [DataMember(Name="statistic_last_mail_converted", EmitDefaultValue=false)]
        public DateTime? StatisticLastMailConverted { get; set; }
        /// <summary>
        /// the last latitude where the recipient has opened a newsletter
        /// </summary>
        /// <value>the last latitude where the recipient has opened a newsletter</value>
        [DataMember(Name="statistic_last_latitude", EmitDefaultValue=false)]
        public double? StatisticLastLatitude { get; set; }
        /// <summary>
        /// the last longitude where the recipient has opened a newsletter
        /// </summary>
        /// <value>the last longitude where the recipient has opened a newsletter</value>
        [DataMember(Name="statistic_last_longitude", EmitDefaultValue=false)]
        public double? StatisticLastLongitude { get; set; }
        /// <summary>
        /// when the recipient was blacklisted the last time
        /// </summary>
        /// <value>when the recipient was blacklisted the last time</value>
        [DataMember(Name="statistic_last_blacklist_list", EmitDefaultValue=false)]
        public DateTime? StatisticLastBlacklistList { get; set; }
        /// <summary>
        /// when the recipient was unsubscribed the last time
        /// </summary>
        /// <value>when the recipient was unsubscribed the last time</value>
        [DataMember(Name="statistic_last_unsubscribe_list", EmitDefaultValue=false)]
        public DateTime? StatisticLastUnsubscribeList { get; set; }
        /// <summary>
        /// the reason the recipient got blacklisted the last time
        /// </summary>
        /// <value>the reason the recipient got blacklisted the last time</value>
        [DataMember(Name="statistic_last_blacklist_list_reason", EmitDefaultValue=false)]
        public string StatisticLastBlacklistListReason { get; set; }
        /// <summary>
        /// the reason the recipient got unsubscribed the last time
        /// </summary>
        /// <value>the reason the recipient got unsubscribed the last time</value>
        [DataMember(Name="statistic_last_unsubscribe_list_reason", EmitDefaultValue=false)]
        public string StatisticLastUnsubscribeListReason { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecipientGet {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  Gender: ").Append(Gender).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  IsUnsubscribed: ").Append(IsUnsubscribed).Append("\n");
            sb.Append("  IsBlacklisted: ").Append(IsBlacklisted).Append("\n");
            sb.Append("  CustomAttribute_: ").Append(CustomAttribute_).Append("\n");
            sb.Append("  Rating: ").Append(Rating).Append("\n");
            sb.Append("  StatisticMailsReceived: ").Append(StatisticMailsReceived).Append("\n");
            sb.Append("  StatisticMailsOpened: ").Append(StatisticMailsOpened).Append("\n");
            sb.Append("  StatisticMailsUniqueOpened: ").Append(StatisticMailsUniqueOpened).Append("\n");
            sb.Append("  StatisticMailsClicked: ").Append(StatisticMailsClicked).Append("\n");
            sb.Append("  StatisticConversions: ").Append(StatisticConversions).Append("\n");
            sb.Append("  StatisticConversionsSum: ").Append(StatisticConversionsSum).Append("\n");
            sb.Append("  StatisticLastMailAction: ").Append(StatisticLastMailAction).Append("\n");
            sb.Append("  StatisticLastMailReceived: ").Append(StatisticLastMailReceived).Append("\n");
            sb.Append("  StatisticLastMailOpened: ").Append(StatisticLastMailOpened).Append("\n");
            sb.Append("  StatisticLastMailClicked: ").Append(StatisticLastMailClicked).Append("\n");
            sb.Append("  StatisticLastMailConverted: ").Append(StatisticLastMailConverted).Append("\n");
            sb.Append("  StatisticLastUseragentDevice: ").Append(StatisticLastUseragentDevice).Append("\n");
            sb.Append("  StatisticLastUseragentFamily: ").Append(StatisticLastUseragentFamily).Append("\n");
            sb.Append("  StatisticLastLatitude: ").Append(StatisticLastLatitude).Append("\n");
            sb.Append("  StatisticLastLongitude: ").Append(StatisticLastLongitude).Append("\n");
            sb.Append("  StatisticLastBlacklistList: ").Append(StatisticLastBlacklistList).Append("\n");
            sb.Append("  StatisticLastUnsubscribeList: ").Append(StatisticLastUnsubscribeList).Append("\n");
            sb.Append("  StatisticLastBlacklistListReason: ").Append(StatisticLastBlacklistListReason).Append("\n");
            sb.Append("  StatisticLastUnsubscribeListReason: ").Append(StatisticLastUnsubscribeListReason).Append("\n");
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
            return this.Equals(obj as RecipientGet);
        }

        /// <summary>
        /// Returns true if RecipientGet instances are equal
        /// </summary>
        /// <param name="other">Instance of RecipientGet to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RecipientGet other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) && 
                (
                    this.Email == other.Email ||
                    this.Email != null &&
                    this.Email.Equals(other.Email)
                ) && 
                (
                    this.Phone == other.Phone ||
                    this.Phone != null &&
                    this.Phone.Equals(other.Phone)
                ) && 
                (
                    this.Gender == other.Gender ||
                    this.Gender != null &&
                    this.Gender.Equals(other.Gender)
                ) && 
                (
                    this.FirstName == other.FirstName ||
                    this.FirstName != null &&
                    this.FirstName.Equals(other.FirstName)
                ) && 
                (
                    this.LastName == other.LastName ||
                    this.LastName != null &&
                    this.LastName.Equals(other.LastName)
                ) && 
                (
                    this.IsUnsubscribed == other.IsUnsubscribed ||
                    this.IsUnsubscribed != null &&
                    this.IsUnsubscribed.Equals(other.IsUnsubscribed)
                ) && 
                (
                    this.IsBlacklisted == other.IsBlacklisted ||
                    this.IsBlacklisted != null &&
                    this.IsBlacklisted.Equals(other.IsBlacklisted)
                ) && 
                (
                    this.CustomAttribute_ == other.CustomAttribute_ ||
                    this.CustomAttribute_ != null &&
                    this.CustomAttribute_.Equals(other.CustomAttribute_)
                ) && 
                (
                    this.Rating == other.Rating ||
                    this.Rating != null &&
                    this.Rating.Equals(other.Rating)
                ) && 
                (
                    this.StatisticMailsReceived == other.StatisticMailsReceived ||
                    this.StatisticMailsReceived != null &&
                    this.StatisticMailsReceived.Equals(other.StatisticMailsReceived)
                ) && 
                (
                    this.StatisticMailsOpened == other.StatisticMailsOpened ||
                    this.StatisticMailsOpened != null &&
                    this.StatisticMailsOpened.Equals(other.StatisticMailsOpened)
                ) && 
                (
                    this.StatisticMailsUniqueOpened == other.StatisticMailsUniqueOpened ||
                    this.StatisticMailsUniqueOpened != null &&
                    this.StatisticMailsUniqueOpened.Equals(other.StatisticMailsUniqueOpened)
                ) && 
                (
                    this.StatisticMailsClicked == other.StatisticMailsClicked ||
                    this.StatisticMailsClicked != null &&
                    this.StatisticMailsClicked.Equals(other.StatisticMailsClicked)
                ) && 
                (
                    this.StatisticConversions == other.StatisticConversions ||
                    this.StatisticConversions != null &&
                    this.StatisticConversions.Equals(other.StatisticConversions)
                ) && 
                (
                    this.StatisticConversionsSum == other.StatisticConversionsSum ||
                    this.StatisticConversionsSum != null &&
                    this.StatisticConversionsSum.Equals(other.StatisticConversionsSum)
                ) && 
                (
                    this.StatisticLastMailAction == other.StatisticLastMailAction ||
                    this.StatisticLastMailAction != null &&
                    this.StatisticLastMailAction.Equals(other.StatisticLastMailAction)
                ) && 
                (
                    this.StatisticLastMailReceived == other.StatisticLastMailReceived ||
                    this.StatisticLastMailReceived != null &&
                    this.StatisticLastMailReceived.Equals(other.StatisticLastMailReceived)
                ) && 
                (
                    this.StatisticLastMailOpened == other.StatisticLastMailOpened ||
                    this.StatisticLastMailOpened != null &&
                    this.StatisticLastMailOpened.Equals(other.StatisticLastMailOpened)
                ) && 
                (
                    this.StatisticLastMailClicked == other.StatisticLastMailClicked ||
                    this.StatisticLastMailClicked != null &&
                    this.StatisticLastMailClicked.Equals(other.StatisticLastMailClicked)
                ) && 
                (
                    this.StatisticLastMailConverted == other.StatisticLastMailConverted ||
                    this.StatisticLastMailConverted != null &&
                    this.StatisticLastMailConverted.Equals(other.StatisticLastMailConverted)
                ) && 
                (
                    this.StatisticLastUseragentDevice == other.StatisticLastUseragentDevice ||
                    this.StatisticLastUseragentDevice != null &&
                    this.StatisticLastUseragentDevice.Equals(other.StatisticLastUseragentDevice)
                ) && 
                (
                    this.StatisticLastUseragentFamily == other.StatisticLastUseragentFamily ||
                    this.StatisticLastUseragentFamily != null &&
                    this.StatisticLastUseragentFamily.Equals(other.StatisticLastUseragentFamily)
                ) && 
                (
                    this.StatisticLastLatitude == other.StatisticLastLatitude ||
                    this.StatisticLastLatitude != null &&
                    this.StatisticLastLatitude.Equals(other.StatisticLastLatitude)
                ) && 
                (
                    this.StatisticLastLongitude == other.StatisticLastLongitude ||
                    this.StatisticLastLongitude != null &&
                    this.StatisticLastLongitude.Equals(other.StatisticLastLongitude)
                ) && 
                (
                    this.StatisticLastBlacklistList == other.StatisticLastBlacklistList ||
                    this.StatisticLastBlacklistList != null &&
                    this.StatisticLastBlacklistList.Equals(other.StatisticLastBlacklistList)
                ) && 
                (
                    this.StatisticLastUnsubscribeList == other.StatisticLastUnsubscribeList ||
                    this.StatisticLastUnsubscribeList != null &&
                    this.StatisticLastUnsubscribeList.Equals(other.StatisticLastUnsubscribeList)
                ) && 
                (
                    this.StatisticLastBlacklistListReason == other.StatisticLastBlacklistListReason ||
                    this.StatisticLastBlacklistListReason != null &&
                    this.StatisticLastBlacklistListReason.Equals(other.StatisticLastBlacklistListReason)
                ) && 
                (
                    this.StatisticLastUnsubscribeListReason == other.StatisticLastUnsubscribeListReason ||
                    this.StatisticLastUnsubscribeListReason != null &&
                    this.StatisticLastUnsubscribeListReason.Equals(other.StatisticLastUnsubscribeListReason)
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
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                if (this.Email != null)
                    hash = hash * 59 + this.Email.GetHashCode();
                if (this.Phone != null)
                    hash = hash * 59 + this.Phone.GetHashCode();
                if (this.Gender != null)
                    hash = hash * 59 + this.Gender.GetHashCode();
                if (this.FirstName != null)
                    hash = hash * 59 + this.FirstName.GetHashCode();
                if (this.LastName != null)
                    hash = hash * 59 + this.LastName.GetHashCode();
                if (this.IsUnsubscribed != null)
                    hash = hash * 59 + this.IsUnsubscribed.GetHashCode();
                if (this.IsBlacklisted != null)
                    hash = hash * 59 + this.IsBlacklisted.GetHashCode();
                if (this.CustomAttribute_ != null)
                    hash = hash * 59 + this.CustomAttribute_.GetHashCode();
                if (this.Rating != null)
                    hash = hash * 59 + this.Rating.GetHashCode();
                if (this.StatisticMailsReceived != null)
                    hash = hash * 59 + this.StatisticMailsReceived.GetHashCode();
                if (this.StatisticMailsOpened != null)
                    hash = hash * 59 + this.StatisticMailsOpened.GetHashCode();
                if (this.StatisticMailsUniqueOpened != null)
                    hash = hash * 59 + this.StatisticMailsUniqueOpened.GetHashCode();
                if (this.StatisticMailsClicked != null)
                    hash = hash * 59 + this.StatisticMailsClicked.GetHashCode();
                if (this.StatisticConversions != null)
                    hash = hash * 59 + this.StatisticConversions.GetHashCode();
                if (this.StatisticConversionsSum != null)
                    hash = hash * 59 + this.StatisticConversionsSum.GetHashCode();
                if (this.StatisticLastMailAction != null)
                    hash = hash * 59 + this.StatisticLastMailAction.GetHashCode();
                if (this.StatisticLastMailReceived != null)
                    hash = hash * 59 + this.StatisticLastMailReceived.GetHashCode();
                if (this.StatisticLastMailOpened != null)
                    hash = hash * 59 + this.StatisticLastMailOpened.GetHashCode();
                if (this.StatisticLastMailClicked != null)
                    hash = hash * 59 + this.StatisticLastMailClicked.GetHashCode();
                if (this.StatisticLastMailConverted != null)
                    hash = hash * 59 + this.StatisticLastMailConverted.GetHashCode();
                if (this.StatisticLastUseragentDevice != null)
                    hash = hash * 59 + this.StatisticLastUseragentDevice.GetHashCode();
                if (this.StatisticLastUseragentFamily != null)
                    hash = hash * 59 + this.StatisticLastUseragentFamily.GetHashCode();
                if (this.StatisticLastLatitude != null)
                    hash = hash * 59 + this.StatisticLastLatitude.GetHashCode();
                if (this.StatisticLastLongitude != null)
                    hash = hash * 59 + this.StatisticLastLongitude.GetHashCode();
                if (this.StatisticLastBlacklistList != null)
                    hash = hash * 59 + this.StatisticLastBlacklistList.GetHashCode();
                if (this.StatisticLastUnsubscribeList != null)
                    hash = hash * 59 + this.StatisticLastUnsubscribeList.GetHashCode();
                if (this.StatisticLastBlacklistListReason != null)
                    hash = hash * 59 + this.StatisticLastBlacklistListReason.GetHashCode();
                if (this.StatisticLastUnsubscribeListReason != null)
                    hash = hash * 59 + this.StatisticLastUnsubscribeListReason.GetHashCode();
                return hash;
            }
        }
    }

}
