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

namespace TradeMe.Api.Client
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// This exception is thrown when the verification code is null. The Verification code is needed to get an Access Token from the API.
    /// </summary>
    [Serializable]
    public class NoVerificationCodeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the NoVerificationCodeException class. This exception is thrown when the verification code is null. The Verification code is needed to get an Access Token from the API.
        /// </summary>
        public NoVerificationCodeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the NoVerificationCodeException class. This exception is thrown when the verification code is null. The Verification code is needed to get an Access Token from the API.
        /// </summary>
        /// <param name="message">The Message.</param>
        public NoVerificationCodeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NoVerificationCodeException class. This exception is thrown when the verification code is null. The Verification code is needed to get an Access Token from the API.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="innerException">The Inner Exception.</param>
        public NoVerificationCodeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NoVerificationCodeException class. This exception is thrown when the verification code is null. The Verification code is needed to get an Access Token from the API.
        /// </summary>
        /// <param name="info">The Information.</param>
        /// <param name="context">The Context.</param>
        protected NoVerificationCodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
