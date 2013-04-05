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
    /// The BiddingMethods class contains the methods requried for making calls to the API related to bidding.
    /// </summary>
    internal class BiddingMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the BiddingMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public BiddingMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

        /// <summary>
        /// <para>Performs the Bidding Method:
        /// Place a bid request. POST
        /// </para><para>
        /// Serializes the given BidRequest into xml and sends the message.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: AuctionBidResponse</returns>
        public XDocument BidRequest(BidRequest request)
        {
            var query = String.Format(Constants.Culture, "{0}/Bid{1}", Constants.BIDDING, Constants.XML);
            return _connection.Post(request, query);
        }

        // place a buy now request 

        /// <summary>
        /// <para>Performs the Bidding Method:
        /// Place a Buy Now request. POST
        /// </para><para>
        /// Serializes the given BuyNowRequest into xml and sends the message.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="request">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument: BuyNowResponse</returns>
        public XDocument BuyNowRequest(BuyNowRequest request)
        {
            var query = String.Format(Constants.Culture, "{0}/BuyNow{1}", Constants.BIDDING, Constants.XML);
            return _connection.Post(request, query);
        }
    }
}
