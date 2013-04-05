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
using System.Diagnostics;
using TradeMe.Api.Client;

namespace ConsoleApplication
{

    public class ConsoleClient
    {
        private Client client = new Client();

        /// <summary>
        /// Brings up a list of commands for the user to choose from.
        /// </summary>
        public void Start()
        {
            Console.Write("Command: " + Environment.NewLine);
            Console.Write(Environment.NewLine + "cat: \t Get a list of Categories - does not require authentication");
            Console.Write(Environment.NewLine + "aut: \t Start the authentication process with the API");
            Console.Write(Environment.NewLine + "wat: \t Get a members Watchlist - this requires authentication");
            Console.Write(Environment.NewLine + "GetListing: \t Get a listing by ID - does not require authentication");
            Console.Write(Environment.NewLine + "SearchGeneral: \t Search listings wih query string - does not require authentication");
            Console.Write(Environment.NewLine + "AttributesForCat: \t Retrieves the list of attributes for a specific category - does not require authentication");
            Console.Write(String.Format("{0}{0}Enter your command :{0}", Environment.NewLine));

            var readLine = Console.ReadLine();
            if (readLine == null) return;
            var input = readLine.Trim().ToLower();
            //var method
            switch (input)
            {
                case "cat":
                    GetCategories();
                    break;

                case "aut":
                    AuthenticationSteps();
                    break;

                case "wat":
                    GetWatchlist();
                    break;

                case "getlisting":
                    GetListing();
                    break;

                case "searchgeneral":
                    SearchGeneral();
                    break;

                case "attributesforcat":
                    AttributesForCategory();
                    break;

                case "addtoblacklist":
                    AddMemberToBlackList();
                    break;

                case "addnote":
                    AddNoteToListing();
                    break;

                case "getblacklist":
                    GetBlacklist();
                    break;

                case "gettravellocality":
                    GetTravelLocalities();
                    break;

                case "weeklystats":
                    GetWeeklySalesStats();
                    break;
                
                default:
                    Start();
                    break;
            }
        }


        /// <summary>
        /// Get a listing details by listing ID - does not require authentication.
        /// </summary>
        private void GetListing()
        {
            try
            {
                Console.Write("{0}{0}Enter listing ID:{0}", Environment.NewLine);

                var readLine = Console.ReadLine();
                if (readLine != null)
                {
                    var listingId = readLine.Trim();

                    var listing = client.ListingDetailById(listingId);

                    Console.Write(Environment.NewLine + PrintMethods.PrintListingInto(listing));
                }
            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }
            
            
            Start();
        }

        private void AddMemberToBlackList()
        {
            try
            {
                Console.Write("{0}{0}Enter ID of the member you want to blacklist :{0}", Environment.NewLine);

                var readLine = Console.ReadLine();
                if (readLine != null)
                {
                    var memberId = Int32.Parse(readLine.Trim());
                    var request = new BlacklistRequest {MemberId = memberId, MemberIdSpecified = true};


                    var result = client.AddMemberToBlackList(request);                    
                }
            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }


            Start();
        }

        private void GetBlacklist()
        {
            try
            {
                var result = client.GetBlacklistedMembers();
            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }


            Start();
        }

