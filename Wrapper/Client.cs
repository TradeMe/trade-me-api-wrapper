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
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;

namespace TradeMe.Api.Client
{
    /// <summary>
    /// A Wrapper for calling the Trade Me API.
    /// </summary>
    public class Client
    {
        // culture information
/*
        private static CultureInfo _culture = new CultureInfo("en-US");
*/
        private readonly ConnectionMethods _connection;
        private CatalogueMethods _catalogue;
        private BiddingMethods _bidding;
        private FavouriteMethods _favourites;
        private MembershipMethods _membership;
        private MyTradeMeMethods _myTradeMe;
        private PhotoMethods _photo;
        private SearchMethods _search;
        private ListingMethods _listing;
        private FixedPriceOfferMethods _fixedPrice;
        private SellingMethods _selling;
        /// <summary>
        /// Initializes a new instance of the Client class.
        /// </summary>
        public Client()
        {
            _connection = new ConnectionMethods();
        }

        /// <summary>
        /// Initializes a new instance of the Client class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public Client(string consumerKey, string consumerSecret)
        {
            _connection = new ConnectionMethods(consumerKey, consumerSecret);
        }

        /// <summary>
        /// Initializes a new instance of the Client class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="scopeOfRequest">The scope of the requests that can be made.</param>
        public Client(string consumerKey, string consumerSecret, string scopeOfRequest)
        {
            _connection = new ConnectionMethods(consumerKey, consumerSecret, scopeOfRequest);
        }

        /// <summary>
        /// Initializes a new instance of the Client class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="accessToken">The access token.</param>
        public Client(string consumerKey, string consumerSecret, IToken accessToken)
        {
            _connection = new ConnectionMethods(consumerKey, consumerSecret, accessToken);
        }

        // getters and setters

        /// <summary>
        /// Gets or sets the Access Token Key.
        /// </summary>
        /// <value>The Access Token Key.</value>
        public string AccessTokenKey
        {
            get { return _connection.AccessTokenKey; }
            set { _connection.AccessTokenKey = value; }
        }

        /// <summary>
        /// Gets or sets the Access Token Secret.
        /// </summary>
        /// <value>The Access Token Secret.</value>
        public string AccessTokenSecret
        {
            get { return _connection.AccessTokenSecret; }
            set { _connection.AccessTokenSecret = value; }
        }

        /// <summary>
        /// Gets or sets the Access Url.
        /// </summary>
        /// <value>The Access Url.</value>
        public string AccessUrl
        {
            get { return _connection.AccessUrl; }
            set { _connection.AccessUrl = value; }
        }

        /// <summary>
        /// Gets or sets the Authorize Url.
        /// </summary>
        /// <value>The Authorize Url.</value>
        public string AuthorizeUrl
        {
            get { return _connection.AuthorizeUrl; }
            set { _connection.AuthorizeUrl = value; }
        }

        /// <summary>
        /// Gets or sets the Base Url.
        /// </summary>
        /// <value>The Base Url.</value>
        public string BaseUrl
        {
            get { return _connection.BaseUrl; }
            set { _connection.BaseUrl = value; }
        }

        /// <summary>
        /// Gets or sets the Consumer Key.
        /// </summary>
        /// <value>The Consumer Key.</value>
        public string ConsumerKey
        {
            get { return _connection.ConsumerKey; }
            set { _connection.ConsumerKey = value; }
        }

        /// <summary>
        /// Gets or sets the Consumer Secret.
        /// </summary>
        /// <value>The Consumer Secret.</value>
        public string ConsumerSecret
        {
            get { return _connection.ConsumerSecret; }
            set { _connection.ConsumerSecret = value; }
        }

        /// <summary>
        /// Gets or sets the Request Token.
        /// </summary>
        /// <value>The Request Token.</value>
        public IToken RequestToken
        {
            get { return _connection.RequestToken; }
            set { _connection.RequestToken = value; }
        }

        /// <summary>
        /// Gets or sets the Access Token - required to make an authenticated call to the API.
        /// </summary>
        /// <value>The Access Token.</value>
        public IToken AccessToken
        {
            get { return _connection.AccessToken; }
            set { _connection.AccessToken = value; }
        }

        /// <summary>
        /// Gets or sets the Request Token Key.
        /// </summary>
        /// <value>The Request Token Key.</value>
        public string RequestTokenKey
        {
            get { return _connection.RequestTokenKey; }
            set { _connection.RequestTokenKey = value; }
        }

        /// <summary>
        /// Gets or sets the Request Token Secret.
        /// </summary>
        /// <value>The Request Token Secret.</value>
        public string RequestTokenSecret
        {
            get { return _connection.RequestTokenSecret; }
            set { _connection.RequestTokenSecret = value; }
        }

        /// <summary>
        /// Gets or sets the Request Token Url.
        /// </summary>
        /// <value>The Request Token Url.</value>
        public string RequestTokenUrl
        {
            get { return _connection.RequestTokenUrl; }
            set { _connection.RequestTokenUrl = value; }
        }

        /// <summary>
        /// Gets or sets the ScopeOfRequest. Can be MyTradeMeWrite, MyTradeMeRead or BiddingAndBuying to access the calls appropriate 
        /// for each permission level. If requiring more than one permission set, separate each with commas.
        /// </summary>
        /// <value>The scope of the request.</value>
        public string ScopeOfRequest
        {
            get { return _connection.ScopeOfRequest; }
            set { _connection.ScopeOfRequest = value; }
        }

        // authenticated connection methods:

        /// <summary>
        /// Performs the first step in the authorisation handshake. It requests a verification code from the TradeMeApi.
        /// </summary>
        /// <returns>String The link to location to get the verification code from.</returns>
        public string GetVerificationCode()
        {
            return _connection.GetVerificationCode();
        }

        /// <summary>
        /// <para>This is the second and final step in the authorisation process. It uses the verification code (retrieved in public string GetVerificationCode()).
        /// </para><para>It exchanges the verification code for an access token.</para>
        /// <para>Once this step has been performed the user will be enabled to perform authenticated requests.</para>
        /// </summary>
        /// <param name="code">The verification code.</param>
        public void AuthenticateWithVerificationCode(string code)
        {
            _connection.AuthenticateWithVerificationCode(code);
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
            return _connection.AuthenticatedQuery(query);
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
            return _connection.Post(toSend, to, false);
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
            return _connection.Post(toSend, to, delete);
        }

        // Methods that DO NOT require authentication:

        // Catalogue Methods:

        // Categories:

        /// <summary>
        /// <para>Performs the category methods:
        /// Retrieve a list of all or part of our category tree,
        /// Retrieve a list of Trade Me Motors used car categories,
        /// Retrieve a list of Trade Me Motors motorbike categories,
        /// using the "query" string provided - should be the  "Categories/UsedCars.xml" part of the url
        /// it shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Category.</returns>
        public Category RetrieveCategoriesByQueryString(string query)
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveCategoriesByQueryString(query);
        }

