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

namespace TradeMe.Api.Client
{
    /// <summary>
    /// The MembershipMethods class contains the methods requried for making calls to the API related to memberships.
    /// </summary>
    internal class MembershipMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the MembershipMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public MembershipMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

        /// <summary>
        /// <para>Performs the membership method:
        /// Retrieve a member’s profile data
        /// using the "query" string provided - should be the  "Member/{member_id}/Profile.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>MemberProfile.</returns>
        public MemberProfile MemberProfileByQueryString(string query)
        {
            var url = _connection.BaseUrl + query;
            return this.MemberProfileHelper(url);
        }

        /// <summary>
        /// <para>Performs the membership method:
        /// Retrieve a member’s profile data.
        /// Creates a query string using the member id provided.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="id">The members id.</param>
        /// <returns>MemberProfile.</returns>
        public MemberProfile MemberProfileById(string id)
        {
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/Profile{3}", _connection.BaseUrl, Constants.MEMBER, id, Constants.XML);
            return this.MemberProfileHelper(url);
        }

        /// <summary>
        /// <para>Performs the membership method:
        /// Retrieve member feedback summary (counts)
        /// using the "query" string provided - should be the  "Search/Member/{member_id}/FeedbackCount.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>FeedbackCount.</returns>
        public FeedbackCount MemberFeedbackCountByQueryString(string query)
        {
            var url = _connection.BaseUrl + query;
            return this.FeedbackCountConnectionHelper(url);
        }

        /// <summary>
        /// <para>Performs the membership method:
        /// Retrieve member feedback summary (counts).
        /// </para><para>
        /// Creates a query string using the member id provided.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="id">The members id.</param>
        /// <returns>FeedbackCount.</returns>
        public FeedbackCount MemberFeedbackCountById(string id)
        {
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/FeedbackCount{3}", _connection.BaseUrl, Constants.MEMBER, id, Constants.XML);
            return this.FeedbackCountConnectionHelper(url);
        }

        /// <summary>
        /// <para>Performs the Membership Method:
        /// Retrieve feedback for a member
        /// </para><para>using the "query" string provided - should be the  "Search/Member/{member_id}/Feedback.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Feedback.</returns>
        public Feedback MemberFeedbackByQueryString(string query)
        {
            var url = _connection.BaseUrl + query;
            return this.FeedbackConnectionHelper(url);
        }

        /// <summary>
        /// <para>Performs the Membership Method:
        /// Retrieve feedback for a member.
        /// </para><para>Creates a query string using the parameters provided - the criteria parameter can be null if it is not required for the request. 
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="id">The members id.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Feedback.</returns>
        public Feedback MemberFeedbackById(string id, MemberFeedbackCriteria criteria)
        {
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/Feedback{3}", _connection.BaseUrl, Constants.MEMBER, id, Constants.XML);

            if (string.IsNullOrEmpty(string.Empty + criteria))
            {
                url += Constants.XML;
            }
            else
            {
                url += criteria + Constants.XML;
            }

            return this.FeedbackConnectionHelper(url);
        }

        /// <summary>
        /// <para>Performs the Membership Method:
        /// Retrieve member ID
        /// </para><para>using the "query" string provided - should be the  "Member/{member_nickname}.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/". 
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>MemberId.</returns>
        public MemberId MemberId(string query)
        {
            var url = _connection.BaseUrl + query;
            return this.MemberIdConnectionHelper(url);
        }

        /// <summary>
        /// <para>Performs the Membership Method:
        /// Retrieve member ID.
        /// </para><para>Creates a query string using the nickname parameter provided
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="nickname">The members nickname.</param>
        /// <returns>MemberId.</returns>
        public MemberId MemberIdByNickname(string nickname)
        {
            var url = String.Format(Constants.Culture, "{0}{1}/{2}{3}", _connection.BaseUrl, Constants.MEMBER, nickname, Constants.XML);
            return this.MemberIdConnectionHelper(url);
        }

        /// <summary>
        /// <para>Performs the Membership Method:
        /// Retrieve a member’s listings. GET
        /// </para><para>using the "query" string provided - should be the  "Member/{member_id}/Listings.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Listings.</returns>
        public Listings MemberListings(string query)
        {
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<Listings>.Deserialize(new Listings(), xml);
        }

        /// <summary>
        /// <para>Performs the Membership Method:
        /// Retrieve a member’s listings.
        /// </para><para>Creates a query string using the parameters provided.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="id">The members id.</param>
        /// <returns>Listings.</returns>
        public Listings MemberListingsById(string id)
        {
            var url = String.Format(Constants.Culture, "{0}/{1}/Listings", Constants.MEMBER, id);
            url += Constants.XML;
            return this.MemberListings(url);
        }

        /// <summary>
        /// <para>A helper method for performing the http request for feedback requests.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="url">The url used to connect to the API.</param>
        /// <returns>Feedback.</returns>
        private Feedback FeedbackConnectionHelper(string url)
        {
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<Feedback>.Deserialize(new Feedback(), xml);
        }

        /// <summary>
        /// A helper method for performing the http request for feedback count requests.
        /// </summary>
        /// <param name="url">The url used to connect to the API.</param>
        /// <returns>FeedbackCount.</returns>
        private FeedbackCount FeedbackCountConnectionHelper(string url)
        {
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<FeedbackCount>.Deserialize(new FeedbackCount(), xml);
        }

        /// <summary>
        /// <para>A helper method for performing the http request for MemberId requests.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="url">The url used to conect to the API.</param>
        /// <returns>MemberId.</returns>
        private MemberId MemberIdConnectionHelper(string url)
        {
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<MemberId>.Deserialize(new MemberId(), xml);
        }

        /// <summary>
        /// A helper method for performing the http request for a member profile request.
        /// </summary>
        /// <param name="url">The url used to connect to the API.</param>
        /// <returns>MemberProfile.</returns>
        private MemberProfile MemberProfileHelper(string url)
        {
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<MemberProfile>.Deserialize(new MemberProfile(), xml);
        }
    }
}
