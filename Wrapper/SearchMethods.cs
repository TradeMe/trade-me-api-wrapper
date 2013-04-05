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
    /// The SearchMethods class contains the methods requried for making calls to the API related to searching.
    /// </summary>
    internal class SearchMethods
    {
        // A helper boolean to know whether or not to add an &amp; sign to the query string 
        private bool _addAnd;
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the SearchMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public SearchMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<SearchResults>.Deserialize(new SearchResults(), xml);
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
            _addAnd = false;
            var url = String.Format(Constants.Culture, "{0}/General{1}", Constants.SEARCH, Constants.XML);
            var conditions = "?";

            // create the parameters for the query string
            conditions += SearchMethods.ConstructQueryHelper(Constants.CATEGORY, category, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.USER_REGION, string.Empty + userRegion, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SORT_ORDER, sortOrder.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BUY, string.Empty + buy, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAY, string.Empty + pay, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.CONDITION, condition.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DATE_FROM, Client.DateToStringConverter(dateFrom),
                                                             _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, string.Empty + page, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ROWS, string.Empty + rows, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.MEMBER_LISTING, string.Empty + memberListing,
                                                             _addAnd);

            // add the paramaters if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.SearchGeneral(url);
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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<MotorBoats>.Deserialize(new MotorBoats(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/Boats{2}", Constants.SEARCH, Constants.MOTORS, Constants.XML);
            _addAnd = false;

            // create the parameters for the query string
            var conditions = "?";
            conditions += SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SORT_ORDER, string.Empty + sortOrder, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MIN, string.Empty + priceMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MAX, string.Empty + priceMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.TYPE, type.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.LENGTH_MIN, string.Empty + lengthMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.LENGTH_MAX, string.Empty + lengthMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DATE_FROM, Client.DateToStringConverter(dateFrom), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, string.Empty + page, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ROWS, string.Empty + rows, _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.SearchMotorBoats(url);
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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<MotorBikes>.Deserialize(new MotorBikes(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/Bikes{2}", Constants.SEARCH, Constants.MOTORS, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SORT_ORDER, sortOrder.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MIN, string.Empty + priceMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MAX, string.Empty + priceMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.MAKE, make, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.TYPE, type.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.YEAR_MIN, string.Empty + yearMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.YEAR_MAX, string.Empty + yearMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ENERGY_SIZE_MIN, string.Empty + energySizeMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ENERGY_SIZE_MAX, string.Empty + energySizeMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DATE_FROM, Client.DateToStringConverter(dateFrom), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, string.Empty + page, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ROWS, string.Empty + rows, _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.SearchMotorBikes(url);
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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<Cars>.Deserialize(new Cars(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/Used{2}", Constants.SEARCH, Constants.MOTORS, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.USER_REGION, string.Empty + userRegion, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MIN, string.Empty + priceMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SORT_ORDER, string.Empty + sortOrder, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MAX, string.Empty + priceMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.MAKE, make, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.MODEL, model, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BODY_STYLE, bodyStyle.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DOORS_MIN, string.Empty + doorsMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DOORS_MAX, string.Empty + doorsMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.TRANSMISSION, transmission.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.YEAR_MIN, string.Empty + yearMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.YEAR_MAX, string.Empty + yearMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ENERGY_SIZE_MAX, string.Empty + energySizeMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ENERGY_SIZE_MIN, string.Empty + energySizeMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ODOMETER_MIN, string.Empty + odometerMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ODOMETER_MAX, string.Empty + odometerMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.LISTING_TYPE, listingType.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DATE_FROM, Client.DateToStringConverter(dateFrom), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, string.Empty + page, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ROWS, string.Empty + rows, _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.SearchUsedMotors(url);
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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<Flatmates>.Deserialize(new Flatmates(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/Flatmates{1}", Constants.SEARCH, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SORT_ORDER, sortOrder.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, string.Empty + page, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ROWS, string.Empty + rows, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.REGION, string.Empty + region, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DISTRICT, string.Empty + district, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SUBURB, string.Empty + suburb, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DATE_FROM, Client.DateToStringConverter(dateFrom), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MIN, string.Empty + priceMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MAX, string.Empty + priceMax, _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.SearchFlatmates(url);
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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<RetirementVillages>.Deserialize(new RetirementVillages(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/Retirement{2}", Constants.SEARCH, Constants.PROPERTY, Constants.XML);

            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SORT_ORDER, sortOrder.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, string.Empty + page, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ROWS, string.Empty + rows, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.REGION, string.Empty + region, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DISTRICT, string.Empty + district, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SUBURB, string.Empty + suburb, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DATE_FROM, Client.DateToStringConverter(dateFrom), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MIN, string.Empty + priceMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MAX, string.Empty + priceMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BEDROOMS_MIN, string.Empty + bedroomsMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BEDROOMS_MAX, string.Empty + bedroomsMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BATHROOMS_MIN, string.Empty + bathroomsMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BATHROOMS_MAX, string.Empty + bathroomsMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.AREA_MIN, string.Empty + areaMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.AREA_MAX, string.Empty + areaMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.LAND_AREA_MIN, string.Empty + landAreaMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.LAND_AREA_MAX, string.Empty + landAreaMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PROPERTY_TYPE, propertyType.ToString(), _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.SearchRetirementVillages(url);
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
            var url = _connection.BaseUrl + query;
            return this.PropertyConnectionHelper(url);
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
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/Residential{3}", _connection.BaseUrl, Constants.SEARCH, Constants.PROPERTY, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += this.PropertyHelper(searchString, sortOrder.ToString(), page, rows, region, district, suburb, dateFrom, priceMin, priceMax, landAreaMin, landAreaMax);
            conditions += ResidentialPropertyHelper(bathroomsMin, bathroomsMax, bedroomsMax, bedroomsMin, areaMax, areaMin, propertyType.ToString(), adjacentSuburbs);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.PropertyConnectionHelper(url);
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
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/Rental{3}", _connection.BaseUrl, Constants.SEARCH, Constants.PROPERTY, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += this.PropertyHelper(searchString, sortOrder.ToString(), page, rows, region, district, suburb, dateFrom, priceMin, priceMax, landAreaMin, landAreaMax);
            conditions += ResidentialPropertyHelper(bathroomsMin, bathroomsMax, bedroomsMax, bedroomsMin, areaMax, areaMin, propertyType.ToString(), adjacentSuburbs);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.PropertyConnectionHelper(url);
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
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/OpenHomes{3}", _connection.BaseUrl, Constants.SEARCH, Constants.PROPERTY, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += this.PropertyHelper(searchString, sortOrder.ToString(), page, rows, region, district, suburb, dateFrom, priceMin, priceMax, landAreaMin, landAreaMax);
            conditions += ResidentialPropertyHelper(bathroomsMin, bathroomsMax, bedroomsMax, bedroomsMin, areaMax, areaMin, propertyType.ToString(), adjacentSuburbs);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.PropertyConnectionHelper(url);
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
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/CommercialSale{3}", _connection.BaseUrl, Constants.SEARCH, Constants.PROPERTY, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += this.PropertyHelper(searchString, sortOrder.ToString(), page, rows, region, district, suburb, dateFrom, priceMin, priceMax, landAreaMin, landAreaMax);
            conditions += ResidentialPropertyHelper(bathroomsMin, bathroomsMax, bedroomsMax, bedroomsMin, areaMax, areaMin, null, adjacentSuburbs);
            conditions += SearchMethods.ConstructQueryHelper(Constants.USAGE, usage.ToString(), _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.PropertyConnectionHelper(url);
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
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/CommercialLease{3}", _connection.BaseUrl, Constants.SEARCH, Constants.PROPERTY, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += this.RuralPropertiesHelper(
                searchString,
                sortOrder.ToString(),
                page,
                rows,
                region,
                district,
                suburb,
                dateFrom,
                priceMin,
                priceMax,
                areaMax,
                areaMin,
                landAreaMin,
                landAreaMax,
                adjacentSuburbs,
                usage.ToString());

            // add the parametrs to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.PropertyConnectionHelper(url);
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
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/Rural{3}", _connection.BaseUrl, Constants.SEARCH, Constants.PROPERTY, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += this.RuralPropertiesHelper(
                searchString,
                sortOrder.ToString(),
                page,
                rows,
                region,
                district,
                suburb,
                dateFrom,
                priceMin,
                priceMax,
                null,
                null,
                landAreaMin,
                landAreaMax,
                null,
                usage.ToString());

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.PropertyConnectionHelper(url);
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
            var url = String.Format(Constants.Culture, "{0}{1}/{2}/CommercialSale{3}", _connection.BaseUrl, Constants.SEARCH, Constants.PROPERTY, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += this.PropertyHelper(searchString, sortOrder.ToString(), page, rows, region, district, suburb, dateFrom, priceMin, priceMax, landAreaMin, landAreaMax);
            conditions += ResidentialPropertyHelper(bathroomsMin, bathroomsMax, bedroomsMax, bedroomsMin, areaMax, areaMin, propertyType.ToString(), adjacentSuburbs);
            conditions += SearchMethods.ConstructQueryHelper(Constants.USAGE, usage.ToString(), _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.PropertyConnectionHelper(url);
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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<Jobs>.Deserialize(new Jobs(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/Jobs{1}", Constants.SEARCH, Constants.XML);
            _addAnd = false;
            var conditions = "?";

            // create the parameters for the query string
            conditions += SearchStringToDistrict(searchString, string.Empty + sortOrder, page, rows, region, district);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SALARY_MIN, string.Empty + salaryMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SALARY_MAX, string.Empty + salaryMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.TYPE, string.Empty + type, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.CATEGORY, category, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SUBCATEGORY, subcategory, _addAnd);

            // add the parameters to the query string if there are any
            if (conditions.Equals("?"))
            {
                url += conditions;
            }

            // perform the request
            return this.SearchJobs(url);
        }

        /// <summary>
        /// <para>Performs the method:</para>
        /// <para>Search for Stores.</para>
        /// <para>As specified in the API Documentation.</para>
        /// <para>Creates a query string using the paramaters provided - parameters can be null if they are not required for the request.</para>
        /// <para> DOES NOT REQURE AUTHENTICATION.</para>
        /// </summary>
        /// <param name="searchString">The string to search for.</param>
        /// <param name="page">The number of the page to retrieve.</param>
        /// <param name="category">The category to search in.</param>
        /// <param name="storeType">The type of the store.</param>
        /// <returns>Stores</returns>
        public Stores SearchStores(string searchString = "", int page = 1, string category = "", StoreType storeType = StoreType.Normal)
        {

            var url = String.Format(Constants.Culture, "{0}/Stores{1}", Constants.SEARCH, Constants.XML);
            var conditions = "?";
            _addAnd = false;
            conditions += category != ""
                              ? SearchMethods.ConstructQueryHelper(Constants.CATEGORY, category, _addAnd)
                              : "";
            conditions += SearchMethods.ConstructQueryHelper("store_type", storeType.ToString(), _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, page.ToString(), _addAnd);
            conditions += searchString != ""
                              ? SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd)
                              : "";

            if (!conditions.Equals("?"))
            {
                url += conditions;
            }

            var finalUrl = _connection.BaseUrl + url;

            var getRequest = _connection.UnauthenticatedConnection(finalUrl);
            var xml = getRequest.ToString();

            return Deserializer<Stores>.Deserialize(new Stores(), xml);
        }

        /// <summary>
        /// A helper method for constructing the "page" and "row" part of a request query.
        /// </summary>
        /// <param name="page">	Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// /// <param name="addAnd">Whether or not an and symbol needs to be added.</param>
        /// <returns>String.</returns>
        internal static string PageAndRowsHelper(string page, string rows, bool addAnd)
        {
            var conditions = string.Empty;
            conditions += ConstructQueryHelper(Constants.PAGE, page, addAnd);
            conditions += ConstructQueryHelper(Constants.ROWS, rows, addAnd);
            return conditions;
        }

        /// <summary>
        /// A helper method for constructing a query string in the form: "name = value" or "&amp;name = value".
        /// </summary>
        /// <param name="name">The name of the paramater to put in the query string.</param>
        /// <param name="value">The value to assign to the paramater in the query string.</param>
        /// <param name="addAnd">Whether or not an and symbol needs to be added.</param>
        /// <returns>This part of the query string.</returns>
        private static string ConstructQueryHelper(string name, string value, bool addAnd)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.Equals(value, "null"))
                {
                    if (!string.Equals(value, "None"))
                    {
                        var toAdd = String.Format(Constants.Culture, "{0}={1}", name, value);
                        addAnd = true;
                        return Client.AddAndHelper(addAnd, toAdd);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// A helper method for constructing a portion of the rural property query string.
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
        /// <returns>String.</returns>
        private string RuralPropertiesHelper(
            string searchString,
            string sortOrder,
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
            string usage)
        {
            var conditions = string.Empty;
            conditions += this.PropertyHelper(searchString, sortOrder, page, rows, region, district, suburb, dateFrom, priceMin, priceMax, landAreaMin, landAreaMax);
            conditions += SearchMethods.ConstructQueryHelper(Constants.AREA_MAX, string.Empty + areaMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.AREA_MIN, string.Empty + areaMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ADJACENT_SUBURBS, string.Empty + adjacentSuburbs, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.USAGE, usage, _addAnd);
            return conditions;
        }

        /// <summary>
        /// A helper method for performing the http request for property requests - using the url provided.
        /// </summary>
        /// <param name="url">The url used to connect to the API.</param>
        /// <returns>Properties.</returns>
        private global::Properties PropertyConnectionHelper(string url)
        {
            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<global::Properties>.Deserialize(new global::Properties(), xml);
        }

        /// <summary>
        /// A helper method for constructing a portion of the query string for residential property requests.
        /// </summary>
        /// <param name="bathroomsMin">Minimum number of bathrooms.</param>
        /// <param name="bathroomsMax">Maximum number of bathrooms.</param>
        /// <param name="bedroomsMax">Maximum number of bedrooms.</param>
        /// <param name="bedroomsMin">Minimum number of bedrooms.</param>
        /// <param name="areaMax">	Maximum floor area in square meters.</param>
        /// <param name="areaMin">Minimum floor area in square meters.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="adjacentSuburbs">Indicates whether the search should include listings in adjacent suburbs.</param>
        /// <returns>String.</returns>
        private string ResidentialPropertyHelper(
            int? bathroomsMin,
            int? bathroomsMax,
            int? bedroomsMax,
            int? bedroomsMin,
            int? areaMax,
            int? areaMin,
            string propertyType,
            bool? adjacentSuburbs)
        {
            var conditions = string.Empty;
            conditions += SearchMethods.ConstructQueryHelper(Constants.BATHROOMS_MIN, string.Empty + bathroomsMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BATHROOMS_MAX, string.Empty + bathroomsMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BEDROOMS_MAX, string.Empty + bedroomsMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.BATHROOMS_MIN, string.Empty + bedroomsMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.AREA_MAX, string.Empty + areaMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.AREA_MIN, string.Empty + areaMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PROPERTY_TYPE, propertyType, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ADJACENT_SUBURBS, string.Empty + adjacentSuburbs, _addAnd);
            return conditions;
        }

        /// <summary>
        /// A helper method for construction a portion of the query string needed for property requests.
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
        /// <returns>String.</returns>
        private string PropertyHelper(
            string searchString,
            string sortOrder,
            int? page,
            int? rows,
            int? region,
            int? district,
            int? suburb,
            DateTime dateFrom,
            int? priceMin,
            int? priceMax,
            int? landAreaMin,
            int? landAreaMax)
        {
            var conditions = string.Empty;
            conditions += SearchStringToDistrict(searchString, sortOrder, page, rows, string.Empty + region, string.Empty + district);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SUBURB, string.Empty + suburb, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DATE_FROM, string.Empty + dateFrom, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MIN, string.Empty + priceMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PRICE_MAX, string.Empty + priceMax, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.LAND_AREA_MIN, string.Empty + landAreaMin, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.LAND_AREA_MAX, string.Empty + landAreaMax, _addAnd);

            return conditions;
        }

        /// <summary>
        /// A helper method for construction part of the query string for requests.
        /// </summary>
        /// <param name="searchString">One or more keywords to use in a search query.</param>
        /// <param name="sortOrder">Sort the returned record-set by a single specified sort order.</param>
        /// <param name="page">	Page number.</param>
        /// <param name="rows">Number of rows per page.</param>
        /// <param name="region">Specifies the search region ID.</param>
        /// <param name="district">Specifies the search district ID.</param>
        /// <returns>This section of the query string.</returns>
        private string SearchStringToDistrict(
            string searchString,
            string sortOrder,
            int? page,
            int? rows,
            string region,
            string district)
        {
            var conditions = string.Empty;
            conditions += SearchMethods.ConstructQueryHelper(Constants.SEARCH_STRING, searchString, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.SORT_ORDER, sortOrder, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.PAGE, string.Empty + page, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.ROWS, string.Empty + rows, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.REGION, string.Empty + region, _addAnd);
            conditions += SearchMethods.ConstructQueryHelper(Constants.DISTRICT, string.Empty + district, _addAnd);
            return conditions;
        }
    }
}
