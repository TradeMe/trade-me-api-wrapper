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
    /// This exception is thrown when the Access Token is null. The Access Token is used in an authorised request to the API.
    /// </summary>
    [Serializable]
    public class AccessTokenIsNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AccessTokenIsNullException class. This exception is thrown when the Access Token is null. The Access Token is used in an authorised request to the API.
        /// </summary>
        public AccessTokenIsNullException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AccessTokenIsNullException class. This exception is thrown when the Access Token is null. The Access Token is used in an authorised request to the API.
        /// </summary>
        /// <param name="message">The Message.</param>
        public AccessTokenIsNullException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AccessTokenIsNullException class. This exception is thrown when the Access Token is null. The Access Token is used in an authorised request to the API.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="innerException">The Inner Exception.</param>
        public AccessTokenIsNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AccessTokenIsNullException class. This exception is thrown when the Access Token is null. The Access Token is used in an authorised request to the API.
        /// </summary>
        /// <param name="info">The Infomation.</param>
        /// <param name="context">The Context.</param>
        protected AccessTokenIsNullException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
