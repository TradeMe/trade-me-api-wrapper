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
    internal class FixedPriceOfferMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the FixedPriceOfferMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public FixedPriceOfferMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }


        /// <summary>
        /// <para>Performs the Fixed Price Offer Method:
        /// Retrieves a list of the outstanding fixed price offers that have been offered to the currently authenticated user. GET
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
        /// <para>Performs the Fixed Price Offer Method:
        /// Retrieves a list of the outstanding fixed price offers that have been offered to the currently authenticated user. GET
        /// </para><para>
        /// Creates a query string and performs the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>FixedPriceOffers.</returns>
        public FixedPriceOffers FixedPriceOffersToMember()
        {
            var query = String.Format("{0}/{1}/List{2}", Constants.MY_TRADEME,Constants.FIXEDPRICEOFFER, Constants.XML);
            return this.FixedPriceOffers(query);
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
            var query = String.Format("{0}/{1}/Offered{2}", Constants.MY_TRADEME, Constants.FIXEDPRICEOFFER, Constants.XML);
            return this.FixedPriceOffers(query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}/Respond{2}", Constants.MY_TRADEME, Constants.FIXEDPRICEOFFER, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}/MakeOffer{2}", Constants.MY_TRADEME, Constants.FIXEDPRICEOFFER, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}/WithdrawOffer{2}", Constants.MY_TRADEME, Constants.FIXEDPRICEOFFER, Constants.XML);
            return _connection.Post(request, query);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/{2}/{3}{4}", Constants.MY_TRADEME, listingId, "Members",
                                    filter, Constants.XML);

            var getRequest = _connection.AuthenticatedQuery(url);
            var xml = getRequest.ToString();
            return Deserializer<FixedPriceOfferMembersResponse>.Deserialize(new FixedPriceOfferMembersResponse(), xml);
        }


    }
}

