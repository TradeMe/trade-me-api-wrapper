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
using System.Xml.Linq;


namespace TradeMe.Api.Client
{
    /// <summary>
    /// The ListingMethods class contains the methods requried for making calls to the API related to Listings.
    /// </summary>
    internal class ListingMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the ListingMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public ListingMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

        // Listing Methods:

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieve the details of a single listing
        /// </para><para>using the "query" string provided - should be the  "Listings/{listing_id}.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>ListedItemDetail.</returns>
        public ListedItemDetail ListingDetail(string query)
        {
            var url = _connection.BaseUrl + query;
            return ListedItemDetailConnectionHelper(url);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieve the details of a single listing.
        /// </para><para>Creates a query string using the listing id parameter provided.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="id">The listing id.</param>
        /// <returns>ListedItemDetail.</returns>
        public ListedItemDetail ListingDetailById(string id)
        {
            var url = String.Format(Constants.Culture, "{0}{1}/{2}{3}", _connection.BaseUrl, Constants.LISTINGS, id, Constants.XML);
            return this.ListedItemDetailConnectionHelper(url);
        }

        // methods that DO require authentication:

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieve a list of unanswered questions for all listings belonging to a member,
        /// Retrieve a list of unanswered questions on a single listing,
        /// </para><para>
        /// using the "query" string provided - should be the  "Listings/questions/unansweredquestions.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>Questions.</returns>
        public Questions UnansweredQuestionsByQueryString(string query)
        {
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<Questions>.Deserialize(new Questions(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieve a list of unanswered questions for all listings belonging to a member,
        /// Retrieve a list of unanswered questions on a single listing.
        /// </para><para>
        /// Creates a query string using the id parameter provided - the parameter should be null if the request is for all listings belonging to a member.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The listing id.</param>
        /// <returns>Questions.</returns>
        public Questions UnansweredQuestionsById(string listingId)
        {
            var query = Constants.LISTINGS;
            if (!string.IsNullOrEmpty(listingId))
            {
                query += "/" + listingId;
            }

            query += String.Format(Constants.Culture, "/{0}/unansweredquestions{1}", Constants.QUESTIONS, Constants.XML);

            return this.UnansweredQuestionsByQueryString(query);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieves a list of all the unanswered questions for all live auctions of the authenticated user. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>Questions.</returns>
        public Questions UnansweredQuestionsAll()
        {

            var url = String.Format(Constants.Culture, "{0}/questions/unansweredquestions{1}", Constants.LISTINGS, Constants.XML);

            var getRequest = _connection.AuthenticatedQuery(url);
            var xml = getRequest.ToString();

            return Deserializer<Questions>.Deserialize(new Questions(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Provide an answer for a specific question. POST
        /// </para><para>
        /// Serializes the given ListingAnswerQuestion into xml.
        /// Creates a query string using the parameters provided.
        /// </para><para>
        /// All the parameters are required.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="answerQuestion">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <param name="listingId">The id of the listing that has the question being answered.</param>
        /// <param name="questionId">The id of the question being answered.</param>
        /// <returns>XDocument.</returns>
        public XDocument AnswerSpecificQuestion(ListingAnswerQuestion answerQuestion, string listingId, string questionId)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}/{2}/{3}/answerquestion{4}", Constants.LISTINGS, listingId, Constants.QUESTIONS, questionId, Constants.XML);
            return _connection.Post(answerQuestion, query);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Post a comment on a listing.
        /// </para><para>
        /// Serializes the given ListingAddComment into xml.
        /// Creates a query string using the listingId provided .
        /// </para><para>
        /// All the parameters are required.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="addComment">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <param name="listingId">The id of the listing the comment should be added to.</param>
        /// <returns>XDocument.</returns>
        public XDocument CommentOnListing(ListingAddComment addComment, string listingId)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}/addcomment{2}", Constants.LISTINGS, listingId, Constants.XML);
            return _connection.Post(addComment, query);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Send an email to the seller of a classified.
        /// </para><para>
        /// Serializes the given EmailRequest into xml.
        /// Creates a query string using the listingId provided .
        /// </para><para>
        /// All the parameters are required.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="emailRequest">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <param name="listingId">The id of the listing that youwant to email about.</param>
        /// <returns>XDocument.</returns>
        public XDocument EmailSellerOfClassified(EmailRequest emailRequest, string listingId)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}/emailseller{2}", Constants.LISTINGS, listingId, Constants.XML);
            return _connection.Post(emailRequest, query);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Send an email regarding an auction to a friend.
        /// </para><para>
        /// Serializes the given EmailFriendRequest into xml.
        /// Creates a query string using the listingId provided .
        /// </para><para>
        /// All the parameters are required.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="email">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <param name="listingId">The id of the listing you want to email about.</param>
        /// <returns>XDocument.</returns>
        public XDocument EmailFriend(EmailFriendRequest email, string listingId)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}/emailfriend{2}", Constants.LISTINGS, listingId, Constants.XML);
            return _connection.Post(email, query);
        }

        /// <summary>
        /// A helper method for performing the http request for ListedItemDetail requests.
        /// </summary>
        /// <param name="url">The url used to connect to the API.</param>
        /// <returns>ListedItemDetail.</returns>
        private ListedItemDetail ListedItemDetailConnectionHelper(string url)
        {
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<ListedItemDetail>.Deserialize(new ListedItemDetail(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieves the featured listings for the whole country or for a single region. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>FeaturedListings.</returns>
        public FeaturedListings GetFeaturedListings()
        {
            var url = String.Format(Constants.Culture, "{0}/Featured{1}", Constants.LISTINGS, Constants.XML);
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<FeaturedListings>.Deserialize(new FeaturedListings(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieves the hot items for the whole country or for a single region. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>HotListings.</returns>
        public HotListings GetHotListings()
        {
            var url = String.Format(Constants.Culture, "{0}/Hot{1}", Constants.LISTINGS, Constants.XML);
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<HotListings>.Deserialize(new HotListings(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieves the latest listed items for the whole country or for a single region. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>LatestListings.</returns>
        public LatestListings GetLatestListings()
        {
            var url = String.Format(Constants.Culture, "{0}/Latest{1}", Constants.LISTINGS, Constants.XML);
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<LatestListings>.Deserialize(new LatestListings(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieves the items closing soon for the whole country or for a single region. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>ClosingSoonListings.</returns>
        public ClosingSoonListings GetClosingSoonListings()
        {
            var url = String.Format(Constants.Culture, "{0}/closing{1}", Constants.LISTINGS, Constants.XML);
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<ClosingSoonListings>.Deserialize(new ClosingSoonListings(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Retrieves the items with one dollar reserves for the whole country or for a single region. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>OneDollarListings.</returns>
        public OneDollarListings GetOneDollarListings()
        {
            var url = String.Format(Constants.Culture, "{0}/oneDollar{1}", Constants.LISTINGS, Constants.XML);
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<OneDollarListings>.Deserialize(new OneDollarListings(), xml);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Sends a complaint about a listing to the Trade Me customer service team.
        /// </para><para>
        /// Serializes the given ComplaintRequest into xml.
        /// Creates a query string using the listingId provided .
        /// </para><para>
        /// All the parameters are required.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="complaintRequest">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <param name="listingId">The id of the listing that youwant to complain about.</param>
        /// <returns>XDocument.</returns>
        public XDocument SendComplaint(ComplaintRequest complaintRequest, string listingId)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}/sendcomplaint{2}", Constants.LISTINGS, listingId, Constants.XML);
            return _connection.Post(complaintRequest, query);
        }
    }
}
