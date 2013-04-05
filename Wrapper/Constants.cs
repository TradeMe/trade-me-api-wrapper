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

using System.Globalization;

namespace TradeMe.Api.Client
{
    /// <summary>
    /// The Constants class contains all the constants used for making calls to the API.
    /// </summary>
    internal class Constants
    {
        public const string XML = ".xml";

        // request methods
        public const string POST = "POST";
        public const string GET = "GET";
        public const string DELETE = "DELETE";
        

        // query methods
        public const string SEARCH = "Search";
        public const string MOTORS = "Motors";
        public const string PROPERTY = "Property";
        public const string MEMBER = "Member";
        public const string LISTINGS = "Listings";
        public const string SELLING = "Selling";
        public const string MY_TRADEME = "MyTradeMe";
        public const string BIDDING = "Bidding";
        public const string QUESTIONS = "questions";
        public const string WATCHLIST = "WatchList";
        public const string CATEGORIES = "Categories";
        public const string PHOTOS = "Photos";
        public const string ATTRIBUTES = "Attributes";
        public const string FEES = "Fees";
        public const string DURATIONS = "Durations";
        public const string LEGALNOTICE = "LegalNotice";
        public const string BLURAY = "Bluray";
        public const string BLURAYVALIDATION = "BluRayValidation";
        public const string DVD = "dvd";
        public const string DVDVALIDATION = "DvdValidation";
        public const string TRAVELAREAS = "TravelAreas";
        public const string EDIT = "Edit";
        public const string WITHDRAW = "Withdraw";
        public const string RELIST = "Relist";
        public const string SIMILAR = "Similar";
        public const string SELLCATALOGUEDVD = "SellCatalogueDVD";
        public const string SELLCATALOGUEBLURAY = "SellCatalogueBluRay";
        public const string DETAILS = "Details";
        public const string NOTES = "Notes";
        public const string FIXEDPRICEOFFER = "FixedPriceOffers";


        // paramaters for requests
        public const string CATEGORY = "category";
        public const string SEARCH_STRING = "search_string";
        public const string USER_REGION = "user_region";
        public const string SORT_ORDER = "sort_order";
        public const string BUY = "buy";
        public const string PAY = "pay";
        public const string CONDITION = "condition";
        public const string DATE_FROM = "date_from";
        public const string PAGE = "page";
        public const string MEMBER_LISTING = "member_listing";
        public const string ROWS = "rows";
        public const string PRICE_MIN = "price_min";
        public const string PRICE_MAX = "price_max";
        public const string LENGTH_MIN = "length_min";
        public const string LENGTH_MAX = "length_max";
        public const string TYPE = "type";

        public const string ENERGY_SIZE_MAX = "energy_size_max";
        public const string ENERGY_SIZE_MIN = "energy_size_min";
        public const string YEAR_MAX = "year_max";
        public const string YEAR_MIN = "year_min";
        public const string MAKE = "make";

        public const string MODEL = "model";
        public const string TRANSMISSION = "transmission";
        public const string BODY_STYLE = "body_style";
        public const string ODOMETER_MAX = "odometer_max";
        public const string ODOMETER_MIN = "odometer_min";

        public const string DOORS_MIN = "doors_min";
        public const string DOORS_MAX = "doors_max";
        public const string LISTING_TYPE = "listing_type";

        public const string SUBURB = "suburb";
        public const string DISTRICT = "district";
        public const string REGION = "region";

        public const string LAND_AREA_MIN = "land_area_min";
        public const string LAND_AREA_MAX = "land_area_max";
        public const string BATHROOMS_MIN = "bathrooms_min";
        public const string BATHROOMS_MAX = "bathrooms_max";
        public const string BEDROOMS_MIN = "bedrooms_min";
        public const string BEDROOMS_MAX = "bedrooms_max";
        public const string AREA_MIN = "area_min";
        public const string AREA_MAX = "area_max";
        public const string PROPERTY_TYPE = "property_type";

        public const string ADJACENT_SUBURBS = "adjacent_suburbs";
        public const string USAGE = "usage";
        public const string SALARY_MIN = "salary_min";
        public const string SALARY_MAX = "salary_max";
        public const string SUBCATEGORY = "subcategory";

        // saved favourites constants
        public const string FAVOURITES = "Favourites";
        public const string SELLER = "Seller";

        // culture information
        private static CultureInfo _culture = new CultureInfo("en-US");
        
        /// <summary>
        /// The culture information used in methods such as String.Format calls.
        /// </summary>
        public static CultureInfo Culture
        {
            get { return Constants._culture; }
            set { Constants._culture = value; }
        }
    }
}
