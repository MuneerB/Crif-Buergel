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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRecipientApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// add single recipient to group
        /// </summary>
        /// <remarks>
        /// add Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>PatchResponse</returns>
        PatchResponse AddRecipientToGroup (string lid, string gid, string id);

        /// <summary>
        /// add single recipient to group
        /// </summary>
        /// <remarks>
        /// add Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        ApiResponse<PatchResponse> AddRecipientToGroupWithHttpInfo (string lid, string gid, string id);
        /// <summary>
        /// add all Recipients to the given group
        /// </summary>
        /// <remarks>
        /// add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        PatchResponse AddRecipientsToGroup (string lid, string gid, string filter = null);

        /// <summary>
        /// add all Recipients to the given group
        /// </summary>
        /// <remarks>
        /// add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        ApiResponse<PatchResponse> AddRecipientsToGroupWithHttpInfo (string lid, string gid, string filter = null);
        /// <summary>
        /// create new recipient(s) or updates existing ones
        /// </summary>
        /// <remarks>
        /// create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>ApiResponsePost</returns>
        ApiResponsePost CreateRecipient (RecipientPost recipient);

        /// <summary>
        /// create new recipient(s) or updates existing ones
        /// </summary>
        /// <remarks>
        /// create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>ApiResponse of ApiResponsePost</returns>
        ApiResponse<ApiResponsePost> CreateRecipientWithHttpInfo (RecipientPost recipient);
        /// <summary>
        /// delete the recipient
        /// </summary>
        /// <remarks>
        /// delete one recipient
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponseDelete</returns>
        ApiResponseDelete DeleteRecipient (string lid, string id);

        /// <summary>
        /// delete the recipient
        /// </summary>
        /// <remarks>
        /// delete one recipient
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponse of ApiResponseDelete</returns>
        ApiResponse<ApiResponseDelete> DeleteRecipientWithHttpInfo (string lid, string id);
        /// <summary>
        /// get all Recipients of selected list
        /// </summary>
        /// <remarks>
        /// Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>RecipientResponse</returns>
        RecipientResponse GetRecipients (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);

        /// <summary>
        /// get all Recipients of selected list
        /// </summary>
        /// <remarks>
        /// Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>ApiResponse of RecipientResponse</returns>
        ApiResponse<RecipientResponse> GetRecipientsWithHttpInfo (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);
        /// <summary>
        /// get all Recipients of selected group
        /// </summary>
        /// <remarks>
        /// get Recipients
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>RecipientResponse</returns>
        RecipientResponse GetRecipientsByGroup (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);

        /// <summary>
        /// get all Recipients of selected group
        /// </summary>
        /// <remarks>
        /// get Recipients
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>ApiResponse of RecipientResponse</returns>
        ApiResponse<RecipientResponse> GetRecipientsByGroupWithHttpInfo (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);
        /// <summary>
        /// Initialize the import of recipients by file
        /// </summary>
        /// <remarks>
        /// Initialize the import recipients by file
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>InlineResponse200</returns>
        InlineResponse200 ImportRecipientsInit (string lid, System.IO.Stream file);

        /// <summary>
        /// Initialize the import of recipients by file
        /// </summary>
        /// <remarks>
        /// Initialize the import recipients by file
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>ApiResponse of InlineResponse200</returns>
        ApiResponse<InlineResponse200> ImportRecipientsInitWithHttpInfo (string lid, System.IO.Stream file);
        /// <summary>
        /// Start the import recipients by file
        /// </summary>
        /// <remarks>
        /// Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>ApiResponsePost</returns>
        ApiResponsePost ImportRecipientsSave (string lid, Data data);

        /// <summary>
        /// Start the import recipients by file
        /// </summary>
        /// <remarks>
        /// Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>ApiResponse of ApiResponsePost</returns>
        ApiResponse<ApiResponsePost> ImportRecipientsSaveWithHttpInfo (string lid, Data data);
        /// <summary>
        /// Get statistics about the import by file
        /// </summary>
        /// <remarks>
        /// Get statistics about the import by file.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>ImportResponse</returns>
        ImportResponse ImportRecipientsStatistics (string id);

        /// <summary>
        /// Get statistics about the import by file
        /// </summary>
        /// <remarks>
        /// Get statistics about the import by file.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>ApiResponse of ImportResponse</returns>
        ApiResponse<ImportResponse> ImportRecipientsStatisticsWithHttpInfo (string id);
        /// <summary>
        /// remove single recipient from group
        /// </summary>
        /// <remarks>
        /// remove Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>PatchResponse</returns>
        PatchResponse RemoveRecipientFromGroup (string lid, string gid, string id);

        /// <summary>
        /// remove single recipient from group
        /// </summary>
        /// <remarks>
        /// remove Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        ApiResponse<PatchResponse> RemoveRecipientFromGroupWithHttpInfo (string lid, string gid, string id);
        /// <summary>
        /// remove all Recipients from given group
        /// </summary>
        /// <remarks>
        /// remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        PatchResponse RemoveRecipientsFromGroup (string lid, string gid, string filter = null);

        /// <summary>
        /// remove all Recipients from given group
        /// </summary>
        /// <remarks>
        /// remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        ApiResponse<PatchResponse> RemoveRecipientsFromGroupWithHttpInfo (string lid, string gid, string filter = null);
        /// <summary>
        /// remove all Recipients from selected list
        /// </summary>
        /// <remarks>
        /// remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        PatchResponse RemoveRecipientsFromList (string lid, string filter = null);

        /// <summary>
        /// remove all Recipients from selected list
        /// </summary>
        /// <remarks>
        /// remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        ApiResponse<PatchResponse> RemoveRecipientsFromListWithHttpInfo (string lid, string filter = null);
        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail
        /// </summary>
        /// <remarks>
        /// Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>RecipientSubscribeResponse</returns>
        RecipientSubscribeResponse SubscribeRecipient (string code, RecipientSubscribe recipient);

        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail
        /// </summary>
        /// <remarks>
        /// Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>ApiResponse of RecipientSubscribeResponse</returns>
        ApiResponse<RecipientSubscribeResponse> SubscribeRecipientWithHttpInfo (string code, RecipientSubscribe recipient);
        /// <summary>
        /// update the recipient.
        /// </summary>
        /// <remarks>
        /// update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>PatchResponse</returns>
        PatchResponse UpdateRecipient (string lid, string id, RecipientPatch recipient);

        /// <summary>
        /// update the recipient.
        /// </summary>
        /// <remarks>
        /// update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        ApiResponse<PatchResponse> UpdateRecipientWithHttpInfo (string lid, string id, RecipientPatch recipient);
        /// <summary>
        /// update all Recipients in selected list
        /// </summary>
        /// <remarks>
        /// Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        PatchResponse UpdateRecipients (string lid, string filter = null);

        /// <summary>
        /// update all Recipients in selected list
        /// </summary>
        /// <remarks>
        /// Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        ApiResponse<PatchResponse> UpdateRecipientsWithHttpInfo (string lid, string filter = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// add single recipient to group
        /// </summary>
        /// <remarks>
        /// add Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of PatchResponse</returns>
        System.Threading.Tasks.Task<PatchResponse> AddRecipientToGroupAsync (string lid, string gid, string id);

        /// <summary>
        /// add single recipient to group
        /// </summary>
        /// <remarks>
        /// add Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PatchResponse>> AddRecipientToGroupAsyncWithHttpInfo (string lid, string gid, string id);
        /// <summary>
        /// add all Recipients to the given group
        /// </summary>
        /// <remarks>
        /// add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        System.Threading.Tasks.Task<PatchResponse> AddRecipientsToGroupAsync (string lid, string gid, string filter = null);

        /// <summary>
        /// add all Recipients to the given group
        /// </summary>
        /// <remarks>
        /// add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PatchResponse>> AddRecipientsToGroupAsyncWithHttpInfo (string lid, string gid, string filter = null);
        /// <summary>
        /// create new recipient(s) or updates existing ones
        /// </summary>
        /// <remarks>
        /// create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>Task of ApiResponsePost</returns>
        System.Threading.Tasks.Task<ApiResponsePost> CreateRecipientAsync (RecipientPost recipient);

        /// <summary>
        /// create new recipient(s) or updates existing ones
        /// </summary>
        /// <remarks>
        /// create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>Task of ApiResponse (ApiResponsePost)</returns>
        System.Threading.Tasks.Task<ApiResponse<ApiResponsePost>> CreateRecipientAsyncWithHttpInfo (RecipientPost recipient);
        /// <summary>
        /// delete the recipient
        /// </summary>
        /// <remarks>
        /// delete one recipient
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponseDelete</returns>
        System.Threading.Tasks.Task<ApiResponseDelete> DeleteRecipientAsync (string lid, string id);

        /// <summary>
        /// delete the recipient
        /// </summary>
        /// <remarks>
        /// delete one recipient
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponse (ApiResponseDelete)</returns>
        System.Threading.Tasks.Task<ApiResponse<ApiResponseDelete>> DeleteRecipientAsyncWithHttpInfo (string lid, string id);
        /// <summary>
        /// get all Recipients of selected list
        /// </summary>
        /// <remarks>
        /// Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of RecipientResponse</returns>
        System.Threading.Tasks.Task<RecipientResponse> GetRecipientsAsync (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);

        /// <summary>
        /// get all Recipients of selected list
        /// </summary>
        /// <remarks>
        /// Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of ApiResponse (RecipientResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<RecipientResponse>> GetRecipientsAsyncWithHttpInfo (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);
        /// <summary>
        /// get all Recipients of selected group
        /// </summary>
        /// <remarks>
        /// get Recipients
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of RecipientResponse</returns>
        System.Threading.Tasks.Task<RecipientResponse> GetRecipientsByGroupAsync (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);

        /// <summary>
        /// get all Recipients of selected group
        /// </summary>
        /// <remarks>
        /// get Recipients
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of ApiResponse (RecipientResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<RecipientResponse>> GetRecipientsByGroupAsyncWithHttpInfo (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null);
        /// <summary>
        /// Initialize the import of recipients by file
        /// </summary>
        /// <remarks>
        /// Initialize the import recipients by file
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>Task of InlineResponse200</returns>
        System.Threading.Tasks.Task<InlineResponse200> ImportRecipientsInitAsync (string lid, System.IO.Stream file);

        /// <summary>
        /// Initialize the import of recipients by file
        /// </summary>
        /// <remarks>
        /// Initialize the import recipients by file
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>Task of ApiResponse (InlineResponse200)</returns>
        System.Threading.Tasks.Task<ApiResponse<InlineResponse200>> ImportRecipientsInitAsyncWithHttpInfo (string lid, System.IO.Stream file);
        /// <summary>
        /// Start the import recipients by file
        /// </summary>
        /// <remarks>
        /// Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>Task of ApiResponsePost</returns>
        System.Threading.Tasks.Task<ApiResponsePost> ImportRecipientsSaveAsync (string lid, Data data);

        /// <summary>
        /// Start the import recipients by file
        /// </summary>
        /// <remarks>
        /// Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>Task of ApiResponse (ApiResponsePost)</returns>
        System.Threading.Tasks.Task<ApiResponse<ApiResponsePost>> ImportRecipientsSaveAsyncWithHttpInfo (string lid, Data data);
        /// <summary>
        /// Get statistics about the import by file
        /// </summary>
        /// <remarks>
        /// Get statistics about the import by file.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>Task of ImportResponse</returns>
        System.Threading.Tasks.Task<ImportResponse> ImportRecipientsStatisticsAsync (string id);

        /// <summary>
        /// Get statistics about the import by file
        /// </summary>
        /// <remarks>
        /// Get statistics about the import by file.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>Task of ApiResponse (ImportResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<ImportResponse>> ImportRecipientsStatisticsAsyncWithHttpInfo (string id);
        /// <summary>
        /// remove single recipient from group
        /// </summary>
        /// <remarks>
        /// remove Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of PatchResponse</returns>
        System.Threading.Tasks.Task<PatchResponse> RemoveRecipientFromGroupAsync (string lid, string gid, string id);

        /// <summary>
        /// remove single recipient from group
        /// </summary>
        /// <remarks>
        /// remove Recipient to Group
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PatchResponse>> RemoveRecipientFromGroupAsyncWithHttpInfo (string lid, string gid, string id);
        /// <summary>
        /// remove all Recipients from given group
        /// </summary>
        /// <remarks>
        /// remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        System.Threading.Tasks.Task<PatchResponse> RemoveRecipientsFromGroupAsync (string lid, string gid, string filter = null);

        /// <summary>
        /// remove all Recipients from given group
        /// </summary>
        /// <remarks>
        /// remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PatchResponse>> RemoveRecipientsFromGroupAsyncWithHttpInfo (string lid, string gid, string filter = null);
        /// <summary>
        /// remove all Recipients from selected list
        /// </summary>
        /// <remarks>
        /// remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        System.Threading.Tasks.Task<PatchResponse> RemoveRecipientsFromListAsync (string lid, string filter = null);

        /// <summary>
        /// remove all Recipients from selected list
        /// </summary>
        /// <remarks>
        /// remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PatchResponse>> RemoveRecipientsFromListAsyncWithHttpInfo (string lid, string filter = null);
        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail
        /// </summary>
        /// <remarks>
        /// Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>Task of RecipientSubscribeResponse</returns>
        System.Threading.Tasks.Task<RecipientSubscribeResponse> SubscribeRecipientAsync (string code, RecipientSubscribe recipient);

        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail
        /// </summary>
        /// <remarks>
        /// Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>Task of ApiResponse (RecipientSubscribeResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<RecipientSubscribeResponse>> SubscribeRecipientAsyncWithHttpInfo (string code, RecipientSubscribe recipient);
        /// <summary>
        /// update the recipient.
        /// </summary>
        /// <remarks>
        /// update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>Task of PatchResponse</returns>
        System.Threading.Tasks.Task<PatchResponse> UpdateRecipientAsync (string lid, string id, RecipientPatch recipient);

        /// <summary>
        /// update the recipient.
        /// </summary>
        /// <remarks>
        /// update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PatchResponse>> UpdateRecipientAsyncWithHttpInfo (string lid, string id, RecipientPatch recipient);
        /// <summary>
        /// update all Recipients in selected list
        /// </summary>
        /// <remarks>
        /// Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        System.Threading.Tasks.Task<PatchResponse> UpdateRecipientsAsync (string lid, string filter = null);

        /// <summary>
        /// update all Recipients in selected list
        /// </summary>
        /// <remarks>
        /// Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PatchResponse>> UpdateRecipientsAsyncWithHttpInfo (string lid, string filter = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class RecipientApi : IRecipientApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipientApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RecipientApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipientApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public RecipientApi(Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = Configuration.Default;
            else
                this.Configuration = configuration;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Configuration Configuration {get; set;}

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public Dictionary<String, String> DefaultHeader()
        {
            return this.Configuration.DefaultHeader;
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// add single recipient to group add Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>PatchResponse</returns>
        public PatchResponse AddRecipientToGroup (string lid, string gid, string id)
        {
             ApiResponse<PatchResponse> localVarResponse = AddRecipientToGroupWithHttpInfo(lid, gid, id);
             return localVarResponse.Data;
        }

        /// <summary>
        /// add single recipient to group add Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        public ApiResponse< PatchResponse > AddRecipientToGroupWithHttpInfo (string lid, string gid, string id)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->AddRecipientToGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->AddRecipientToGroup");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->AddRecipientToGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientToGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientToGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// add single recipient to group add Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of PatchResponse</returns>
        public async System.Threading.Tasks.Task<PatchResponse> AddRecipientToGroupAsync (string lid, string gid, string id)
        {
             ApiResponse<PatchResponse> localVarResponse = await AddRecipientToGroupAsyncWithHttpInfo(lid, gid, id);
             return localVarResponse.Data;

        }

        /// <summary>
        /// add single recipient to group add Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PatchResponse>> AddRecipientToGroupAsyncWithHttpInfo (string lid, string gid, string id)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->AddRecipientToGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->AddRecipientToGroup");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->AddRecipientToGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientToGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientToGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// add all Recipients to the given group add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        public PatchResponse AddRecipientsToGroup (string lid, string gid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = AddRecipientsToGroupWithHttpInfo(lid, gid, filter);
             return localVarResponse.Data;
        }

        /// <summary>
        /// add all Recipients to the given group add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        public ApiResponse< PatchResponse > AddRecipientsToGroupWithHttpInfo (string lid, string gid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->AddRecipientsToGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->AddRecipientsToGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientsToGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientsToGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// add all Recipients to the given group add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        public async System.Threading.Tasks.Task<PatchResponse> AddRecipientsToGroupAsync (string lid, string gid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = await AddRecipientsToGroupAsyncWithHttpInfo(lid, gid, filter);
             return localVarResponse.Data;

        }

        /// <summary>
        /// add all Recipients to the given group add all Recipients to the given group. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting added to the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PatchResponse>> AddRecipientsToGroupAsyncWithHttpInfo (string lid, string gid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->AddRecipientsToGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->AddRecipientsToGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientsToGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling AddRecipientsToGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// create new recipient(s) or updates existing ones create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>ApiResponsePost</returns>
        public ApiResponsePost CreateRecipient (RecipientPost recipient)
        {
             ApiResponse<ApiResponsePost> localVarResponse = CreateRecipientWithHttpInfo(recipient);
             return localVarResponse.Data;
        }

        /// <summary>
        /// create new recipient(s) or updates existing ones create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>ApiResponse of ApiResponsePost</returns>
        public ApiResponse< ApiResponsePost > CreateRecipientWithHttpInfo (RecipientPost recipient)
        {
            // verify the required parameter 'recipient' is set
            if (recipient == null)
                throw new ApiException(400, "Missing required parameter 'recipient' when calling RecipientApi->CreateRecipient");

            var localVarPath = "/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (recipient != null && recipient.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(recipient); // http body (model) parameter
            }
            else
            {
                localVarPostBody = recipient; // byte array
            }

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling CreateRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling CreateRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ApiResponsePost>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiResponsePost) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiResponsePost)));
            
        }

        /// <summary>
        /// create new recipient(s) or updates existing ones create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>Task of ApiResponsePost</returns>
        public async System.Threading.Tasks.Task<ApiResponsePost> CreateRecipientAsync (RecipientPost recipient)
        {
             ApiResponse<ApiResponsePost> localVarResponse = await CreateRecipientAsyncWithHttpInfo(recipient);
             return localVarResponse.Data;

        }

        /// <summary>
        /// create new recipient(s) or updates existing ones create new recipient(s).&lt;br&gt;If a custom attribute shall by saved for the new recipient just submit them in the json body by name.&lt;br&gt;If &lt;b&gt;multiple recipients&lt;/b&gt; shall by saved at once, submit multiple recipient-objects as a json array.&lt;br&gt;&lt;b&gt;In this case all objects must share the same attributes.&lt;/b&gt;&lt;br/&gt;If the e-mail-address exists already the existing recipient gets updated.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="recipient">the data to save</param>
        /// <returns>Task of ApiResponse (ApiResponsePost)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ApiResponsePost>> CreateRecipientAsyncWithHttpInfo (RecipientPost recipient)
        {
            // verify the required parameter 'recipient' is set
            if (recipient == null)
                throw new ApiException(400, "Missing required parameter 'recipient' when calling RecipientApi->CreateRecipient");

            var localVarPath = "/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (recipient != null && recipient.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(recipient); // http body (model) parameter
            }
            else
            {
                localVarPostBody = recipient; // byte array
            }

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling CreateRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling CreateRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ApiResponsePost>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiResponsePost) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiResponsePost)));
            
        }

        /// <summary>
        /// delete the recipient delete one recipient
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponseDelete</returns>
        public ApiResponseDelete DeleteRecipient (string lid, string id)
        {
             ApiResponse<ApiResponseDelete> localVarResponse = DeleteRecipientWithHttpInfo(lid, id);
             return localVarResponse.Data;
        }

        /// <summary>
        /// delete the recipient delete one recipient
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponse of ApiResponseDelete</returns>
        public ApiResponse< ApiResponseDelete > DeleteRecipientWithHttpInfo (string lid, string id)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->DeleteRecipient");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->DeleteRecipient");

            var localVarPath = "/lists/{lid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling DeleteRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling DeleteRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ApiResponseDelete>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiResponseDelete) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiResponseDelete)));
            
        }

        /// <summary>
        /// delete the recipient delete one recipient
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponseDelete</returns>
        public async System.Threading.Tasks.Task<ApiResponseDelete> DeleteRecipientAsync (string lid, string id)
        {
             ApiResponse<ApiResponseDelete> localVarResponse = await DeleteRecipientAsyncWithHttpInfo(lid, id);
             return localVarResponse.Data;

        }

        /// <summary>
        /// delete the recipient delete one recipient
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponse (ApiResponseDelete)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ApiResponseDelete>> DeleteRecipientAsyncWithHttpInfo (string lid, string id)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->DeleteRecipient");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->DeleteRecipient");

            var localVarPath = "/lists/{lid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling DeleteRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling DeleteRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ApiResponseDelete>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiResponseDelete) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiResponseDelete)));
            
        }

        /// <summary>
        /// get all Recipients of selected list Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>RecipientResponse</returns>
        public RecipientResponse GetRecipients (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
             ApiResponse<RecipientResponse> localVarResponse = GetRecipientsWithHttpInfo(lid, filter, limit, offset, expand, fields);
             return localVarResponse.Data;
        }

        /// <summary>
        /// get all Recipients of selected list Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>ApiResponse of RecipientResponse</returns>
        public ApiResponse< RecipientResponse > GetRecipientsWithHttpInfo (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->GetRecipients");

            var localVarPath = "/lists/{lid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter
            if (limit != null) localVarQueryParams.Add("_limit", Configuration.ApiClient.ParameterToString(limit)); // query parameter
            if (offset != null) localVarQueryParams.Add("_offset", Configuration.ApiClient.ParameterToString(offset)); // query parameter
            if (expand != null) localVarQueryParams.Add("_expand", Configuration.ApiClient.ParameterToString(expand)); // query parameter
            if (fields != null) localVarQueryParams.Add("_fields", Configuration.ApiClient.ParameterToString(fields)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipients: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipients: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<RecipientResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (RecipientResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(RecipientResponse)));
            
        }

        /// <summary>
        /// get all Recipients of selected list Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of RecipientResponse</returns>
        public async System.Threading.Tasks.Task<RecipientResponse> GetRecipientsAsync (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
             ApiResponse<RecipientResponse> localVarResponse = await GetRecipientsAsyncWithHttpInfo(lid, filter, limit, offset, expand, fields);
             return localVarResponse.Data;

        }

        /// <summary>
        /// get all Recipients of selected list Get Recipients. If the API shall return custom attributes also, submit them in the _fields-Parameter by name.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of ApiResponse (RecipientResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<RecipientResponse>> GetRecipientsAsyncWithHttpInfo (string lid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->GetRecipients");

            var localVarPath = "/lists/{lid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter
            if (limit != null) localVarQueryParams.Add("_limit", Configuration.ApiClient.ParameterToString(limit)); // query parameter
            if (offset != null) localVarQueryParams.Add("_offset", Configuration.ApiClient.ParameterToString(offset)); // query parameter
            if (expand != null) localVarQueryParams.Add("_expand", Configuration.ApiClient.ParameterToString(expand)); // query parameter
            if (fields != null) localVarQueryParams.Add("_fields", Configuration.ApiClient.ParameterToString(fields)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipients: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipients: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<RecipientResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (RecipientResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(RecipientResponse)));
            
        }

        /// <summary>
        /// get all Recipients of selected group get Recipients
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>RecipientResponse</returns>
        public RecipientResponse GetRecipientsByGroup (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
             ApiResponse<RecipientResponse> localVarResponse = GetRecipientsByGroupWithHttpInfo(lid, gid, filter, limit, offset, expand, fields);
             return localVarResponse.Data;
        }

        /// <summary>
        /// get all Recipients of selected group get Recipients
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>ApiResponse of RecipientResponse</returns>
        public ApiResponse< RecipientResponse > GetRecipientsByGroupWithHttpInfo (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->GetRecipientsByGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->GetRecipientsByGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter
            if (limit != null) localVarQueryParams.Add("_limit", Configuration.ApiClient.ParameterToString(limit)); // query parameter
            if (offset != null) localVarQueryParams.Add("_offset", Configuration.ApiClient.ParameterToString(offset)); // query parameter
            if (expand != null) localVarQueryParams.Add("_expand", Configuration.ApiClient.ParameterToString(expand)); // query parameter
            if (fields != null) localVarQueryParams.Add("_fields", Configuration.ApiClient.ParameterToString(fields)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipientsByGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipientsByGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<RecipientResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (RecipientResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(RecipientResponse)));
            
        }

        /// <summary>
        /// get all Recipients of selected group get Recipients
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of RecipientResponse</returns>
        public async System.Threading.Tasks.Task<RecipientResponse> GetRecipientsByGroupAsync (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
             ApiResponse<RecipientResponse> localVarResponse = await GetRecipientsByGroupAsyncWithHttpInfo(lid, gid, filter, limit, offset, expand, fields);
             return localVarResponse.Data;

        }

        /// <summary>
        /// get all Recipients of selected group get Recipients
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <param name="limit">a limit for list-responses (optional, default to 50)</param>
        /// <param name="offset">an offset for list-responses (optional, default to 0)</param>
        /// <param name="expand">true if attributes should be returned or not (optional)</param>
        /// <param name="fields">list of case-sensetive fields which should be returned.    Only needed if _expand is false or special attributes are needed (optional)</param>
        /// <returns>Task of ApiResponse (RecipientResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<RecipientResponse>> GetRecipientsByGroupAsyncWithHttpInfo (string lid, string gid, string filter = null, int? limit = null, int? offset = null, bool? expand = null, List<string> fields = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->GetRecipientsByGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->GetRecipientsByGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter
            if (limit != null) localVarQueryParams.Add("_limit", Configuration.ApiClient.ParameterToString(limit)); // query parameter
            if (offset != null) localVarQueryParams.Add("_offset", Configuration.ApiClient.ParameterToString(offset)); // query parameter
            if (expand != null) localVarQueryParams.Add("_expand", Configuration.ApiClient.ParameterToString(expand)); // query parameter
            if (fields != null) localVarQueryParams.Add("_fields", Configuration.ApiClient.ParameterToString(fields)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipientsByGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling GetRecipientsByGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<RecipientResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (RecipientResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(RecipientResponse)));
            
        }

        /// <summary>
        /// Initialize the import of recipients by file Initialize the import recipients by file
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>InlineResponse200</returns>
        public InlineResponse200 ImportRecipientsInit (string lid, System.IO.Stream file)
        {
             ApiResponse<InlineResponse200> localVarResponse = ImportRecipientsInitWithHttpInfo(lid, file);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Initialize the import of recipients by file Initialize the import recipients by file
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>ApiResponse of InlineResponse200</returns>
        public ApiResponse< InlineResponse200 > ImportRecipientsInitWithHttpInfo (string lid, System.IO.Stream file)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->ImportRecipientsInit");
            // verify the required parameter 'file' is set
            if (file == null)
                throw new ApiException(400, "Missing required parameter 'file' when calling RecipientApi->ImportRecipientsInit");

            var localVarPath = "/lists/{lid}/recipients/import/init";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "multipart/form-data"
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (file != null) localVarFileParams.Add("file", Configuration.ApiClient.ParameterToFile("file", file));

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsInit: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsInit: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<InlineResponse200>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (InlineResponse200) Configuration.ApiClient.Deserialize(localVarResponse, typeof(InlineResponse200)));
            
        }

        /// <summary>
        /// Initialize the import of recipients by file Initialize the import recipients by file
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>Task of InlineResponse200</returns>
        public async System.Threading.Tasks.Task<InlineResponse200> ImportRecipientsInitAsync (string lid, System.IO.Stream file)
        {
             ApiResponse<InlineResponse200> localVarResponse = await ImportRecipientsInitAsyncWithHttpInfo(lid, file);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Initialize the import of recipients by file Initialize the import recipients by file
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="file">recipient file to upload</param>
        /// <returns>Task of ApiResponse (InlineResponse200)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<InlineResponse200>> ImportRecipientsInitAsyncWithHttpInfo (string lid, System.IO.Stream file)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->ImportRecipientsInit");
            // verify the required parameter 'file' is set
            if (file == null)
                throw new ApiException(400, "Missing required parameter 'file' when calling RecipientApi->ImportRecipientsInit");

            var localVarPath = "/lists/{lid}/recipients/import/init";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "multipart/form-data"
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (file != null) localVarFileParams.Add("file", Configuration.ApiClient.ParameterToFile("file", file));

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsInit: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsInit: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<InlineResponse200>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (InlineResponse200) Configuration.ApiClient.Deserialize(localVarResponse, typeof(InlineResponse200)));
            
        }

        /// <summary>
        /// Start the import recipients by file Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>ApiResponsePost</returns>
        public ApiResponsePost ImportRecipientsSave (string lid, Data data)
        {
             ApiResponse<ApiResponsePost> localVarResponse = ImportRecipientsSaveWithHttpInfo(lid, data);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Start the import recipients by file Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>ApiResponse of ApiResponsePost</returns>
        public ApiResponse< ApiResponsePost > ImportRecipientsSaveWithHttpInfo (string lid, Data data)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->ImportRecipientsSave");
            // verify the required parameter 'data' is set
            if (data == null)
                throw new ApiException(400, "Missing required parameter 'data' when calling RecipientApi->ImportRecipientsSave");

            var localVarPath = "/lists/{lid}/recipients/import/save";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (data != null && data.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(data); // http body (model) parameter
            }
            else
            {
                localVarPostBody = data; // byte array
            }

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsSave: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsSave: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ApiResponsePost>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiResponsePost) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiResponsePost)));
            
        }

        /// <summary>
        /// Start the import recipients by file Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>Task of ApiResponsePost</returns>
        public async System.Threading.Tasks.Task<ApiResponsePost> ImportRecipientsSaveAsync (string lid, Data data)
        {
             ApiResponse<ApiResponsePost> localVarResponse = await ImportRecipientsSaveAsyncWithHttpInfo(lid, data);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Start the import recipients by file Start the import recipients by file. The returned id can be used to fetch further statistics about the import.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="data">the data to start the import</param>
        /// <returns>Task of ApiResponse (ApiResponsePost)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ApiResponsePost>> ImportRecipientsSaveAsyncWithHttpInfo (string lid, Data data)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->ImportRecipientsSave");
            // verify the required parameter 'data' is set
            if (data == null)
                throw new ApiException(400, "Missing required parameter 'data' when calling RecipientApi->ImportRecipientsSave");

            var localVarPath = "/lists/{lid}/recipients/import/save";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (data != null && data.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(data); // http body (model) parameter
            }
            else
            {
                localVarPostBody = data; // byte array
            }

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsSave: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsSave: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ApiResponsePost>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiResponsePost) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiResponsePost)));
            
        }

        /// <summary>
        /// Get statistics about the import by file Get statistics about the import by file.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>ImportResponse</returns>
        public ImportResponse ImportRecipientsStatistics (string id)
        {
             ApiResponse<ImportResponse> localVarResponse = ImportRecipientsStatisticsWithHttpInfo(id);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get statistics about the import by file Get statistics about the import by file.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>ApiResponse of ImportResponse</returns>
        public ApiResponse< ImportResponse > ImportRecipientsStatisticsWithHttpInfo (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->ImportRecipientsStatistics");

            var localVarPath = "/import/{id}/info";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsStatistics: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsStatistics: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ImportResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ImportResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ImportResponse)));
            
        }

        /// <summary>
        /// Get statistics about the import by file Get statistics about the import by file.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>Task of ImportResponse</returns>
        public async System.Threading.Tasks.Task<ImportResponse> ImportRecipientsStatisticsAsync (string id)
        {
             ApiResponse<ImportResponse> localVarResponse = await ImportRecipientsStatisticsAsyncWithHttpInfo(id);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get statistics about the import by file Get statistics about the import by file.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">the id of the import job</param>
        /// <returns>Task of ApiResponse (ImportResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ImportResponse>> ImportRecipientsStatisticsAsyncWithHttpInfo (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->ImportRecipientsStatistics");

            var localVarPath = "/import/{id}/info";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsStatistics: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling ImportRecipientsStatistics: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<ImportResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ImportResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ImportResponse)));
            
        }

        /// <summary>
        /// remove single recipient from group remove Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>PatchResponse</returns>
        public PatchResponse RemoveRecipientFromGroup (string lid, string gid, string id)
        {
             ApiResponse<PatchResponse> localVarResponse = RemoveRecipientFromGroupWithHttpInfo(lid, gid, id);
             return localVarResponse.Data;
        }

        /// <summary>
        /// remove single recipient from group remove Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        public ApiResponse< PatchResponse > RemoveRecipientFromGroupWithHttpInfo (string lid, string gid, string id)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->RemoveRecipientFromGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->RemoveRecipientFromGroup");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->RemoveRecipientFromGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientFromGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientFromGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// remove single recipient from group remove Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of PatchResponse</returns>
        public async System.Threading.Tasks.Task<PatchResponse> RemoveRecipientFromGroupAsync (string lid, string gid, string id)
        {
             ApiResponse<PatchResponse> localVarResponse = await RemoveRecipientFromGroupAsyncWithHttpInfo(lid, gid, id);
             return localVarResponse.Data;

        }

        /// <summary>
        /// remove single recipient from group remove Recipient to Group
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="id">the id of the recipient</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PatchResponse>> RemoveRecipientFromGroupAsyncWithHttpInfo (string lid, string gid, string id)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->RemoveRecipientFromGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->RemoveRecipientFromGroup");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->RemoveRecipientFromGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientFromGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientFromGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// remove all Recipients from given group remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        public PatchResponse RemoveRecipientsFromGroup (string lid, string gid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = RemoveRecipientsFromGroupWithHttpInfo(lid, gid, filter);
             return localVarResponse.Data;
        }

        /// <summary>
        /// remove all Recipients from given group remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        public ApiResponse< PatchResponse > RemoveRecipientsFromGroupWithHttpInfo (string lid, string gid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->RemoveRecipientsFromGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->RemoveRecipientsFromGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// remove all Recipients from given group remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        public async System.Threading.Tasks.Task<PatchResponse> RemoveRecipientsFromGroupAsync (string lid, string gid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = await RemoveRecipientsFromGroupAsyncWithHttpInfo(lid, gid, filter);
             return localVarResponse.Data;

        }

        /// <summary>
        /// remove all Recipients from given group remove all Recipients from given group. Pay attention to the filter, if you forget to submit it, all recipients are getting removed from the group!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="gid">the id of the group</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PatchResponse>> RemoveRecipientsFromGroupAsyncWithHttpInfo (string lid, string gid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->RemoveRecipientsFromGroup");
            // verify the required parameter 'gid' is set
            if (gid == null)
                throw new ApiException(400, "Missing required parameter 'gid' when calling RecipientApi->RemoveRecipientsFromGroup");

            var localVarPath = "/lists/{lid}/groups/{gid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (gid != null) localVarPathParams.Add("gid", Configuration.ApiClient.ParameterToString(gid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromGroup: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromGroup: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// remove all Recipients from selected list remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        public PatchResponse RemoveRecipientsFromList (string lid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = RemoveRecipientsFromListWithHttpInfo(lid, filter);
             return localVarResponse.Data;
        }

        /// <summary>
        /// remove all Recipients from selected list remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        public ApiResponse< PatchResponse > RemoveRecipientsFromListWithHttpInfo (string lid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->RemoveRecipientsFromList");

            var localVarPath = "/lists/{lid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromList: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromList: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// remove all Recipients from selected list remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        public async System.Threading.Tasks.Task<PatchResponse> RemoveRecipientsFromListAsync (string lid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = await RemoveRecipientsFromListAsyncWithHttpInfo(lid, filter);
             return localVarResponse.Data;

        }

        /// <summary>
        /// remove all Recipients from selected list remove Recipients from selected list. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting removed!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PatchResponse>> RemoveRecipientsFromListAsyncWithHttpInfo (string lid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->RemoveRecipientsFromList");

            var localVarPath = "/lists/{lid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromList: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling RemoveRecipientsFromList: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>RecipientSubscribeResponse</returns>
        public RecipientSubscribeResponse SubscribeRecipient (string code, RecipientSubscribe recipient)
        {
             ApiResponse<RecipientSubscribeResponse> localVarResponse = SubscribeRecipientWithHttpInfo(code, recipient);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>ApiResponse of RecipientSubscribeResponse</returns>
        public ApiResponse< RecipientSubscribeResponse > SubscribeRecipientWithHttpInfo (string code, RecipientSubscribe recipient)
        {
            // verify the required parameter 'code' is set
            if (code == null)
                throw new ApiException(400, "Missing required parameter 'code' when calling RecipientApi->SubscribeRecipient");
            // verify the required parameter 'recipient' is set
            if (recipient == null)
                throw new ApiException(400, "Missing required parameter 'recipient' when calling RecipientApi->SubscribeRecipient");

            var localVarPath = "/forms/submit/{code}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (code != null) localVarPathParams.Add("code", Configuration.ApiClient.ParameterToString(code)); // path parameter
            if (recipient != null && recipient.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(recipient); // http body (model) parameter
            }
            else
            {
                localVarPostBody = recipient; // byte array
            }


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling SubscribeRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling SubscribeRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<RecipientSubscribeResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (RecipientSubscribeResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(RecipientSubscribeResponse)));
            
        }

        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>Task of RecipientSubscribeResponse</returns>
        public async System.Threading.Tasks.Task<RecipientSubscribeResponse> SubscribeRecipientAsync (string code, RecipientSubscribe recipient)
        {
             ApiResponse<RecipientSubscribeResponse> localVarResponse = await SubscribeRecipientAsyncWithHttpInfo(code, recipient);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Creates a new recipient and sends a DOI-Mail Creates a new recipient and sends the corresponding DOI-Mail attached to the previously created subscribe-form. The recipient will be available as soon as the recipient has confirmed the DOI-Mail.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="code">the subscribe-code displayed when creating a subscribe-form</param>
        /// <param name="recipient">the recipient to subscribe</param>
        /// <returns>Task of ApiResponse (RecipientSubscribeResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<RecipientSubscribeResponse>> SubscribeRecipientAsyncWithHttpInfo (string code, RecipientSubscribe recipient)
        {
            // verify the required parameter 'code' is set
            if (code == null)
                throw new ApiException(400, "Missing required parameter 'code' when calling RecipientApi->SubscribeRecipient");
            // verify the required parameter 'recipient' is set
            if (recipient == null)
                throw new ApiException(400, "Missing required parameter 'recipient' when calling RecipientApi->SubscribeRecipient");

            var localVarPath = "/forms/submit/{code}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (code != null) localVarPathParams.Add("code", Configuration.ApiClient.ParameterToString(code)); // path parameter
            if (recipient != null && recipient.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(recipient); // http body (model) parameter
            }
            else
            {
                localVarPostBody = recipient; // byte array
            }


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling SubscribeRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling SubscribeRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<RecipientSubscribeResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (RecipientSubscribeResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(RecipientSubscribeResponse)));
            
        }

        /// <summary>
        /// update the recipient. update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>PatchResponse</returns>
        public PatchResponse UpdateRecipient (string lid, string id, RecipientPatch recipient)
        {
             ApiResponse<PatchResponse> localVarResponse = UpdateRecipientWithHttpInfo(lid, id, recipient);
             return localVarResponse.Data;
        }

        /// <summary>
        /// update the recipient. update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        public ApiResponse< PatchResponse > UpdateRecipientWithHttpInfo (string lid, string id, RecipientPatch recipient)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->UpdateRecipient");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->UpdateRecipient");
            // verify the required parameter 'recipient' is set
            if (recipient == null)
                throw new ApiException(400, "Missing required parameter 'recipient' when calling RecipientApi->UpdateRecipient");

            var localVarPath = "/lists/{lid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (recipient != null && recipient.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(recipient); // http body (model) parameter
            }
            else
            {
                localVarPostBody = recipient; // byte array
            }

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.PATCH, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// update the recipient. update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>Task of PatchResponse</returns>
        public async System.Threading.Tasks.Task<PatchResponse> UpdateRecipientAsync (string lid, string id, RecipientPatch recipient)
        {
             ApiResponse<PatchResponse> localVarResponse = await UpdateRecipientAsyncWithHttpInfo(lid, id, recipient);
             return localVarResponse.Data;

        }

        /// <summary>
        /// update the recipient. update one Recipient. If a value of a custom attribute shall by changed just submit it in the json body
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="id">the id of the recipient</param>
        /// <param name="recipient">the data to update</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PatchResponse>> UpdateRecipientAsyncWithHttpInfo (string lid, string id, RecipientPatch recipient)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->UpdateRecipient");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling RecipientApi->UpdateRecipient");
            // verify the required parameter 'recipient' is set
            if (recipient == null)
                throw new ApiException(400, "Missing required parameter 'recipient' when calling RecipientApi->UpdateRecipient");

            var localVarPath = "/lists/{lid}/recipients/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (recipient != null && recipient.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(recipient); // http body (model) parameter
            }
            else
            {
                localVarPostBody = recipient; // byte array
            }

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.PATCH, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipient: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipient: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// update all Recipients in selected list Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>PatchResponse</returns>
        public PatchResponse UpdateRecipients (string lid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = UpdateRecipientsWithHttpInfo(lid, filter);
             return localVarResponse.Data;
        }

        /// <summary>
        /// update all Recipients in selected list Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>ApiResponse of PatchResponse</returns>
        public ApiResponse< PatchResponse > UpdateRecipientsWithHttpInfo (string lid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->UpdateRecipients");

            var localVarPath = "/lists/{lid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.PATCH, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipients: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipients: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

        /// <summary>
        /// update all Recipients in selected list Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of PatchResponse</returns>
        public async System.Threading.Tasks.Task<PatchResponse> UpdateRecipientsAsync (string lid, string filter = null)
        {
             ApiResponse<PatchResponse> localVarResponse = await UpdateRecipientsAsyncWithHttpInfo(lid, filter);
             return localVarResponse.Data;

        }

        /// <summary>
        /// update all Recipients in selected list Updates all recipients in the given list where the filter applies. If a value of a custom attribute shall by changed just submit it in the json body. Pay attention to the filter, if you forget to submit it, all recipients of the current list are getting updated!
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="lid">the id of the list</param>
        /// <param name="filter">a FIQL-Filter (optional)</param>
        /// <returns>Task of ApiResponse (PatchResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PatchResponse>> UpdateRecipientsAsyncWithHttpInfo (string lid, string filter = null)
        {
            // verify the required parameter 'lid' is set
            if (lid == null)
                throw new ApiException(400, "Missing required parameter 'lid' when calling RecipientApi->UpdateRecipients");

            var localVarPath = "/lists/{lid}/recipients";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (lid != null) localVarPathParams.Add("lid", Configuration.ApiClient.ParameterToString(lid)); // path parameter
            if (filter != null) localVarQueryParams.Add("_filter", Configuration.ApiClient.ParameterToString(filter)); // query parameter

            // authentication (OAuth) required
            // oauth required
            if (!String.IsNullOrEmpty(Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.PATCH, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipients: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling UpdateRecipients: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return new ApiResponse<PatchResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (PatchResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(PatchResponse)));
            
        }

    }
}
