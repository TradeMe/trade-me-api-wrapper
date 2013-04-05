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
    /// The CatalogueMethods class contains the methods requried for making calls to the API related to the catalogue.
    /// </summary>
    internal class CatalogueMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the CatalogueMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public CatalogueMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

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
            var getRequest = _connection.UnauthenticatedConnection(query);
            var xml = getRequest.ToString();

            return Deserializer<Category>.Deserialize(new Category(), xml);
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
            var url = Constants.CATEGORIES;
            url += Constants.XML;
            return RetrieveCategoriesByQueryString(url);
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
            var url = Constants.CATEGORIES;
            if (string.IsNullOrEmpty(id))
            {
                url += Constants.XML;
            }
            else
            {
                url += String.Format(Constants.Culture, "/{0}{1}", id, Constants.XML);
            }

            return RetrieveCategoriesByQueryString(url);
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
            var url = String.Format(Constants.Culture, "{0}/UsedCars{1}", Constants.CATEGORIES, Constants.XML);

            return RetrieveCategoriesByQueryString(url);
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
            var url = String.Format(Constants.Culture, "{0}/MotorBikes{1}", Constants.CATEGORIES, Constants.XML);

            return RetrieveCategoriesByQueryString(url);
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
            var url = String.Format(Constants.Culture, "{0}/Jobs{1}", Constants.CATEGORIES, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<JobCategories>.Deserialize(new JobCategories(), xml);
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
            var getRequest = _connection.UnauthenticatedConnection(query);

            var xml = getRequest.ToString();

            return Deserializer<LocalityCollection>.Deserialize(new LocalityCollection(), xml);
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
            const string url = "Localities" + Constants.XML;
            return RetrieveLocalities(url);
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
            const string url = "TmAreas" + Constants.XML;
            return RetrieveLocalities(url);
        }

        /// <summary>
        /// <para>Performs the category method:
        /// Retrieves the list of attributes which are applicable to a specific category. GET
        /// </para>
        /// DOES NOT REQUIRE AUTHENTICATION.
        /// </summary>
        /// <param name="categoryNumber">The category number for which you wish to see the attributes for. 
        /// This must be a leaf category (for example, you cannot view the attributes for Computers > Desktops, 
        /// but you can for Computers > Desktops > CRT monitors).</param>
        /// <returns>Attributes</returns>
        public Attributes RetrieveAttributesForCategory(string categoryNumber)
        {
            var url = String.Format(Constants.Culture, "{0}/{1}/{2}{3}", Constants.CATEGORIES, categoryNumber,
                                    Constants.ATTRIBUTES, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<Attributes>.Deserialize(new Attributes(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/{2}{3}", Constants.CATEGORIES, categoryNumber,
                                    Constants.LEGALNOTICE, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<LegalNotice>.Deserialize(new LegalNotice(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/{2}{3}", Constants.CATEGORIES, categoryNumber,
                                    Constants.DURATIONS, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<ListingDurations>.Deserialize(new ListingDurations(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}/{2}{3}", Constants.CATEGORIES, categoryNumber,
                                    Constants.FEES, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<ListingFees>.Deserialize(new ListingFees(), xml);
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
            var url = String.Format(Constants.Culture, "{0}{1}", Constants.TRAVELAREAS, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<LocalityCollection>.Deserialize(new LocalityCollection(), xml);
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
            var url = String.Format(Constants.Culture, "{0}{1}", Constants.DVDVALIDATION, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<DvdValidations>.Deserialize(new DvdValidations(), xml);
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
            var url = String.Format(Constants.Culture, "{0}{1}", Constants.BLURAYVALIDATION, Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<BluRayValidations>.Deserialize(new BluRayValidations(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.BLURAY,
                                    "find", Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<MovieTitles>.Deserialize(new MovieTitles(), xml);
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
            var url = String.Format(Constants.Culture, "{0}/{1}{2}", Constants.DVD,
                                    "find", Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<MovieTitles>.Deserialize(new MovieTitles(), xml);
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
            var url = String.Format(Constants.Culture, "{0}{1}", "ComplaintSubjects", Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<ComplaintSubjectCollection>.Deserialize(new ComplaintSubjectCollection(), xml);
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
            var url = String.Format(Constants.Culture, "{0}{1}", "MotorbikeMakes", Constants.XML);

            var getRequest = _connection.UnauthenticatedConnection(url);
            var xml = getRequest.ToString();

            return Deserializer<MotorbikeMakeCollection>.Deserialize(new MotorbikeMakeCollection(), xml);
        }


    }
}
