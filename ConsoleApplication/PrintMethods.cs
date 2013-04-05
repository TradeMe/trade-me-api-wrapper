using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class PrintMethods
    {
        /// <summary>
        /// Returns a string representation of the given Category.
        /// </summary>
        /// <param name="categoryTest"></param>
        /// <returns>String representation of the given Category</returns>
        public static string PrintCategories(Category categoryTest)
        {
            var result = new StringBuilder();

            if (categoryTest != null)
            {
                result.AppendLine(Environment.NewLine + "The Category Object:");
                result.Append(PrintCategory(categoryTest, String.Empty) + Environment.NewLine);
            }
            return result.ToString();
        }

        /// <summary>
        /// Returns a string representation of the given Category - indenting for each subsequent sub-category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="tab"></param>
        /// <returns>String representation of the given Category - indenting for each subsequent sub-category</returns>
        public static string PrintCategory(Category category, string tab)
        {
            var result = new StringBuilder();

            result.AppendFormat("{0}{1}Name =  {2}", Environment.NewLine, tab, category.Name);

            if (category.CountSpecified)
            {
                result.AppendFormat("{0}{1}Count =  {2}", Environment.NewLine, tab, category.Count);
            }
            result.AppendFormat("{0}{1}Number =  {2}", Environment.NewLine, tab, category.Number);
            result.AppendFormat("{0}{1}Path =  {2}", Environment.NewLine, tab, category.Path);

            var subs = category.Subcategories;
            if (subs != null)
            {
                var t = tab + "\t";
                result.AppendFormat("{0}{1}Subcategories:  ", Environment.NewLine, tab);
                foreach (var c in subs)
                {
                    result.Append(String.Format("{0}{1}", PrintCategory(c, t), Environment.NewLine));
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Returns a string representation of the given Watchlist.
        /// </summary>
        /// <param name="watchList"></param>
        /// <returns>String representation of the given Watchlist.</returns>
        public static string PrintWatchlist(Watchlist watchList)
        {
            var result = new StringBuilder();
            if (watchList != null)
            {
                foreach (var item in watchList.List)
                {
                    result.AppendFormat("{0}As At: {1}", Environment.NewLine, item.AsAt);
                    result.AppendFormat("{0}Bid Count {1}", Environment.NewLine, item.BidCount);
                    result.AppendFormat("{0}Buy Now price {1}", Environment.NewLine, item.BuyNowPrice);
                    result.AppendFormat("{0}Category name: {1}", Environment.NewLine, item.CategoryName);
                    result.AppendFormat("{0}Has Pay Now: {1}", Environment.NewLine, item.HasPayNow);
                    result.AppendFormat("{0}Is New: {1}", Environment.NewLine, item.IsNew);
                    result.AppendFormat("{0}Title: {1}", Environment.NewLine, item.Title);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Returns a string representation of the given Listing
        /// </summary>
        /// <param name="listing"></param>
        /// <returns>String representation of the given Listing</returns>
        public static string PrintListingInto(ListedItemDetail listing)
        {
            var result = new StringBuilder();

            result.AppendFormat("{0}Title =  {1}", Environment.NewLine, listing.Title);
            result.AppendFormat("{0}Desc =  {1}", Environment.NewLine, listing.Body);
            result.AppendFormat("{0}BuyNow =  {1}{0}", Environment.NewLine, listing.BuyNowPrice);

            return result.ToString();
        }
    }
}
