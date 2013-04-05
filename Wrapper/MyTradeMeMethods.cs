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
    /// The MyTradeMeMethods class contains the methods requried for making calls to the API related to My TradeMe.
    /// </summary>
    internal class MyTradeMeMethods
    {
        // A helper boolean to know whether or not to add an &amp; sign to the query string 
        private bool _addAnd = false;
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the MyTradeMeMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public MyTradeMeMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieve a list of lost items GET
        /// </para><para>using the "query" string provided - should be the  "MyTradeMe/Lost.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API with.</param>
        /// <returns>Listings.</returns>
        public Listings LostItems(string query)
        {
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<Listings>.Deserialize(new Listings(), xml);
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
            var query = Constants.MY_TRADEME + "/Lost";
            _addAnd = false;
            var conditions = "?";

            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            conditions += SearchMethods.PageAndRowsHelper(page, rows, _addAnd);

            if (_addAnd)
            {
                query += conditions;
            }

            return this.LostItems(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieves statistics and profile information for the authenticated user.
        /// </para><para>
        /// using the "query" string provided - should be the  "MyTradeMe/Summary.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>MemberSummary.</returns>
        public MemberSummary MemberProfileSummary(string query)
        {
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<MemberSummary>.Deserialize(new MemberSummary(), xml);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Retrieves statistics and profile information for the authenticated user.
        /// </para><para>
        /// Constructs the query and performs the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>MemberSummary.</returns>
        public MemberSummary MemberProfileSummary()
        {
            var query = String.Format(Constants.Culture, "{0}/Summary{1}", Constants.MY_TRADEME, Constants.XML);
            return this.MemberProfileSummary(query);
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<Watchlist>.Deserialize(new Watchlist(), xml);
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
            var query = String.Format(Constants.Culture, "{0}/{1}", Constants.MY_TRADEME, Constants.WATCHLIST);
            var conditions = "?";
            _addAnd = false;

            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            conditions += SearchMethods.PageAndRowsHelper(page, rows, _addAnd);

            if (_addAnd)
            {
                query += conditions;
            }

            return this.Watchlist(query);
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<MemberLedger>.Deserialize(new MemberLedger(), xml);
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
            var query = Constants.MY_TRADEME + "/MemberLedger";
            var conditions = "?";
            _addAnd = false;

            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            conditions += SearchMethods.PageAndRowsHelper(page, rows, _addAnd);
            if (conditions.Equals("?"))
            {
                query += conditions;
            }

            return this.MemberLedger(query);
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<PayNowLedger>.Deserialize(new PayNowLedger(), xml);
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
            var query = Constants.MY_TRADEME + "/PayNowLedger";
            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            return this.PayNowLedger(query);
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<UnsoldItems>.Deserialize(new UnsoldItems(), xml);
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
            var query = Constants.MY_TRADEME + "/UnsoldItems";
            var conditions = "?";
            _addAnd = false;

            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            conditions += SearchMethods.PageAndRowsHelper(page, rows, _addAnd);

            if (conditions.Equals("?"))
            {
                query += conditions;
            }

            return this.UnsoldItems(query);
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<SoldItems>.Deserialize(new SoldItems(), xml);
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
            var query = Constants.MY_TRADEME + "/SoldItems";
            var conditions = "?";
            _addAnd = false;

            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            conditions += SearchMethods.PageAndRowsHelper(page, rows, _addAnd);

            if (conditions.Equals("?"))
            {
                query += conditions;
            }

            return this.SoldItems(query);
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<Items>.Deserialize(new Items(), xml);
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
            var query = Constants.MY_TRADEME + "/SellingItems";

            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            return this.SellingItems(query);
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<WonItems>.Deserialize(new WonItems(), xml);
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
            var query = Constants.MY_TRADEME + "/Won";
            var conditions = "?";
            _addAnd = false;

            if (!string.IsNullOrEmpty(string.Empty + criteria))
            {
                query += "/" + criteria;
            }

            query += Constants.XML;

            conditions += SearchMethods.PageAndRowsHelper(page, rows, _addAnd);

            if (conditions.Equals("?"))
            {
                query += conditions;
            }

            return this.WonItems(query);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
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
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<FixedPriceOffers>.Deserialize(new FixedPriceOffers(), xml);
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
            var query = String.Format("{0}/FixedPriceOffers{1}", Constants.MY_TRADEME, Constants.XML);
            return FixedPriceOffers(query);
        }


        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Adds a listing to the authenticated user’s watchlist 
        /// with the option to control when and if an email is sent to the member warning that the auction is closing soon. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">SaveToWatchlistRequest.</param>
        /// <returns>XDocument: WatchListResponse.</returns>
        public XDocument SaveListingToWatchlist(SaveToWatchlistRequest request)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.MY_TRADEME, Constants.WATCHLIST, Constants.XML);
            return _connection.Post(request, query);   
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Save a listing to a member’s watchlist. DELETE
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
            var query = String.Format(Constants.Culture, "{0}/{1}/{2}{3}", Constants.MY_TRADEME, Constants.WATCHLIST, listingId, Constants.XML);
            return _connection.Post(null, query, true);
        }
 

        /// <summary>
        /// <para>Retrieves a list of product codes and stock levels associated with the authenticated user’s active listings. GET
        /// The results will return data from feeds and My Products.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>ProductMappings.</returns>
        public ProductMappings GetProductMappings()
        {
            var query = String.Format("{0}/ProductMappings{1}", Constants.MY_TRADEME, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<ProductMappings>.Deserialize(new ProductMappings(), xml);
        }

        /// <summary>
        /// <para>Returns Pay Now ledger entries for a settlement into the authenticated user’s bank account. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="batchId">The ID of the settlement. This can be retrieved from the Pay Now ledger API.</param>
        /// <returns>PayNowLedger.</returns>
        public PayNowLedger GetPayNowLedgerBySettlement(string batchId)
        {
            var query = String.Format("{0}/PayNowSettlement/{1}{2}", Constants.MY_TRADEME, batchId, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<PayNowLedger>.Deserialize(new PayNowLedger(), xml);
        }

        /// <summary>
        /// <para>Retrieve sales statistics for the authenticated user. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>WeeklySales</returns>
        public WeeklySales GetWeeklySalesStats()
        {
            var query = String.Format("{0}/SalesSummary{1}", Constants.MY_TRADEME, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<WeeklySales>.Deserialize(new WeeklySales(), xml);
        }

        /// <summary>
        /// <para>Retrieves the Job Agent report for the authenticated user. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>JobAgentReport</returns>
        public JobAgentReport GetJobAgentReport()
        {
            var query = String.Format("{0}/jobagentreport{1}", Constants.MY_TRADEME, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<JobAgentReport>.Deserialize(new JobAgentReport(), xml);
        }

        /// <summary>
        /// <para>Retrieves the Property Agent report for the authenticated user. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>PropertyAgentReport</returns>
        public PropertyAgentReport GetPropertyAgentReport()
        {
            var query = String.Format("{0}/propertyagentreport{1}", Constants.MY_TRADEME, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<PropertyAgentReport>.Deserialize(new PropertyAgentReport(), xml);
        }


        /// <summary>
        /// <para>Retrieves information about fees for a single listing. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The ID of listing</param>
        /// <returns>SellingFee.</returns>
        public SellingFee GetLisingFees(string listingId)
        {
            var query = String.Format("{0}/ListingFees/{1}{2}", Constants.MY_TRADEME, listingId, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<SellingFee>.Deserialize(new SellingFee(), xml);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.MY_TRADEME, Constants.NOTES, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format("{0}/{1}/{2}{3}", Constants.MY_TRADEME,Constants.NOTES, listingId, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<NoteResponse>.Deserialize(new NoteResponse(), xml);
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
            var query = String.Format(Constants.Culture, "{0}/{1}/{2}/{3}/{4}.{5}", Constants.MY_TRADEME,
                                      Constants.NOTES, listingId, noteId, offerId, Constants.XML);
            return _connection.Post(null, query, true);
        }

        /// <summary>
        /// <para>Saves a status to a listing in the authenticated user’s Sold Items list. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The id of the listing that will be removed from the watchlist.</param>
        /// <param name="offerId">The ID of the note.</param>
        /// <param name="stat">The status you want to save, should be one of the following: EmailSent, PaymentReceived, GoodsShipped, SaleCompleted.</param>
        /// <returns>StatusResponse.</returns>
        public StatusResponse SaveOrUpdateListingStatus(string listingId, string offerId, string stat)
        {
            var query = String.Format("{0}/Status/{1}/{2}/{3}{4}", Constants.MY_TRADEME, listingId, offerId, stat, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<StatusResponse>.Deserialize(new StatusResponse(), xml);
        }

        /// <summary>
        /// <para>Saves a status to a listing in the authenticated user’s Sold Items list. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="purchaseId">The ID of the purchase.</param>
        /// <param name="stat">The status you want to save, should be one of the following: EmailSent, PaymentReceived, GoodsShipped, SaleCompleted.</param>
        /// <returns>StatusResponse.</returns>
        public StatusResponse SaveOrUpdateListingStatus(string purchaseId, string stat)
        {
            var query = String.Format("{0}/Status/{1}/{2}{3}", Constants.MY_TRADEME, purchaseId, stat, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<StatusResponse>.Deserialize(new StatusResponse(), xml);
        }


        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Deletes the status from a listing in the authenticated user’s Sold Items list. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="listingId">The id of the listing that will be removed from the watchlist.</param>
        /// <param name="offerId">The ID of the note.</param>
        /// <returns>XDocument: StatusResponse.</returns>
        public XDocument DeleteListingStatus(string listingId, string offerId)
        {
            var query = String.Format(Constants.Culture, "{0}/Status/{1}/{2}{3}", Constants.MY_TRADEME, listingId, offerId, Constants.XML);
            return _connection.Post(null, query, true);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Deletes the status from a listing in the authenticated user’s Sold Items list. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="purchaseId">The ID of the purchase to modify.</param>
        /// <returns>XDocument: StatusResponse.</returns>
        public XDocument DeleteListingStatus(string purchaseId)
        {
            var query = String.Format(Constants.Culture, "{0}/Status/{1}{2}", Constants.MY_TRADEME, purchaseId, Constants.XML);
            return _connection.Post(null, query, true);
        }

        /// <summary>
        /// <para>Retrieves a list of delivery addresses for the authenticated user. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>Address.</returns>
        public Address GetListOfDeliveryAddresses()
        {
            var query = String.Format("{0}/DeliveryAddresses{1}", Constants.MY_TRADEME, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<Address>.Deserialize(new Address(), xml);
        }

        /// <summary>
        /// <para>Removes a delivery address from the authenticated user’s profile. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="deliveryId">deliveryId</param>
        /// <returns>XDocument: StatusResponse.</returns>
        public XDocument DeleteDeliveryAddress(string deliveryId)
        {
            var query = String.Format(Constants.Culture, "{0}/DeliveryAddresses/{1}{2}", Constants.MY_TRADEME, deliveryId, Constants.XML);
            return _connection.Post(null, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/DeliveryAddresses{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/DeliveryAddresses/Update{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/Feedback/Update{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/Feedback/Update{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/Feedback/{1}{2}", Constants.MY_TRADEME, feedbackId, Constants.XML);
            return _connection.Post(null, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/Feedback/Reply{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
        }

        /// <summary>
        /// <para>Retrieves a list of all the members on the authenticated user’s blacklist. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>Blacklist</returns>
        public Blacklist GetBlacklistedMembers()
        {
            var query = String.Format("{0}/Blacklist{1}", Constants.MY_TRADEME, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<Blacklist>.Deserialize(new Blacklist(), xml);
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
            var query = String.Format(Constants.Culture, "{0}/Blacklist/Add{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/Blacklist/{1}{2}", Constants.MY_TRADEME, memberId, Constants.XML);
            return _connection.Post(null, query, true);
        }


        /// <summary>
        /// <para>Retrieves the authenticated user’s email preferences. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>EmailOptions.</returns>
        public EmailOptions GetEmailPreferences()
        {
            var query = String.Format("{0}/EmailOptions{1}", Constants.MY_TRADEME, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<EmailOptions>.Deserialize(new EmailOptions(), xml);
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
            var query = String.Format(Constants.Culture, "{0}/EmailOptions{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
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
            var query = String.Format(Constants.Culture, "{0}/PayNowRefund{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(request, query, true);
        }

        /// <summary>
        /// <para>Performs the My Trade Me Method:
        /// Clears the authenticated member’s saved credit card information. DELETE
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>XDocument.</returns>
        public XDocument ClearSavedCreditCard()
        {
            var query = String.Format(Constants.Culture, "{0}/ClearCreditCard{1}", Constants.MY_TRADEME, Constants.XML);
            return _connection.Post(null, query, true);
        }
    }
}
