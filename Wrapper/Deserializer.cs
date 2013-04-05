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
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TradeMe.Api.Client
{
    /// <summary>
    /// A class for deserializing the xml returned from a call to the API.
    /// </summary>
    /// <typeparam name="T">The type of the object you wish to deserialize the xml object into.</typeparam>
    internal class Deserializer<T>
    {
        /// <summary>
        /// Deserializes the xml into and object the same type as T.
        /// </summary>
        /// <param name="type">An object of the type you want to deserialize the xml into.</param>
        /// <param name="xml">The xml you want to deserialize.</param>
        /// <returns>The xml deserialized into an object.</returns>
        public static T Deserialize(T type, string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                var reader = XmlReader.Create(new StringReader(xml));
                var serializer = new XmlSerializer(typeof(T));
                var result = (T)serializer.Deserialize(reader);
                return result;
            }

            return default(T);
        }
    }
}