        /// <summary>
        /// <para>Performs the category methods:
        /// Retrieve a list of all or part of our category tree.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>Category.</returns>
        public Category RetrieveCategories()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveCategories();
        }

        /// <summary>
        /// <para>Performs the category methods:
        /// Retrieve a list of all or part of our category tree.
        /// Does this by using the id string provided - should be the id of the category you wish to retrieve.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="id">The id of the category you wish to retrieve.</param>
        /// <returns>Category.</returns>
        public Category RetrieveCategoriesById(string id)
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveCategoriesById(id);
        }

        /// <summary>
        /// <para>Performs the category methods:
        /// Retrieve a list of Trade Me Motors used car categories.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>Category.</returns>
        public Category RetrieveUsedCarCategories()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveUsedCarCategories();
        }

        /// <summary>
        /// <para>Performs the category methods:
        /// Retrieve a list of Trade Me Motors motorbike categories.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>Category.</returns>
        public Category RetrieveMotorBikeCategories()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveMotorBikeCategories();
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Retrieve a list of Trade Me Jobs categories.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>JobCategories.</returns>
        public JobCategories RetrieveJobCategories()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveJobCategories();
        }

        // Localites:

        /// <summary>
        /// <para>Performs the Localities methods:
        /// three-tier locality dataset,
        /// two-tier locality dataset,
        /// </para><para>
        /// using the "query" string provided - should be the  "Localities.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Localities.</returns>
        public LocalityCollection RetrieveLocalities(string query)
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveLocalities(query);
        }

        /// <summary>
        /// <para>Performs the Localities methods:
        /// three-tier locality dataset
        /// </para><para>
        /// Returns three-tier locality hierarchy of regions, districts and their respective suburbs.
        /// These values are used in Trade Me Property, Trade Me Jobs and Services.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>LocalityCollection.</returns>
        public LocalityCollection RetrieveLocalitiesThreeTier()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveLocalitiesThreeTier();
        }

        /// <summary>
        /// <para>Performs the Localities method:
        /// two-tier locality dataset.
        /// </para><para>
        /// Returns list of towns used in member registration. 
        /// This information is displayed on member profile as “suburb” and
        /// also on listings where the approximate location of goods is important,
        /// such as in Trade Me Motors.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>LocalityCollection.</returns>
        public LocalityCollection RetrieveLocalitiesTwoTier()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveLocalitiesTwoTier();
        }


        /// <summary>
        /// <para>Performs the category method:
        /// Retrieves the list of attributes which are applicable to a specific category.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="categoryNumber">The category number for which you wish to see the attributes for. 
        /// This must be a leaf category (for example, you cannot view the attributes for Computers > Desktops, 
        /// but you can for Computers > Desktops > CRT monitors).</param>
        /// <returns>Attributes</returns>
        public Attributes RetrieveAttributesForCategory(string categoryNumber)
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveAttributesForCategory(categoryNumber);
        }


        /// <summary>
        /// <para>Performs the category method:
        /// Retrieves the legal notice that the user is required to agree to before listing. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="categoryNumber">The category number for which you wish to see the legal notice for. 
        /// The number of the category to retrieve the legal notice for. This must be a leaf category 
        /// (for example, you cannot view the legal notice for Business, Farming and Industry, but you can 
        /// for Business, Farming and Industry > Carbon credits).</param>
        /// <returns>LegalNotice</returns>
        public LegalNotice RetrieveLegalNoticeForCategory(string categoryNumber)
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveLegalNoticeForCategory(categoryNumber);
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Retrieves the default duration and the duration options that are available for listings in a specific category. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="categoryNumber">The number of the category to retrieve durations for.</param>
        /// <returns>ListingDurations</returns>
        public ListingDurations RetrieveDurationOptionsForCategory(string categoryNumber)
        {
           if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveDurationOptionsForCategory(categoryNumber);
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Retrieves a list of fees for a specific category. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="categoryNumber">The number of the category to retrieve fees for.</param>
        /// <returns>ListingFees</returns>
        public ListingFees RetrieveFeesForCategory(string categoryNumber)
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveFeesForCategory(categoryNumber);
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Returns a list of districts and suburbs as used by TradeMe travel. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>LocalityCollection</returns>
        public LocalityCollection RetrieveTravelLocalities()
        {
           if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveTravelLocalities();
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Returns a list of DVD catalogue ID numbers. You can use this list to validate the catalogue ID before listing a DVD. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>DvdValidations</returns>
        public DvdValidations RetrieveDvdIds()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveDvdIds();
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Returns a the list of Blu-ray catalogue ID numbers. You can use this list to validate the catalogue ID before listing a Blu-ray. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>BluRayValidations</returns>
        public BluRayValidations RetrieveBluRayIds()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.RetrieveBluRayIds();
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Searches the Trade Me Blu-ray catalogue for movie titles. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="search">The partial title to search for.</param>
        /// <returns>MovieTitles</returns>
        public MovieTitles SearchBluRayCatalog(String search = "")
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.SearchBluRayCatalog(search);
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Searches the Trade Me DVD catalogue for movie titles. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="search">The partial title to search for.</param>
        /// <returns>MovieTitles</returns>
        public MovieTitles SearchDvdCatalog(String search = "")
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.SearchDvdCatalog(search);
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Retrieves a list of possible complaint subjects. This is used in conjunction with the send a complaint API. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>ComplaintSubject</returns>
        public ComplaintSubjectCollection GetComplaintSubjects()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.GetComplaintSubjects();
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Retrieves a list of all motorbike makes. Can be used when searching. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <returns>MotorbikeMakeCollection</returns>
        public MotorbikeMakeCollection GetMotorbikeMakes()
        {
            if (_catalogue == null)
            {
                _catalogue = new CatalogueMethods(_connection);
            }

            return _catalogue.GetMotorbikeMakes();
        }


        // Search Methods:

        // general search

        /// <summary>
        /// <para>Performs Search method:
        /// Search General
        /// using the "query" string provided - should be the  "Search/General.xml?search_string=Playstation" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>SearchResults.</returns>
        public SearchResults SearchGeneral(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchGeneral(query);
        }

        /// <summary>
        /// <para>Performs Search method:
        /// Search General
        /// using the query paramaters provided it will construct a query string for you - can use null if the parameter is not required for your request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="category">Specifies the category in which you want to perform the search.</param>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="userRegion">Restricts search results to items from sellers located in the specified region.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="buy">Return only listings with BuyNow price.</param>
        /// <param name="pay">Return only listings with PayNow.</param>
        /// <param name="condition">Filter listings by condition.</param>
        /// <param name="dateFrom">Return only listings started from this date.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">	Number of rows per page.</param>
        /// <param name="memberListing">Returns only listing from specified member ID.</param>
        /// <returns>SearchResults.</returns>
        public SearchResults SearchGeneral(
            string category,
            string searchString,
            int? userRegion,
            SortOrder sortOrder,
            bool? buy,
            bool? pay,
            Condition condition,
            DateTime dateFrom,
            int? page,
            int? rows,
            int? memberListing)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchGeneral(category, searchString, userRegion, sortOrder, buy, pay, condition, dateFrom, page, rows, memberListing);
        }

        // motor boat search

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search Motor Boats</para>
        /// <para>using the "query" string provided - should be the  &quot;Search/Motors/Boats.xml?length_max=5000&amp;type=inflatable&quot; part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>MotorBoats.</returns>
        public MotorBoats SearchMotorBoats(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchMotorBoats(query);
        }

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search Motor Boats
        /// using the parameters provided - can use null if the parameter is not required.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="priceMin">Minimum price.</param>
        /// <param name="priceMax">Maximum price.</param>
        /// <param name="type">Type of the Motor Boat.</param>
        /// <param name="lengthMin">Minimum length of a boat in metres.</param>
        /// <param name="lengthMax">Maximum length of a boat in metres.</param>
        /// <param name="dateFrom">Return only listings started from this date.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>MotorBoats.</returns>
        public MotorBoats SearchMotorBoats(string searchString, SortOrder sortOrder, decimal priceMin, decimal priceMax, BoatType type, int? lengthMin, int? lengthMax, DateTime dateFrom, int? page, int? rows)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchMotorBoats(searchString, sortOrder, priceMin, priceMax, type, lengthMin, lengthMax, dateFrom, page, rows);
        }

        // motor bike search

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search Motor Bikes
        /// using the "query" string provided - should be the  "Search/Motors/Bikes.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>MotorBikes.</returns>
        public MotorBikes SearchMotorBikes(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchMotorBikes(query);
        }

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search Motor Bikes.
        /// It creates the query string using the paramaters - can be null if the parameter is not required for the request. 
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="priceMin">Minimum price.</param>
        /// <param name="priceMax">Maximum price.</param>
        /// <param name="make">	Motorbike make.</param>
        /// <param name="type">Type of the Motor Bike.</param>
        /// <param name="yearMin">	Minimum year of manufacture.</param>
        /// <param name="yearMax">Maximum year of manufacture.</param>
        /// <param name="energySizeMin">Minimum engine size in cubic centimetres (e.g. 2000 for 2 litre engine).</param>
        /// <param name="energySizeMax">Maximum engine size.</param>
        /// <param name="dateFrom">Return only listings started from this date.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>MotorBikes.</returns>
        public MotorBikes SearchMotorBikes(
            string searchString,
            SortOrder sortOrder,
            decimal priceMin,
            decimal priceMax,
            string make,
            BikeType type,
            int? yearMin,
            int? yearMax,
            int? energySizeMin,
            int? energySizeMax,
            DateTime dateFrom,
            int? page,
            int? rows)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchMotorBikes(searchString, sortOrder, priceMin, priceMax, make, type, yearMin, yearMax, energySizeMin, energySizeMax, dateFrom, page, rows);
        }

        // car search

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search Used Motors
        /// using the "query" string provided - should be the  "Search/Motors/Used.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Cars.</returns>
        public Cars SearchUsedMotors(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchUsedMotors(query);
        }

        // car search

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search Used Motors.
        /// Creates a query string based on the parameters provided, can be null if the parameter is not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="userRegion">Restricts search results to items from sellers located in the specified region.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="priceMin">Minimum price.</param>
        /// <param name="priceMax">Maximum price.</param>
        /// <param name="make">Car make.</param>
        /// <param name="model">Car model.</param>
        /// <param name="bodyStyle">Car body style.</param>
        /// <param name="doorsMin">Minimum number of doors (range from 2 to 5).</param>
        /// <param name="doorsMax">Maximum number of doors. </param>
        /// <param name="transmission">Transmission type.</param>
        /// <param name="yearMax">Maximum year of manufacture.</param>
        /// <param name="yearMin">Minimum year of manufacture.</param>
        /// <param name="energySizeMin">Minimum engine size in cubic centimetres (e.g. 2000 for 2 litre engine). </param>
        /// <param name="energySizeMax">Maximum engine size.</param>
        /// <param name="odometerMin">	Minimum odometer value in kilometres.</param>
        /// <param name="odometerMax">Maximum odometer value.</param>
        /// <param name="listingType">Type of listing.</param>
        /// <param name="dateFrom">Return only listings started from this date.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>Cars.</returns>
        public Cars SearchUsedMotors(
            string searchString,
            int? userRegion,
            SortOrder sortOrder,
            decimal priceMin,
            decimal priceMax,
            string make,
            string model,
            BodyStyle bodyStyle,
            int? doorsMin,
            int? doorsMax,
            Transmission transmission,
            int? yearMax,
            int? yearMin,
            int? energySizeMin,
            int? energySizeMax,
            int? odometerMin,
            int? odometerMax,
            ListingType listingType,
            DateTime dateFrom,
            int? page,
            int? rows)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchUsedMotors(searchString, userRegion, sortOrder, priceMin, priceMax, make, model, bodyStyle, doorsMin, doorsMax, transmission, yearMax, yearMin, energySizeMin, energySizeMax, odometerMin, odometerMax, listingType, dateFrom, page, rows);
        }

        // flatmate search

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search for Flatmate
        /// using the "query" string provided - should be the  "Search/Flatmates.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>Flatmates.</returns>
        public Flatmates SearchFlatmates(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchFlatmates(query);
        }

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search for Flatmate
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <returns>Flatmates.</returns>
        public Flatmates SearchFlatmates(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchFlatmates(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Retirement Villages
        /// using the "query" string provided - should be the  "Search/Retirement.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>RetirementVillages.</returns>
        public RetirementVillages SearchRetirementVillages(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchRetirementVillages(query);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Retirement Villages.
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="bathroomsMin">Minimum number of bathrooms..</param>
        /// <param name="bathroomsMax">Maximum number of bathrooms.</param>
        /// <param name="areaMin">	Minimum floor area in square meters.</param>
        /// <param name="areaMax">	Maximum square area in square meters.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="propertyType">The property type.</param>
        /// <param name="bedroomsMin">Minimum number of bedrooms.</param>
        /// <param name="bedroomsMax">Maximum number of bedrooms.</param>
        /// <returns>RetirementVillages.</returns>
        public RetirementVillages SearchRetirementVillages(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? bathroomsMin,
            int? bathroomsMax,
            int? areaMin,
            int? areaMax,
            int? landAreaMin,
            int? landAreaMax,
            RetirementVillagePropertyType propertyType,
            int? bedroomsMin,
            int? bedroomsMax)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchRetirementVillages(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, bathroomsMin, bathroomsMax, areaMin, areaMax, landAreaMin, landAreaMax, propertyType, bedroomsMin, bedroomsMax);
        }

        // general property search

        /// <summary>
        /// <para>Performs the Search Methods:
        /// Search Residential Property,
        /// Search Residential Rental Property,
        /// Residential Open Homes,
        /// Search Commercial Property,
        /// Search Commercial Lease Property,
        /// Search Rural Property,
        /// Search Lifestyle Property,
        /// Search Retirement Villages,
        /// </para><para>
        /// using the "query" string provided - should be the  "Search/Property/Retirement.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/". 
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchProperties(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchProperties(query);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Residential Property.
        /// </para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">	Page number.</param>
        /// <param name="rows">	Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="bathroomsMin">Minimum number of bathrooms.</param>
        /// <param name="bathroomsMax">Maximum number of bathrooms.</param>
        /// <param name="bedroomsMin">Minimum number of bedrooms.</param>
        /// <param name="bedroomsMax">Maximum number of bedrooms.</param>
        /// <param name="areaMax">Maximum square area in square meters.</param>
        /// <param name="areaMin">	Minimum floor area in square meters.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="adjacentSuburbs">Indicates whether the search should include listings in adjacent suburbs.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchResidentialProperties(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? bathroomsMin,
            int? bathroomsMax,
            int? bedroomsMin,
            int? bedroomsMax,
            int? areaMax,
            int? areaMin,
            int? landAreaMin,
            int? landAreaMax,
            PropertyType propertyType,
            bool? adjacentSuburbs)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchResidentialProperties(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, bathroomsMin, bathroomsMax, bedroomsMin, bedroomsMax, areaMax, areaMin, landAreaMin, landAreaMax, propertyType, adjacentSuburbs);
        }

        /// <summary>
        /// <para>Performs the Search Method:
        /// Search Residential Rental Property.
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="bathroomsMin">Minimum number of bathrooms.</param>
        /// <param name="bathroomsMax">Maximum number of bathrooms.</param>
        /// <param name="bedroomsMin">Minimum number of bedrooms.</param>
        /// <param name="bedroomsMax">Maximum number of bedrooms.</param>
        /// <param name="areaMax">Maximum square area in square meters.</param>
        /// <param name="areaMin">Minimum floor area in square meters.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="adjacentSuburbs">Indicates whether the search should include listings in adjacent suburbs.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchResidentialRentalProperties(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? bathroomsMin,
            int? bathroomsMax,
            int? bedroomsMin,
            int? bedroomsMax,
            int? areaMax,
            int? areaMin,
            int? landAreaMin,
            int? landAreaMax,
            PropertyType propertyType,
            bool? adjacentSuburbs)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchResidentialRentalProperties(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, bathroomsMin, bathroomsMax, bedroomsMin, bedroomsMax, areaMax, areaMin, landAreaMin, landAreaMax, propertyType, adjacentSuburbs);
        }

        /// <summary>
        /// <para>Performs the Search method:
        /// Search Residential Open Homes.
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="bathroomsMin">Minimum number of bedrooms.</param>
        /// <param name="bathroomsMax">Maximum number of bedrooms.</param>
        /// <param name="bedroomsMin">Minimum number of bathrooms.</param>
        /// <param name="bedroomsMax">Maximum number of bathrooms.</param>
        /// <param name="areaMax">Maximum square area in square meters.</param>
        /// <param name="areaMin">Minimum floor area in square meters.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="adjacentSuburbs">Indicates whether the search should include listings in adjacent suburbs.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchOpenHomeProperties(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? bathroomsMin,
            int? bathroomsMax,
            int? bedroomsMin,
            int? bedroomsMax,
            int? areaMax,
            int? areaMin,
            int? landAreaMin,
            int? landAreaMax,
            PropertyType propertyType,
            bool? adjacentSuburbs)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchOpenHomeProperties(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, bathroomsMin, bathroomsMax, bedroomsMin, bedroomsMax, areaMax, areaMin, landAreaMin, landAreaMax, propertyType, adjacentSuburbs);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Commercial Property.
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">	Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="bathroomsMin">Minimum number of bathrooms.</param>
        /// <param name="bathroomsMax">Maximum number of bathrooms.</param>
        /// <param name="bedroomsMin">Minimum number of bedrooms.</param>
        /// <param name="bedroomsMax">Maximum number of bedrooms.</param>
        /// <param name="areaMax">	Maximum floor area in square meters.</param>
        /// <param name="areaMin">Minimum floor area in square meters.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="adjacentSuburbs">Indicates whether the search should include listings in adjacent suburbs.</param>
        /// <param name="usage">The usage of the property.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchCommercialSaleProperties(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? bathroomsMin,
            int? bathroomsMax,
            int? bedroomsMin,
            int? bedroomsMax,
            int? areaMax,
            int? areaMin,
            int? landAreaMin,
            int? landAreaMax,
            bool? adjacentSuburbs,
            PropertyUsage usage)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchCommercialSaleProperties(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, bathroomsMin, bathroomsMax, bedroomsMin, bedroomsMax, areaMax, areaMin, landAreaMin, landAreaMax, adjacentSuburbs, usage);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Commercial Lease Property.
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">	Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="areaMax">	Maximum floor area in square meters.</param>
        /// <param name="areaMin">Minimum floor area in square meters.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="adjacentSuburbs">Indicates whether the search should include listings in adjacent suburbs.</param>
        /// <param name="usage">The usage of the property.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchCommercialLeaseProperties(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? areaMax,
            int? areaMin,
            int? landAreaMin,
            int? landAreaMax,
            bool? adjacentSuburbs,
            PropertyUsage usage)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchCommercialLeaseProperties(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, areaMax, areaMin, landAreaMin, landAreaMax, adjacentSuburbs, usage);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Rural Property.
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">	Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="usage">The usage of the property.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchRuralProperties(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? landAreaMin,
            int? landAreaMax,
            RuralPropertyUsage usage)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchRuralProperties(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, landAreaMin, landAreaMax, usage);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Lifestyle Property.
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">	Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <param name="suburb">Specifies the search suburb ID.</param>
        /// <param name="dateFrom">Specifies minimum start date for returned listings.</param>
        /// <param name="priceMin">Minimum property price.</param>
        /// <param name="priceMax">Maximum property price.</param>
        /// <param name="bathroomsMin">Minimum number of bathrooms.</param>
        /// <param name="bathroomsMax">Maximum number of bathrooms.</param>
        /// <param name="bedroomsMin">Minimum number of bedrooms.</param>
        /// <param name="bedroomsMax">Maximum number of bedrooms.</param>
        /// <param name="areaMax">	Maximum floor area in square meters.</param>
        /// <param name="areaMin">Minimum floor area in square meters.</param>
        /// <param name="landAreaMin">Minimum land area in square meters.</param>
        /// <param name="landAreaMax">Maximum land area in square meters.</param>
        /// <param name="adjacentSuburbs">Indicates whether the search should include listings in adjacent suburbs.</param>
        /// <param name="usage">The usage of the property.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <returns>Properties.</returns>
        public global::Properties SearchLifestyleProperties(
            string searchString,
            PropertySortOrder sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? bathroomsMin,
            int? bathroomsMax,
            int? bedroomsMin,
            int? bedroomsMax,
            int? areaMax,
            int? areaMin,
            int? landAreaMin,
            int? landAreaMax,
            bool? adjacentSuburbs,
            LifestylePropertyUsage usage,
            LifestylePropertyType propertyType)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchLifestyleProperties(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, bathroomsMin, bathroomsMax, bedroomsMin, bedroomsMax, areaMax, areaMin, landAreaMin, landAreaMax, adjacentSuburbs, usage, propertyType);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Jobs</para><para>
        /// using the "query" string provided - should be the  "Search/Jobs.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Jobs.</returns>
        public Jobs SearchJobs(string query)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchJobs(query);
        }

        /// <summary>
        /// <para>Performs the search method:
        /// Search Jobs.
        /// </para><para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="salaryMin">Minimum salary.</param>
        /// <param name="salaryMax">Maximum salary.</param>
        /// <param name="region">Job offer region.</param>
        /// <param name="district">Job offer district.</param>
        /// <param name="type">Type of position.</param>
        /// <param name="category">Category.</param>
        /// <param name="subcategory">Subcategory.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>Jobs.</returns>
        public Jobs SearchJobs(
            string searchString,
            SortOrder sortOrder,
            decimal salaryMin,
            decimal salaryMax,
            string region,
            string district,
            JobType type,
            string category,
            string subcategory,
            int? page,
            int? rows)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchJobs(searchString, sortOrder, salaryMin, salaryMax, region, district, type, category, subcategory, page, rows);
        }

        /// <summary>
        /// <para>Performs the membership method:
        /// Retrieve a member’s profile data
        /// using the "query" string provided - should be the  "Member/{member_id}/Profile.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="searchString">The string to search for.</param>
        /// <param name="page">The page number you would like to retrieve.</param>
        /// <param name="category">The category the search is in.</param>
        /// <param name="storeType">The type of store.</param>
        /// <returns>Stores</returns>
        public Stores SearchStores(string searchString = "", int page = 1, string category = "", StoreType storeType = StoreType.Normal)
        {
            if (_search == null)
            {
                _search = new SearchMethods(_connection);
            }

            return _search.SearchStores(searchString, page, category, storeType);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberProfileByQueryString(query);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberProfileById(id);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberFeedbackCountByQueryString(query);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberFeedbackCountById(id);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberFeedbackByQueryString(query);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberFeedbackById(id, criteria);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberId(query);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberIdByNickname(nickname);
        }

        /// <summary>
        /// <para>Performs the Membership Method:
        /// Retrieve a member’s listings
        /// </para><para>using the "query" string provided - should be the  "Member/{member_id}/Listings.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Listings.</returns>
        public Listings MemberListings(string query)
        {
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberListings(query);
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
            if (_membership == null)
            {
                _membership = new MembershipMethods(_connection);
            }

            return _membership.MemberListingsById(id);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.ListingDetail(query);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.ListingDetailById(id);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.GetFeaturedListings();
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.GetHotListings();
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.GetLatestListings();
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.GetClosingSoonListings();
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.GetOneDollarListings();
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.UnansweredQuestionsByQueryString(query);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.UnansweredQuestionsById(listingId);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.UnansweredQuestionsAll();
        }

        // My TradeMe Methods:

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of lost items
        /// </para><para>using the "query" string provided - should be the  "MyTradeMe/Lost.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Listings.</returns>
        public Listings LostItems(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.LostItems(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of lost items.
        /// </para><para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>Listings.</returns>
        public Listings LostItems(LostItemsCriteria criteria, string page, string rows)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.LostItems(criteria, page, rows);
        }



        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a member’s watchlist
        /// </para><para>using the "query" string provided - should be the  "MyTradeMe/Watchlist.xml part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>Watchlist.</returns>
        public Watchlist Watchlist(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.Watchlist(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a member’s watchlist.
        /// </para><para>Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>Watchlist.</returns>
        public Watchlist Watchlist(WatchlistCriteria criteria, string page, string rows)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.Watchlist(criteria, page, rows);
        }

        /// <summary>
        /// <para>Performs the My Trade Me method:
        /// Retrieve a member’s ledger
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/MemberLedger/All.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/". 
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>MemberLedger.</returns>
        public MemberLedger MemberLedger(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.MemberLedger(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me method:
        /// Retrieve a member’s ledger.
        /// </para><para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>MemberLedger.</returns>
        public MemberLedger MemberLedger(MemberLedgerCriteria criteria, string page, string rows)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.MemberLedger(criteria, page, rows);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a member’s Pay Now ledger
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/PayNowLedger/{criteria}.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>PayNowLedger.</returns>
        public PayNowLedger PayNowLedger(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.PayNowLedger(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a member’s Pay Now ledger.
        /// </para><para>
        /// Creates a query string using the parameter provided - parameter can be null if it is not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>PayNowLedger.</returns>
        public PayNowLedger PayNowLedgerWithCriteria(PayNowLedgerCriteria criteria)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.PayNowLedgerWithCriteria(criteria);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of unsold items
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/UnsoldItems.xml" part of the url
        /// it shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>UnsoldItems.</returns>
        public UnsoldItems UnsoldItems(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.UnsoldItems(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of unsold items.
        /// </para>
        /// <para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>UnsoldItems.</returns>
        public UnsoldItems UnsoldItems(UnsoldItemsCriteria criteria, string page, string rows)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.UnsoldItems(criteria, page, rows);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of sold items
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/SoldItems/{criteria}.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/". 
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>SoldItems.</returns>
        public SoldItems SoldItems(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SoldItems(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of sold items.
        /// </para><para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">	Number of rows per page.</param>
        /// <returns>SoldItems.</returns>
        public SoldItems SoldItems(SoldItemsCriteria criteria, string page, string rows)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SoldItems(criteria, page, rows);
        }

        /// <summary>
        /// <para>Performs the MyTradeMe method:
        /// Retrieve a list of items currently for sale
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/SellingItems/All.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>SellingItems.</returns>
        public Items SellingItems(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SellingItems(query);
        }

        /// <summary>
        /// <para>Performs the MyTradeMe method:
        /// Retrieve a list of items currently for sale.
        /// </para><para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Items.</returns>
        public Items SellingItemsWithCriteria(SellingItemsCriteria criteria)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SellingItemsWithCriteria(criteria);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of won items
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/Won.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/". 
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>WonItems.</returns>
        public WonItems WonItems(string query)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.WonItems(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of won items.
        /// </para><para>
        /// Creates a query string using the parameters provided - parameters can be null if they are not required for the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <returns>WonItems.</returns>
        public WonItems WonItems(WonItemsCriteria criteria, string page, string rows)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.WonItems(criteria, page, rows);
        }


        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of Fixed Price Offers.
        /// </para><para>
        /// Creates a query string and performs the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>FixedPriceOffers.</returns>
        public FixedPriceOffers FixedPriceOffers()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.FixedPriceOffers();
        }


        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Adds a listing to the authenticated user’s watchlist 
        /// with the option to control when and if an email is sent to the member warning that the auction is closing soon.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">SaveToWatchlistRequest.</param>
        /// <returns>XDocument: WatchListResponse.</returns>
        public XDocument SaveListingToWatchlist(SaveToWatchlistRequest request)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SaveListingToWatchlist(request);
        }


        /// <summary>
        /// <para>Retrieves a list of product codes and stock levels associated with the authenticated user’s active listings. 
        /// The results will return data from feeds and My Products.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>ProductMappings.</returns>
        public ProductMappings GetProductMappings()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetProductMappings();
        }

        /// <summary>
        /// <para>Returns Pay Now ledger entries for a settlement into the authenticated user’s bank account.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="batchId">The ID of the settlement. This can be retrieved from the Pay Now ledger API.</param>
        /// <returns>PayNowLedger.</returns>
        public PayNowLedger GetPayNowLedgerBySettlement(string batchId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetPayNowLedgerBySettlement(batchId);
        }

        /// <summary>
        /// <para>Retrieve sales statistics for the authenticated user.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>WeeklySales</returns>
        public WeeklySales GetWeeklySalesStats()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetWeeklySalesStats();
        }

        /// <summary>
        /// <para>Retrieves the Job Agent report for the authenticated user.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>JobAgentReport</returns>
        public JobAgentReport GetJobAgentReport()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetJobAgentReport();
        }

        /// <summary>
        /// <para>Retrieves the Property Agent report for the authenticated user.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>PropertyAgentReport</returns>
        public PropertyAgentReport GetPropertyAgentReport()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetPropertyAgentReport();
        }


        /// <summary>
        /// <para>Retrieves information about fees for a single listing.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The ID of listing</param>
        /// <returns>SellingFee.</returns>
        public SellingFee GetLisingFees(string listingId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetLisingFees(listingId);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Creates or updates the note on a listing. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">SaveNoteRequest.</param>
        /// <returns>XDocument: NoteResponse.</returns>
        public XDocument AddNote(SaveNoteRequest request)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.AddNote(request);
        }

        /// <summary>
        /// <para>Retrieves the note for a listing, if there is one. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The ID of the listing to retrieve the note for.</param>
        /// <returns>NoteResponse.</returns>
        public NoteResponse GetNote(string listingId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetNote(listingId);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Removes the note from a listing. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The id of the listing that will be removed from the watchlist.</param>
        /// <param name="noteId">The ID of the note.</param>
        /// <param name="offerId">The ID of the offer the note is associated with. Should be 0 if the note is not associated with a fixed price offer.</param>
        /// <returns>XDocument: NoteResponse.</returns>
        public XDocument DeleteNote(string listingId, string noteId, string offerId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.DeleteNote(listingId, noteId, offerId);
        }

        /// <summary>
        /// <para>Saves a status to a listing in the authenticated user’s Sold Items list. (POST)
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The id of the listing that will be removed from the watchlist.</param>
        /// <param name="offerId">The ID of the note.</param>
        /// <param name="stat">The status you want to save, should be one of the following: EmailSent, PaymentReceived, GoodsShipped, SaleCompleted.</param>
        /// <returns>StatusResponse.</returns>
        public StatusResponse SaveOrUpdateListingStatus(string listingId, string offerId, string stat)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SaveOrUpdateListingStatus(listingId, offerId, stat);
        }

        /// <summary>
        /// <para>Saves a status to a listing in the authenticated user’s Sold Items list. (POST)
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="purchaseId">The ID of the purchase.</param>
        /// <param name="stat">The status you want to save, should be one of the following: EmailSent, PaymentReceived, GoodsShipped, SaleCompleted.</param>
        /// <returns>StatusResponse.</returns>
        public StatusResponse SaveOrUpdateListingStatus(string purchaseId, string stat)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SaveOrUpdateListingStatus(purchaseId, stat);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Deletes the status from a listing in the authenticated user’s Sold Items list. (DELETE).
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The id of the listing that will be removed from the watchlist.</param>
        /// <param name="offerId">The ID of the note.</param>
        /// <returns>XDocument: StatusResponse.</returns>
        public XDocument DeleteListingStatus(string listingId, string offerId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.DeleteListingStatus(listingId, offerId);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Deletes the status from a listing in the authenticated user’s Sold Items list. (DELETE).
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="purchaseId">The ID of the purchase to modify.</param>
        /// <returns>XDocument: StatusResponse.</returns>
        public XDocument DeleteListingStatus(string purchaseId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.DeleteDeliveryAddress(purchaseId);
        }

        /// <summary>
        /// <para>Retrieves a list of delivery addresses for the authenticated user. (GET)
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>Address.</returns>
        public Address GetListOfDeliveryAddresses()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetListOfDeliveryAddresses();
        }

        /// <summary>
        /// <para>Removes a delivery address from the authenticated user’s profile. (DELETE).
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="deliveryId">deliveryId</param>
        /// <returns>XDocument: StatusResponse.</returns>
        public XDocument DeleteDeliveryAddress(string deliveryId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.DeleteDeliveryAddress(deliveryId);
        }

        /// <summary>
        /// <para>Adds a delivery address to the authenticated user’s profile. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">DeliveryAddress</param>
        /// <returns>XDocument: DeliveryAddressResponse.</returns>
        public XDocument AddDeliveryAddress(DeliveryAddress request)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.AddDeliveryAddress(request);
        }

        /// <summary>
        /// <para>Modifies a delivery addresses in the authenticated user’s profile. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">DeliveryAddress</param>
        /// <returns>XDocument: DeliveryAddressResponse.</returns>
        public XDocument UpdateDeliveryAddress(DeliveryAddress request)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.UpdateDeliveryAddress(request);
        }

         /// <summary>
        /// <para>Adds feedback to a listing. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">FeedbackRequest</param>
        /// <returns>XDocument: FeedbackResponse.</returns>
        public XDocument AddFeedback(FeedbackRequest request)
        {
             if (_myTradeMe == null)
             {
                 _myTradeMe = new MyTradeMeMethods(_connection);
             }

             return _myTradeMe.AddFeedback(request);
        }

        /// <summary>
        /// <para>Modifies feedback the authenticated user has posted against a listing. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">FeedbackUpdateRequest</param>
        /// <returns>XDocument: FeedbackResponse.</returns>
        public XDocument UpdateFeedback(FeedbackUpdateRequest request)
        {
             if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.UpdateFeedback(request);
        }
        
        /// <summary>
        /// <para>Removes feedback the authenticated user has posted against a listing. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="feedbackId">The ID of the feedback to remove.</param>
        /// <returns>XDocument: FeedbackResponse.</returns>
        public XDocument RemoveFeedback(string feedbackId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.RemoveFeedback(feedbackId);
        }


        /// <summary>
        /// <para>Replies to feedback placed against a listing where the authenticated user was the seller. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">FeedbackReplyRequest</param>
        /// <returns>XDocument: FeedbackResponse.</returns>
        public XDocument RespondToFeedback(FeedbackReplyRequest request)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.RespondToFeedback(request);
        }

         /// <summary>
        /// <para>Retrieves a list of all the members on the authenticated user’s blacklist. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>Blacklist</returns>
        public Blacklist GetBlacklistedMembers()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetBlacklistedMembers();
        }

        /// <summary>
        /// <para>Adds a member to the authenticated user’s blacklist. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">BlacklistRequest</param>
        /// <returns>XDocument: BlacklistResponse.</returns>
        public XDocument AddMemberToBlackList(BlacklistRequest request)
        {
             if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.AddMemberToBlackList(request);
        }

        /// <summary>
        /// <para>Removes a member from the authenticated user’s blacklist. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="memberId">memberId</param>
        /// <returns>XDocument: BlacklistResponse.</returns>
        public XDocument RemoveMemberFromBlacklist(string memberId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.RemoveMemberFromBlacklist(memberId);
        }

        /// <summary>
        /// <para>Retrieves the authenticated user’s email preferences. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>EmailOptions.</returns>
        public EmailOptions GetEmailPreferences()
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.GetEmailPreferences();
        }

        /// <summary>
        /// <para>Sets the authenticated user’s email preferences. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">EmailOptions</param>
        /// <returns>XDocument: EmailOptions.</returns>
        public XDocument SetEmailPreferences(EmailOptions request)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.SetEmailPreferences(request);
        }

        /// <summary>
        /// <para>Refunds a Pay Now payment where the authenticated user is the seller. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">PayNowRefundRequest</param>
        /// <returns>XDocument: PayNowRefundResponse.</returns>
        public XDocument RefundPayNowPayment(PayNowRefundRequest request)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.RefundPayNowPayment(request);
        }

        // Fixed Price Offer Methods

        /// <summary>
        /// <para>Performs the Fixed Price Offer Method:
        /// Retrieve a list of Fixed Price Offers
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/FixedPriceOffers.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>FixedPriceOffers.</returns>
        public FixedPriceOffers FixedPriceOffers(string query)
        {
            if (_fixedPrice == null)
            {
                _fixedPrice = new FixedPriceOfferMethods(_connection);
            }

            return _fixedPrice.FixedPriceOffers(query);
        }

        /// <summary>
        /// <para>Performs the Fixed Price Offer Method:
        /// Retrieve a list of Fixed Price Offers.
        /// </para><para>
        /// Creates a query string and performs the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>FixedPriceOffers.</returns>
        public FixedPriceOffers FixedPriceOffersToMember()
        {
            if (_fixedPrice == null)
            {
                _fixedPrice = new FixedPriceOfferMethods(_connection);
            }

            return _fixedPrice.FixedPriceOffersToMember();
        }


        /// <summary>
        /// <para>Performs the Fixed Price Offer Method:
        /// Retrieve the fixed price offers offered by a member. GET
        /// </para><para>
        /// Creates a query string and performs the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>FixedPriceOffers.</returns>
        public FixedPriceOffers FixedPriceOffersByMember()
        {
            if (_fixedPrice == null)
            {
                _fixedPrice = new FixedPriceOfferMethods(_connection);
            }

            return _fixedPrice.FixedPriceOffersByMember();
        }

        /// <summary>
        /// <para>Performs the Fixed Price Offer Method:
        /// Accepts or rejects a fixed price offer. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: FixedPriceOfferResponse</returns>
        public XDocument RespondToFixedPriceOffer(FixedPriceOfferRequest request)
        {
            if (_fixedPrice == null)
            {
                _fixedPrice = new FixedPriceOfferMethods(_connection);
            }

            return _fixedPrice.RespondToFixedPriceOffer(request);
        }

        /// <summary>
        /// <para>Performs the Fixed Price Offer Method:
        /// Makes a fixed price offer for an auction to the specified members. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: FixedPriceOfferResponse</returns>
        public XDocument MakeFixedPriceOffer(FixedPriceOfferToMembersRequest request)
        {
            if (_fixedPrice == null)
            {
                _fixedPrice = new FixedPriceOfferMethods(_connection);
            }

            return _fixedPrice.MakeFixedPriceOffer(request);
        }

        /// <summary>
        /// <para>Performs the Fixed Price Offer Method:
        /// Withdraws an offer that is current and not expired, accepted or rejected by all users. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: FixedPriceOfferResponse</returns>
        public XDocument WithdrawFixedPriceOffer(FixedPriceOfferWithdrawalRequest request)
        {
            if (_fixedPrice == null)
            {
                _fixedPrice = new FixedPriceOfferMethods(_connection);
            }

            return _fixedPrice.WithdrawFixedPriceOffer(request);
        }

        /// <summary>
        /// <para>Performs the Fixed Price Offer method:
        /// Returns a list of members you can make a fixed price offer to for a particular auction. GET
        /// </para>
        /// REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The listing ID of the auction you wish to make a fixed price offer for. The listing must be closed.</param>
        /// <param name="filter">Filters the returned list to a subset of possible members 
        /// (“All”, “Bidders” – only return bidders, “Watchers” – only return watchers).</param>
        /// <returns>FixedPriceOfferMembersResponse</returns>
        public FixedPriceOfferMembersResponse RetrieveListOfMembersForFixedPriceOffer(string listingId, string filter)
        {
            if (_fixedPrice == null)
            {
                _fixedPrice = new FixedPriceOfferMethods(_connection);
            }

            return _fixedPrice.RetrieveListOfMembersForFixedPriceOffer(listingId, filter);
        }

        // Photo Methods:

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Retrieve a list of member photos
        /// </para><para>
        /// using the "query" string provided - should be the  "Photos.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>MemberPhotos.</returns>
        public MemberPhotos MemberPhotos(string query)
        {
            if (_photo == null)
            {
                _photo = new PhotoMethods(_connection);
            }

            return _photo.MemberPhotos(query);
        }

        /// <summary>
        /// Performs the Photo Method:
        /// Retrieve a list of member photos.
        /// <para>
        /// Creates a query string and performs the request.
        /// </para><para>
        /// REQUIRES AUTHENTICATION.</para>
        /// </summary>
        /// <returns>MemberPhotos.</returns>
        public MemberPhotos MemberPhotos()
        {
            if (_photo == null)
            {
                _photo = new PhotoMethods(_connection);
            }

            return _photo.MemberPhotos();
        }

        /// <summary>
        /// <para>
        /// Performs the Photo Method:
        /// Upload a photo.
        /// </para><para>
        /// Serializes the given PhotoUploadRequest into xml and sends the message.
        /// Loads the file and converts it to the appropriate data format for the request - it does not require the "PhotoData" field of the PhotoUploadRequest object to have anything in it.
        ///  </para>
        /// </summary>
        /// <param name="up">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument UploadPhotoFormat(PhotoUploadRequest up)
        {
            if (_photo == null)
            {
                _photo = new PhotoMethods(_connection);
            }

            return _photo.UploadPhotoFormat(up);
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Upload a photo.
        /// </para>
        /// Serializes the given PhotoUploadRequest into xml and sends the message.
        /// </summary>
        /// <param name="up">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument UploadPhoto(PhotoUploadRequest up)
        {
            if (_photo == null)
            {
                _photo = new PhotoMethods(_connection);
            }

            return _photo.UploadPhoto(up);
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Remove a photo.
        /// </para><para>
        /// Creates a query string using the photo id provided.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="photoId">The id of the photo to be removed.</param>
        /// <returns>RemovePhoto.</returns>
        public string RemovePhoto(string photoId)
        {
            if (_photo == null)
            {
                _photo = new PhotoMethods(_connection);
            }

            return _photo.RemovePhoto(photoId);
        }


        /// <summary>
        /// <para>Performs the Photo Method:
        /// Adds a photo to an auction. The currently authenticated user must be the seller.Remove a photo.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="photoId">The id of the photo to add.</param>
        /// /// <param name="listingId">The ID of the listing to add the photo to.</param>
        /// <returns>XDocument: PhotoResponse.</returns>
        public XDocument AddPhotoToListing(string photoId, string listingId)
        {
            if (_photo == null)
            {
                _photo = new PhotoMethods(_connection);
            }

            return _photo.AddPhotoToListing(photoId, listingId);
        }

        // Post Methods

        /// <summary>
        /// <para>Performs the Bidding Method:
        /// Place a bid request.
        /// </para><para>
        /// Serializes the given BidRequest into xml and sends the message.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument BidRequest(BidRequest request)
        {
            if (_bidding == null)
            {
                _bidding = new BiddingMethods(_connection);
            }

            return _bidding.BidRequest(request);
        }

        // place a buy now request 

        /// <summary>
        /// <para>Performs the Bidding Method:
        /// Place a Buy Now request.
        /// </para><para>
        /// Serializes the given BuyNowRequest into xml and sends the message.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument BuyNowRequest(BuyNowRequest request)
        {
            if (_bidding == null)
            {
                _bidding = new BiddingMethods(_connection);
            }

            return _bidding.BuyNowRequest(request);
        }

        /// <summary>
        /// <para>Performs the Listing Method:
        /// Provide an answer for a specific question.
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.AnswerSpecificQuestion(answerQuestion, listingId, questionId);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.CommentOnListing(addComment, listingId);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.EmailSellerOfClassified(emailRequest, listingId);
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
            if (_listing == null)
            {
                _listing = new ListingMethods(_connection);
            }

            return _listing.EmailFriend(email, listingId);
        }



        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Save a listing to a member’s watchlist (DELETE).
        /// </para><para>
        /// Will remove the listing with the listingId provided from the members watchlist.
        /// Creates a query string using the listingId provided.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The id of the listing that will be removed from the watchlist.</param>
        /// <returns>XDocument.</returns>
        public XDocument DeleteListingFromWatchlist(string listingId)
        {
            if (_myTradeMe == null)
            {
                _myTradeMe = new MyTradeMeMethods(_connection);
            }

            return _myTradeMe.DeleteListingFromWatchlist(listingId);
        }

        // Favourite methods:

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Save Search.
        /// </para>
        /// <para>Serializes the given SaveSearchRequest into xml.</para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="saveSearch">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument SaveSearch(SaveSearchRequest saveSearch)
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.SaveSearch(saveSearch);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Save Category.
        /// </para>
        /// <para>Serializes the given SaveCategoryRequest into xml.</para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="saveCategory">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument SaveCategory(SaveCategoryRequest saveCategory)
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.SaveCategory(saveCategory);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Save Seller.
        /// </para>
        /// <para>Serializes the given SaveSellerRequest into xml.</para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="saveSeller">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument SaveSeller(SaveSellerRequest saveSeller)
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.SaveSeller(saveSeller);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Deletes a saved search, category or seller from the currently authenticated user’s favourites. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="favoriteId">The ID of the favourite.</param>
        /// <param name="type">The type of favourite (must be “Category”, “Search”, “AttributeSearch” or “Seller”).</param>
        /// <returns>XDocument.</returns>
        public XDocument RemoveSavedFavorite(string favoriteId, string type)
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.RemoveSavedFavorite(favoriteId, type);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Modifies the email frequency for a saved favourite. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="favoriteId">The ID of the favourite.</param>
        /// <param name="type">The type of favourite (must be “Category”, “Search”, “AttributeSearch” or “Seller”).</param>
        /// <param name="frequency">The frequency that emails should be sent (must be “None”, “Daily”, “Every3Days” or “Weekly”).</param>
        /// <returns>XDocument.</returns>
        public XDocument UpdateSavedFavorite(string favoriteId, string type, string frequency)
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.UpdateSavedFavorite(favoriteId, type, frequency);
        }

        /// <summary>
        /// <para>Performs the Favourites Method:
        /// Retrieve Favourite Categories.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>SavedCategories.</returns>
        public SavedCategories RetrieveFavouriteCategories()
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.RetrieveFavouriteCategories();
        }

        /// <summary>
        /// <para>Performs the Favourites Method:
        /// Retrieve Favourite Searches.
        /// </para>
        /// REQURIES AUTHENTICATION.
        /// </summary>
        /// <param name="filter">The filter for the favourite searches.</param>
        /// <returns>SaveSearches.</returns>
        public SavedSearches RetrieveFavouriteSearches(SavedSearchType filter)
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.RetrieveFavouriteSearches(filter);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Retrieve Favourite Sellers.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>SavedSellers.</returns>
        public SavedSellers RetrieveFavouriteSellers()
        {
            if (_favourites == null)
            {
                _favourites = new FavouriteMethods(_connection);
            }

            return _favourites.RetrieveFavouriteSellers();
        }

        // Selling Methods

        /// <summary>
        /// <para>Starts a new auction or classified.
        /// </para><para>
        /// There are several endpoints that should be used to get information needed to list an item. 
        /// Check Category information, Legal Notice information, attributes, fees and listing duration options.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: ListingResponse</returns>
        public XDocument ListItem(ListingRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.ListItem(request);
        }

        /// <summary>
        /// <para>Returns the fees that will be charged for a listing once the auction or classified has been created.
        /// </para><para>
        /// There are several endpoints that should be used to get information needed to list an item. 
        /// Check Category information, Legal Notice information, attributes, fees and listing duration options.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: FeeResponse</returns>
        public XDocument GetFeesForListingItems(ListingRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.GetFeesForListingItems(request);
        }

        /// <summary>
        /// <para>Edit an auction or classified that you have created.
        /// </para><para>
        /// You will need to resubmit the complete sell form as used when you created the listing but with the fields you wish to edit modified.
        /// Request format will error if fields are locked and cannot be edited.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: ListingResponse</returns>
        public XDocument EditListedItem(EditListingRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.EditListedItem(request);
        }

        /// <summary>
        /// <para>Withdraw a listing from Trade Me for either being sold or unsold.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: ListingResponse</returns>
        public XDocument WithdrawListedItem(WithdrawRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.WithdrawListedItem(request);
        }

        /// <summary>
        /// <para>Relist an item that has expired. Relists an item without modifying editable fields.
        /// </para><para>
        /// Relist copies all data from original listing and applies them to the new listing. 
        /// The only exceptions are where there are default durations that are applied on relisted items.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: ListingResponse</returns>
        public XDocument RelistItem(RelistListingRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.RelistItem(request);
        }

        /// <summary>
        /// <para>Sell similar from an existing or expired auction or classified. 
        /// Creates a new listing without modifying editable fields.Relist an item that has expired. 
        /// Relists an item without modifying editable fields.
        /// </para><para>
        /// Sell similar from an existing or expired auction or classified. 
        /// Creates a new listing without modifying editable fields.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: ListingResponse</returns>
        public XDocument SellSimilarItem(SellSimilarListingRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.SellSimilarItem(request);
        }

        /// <summary>
        /// <para>List a DVD using listing details that are sourced from the Trade Me DVD catalogue.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: ListingResponse</returns>
        public XDocument SellDvdFromCatalogue(SellCatalogueRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.SellDvdFromCatalogue(request);
        }

        /// <summary>
        /// <para>List a Blu-ray disc using listing details that are sourced from the Trade Me Blu-ray catalogue.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: ListingResponse</returns>
        public XDocument SellBluRayFromCatalogue(SellCatalogueRequest request)
        {
            if (_selling == null)
            {
                _selling = new SellingMethods(_connection);
            }

            return _selling.SellBluRayFromCatalogue(request);
        }

        // Helper Methods

        /// <summary>
        /// A helper method for determinging if the &amp; symbol needs to be added to the query string based on the boolean "extra".
        /// </summary>
        /// <param name="extra">True if the &amp; symbol should be added.</param>
        /// <param name="toAdd">The string that needs to be added to the query string.</param>
        /// <returns>The toAdd string with or without a &amp; symbol infront of it, depending on the boolean extra.</returns>
        internal static string AddAndHelper(bool extra, string toAdd)
        {
            var toReturn = string.Empty;
            if (!extra)
            {
                toReturn += toAdd;
            }
            else
            {
                toReturn += "&" + toAdd;
            }

            return toReturn;
        }

        /// <summary>
        /// A helper function to convert a DateTime into the correct string format.
        /// </summary>
        /// <param name="dateFrom">The date to convert into a string.</param>
        /// <returns>String.</returns>
        internal static string DateToStringConverter(DateTime dateFrom)
        {
            return String.Format(Constants.Culture, "{0}-{1}-{2}", dateFrom.Day, dateFrom.Month, dateFrom.Year);
        }
    }
}