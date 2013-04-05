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
    /// The FavouriteMethods class contains the methods requried for making calls to the API related to Favourites.
    /// </summary>
    internal class FavouriteMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the FavouriteMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public FavouriteMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

        // Favourite methods:

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Save Search. POST
        /// </para>
        /// <para>Serializes the given SaveSearchRequest into xml.</para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="saveSearch">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument SaveSearch(SaveSearchRequest saveSearch)
        {
            const string query = Constants.FAVOURITES + "/" + Constants.SEARCH + Constants.XML;
            return _connection.Post(saveSearch, query);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Save Category. POST
        /// </para>
        /// <para>Serializes the given SaveCategoryRequest into xml.</para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="saveCategory">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument SaveCategory(SaveCategoryRequest saveCategory)
        {
            const string query = Constants.FAVOURITES + "/" + Constants.CATEGORY + Constants.XML;
            return _connection.Post(saveCategory, query);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Save Seller. POST
        /// </para>
        /// <para>Serializes the given SaveSellerRequest into xml.</para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="saveSeller">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument SaveSeller(SaveSellerRequest saveSeller)
        {
            const string query = Constants.FAVOURITES + "/" + Constants.SELLER + Constants.XML;
            return _connection.Post(saveSeller, query);
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
            var query = String.Format("{0}/{1}/{2}{3}", Constants.FAVOURITES, favoriteId, type, Constants.XML);
            return _connection.Post(null, query, true);
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
            var query = String.Format("{0}/{1}/{2}/{3}{4}", Constants.FAVOURITES, favoriteId, type, frequency, Constants.XML);
            return _connection.Post(null, query);
        }

        /// <summary>
        /// <para>Performs the Favourites Method:
        /// Get Saved Categories. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>SavedCategories.</returns>
        public SavedCategories RetrieveFavouriteCategories()
        {
            var query = String.Format("{0}/{1}{2}", Constants.FAVOURITES, Constants.CATEGORIES, Constants.XML);

            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();
            return Deserializer<SavedCategories>.Deserialize(new SavedCategories(), xml);
        }

        /// <summary>
        /// <para>Performs the Favourites Method:
        /// Get Saved Searches. GET
        /// </para>
        /// REQURIES AUTHENTICATION.
        /// </summary>
        /// <returns>SavedSearches.</returns>
        public SavedSearches RetrieveFavouriteSearches(SavedSearchType filter)
        {
            var query = String.Format("{0}/{1}es/{2}{3}", Constants.FAVOURITES, Constants.SEARCH, filter, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();
            return Deserializer<SavedSearches>.Deserialize(new SavedSearches(), xml);
        }

        /// <summary>
        /// <para>Performs the Favourites method:
        /// Get Saved Sellers. GET
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>SavedSellers.</returns>
        public SavedSellers RetrieveFavouriteSellers()
        {
            var query = String.Format("{0}/{1}s{2}", Constants.FAVOURITES, Constants.SELLER, Constants.XML);
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();
            return Deserializer<SavedSellers>.Deserialize(new SavedSellers(), xml);
        }
    }
}
