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
    /// Sort the returned record-set by a single specified sort order.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Default.
        /// </summary>
        Default,

        /// <summary>
        /// Expiry Ascending.
        /// </summary>
        ExpiryAsc,

        /// <summary>
        /// Expiry Descending.
        /// </summary>
        ExpiryDesc,

        /// <summary>
        /// Price Ascending.
        /// </summary>
        PriceAsc,

        /// <summary>
        /// Price Descending.
        /// </summary>
        PriceDesc,

        /// <summary>
        /// Title Ascending.
        /// </summary>
        TitleAsc,

        /// <summary>
        /// Bids Most.
        /// </summary>
        BidsMost,

        /// <summary>
        /// Buy Now Ascending.
        /// </summary>
        BuyNowAsc,

        /// <summary>
        /// Buy Now Descending.
        /// </summary>
        BuyNowDesc,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// Sort the returned record-set by a single specified sort order. Specific to property searches.
    /// </summary>
    public enum PropertySortOrder
    {
        /// <summary>
        /// Default.
        /// </summary>
        Default,

        /// <summary>
        /// Expiry Ascending.
        /// </summary>
        ExpiryAsc,

        /// <summary>
        /// Expiry Descending.
        /// </summary>
        ExpiryDesc,

        /// <summary>
        /// Price Ascending.
        /// </summary>
        PriceAsc,

        /// <summary>
        /// Price Descending.
        /// </summary>
        PriceDesc,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The condition of an item.
    /// </summary>
    public enum Condition
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// New.
        /// </summary>
        New,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The transmission of a motor vehicle.
    /// </summary>
    public enum Transmission
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Manual.
        /// </summary>
        Manual,

        /// <summary>
        /// Automatic.
        /// </summary>
        Automatic,

        /// <summary>
        /// Triptronic.
        /// </summary>
        Triptronic,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The style for the body of car.
    /// </summary>
    public enum BodyStyle
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Convertible.
        /// </summary>
        Convertible,

        /// <summary>
        /// Coupe.
        /// </summary>
        Coupe,

        /// <summary>
        /// Hatchback.
        /// </summary>
        Hatchback,

        /// <summary>
        /// Sedan.
        /// </summary>
        Sedan,

        /// <summary>
        /// Station Wagon.
        /// </summary>
        StationWagon,

        /// <summary>
        /// Suv.
        /// </summary>
        Suv,

        /// <summary>
        /// Ute.
        /// </summary>
        Ute,

        /// <summary>
        /// Van.
        /// </summary>
        Van,
        
        /// <summary>
        /// Other.
        /// </summary>
        Other,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The listing type.
    /// </summary>
    public enum ListingType
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Private.
        /// </summary>
        Private,

        /// <summary>
        /// Dealer.
        /// </summary>
        Dealer,

        /// <summary>
        /// None.
        /// </summary>
        None
    }

    /// <summary>
    /// The type of a boat.
    /// </summary>
    public enum BoatType
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Fiberglass.
        /// </summary>
        Fiberglass,

        /// <summary>
        /// Aluminium.
        /// </summary>
        Aluminium,

        /// <summary>
        /// Wooden.
        /// </summary>
        Wooden,

        /// <summary>
        /// Inflatable.
        /// </summary>
        Inflatable,

        /// <summary>
        /// Steel.
        /// </summary>
        Steel,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The type of a bike.
    /// </summary>
    public enum BikeType
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Classic.
        /// </summary>
        Classic,

        /// <summary>
        /// Cruiser.
        /// </summary>
        Cruiser,

        /// <summary>
        /// Dirt.
        /// </summary>
        Dirt,

        /// <summary>
        /// Dual.
        /// </summary>
        Dual,

        /// <summary>
        /// Pocket.
        /// </summary>
        Pocket,

        /// <summary>
        /// Quad.
        /// </summary>
        Quad,

        /// <summary>
        /// Scooters.
        /// </summary>
        Scooters,

        /// <summary>
        /// Sports.
        /// </summary>
        Sports,

        /// <summary>
        /// Trousers.
        /// </summary>
        Trousers,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The type of a job.
    /// </summary>
    public enum JobType
    {
        /// <summary>
        /// Contract.
        /// </summary>
        Contract,

        /// <summary>
        /// Permanent.
        /// </summary>
        Permanent,

        /// <summary>
        /// FullTime.
        /// </summary>
        FullTime,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The type of a property.
    /// </summary>
    public enum PropertyType
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Apartment.
        /// </summary>
        Apartment,

        /// <summary>
        /// House.
        /// </summary>
        House,

        /// <summary>
        /// Section.
        /// </summary>
        Section,

        /// <summary>
        /// Townhouse.
        /// </summary>
        Townhouse,

        /// <summary>
        /// Unit.
        /// </summary>
        Unit,

        /// <summary>
        /// Other.
        /// </summary>
        Other,

        /// <summary>
        /// Villa.
        /// </summary>
        Villa,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The property usage.
    /// </summary>
    public enum PropertyUsage
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Carpark.
        /// </summary>
        Carpark,

        /// <summary>
        /// Delevopment Site.
        /// </summary>
        DevelopmentSite,

        /// <summary>
        /// Hotel Leisure.
        /// </summary>
        HotelLeisure,

        /// <summary>
        /// Industrial.
        /// </summary>
        Industrial,

        /// <summary>
        /// Office.
        /// </summary>
        Office,

        /// <summary>
        /// Retail.
        /// </summary>
        Retail,

        /// <summary>
        /// Tourism.
        /// </summary>
        Tourism,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The usage of property - specific to rural properties.
    /// </summary>
    public enum RuralPropertyUsage
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Dare Land.
        /// </summary>
        DareLand,

        /// <summary>
        /// Cropping.
        /// </summary>
        Cropping,

        /// <summary>
        /// Dairy.
        /// </summary>
        Dairy,

        /// <summary>
        /// Deer.
        /// </summary>
        Deer,

        /// <summary>
        /// Forestry.
        /// </summary>
        Forestry,

        /// <summary>
        /// High Country.
        /// </summary>
        HighCountry,

        /// <summary>
        /// Horticulture.
        /// </summary>
        Horticulture,

        /// <summary>
        /// Mixed.
        /// </summary>
        Mixed,

        /// <summary>
        /// Sheep Beef.
        /// </summary>
        SheepBeef,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The type of a property - specific to lifestyle properties.
    /// </summary>
    public enum LifestylePropertyType
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Dwelling.
        /// </summary>
        Dwelling,

        /// <summary>
        /// BareLand.
        /// </summary>
        BareLand,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The usage of a property - specific to lifestyle properties.
    /// </summary>
    public enum LifestylePropertyUsage
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Dwelling.
        /// </summary>
        Dwelling,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// The type of a property - specific to retirement villages.
    /// </summary>
    public enum RetirementVillagePropertyType
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Apartment.
        /// </summary>
        Apartment,

        /// <summary>
        /// House.
        /// </summary>
        House,

        /// <summary>
        /// Section.
        /// </summary>
        Section,

        /// <summary>
        /// Townhouse.
        /// </summary>
        Townhouse,

        /// <summary>
        /// Unit.
        /// </summary>
        Unit,

        /// <summary>
        /// Other.
        /// </summary>
        Other,

        /// <summary>
        /// Villa.
        /// </summary>
        Villa,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// Member Feedback Criteria.
    /// </summary>
    public enum MemberFeedbackCriteria
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Selling.
        /// </summary>
        Selling,

        /// <summary>
        /// Buying.
        /// </summary>
        Buying,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// Sold Items Criteria.
    /// </summary>
    public enum SoldItemsCriteria
    {
        /// <summary>
        /// Returns items sold in the last 45 days.
        /// </summary>
        Last45Days,	// Returns items sold in the last 45 days.
        
        /// <summary>
        /// Returns items sold in the last 30 days.
        /// </summary>
        Last30Days,	// Returns items sold in the last 30 days.
        
        /// <summary>
        /// Returns items sold in the last 7 days.
        /// </summary>
        Last7Days,	// Returns items sold in the last 7 days.
        
        /// <summary>
        /// Returns items sold in the last 3 days.
        /// </summary>
        Last3Days,	// Returns items sold in the last 3 days.
        
        /// <summary>
        /// Returns items sold in the last 24 hours.
        /// </summary>
        Last24Hours,	// Returns items sold in the last 24 hours.
        
        /// <summary>
        /// Returns items without sent payment instructions.
        /// </summary>
        PaymentInstructionsToSend, // Returns items without sent payment instructions.
        
        /// <summary>
        /// Returns items with pending feedback.
        /// </summary>
        FeedbackToPlace,	// Returns items with pending feedback.
        
        /// <summary>
        /// Returns sold items marked with "Email sent" status.
        /// </summary>
        EmailSent,	// Returns sold items marked with "Email sent" status.
        
        /// <summary>
        /// Returns sold items marked with "Payment received" status.
        /// </summary>
        PaymentReceived,	// Returns sold items marked with "Payment received" status.
        
        /// <summary>
        /// Returns sold items marked with "Goods shipped" status.
        /// </summary>
        GoodsShipped,	// Returns sold items marked with "Goods shipped" status.
        
        /// <summary>
        /// Returns sold items marked with "Sale completed" status.
        /// </summary>
        SaleCompleted, // Returns sold items marked with "Sale completed" status.
        
        /// <summary>
        /// Returns sold items marked with "Sale completed" status.
        /// </summary>
        None,
    }

    /// <summary>
    /// Unsold Items Criteria.
    /// </summary>
    public enum UnsoldItemsCriteria
    {
        /// <summary>
        /// Returns items sold in the last 45 days.
        /// </summary>
        Last45Days,
        
        /// <summary>
        /// Returns items sold in the last 7 days.
        /// </summary>
        Last7Days,
        
        /// <summary>
        /// Returns items sold in the last 24 hours.
        /// </summary>
        Last24Hours,
        
        /// <summary>
        /// Returns items with pending offers.
        /// </summary>
        ItemsIHaveOffered,
        
        /// <summary>
        /// Returns unsold items that can be relisted.
        /// </summary>
        ItemsICanRelist,
        
        /// <summary>
        /// None.
        /// </summary>
        None,
    }
    
    /// <summary>
    /// Selling Items Criteria.
    /// </summary>
    public enum SellingItemsCriteria
    {
        /// <summary>
        /// Returns all currently active listings.
        /// </summary>
        All,
        
        /// <summary>
        /// Return only listings that are due to close in 24 hours.
        /// </summary>
        ClosingToday,
        
        /// <summary>
        /// Returns listings with bids.
        /// </summary>
        ListingsWithBids,
        
        /// <summary>
        /// Returns listings where the reserve price is met.
        /// </summary>
        ReserveMet,
        
        /// <summary>
        /// Returns listings where the reserve price is not met.
        /// </summary>
        ReserveNotMet,
       
        /// <summary>
        /// Returns listings with unanswered questions.
        /// </summary>
        UnansweredQuestions,
        
        /// <summary>
        /// None.
        /// </summary>
        None,
    }
   
    /// <summary>
    /// Watchlist Criteria.
    /// </summary>
    public enum WatchlistCriteria
    {
        /// <summary>
        /// Returns all items from the Watchlist.
        /// </summary>
        All,
       
        /// <summary>
        /// Returns listings that are closing today.
        /// </summary>
        ClosingToday,
       
        /// <summary>
        /// Returns listings where the authenticated user leads the bidding.
        /// </summary>
        LeadingBids,
       
        /// <summary>
        /// Returns listings where highest bid is above the Reserve price or the Reserve price equals the Start price (= No reserve).
        /// </summary>
        ReserveMet,
        
        /// <summary>
        /// Returns only listings where the reserve has not been met.
        /// </summary>
        ReserveNotMet,
        
        /// <summary>
        /// None.
        /// </summary>
        None,
    }
    
    /// <summary>
    /// Won Items Criteria.
    /// </summary>
    public enum WonItemsCriteria
    {
        /// <summary>
        /// Return all won items.
        /// </summary>
        All,
        
        /// <summary>
        /// Return items won in the last 30 days.
        /// </summary>
        Last30Days,
       
        /// <summary>
        /// Return items won in the last 7 days.
        /// </summary>
        Last7Days,
        
        /// <summary>
        /// Return items won in the last 24 hours.
        /// </summary>
        Last24Hours,
       
        /// <summary>
        /// None.
        /// </summary>
        None,
    }
    
    /// <summary>
    /// Lost Items Criteria.
    /// </summary>
    public enum LostItemsCriteria
    {
        /// <summary>
        /// Return all lost items.
        /// </summary>
        All,
       
        /// <summary>
        /// Return lost items from the last 7 days.
        /// </summary>
        Last7Days,
       
        /// <summary>
        /// Return lost items from the last 24 hours.
        /// </summary>
        Last24Hours,
       
        /// <summary>
        /// None.
        /// </summary>
        None,
    }
   
    /// <summary>
    /// Member Ledger Criteria.
    /// </summary>
    public enum MemberLedgerCriteria
    {
        /// <summary>
        /// Return all member ledger records.
        /// </summary>
        All,
        
        /// <summary>
        /// Return member ledger records from the last 45 days.
        /// </summary>
        Last45Days,
       
        /// <summary>
        /// Return member ledger records from the last 28 days.
        /// </summary>
        Last28Days,
       
        /// <summary>
        /// Return member ledger records from the last 14 days.
        /// </summary>
        Last14Days,
        
        /// <summary>
        /// Return member ledger records from the last 7 days.
        /// </summary>
        Last7Days,
       
        /// <summary>
        /// Return member ledger records from the last 24 hours.
        /// </summary>
        Last24Hours,
        
        /// <summary>
        /// None.
        /// </summary>
        None,
    }
    
    /// <summary>
    /// Pay Now Ledger Criteria.
    /// </summary>
    public enum PayNowLedgerCriteria
    {
        /// <summary>
        /// Returns all Pay Now ledger records.
        /// </summary>
        All,
       
        /// <summary>
        /// Return only sale payments.
        /// </summary>
        Sales,
        
        /// <summary>
        /// Return only ledger records with payments to the member's bank account.
        /// </summary>
        PaymentsToMyBankAccount,
        
        /// <summary>
        /// Returns only Pay Now refunds.
        /// </summary>
        Refunds,
       
        /// <summary>
        /// None.
        /// </summary>
        None, 
    }
}