        /// <summary>
        /// Get a listing details by listing ID - does not require authentication.
        /// </summary>
        private void SearchGeneral()
        {
            try
            {
                Console.Write("{0}{0}Enter query string:{0}", Environment.NewLine);

                var readLine = Console.ReadLine();
                if (readLine != null)
                {
                    var query = readLine.Trim();

                    var result = client.SearchGeneral(query);

                    if (result.TotalCount > 0)
                    {
                        Console.Write("{0}Found {1} results:{0}", Environment.NewLine, result.TotalCount);
                        foreach (var listing in result.List)
                        {
                            Console.Write(listing.Title + Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }


            Start();
        }

        /// <summary>
        /// Retrieves the list of attributes for a specific category - does not require authentication.
        /// </summary>
        private void AttributesForCategory()
        {
            try
            {
                Console.Write("{0}{0}Enter category number:{0}", Environment.NewLine);

                var readLine = Console.ReadLine();
                if (readLine != null)
                {
                    var catNumber = readLine.Trim();

                    var result = client.RetrieveAttributesForCategory(catNumber);

                    if (result.Attribute.Length > 0)
                    {
                        foreach (var attribute in result.Attribute)
                        {
                            Console.Write("Name : {0}; Display Name : {1}{2}", attribute.Name, attribute.DisplayName, Environment.NewLine);
                        }
                    }
                    else
                    {
                        Console.Write("Category has no attributes or it's not a leaf category");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }


            Start();
        }

        /// <summary>
        /// Get a list of Categories - does not require authentication.
        /// </summary>
        private void GetCategories()
        {
            try
            {
                Console.Write("Un-Authenticated Call:" + Environment.NewLine);
                Console.Write("Retrieve Categories:" + Environment.NewLine);

                //get a list of categories
                var cat = client.RetrieveCategories();

                Console.Write(Environment.NewLine + PrintMethods.PrintCategories(cat));
            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }

            //return to the command list
            Start();
        }

        private void GetTravelLocalities()
        {
            var cat = client.RetrieveTravelLocalities();
            Start();
        }

        private void GetWeeklySalesStats()
        {
            var stats = client.GetWeeklySalesStats();
        }

        /// <summary>
        /// Performs the steps required to enable requests to the API that require authentication.
        /// </summary>
        private void AuthenticationSteps()
        {
            try
            {
                Console.Write("Authentication steps:{0}", Environment.NewLine);

                //set the consumer key and token to the values given to you by TradeMe
                Console.Write(String.Format("{0}Enter your Consumer Key:{0}", Environment.NewLine));
                var readLine = Console.ReadLine();
                if (readLine != null) client.ConsumerKey = readLine.Trim();

                Console.Write(String.Format("{0}Enter your Consumer Secret:{0}", Environment.NewLine));
                var line = Console.ReadLine();
                if (line != null) client.ConsumerSecret = line.Trim();

                //step one
                Console.Write("Step One - get the verification code:" + Environment.NewLine);

                //returns the link to the verification code
                var link = client.GetVerificationCode();

                //opens the link in a web browser
                Process.Start(link);

                Console.Write("Go to this address to get the verification code:{0}{1}", Environment.NewLine, link);
                Console.Write("{0}{0}Enter the verification code:\n", Environment.NewLine);

                //read the verification code in from the console
                var code = Console.ReadLine();

                Console.Write("you wrote: {0}{1}", code, Environment.NewLine);
                Console.Write("Step Two - use the verification code to get an access token from the API:{0}", Environment.NewLine);

                //step two - use the code to get an access token
                client.AuthenticateWithVerificationCode(code);

                Console.Write("You have completed the Authentication process:" + Environment.NewLine);


            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }

            //return to the command list
            Start();
        }

        /// <summary>
        /// Gets a members Watchlist - requires authentication.
        /// </summary>
        private void GetWatchlist()
        {
            //get the watchlist
            var watch = client.Watchlist(WatchlistCriteria.None, null, null);

            //display it in the console
            Console.Write("{0}Watchlist Response:{0}", Environment.NewLine);
            Console.Write(PrintMethods.PrintWatchlist(watch));

            //return to the command list
            Start();
        }

        //helper print methods



        private void AddNoteToListing()
        {
            try
            {
                Console.Write("{0}{0}Enter ID of the member you want to blacklist :{0}", Environment.NewLine);

                var readLine = Console.ReadLine();
                if (readLine != null)
                {
                    var memberId = Int32.Parse(readLine.Trim());
                    var request = new BlacklistRequest { MemberId = memberId, MemberIdSpecified = true };


                    var result = client.AddMemberToBlackList(request);


                }
            }
            catch (Exception e)
            {
                Console.Write("Exception occured: " + e);
            }


            Start();
        }
    }
}