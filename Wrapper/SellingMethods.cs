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
    /// The SellingMethods class 
    /// These methods allow you to list an item on the site. 
    /// When selling you can choose to either list and not view the fees that you will be charged, 
    /// or get a response back to confirm the total fees for the particular listing you are creating.
    /// </summary>
    internal class SellingMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the SellingMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public SellingMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

        // Selling Methods:

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
            var query = String.Format(Constants.Culture, "{0}{1}", Constants.SELLING, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.SELLING, Constants.FEES, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.SELLING, Constants.EDIT, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.SELLING, Constants.WITHDRAW, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.SELLING, Constants.RELIST, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.SELLING, Constants.SIMILAR, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.SELLING, Constants.SELLCATALOGUEDVD, Constants.XML);
            return _connection.Post(request, query);
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
            var query = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.SELLING, Constants.SELLCATALOGUEBLURAY, Constants.XML);
            return _connection.Post(request, query);
        }
    }
}