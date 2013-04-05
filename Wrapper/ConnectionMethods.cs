 /*
	Trade Me API Wrapper
	Copyright (C) 2010	Trade Me

	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.

	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.

	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/

/*
 * The Trade Me "object" provides access to the API, as well as containing helper
 * OAuth functions. 
 * @author	Amy Chard   http://www.trademe.co.nz
 * @version	0.1
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;

namespace TradeMe.Api.Client
{
    /// <summary>
    /// The ConnectionMethods class contains the methods requried for connecting and making calls to the API.
    /// </summary>
    internal class ConnectionMethods
    {
        /// <summary>
        /// Initializes a new instance of the ConnectionMethods class.
        /// </summary>
        public ConnectionMethods()
        {
            ScopeOfRequest = "?scope=MyTradeMeRead,MyTradeMeWrite,BiddingAndBuying";

            AccessTokenKey = " ";
            AccessTokenSecret = " ";

            ConsumerKey = " ";
            ConsumerSecret = " ";

            AccessUrl = "https://secure.tmsandbox.co.nz/Oauth/AccessToken";
            BaseUrl = "https://api.tmsandbox.co.nz/v1/";
            RequestTokenUrl = "https://secure.tmsandbox.co.nz/Oauth/RequestToken";
            AuthorizeUrl = "https://secure.tmsandbox.co.nz/Oauth/Authorize";

            RequestTokenKey = string.Empty;
            RequestTokenSecret = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionMethods class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public ConnectionMethods(string consumerKey, string consumerSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionMethods class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="scopeOfRequest">The scope of the requests that can be made.</param>
        public ConnectionMethods(string consumerKey, string consumerSecret, string scopeOfRequest)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            ScopeOfRequest = scopeOfRequest;
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionMethods class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="accessToken">The access token.</param>
        public ConnectionMethods(string consumerKey, string consumerSecret, IToken accessToken)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AccessToken = accessToken;
        }

        // getters and setters

        /// <summary>
        /// Gets or sets the Access Token Key.
        /// </summary>
        /// <value>The Access Token Key.</value>
        public string AccessTokenKey { get; set; }

        /// <summary>
        /// Gets or sets the Access Token Secret.
        /// </summary>
        /// <value>The Access Token Secret.</value>
        public string AccessTokenSecret { get; set; }

        /// <summary>
        /// Gets or sets the Access Url.
        /// </summary>
        /// <value>The Access Url.</value>
        public string AccessUrl { get; set; }

        /// <summary>
        /// Gets or sets the Authorize Url.
        /// </summary>
        /// <value>The Authorize Url.</value>
        public string AuthorizeUrl { get; set; }

        /// <summary>
        /// Gets or sets the Base Url.
        /// </summary>
        /// <value>The Base Url.</value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the Consumer Key.
        /// </summary>
        /// <value>The Consumer Key.</value>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the Consumer Secret.
        /// </summary>
        /// <value>The Consumer Secret.</value>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the Request Token.
        /// </summary>
        /// <value>The Request Token.</value>
        public IToken RequestToken { get; set; }

        /// <summary>
        /// Gets or sets the Access Token - required to make an authenticated call to the API.
        /// </summary>
        /// <value>The Access Token.</value>
        public IToken AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the Request Token Key.
        /// </summary>
        /// <value>The Request Token Key.</value>
        public string RequestTokenKey { get; set; }

        /// <summary>
        /// Gets or sets the Request Token Secret.
        /// </summary>
        /// <value>The Request Token Secret.</value>
        public string RequestTokenSecret { get; set; }

        /// <summary>
        /// Gets or sets the Request Token Url.
        /// </summary>
        /// <value>The Request Token Url.</value>
        public string RequestTokenUrl { get; set; }

        /// <summary>
        /// Gets or sets the ScopeOfRequest. Can be MyTradeMeWrite, MyTradeMeRead or BiddingAndBuying to access the calls appropriate 
        /// for each permission level. If requiring more than one permission set, separate each with commas.
        /// </summary>
        /// <value>The scope of the request.</value>
        public string ScopeOfRequest { get; set; }

        // unauthenticated connection method:

        /// <summary>
        /// Creates a connection with no authorization headers - used for requests that don't require authentication.
        /// This method does not actually perform the request - it returns the object to make the request with.
        /// </summary>
        /// <param name="query">The url to connect to.</param>
        /// <returns>IConsumerRequest.</returns>
        public IConsumerRequest UnauthenticatedConnection(string query)
        {
            var url = BaseUrl + query;
            if (AccessToken != null)
            {
                return AuthenticatedQuery(query);
            } 

            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = " ",
                ConsumerSecret = " ",
                SignatureMethod = DevDefined.OAuth.Framework.SignatureMethod.PlainText,
                UseHeaderForOAuthParameters = false
            };

            var consumerSession = new OAuthSession(consumerContext, RequestTokenUrl, AuthorizeUrl, AccessUrl);

            var getRequest = consumerSession
                .Request()
                .ForMethod(Constants.GET)
                .ForUri(new Uri(url));

            return getRequest;
        }

        // authenticated connection methods:

        /// <summary>
        /// Performs the first step in the authorisation handshake. It requests a verification code from the TradeMeApi.
        /// </summary>
        /// <returns>String The link to location to get the verification code from.</returns>
        public string GetVerificationCode()
        {
            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                SignatureMethod = DevDefined.OAuth.Framework.SignatureMethod.HmacSha1,
                UseHeaderForOAuthParameters = true
            };
            var session = new OAuthSession(
                consumerContext,
                RequestTokenUrl + ScopeOfRequest,
                AuthorizeUrl,
                AccessUrl);

            RequestToken = session.GetRequestToken();

            // link to website
            var link = session.GetUserAuthorizationUrlForToken(RequestToken);
            return link;
        }

        /// <summary>
        /// <para>This is the second and final step in the authorisation process. It uses the verification code (retrieved in public string GetVerificationCode()).
        /// </para><para>It exchanges the verification code for an access token.</para>
        /// <para>Once this step has been performed the user will be enabled to perform authenticated requests.</para>
        /// </summary>
        /// <param name="code">The verification code.</param>
        public void AuthenticateWithVerificationCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new NoVerificationCodeException();
            }

            code = code.Trim();

            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1,
                UseHeaderForOAuthParameters = true
            };

            var session = new OAuthSession(
                consumerContext,
                RequestTokenUrl + ScopeOfRequest,
                AuthorizeUrl,
                AccessUrl);

            AccessToken = session.ExchangeRequestTokenForAccessToken(RequestToken, code);
        }

        /// <summary>
        /// <para>Performs the query specified in the "query" string to perform a request that requires authorization.
        /// </para><para>This method requires that either an accessToken has been set manually or 
        /// that the GetVerificationCode() and AuthenticateWithVerificationCode(string code) methods have been called previously.
        /// </para><para>It returns the IConsumerRequest fully enabled to make the request specified in the "query" string.</para>
        /// </summary>
        /// <param name="query">The query string that will be added to the url and used to connect to the API with.</param>
        /// <returns>IConsumerRequest.</returns>
        public IConsumerRequest AuthenticatedQuery(string query)
        {
            var url = BaseUrl + query;

            if (AccessToken == null)
            {
                throw new AccessTokenIsNullException();
            }

            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1,
                UseHeaderForOAuthParameters = true
            };

            var consumerSession = new OAuthSession(consumerContext, RequestTokenUrl + ScopeOfRequest, AuthorizeUrl, AccessUrl) { AccessToken = AccessToken };

            var getRequest = consumerSession
                .Request()
                .ForMethod(Constants.GET)
                .ForUri(new Uri(url))
                .SignWithToken(AccessToken);

            return getRequest;
        }

        // Post Method:

        /// <summary>
        /// <para>It serializes the toSend object into xml and sends the post message specified in the "to" string using an authorized connection. 
        /// It returns the response from the server as an XDocument.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="toSend"> The object that will be serialized into xml and sent.</param>
        /// <param name="to">The url the post message will be sent to.</param>
        /// <returns>XDocument.</returns>
        public XDocument Post(object toSend, string to)
        {
            return Post(toSend, to, false);
        }

        /// <summary>
        /// <para>It serializes the toSend object into xml and sends the post message specified in the "to" string using an authorized connection. 
        /// It returns the response from the server as an XDocument.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="toSend">The object that will be serialized into xml and then sent in the post message.</param>
        /// <param name="to">The query string that will be added to the base url and used to connect to the API.</param>
        /// <param name="delete">True if you want the request to be of the method DELETE instead of POST.</param>
        /// <returns>XDocument.</returns>
        public XDocument Post(object toSend, string to, bool delete)
        {
            var url = BaseUrl + to;
            var xmlToSend = string.Empty;
            if (AccessToken == null)
            {
                throw new AccessTokenIsNullException();
            }

            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1,
                UseHeaderForOAuthParameters = true
            };

            var consumerSession = new OAuthSession(consumerContext, RequestTokenUrl + ScopeOfRequest, AuthorizeUrl, AccessUrl) { AccessToken = AccessToken };

            // convert the object into xml
            if (toSend == null)
            {
                xmlToSend = string.Empty;
            }
            else
            {
                var serializer = new XmlSerializer(toSend.GetType());

                // Create a new file stream to write the serialized object to a file
                using (TextWriter writeFileStream = new StreamWriter(@"C:\test.xml"))
                {
                    serializer.Serialize(writeFileStream, toSend);

                    // Cleanup
                    writeFileStream.Close();
                }

                xmlToSend += Environment.NewLine + System.IO.File.ReadAllText(@"C:\test.xml");
                xmlToSend = xmlToSend.TrimStart();
            }

            // send the post request
            var testReq = consumerSession.Request();

            if (delete)
            {
                testReq.Delete();
            }
            else
            {
                testReq.Post();
            }

            testReq.ForUrl(url);
            testReq.SignWithToken();

            // Here's your Authorization Header
            var oAuthHeader =
                testReq.Context.GenerateOAuthParametersForHeader();

            var address = new Uri(url);

            var webRequest = WebRequest.Create(address) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = delete ? Constants.DELETE : Constants.POST;

                webRequest.ContentType = "text/xml";

                // Set the generated Header
                webRequest.Headers.Add("Authorization", oAuthHeader);

                if (string.IsNullOrEmpty(xmlToSend))
                {
                    webRequest.ContentLength = 0;
                }
                else
                {
                    var dataAsBytes = (new UTF8Encoding()).GetBytes(xmlToSend);
                    webRequest.ContentLength = dataAsBytes.Length;

                    using (var newStream = webRequest.GetRequestStream())
                    {
                        // Send the data.
                        newStream.Write(dataAsBytes, 0, dataAsBytes.Length);
                        newStream.Close();
                    }
                }
            }

            // make the call and return the results
            if (webRequest != null)
                using (var response = webRequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream()))
                    {
                        var doc = XDocument.Parse(stream.ReadToEnd(), LoadOptions.None);

                        return doc;
                    }
                }
            return null;
        }
    }
}
