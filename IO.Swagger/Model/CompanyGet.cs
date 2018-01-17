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
    /// CompanyGet
    /// </summary>
    [DataContract]
    public partial class CompanyGet :  IEquatable<CompanyGet>
    {
        /// <summary>
        /// the state of the company (allowed to send or not)
        /// </summary>
        /// <value>the state of the company (allowed to send or not)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StateEnum
        {
            
            /// <summary>
            /// Enum Verified for "verified"
            /// </summary>
            [EnumMember(Value = "verified")]
            Verified,
            
            /// <summary>
            /// Enum Pending for "pending"
            /// </summary>
            [EnumMember(Value = "pending")]
            Pending,
            
            /// <summary>
            /// Enum Declided for "declided"
            /// </summary>
            [EnumMember(Value = "declided")]
            Declided
        }

        /// <summary>
        /// the state of the company (allowed to send or not)
        /// </summary>
        /// <value>the state of the company (allowed to send or not)</value>
        [DataMember(Name="state", EmitDefaultValue=false)]
        public StateEnum? State { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyGet" /> class.
        /// </summary>
        /// <param name="Id">the id of the company.</param>
        /// <param name="Name">name of the company.</param>
        /// <param name="Address">the address of the company.</param>
        /// <param name="City">the city of the company.</param>
        /// <param name="Postcode">the city of the company.</param>
        /// <param name="Country">the ISO 3166-1 alpha-2 country code of the company.</param>
        /// <param name="BillName">name of the company for bills.</param>
        /// <param name="BillFirstName">first name of the contact person of the company for bills.</param>
        /// <param name="BillLastName">last name of the contact person of the company for bills.</param>
        /// <param name="BillAddress">the address of the company for bills.</param>
        /// <param name="BillCity">the city of the company for bills.</param>
        /// <param name="BillPostcode">the city of the company for bills.</param>
        /// <param name="BillCountry">the ISO 3166-1 alpha-2 country code of the company for bills.</param>
        /// <param name="DefaultListId">the id of the list which should be used as default.</param>
        /// <param name="State">the state of the company (allowed to send or not).</param>
        /// <param name="CreditsEmail">the amount of prepaid credits.</param>
        /// <param name="CreditsFreemail">the number of free credits.</param>
        /// <param name="CreditsAbo">the number of abo credits.</param>
        public CompanyGet(string Id = null, string Name = null, string Address = null, string City = null, string Postcode = null, string Country = null, string BillName = null, string BillFirstName = null, string BillLastName = null, string BillAddress = null, string BillCity = null, string BillPostcode = null, string BillCountry = null, string DefaultListId = null, StateEnum? State = null, int? CreditsEmail = null, int? CreditsFreemail = null, int? CreditsAbo = null)
        {
            this.Id = Id;
            this.Name = Name;
            this.Address = Address;
            this.City = City;
            this.Postcode = Postcode;
            this.Country = Country;
            this.BillName = BillName;
            this.BillFirstName = BillFirstName;
            this.BillLastName = BillLastName;
            this.BillAddress = BillAddress;
            this.BillCity = BillCity;
            this.BillPostcode = BillPostcode;
            this.BillCountry = BillCountry;
            this.DefaultListId = DefaultListId;
            this.State = State;
            this.CreditsEmail = CreditsEmail;
            this.CreditsFreemail = CreditsFreemail;
            this.CreditsAbo = CreditsAbo;
        }
        
        /// <summary>
        /// the id of the company
        /// </summary>
        /// <value>the id of the company</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }
        /// <summary>
        /// name of the company
        /// </summary>
        /// <value>name of the company</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }
        /// <summary>
        /// the address of the company
        /// </summary>
        /// <value>the address of the company</value>
        [DataMember(Name="address", EmitDefaultValue=false)]
        public string Address { get; set; }
        /// <summary>
        /// the city of the company
        /// </summary>
        /// <value>the city of the company</value>
        [DataMember(Name="city", EmitDefaultValue=false)]
        public string City { get; set; }
        /// <summary>
        /// the city of the company
        /// </summary>
        /// <value>the city of the company</value>
        [DataMember(Name="postcode", EmitDefaultValue=false)]
        public string Postcode { get; set; }
        /// <summary>
        /// the ISO 3166-1 alpha-2 country code of the company
        /// </summary>
        /// <value>the ISO 3166-1 alpha-2 country code of the company</value>
        [DataMember(Name="country", EmitDefaultValue=false)]
        public string Country { get; set; }
        /// <summary>
        /// name of the company for bills
        /// </summary>
        /// <value>name of the company for bills</value>
        [DataMember(Name="bill_name", EmitDefaultValue=false)]
        public string BillName { get; set; }
        /// <summary>
        /// first name of the contact person of the company for bills
        /// </summary>
        /// <value>first name of the contact person of the company for bills</value>
        [DataMember(Name="bill_first_name", EmitDefaultValue=false)]
        public string BillFirstName { get; set; }
        /// <summary>
        /// last name of the contact person of the company for bills
        /// </summary>
        /// <value>last name of the contact person of the company for bills</value>
        [DataMember(Name="bill_last_name", EmitDefaultValue=false)]
        public string BillLastName { get; set; }
        /// <summary>
        /// the address of the company for bills
        /// </summary>
        /// <value>the address of the company for bills</value>
        [DataMember(Name="bill_address", EmitDefaultValue=false)]
        public string BillAddress { get; set; }
        /// <summary>
        /// the city of the company for bills
        /// </summary>
        /// <value>the city of the company for bills</value>
        [DataMember(Name="bill_city", EmitDefaultValue=false)]
        public string BillCity { get; set; }
        /// <summary>
        /// the city of the company for bills
        /// </summary>
        /// <value>the city of the company for bills</value>
        [DataMember(Name="bill_postcode", EmitDefaultValue=false)]
        public string BillPostcode { get; set; }
        /// <summary>
        /// the ISO 3166-1 alpha-2 country code of the company for bills
        /// </summary>
        /// <value>the ISO 3166-1 alpha-2 country code of the company for bills</value>
        [DataMember(Name="bill_country", EmitDefaultValue=false)]
        public string BillCountry { get; set; }
        /// <summary>
        /// the id of the list which should be used as default
        /// </summary>
        /// <value>the id of the list which should be used as default</value>
        [DataMember(Name="default_list_id", EmitDefaultValue=false)]
        public string DefaultListId { get; set; }
        /// <summary>
        /// the amount of prepaid credits
        /// </summary>
        /// <value>the amount of prepaid credits</value>
        [DataMember(Name="credits_email", EmitDefaultValue=false)]
        public int? CreditsEmail { get; set; }
        /// <summary>
        /// the number of free credits
        /// </summary>
        /// <value>the number of free credits</value>
        [DataMember(Name="credits_freemail", EmitDefaultValue=false)]
        public int? CreditsFreemail { get; set; }
        /// <summary>
        /// the number of abo credits
        /// </summary>
        /// <value>the number of abo credits</value>
        [DataMember(Name="credits_abo", EmitDefaultValue=false)]
        public int? CreditsAbo { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CompanyGet {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  Postcode: ").Append(Postcode).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  BillName: ").Append(BillName).Append("\n");
            sb.Append("  BillFirstName: ").Append(BillFirstName).Append("\n");
            sb.Append("  BillLastName: ").Append(BillLastName).Append("\n");
            sb.Append("  BillAddress: ").Append(BillAddress).Append("\n");
            sb.Append("  BillCity: ").Append(BillCity).Append("\n");
            sb.Append("  BillPostcode: ").Append(BillPostcode).Append("\n");
            sb.Append("  BillCountry: ").Append(BillCountry).Append("\n");
            sb.Append("  DefaultListId: ").Append(DefaultListId).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  CreditsEmail: ").Append(CreditsEmail).Append("\n");
            sb.Append("  CreditsFreemail: ").Append(CreditsFreemail).Append("\n");
            sb.Append("  CreditsAbo: ").Append(CreditsAbo).Append("\n");
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
            return this.Equals(obj as CompanyGet);
        }

        /// <summary>
        /// Returns true if CompanyGet instances are equal
        /// </summary>
        /// <param name="other">Instance of CompanyGet to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CompanyGet other)
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
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) && 
                (
                    this.Address == other.Address ||
                    this.Address != null &&
                    this.Address.Equals(other.Address)
                ) && 
                (
                    this.City == other.City ||
                    this.City != null &&
                    this.City.Equals(other.City)
                ) && 
                (
                    this.Postcode == other.Postcode ||
                    this.Postcode != null &&
                    this.Postcode.Equals(other.Postcode)
                ) && 
                (
                    this.Country == other.Country ||
                    this.Country != null &&
                    this.Country.Equals(other.Country)
                ) && 
                (
                    this.BillName == other.BillName ||
                    this.BillName != null &&
                    this.BillName.Equals(other.BillName)
                ) && 
                (
                    this.BillFirstName == other.BillFirstName ||
                    this.BillFirstName != null &&
                    this.BillFirstName.Equals(other.BillFirstName)
                ) && 
                (
                    this.BillLastName == other.BillLastName ||
                    this.BillLastName != null &&
                    this.BillLastName.Equals(other.BillLastName)
                ) && 
                (
                    this.BillAddress == other.BillAddress ||
                    this.BillAddress != null &&
                    this.BillAddress.Equals(other.BillAddress)
                ) && 
                (
                    this.BillCity == other.BillCity ||
                    this.BillCity != null &&
                    this.BillCity.Equals(other.BillCity)
                ) && 
                (
                    this.BillPostcode == other.BillPostcode ||
                    this.BillPostcode != null &&
                    this.BillPostcode.Equals(other.BillPostcode)
                ) && 
                (
                    this.BillCountry == other.BillCountry ||
                    this.BillCountry != null &&
                    this.BillCountry.Equals(other.BillCountry)
                ) && 
                (
                    this.DefaultListId == other.DefaultListId ||
                    this.DefaultListId != null &&
                    this.DefaultListId.Equals(other.DefaultListId)
                ) && 
                (
                    this.State == other.State ||
                    this.State != null &&
                    this.State.Equals(other.State)
                ) && 
                (
                    this.CreditsEmail == other.CreditsEmail ||
                    this.CreditsEmail != null &&
                    this.CreditsEmail.Equals(other.CreditsEmail)
                ) && 
                (
                    this.CreditsFreemail == other.CreditsFreemail ||
                    this.CreditsFreemail != null &&
                    this.CreditsFreemail.Equals(other.CreditsFreemail)
                ) && 
                (
                    this.CreditsAbo == other.CreditsAbo ||
                    this.CreditsAbo != null &&
                    this.CreditsAbo.Equals(other.CreditsAbo)
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
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
                if (this.Address != null)
                    hash = hash * 59 + this.Address.GetHashCode();
                if (this.City != null)
                    hash = hash * 59 + this.City.GetHashCode();
                if (this.Postcode != null)
                    hash = hash * 59 + this.Postcode.GetHashCode();
                if (this.Country != null)
                    hash = hash * 59 + this.Country.GetHashCode();
                if (this.BillName != null)
                    hash = hash * 59 + this.BillName.GetHashCode();
                if (this.BillFirstName != null)
                    hash = hash * 59 + this.BillFirstName.GetHashCode();
                if (this.BillLastName != null)
                    hash = hash * 59 + this.BillLastName.GetHashCode();
                if (this.BillAddress != null)
                    hash = hash * 59 + this.BillAddress.GetHashCode();
                if (this.BillCity != null)
                    hash = hash * 59 + this.BillCity.GetHashCode();
                if (this.BillPostcode != null)
                    hash = hash * 59 + this.BillPostcode.GetHashCode();
                if (this.BillCountry != null)
                    hash = hash * 59 + this.BillCountry.GetHashCode();
                if (this.DefaultListId != null)
                    hash = hash * 59 + this.DefaultListId.GetHashCode();
                if (this.State != null)
                    hash = hash * 59 + this.State.GetHashCode();
                if (this.CreditsEmail != null)
                    hash = hash * 59 + this.CreditsEmail.GetHashCode();
                if (this.CreditsFreemail != null)
                    hash = hash * 59 + this.CreditsFreemail.GetHashCode();
                if (this.CreditsAbo != null)
                    hash = hash * 59 + this.CreditsAbo.GetHashCode();
                return hash;
            }
        }
    }

}
