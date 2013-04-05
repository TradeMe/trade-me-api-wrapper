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
using System.Text;
using System.Xml.Linq;

namespace TradeMe.Api.Client
{
    /// <summary>
    /// The PhotoMethods class contains the methods requried for making calls to the API related to photos.
    /// </summary>
    internal class PhotoMethods
    {
        private readonly ConnectionMethods _connection;

        /// <summary>
        /// Initializes a new instance of the PhotoMethods class.
        /// </summary>
        /// <param name="connect">A ConnectionMethods class used to make calls to the API</param>
        public PhotoMethods(ConnectionMethods connect)
        {
            _connection = connect;
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Retrieve a list of member photos
        /// </para><para>
        /// using the "query" string provided - should be the  "Photos.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>MemberPhotos.</returns>
        public MemberPhotos MemberPhotos(string query)
        {
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();
            return Deserializer<MemberPhotos>.Deserialize(new MemberPhotos(), xml);
        }

        /// <summary>
        /// Performs the Photo Method:
        /// Retrieve a list of member photos.
        /// <para>
        /// Creates a query string and performs the request.
        /// </para><para>
        /// REQUIRES AUTHENTICATION.</para>
        /// </summary>
        /// <returns>MemberPhotos.</returns>
        public MemberPhotos MemberPhotos()
        {
            const string query = Constants.PHOTOS + Constants.XML;

            return MemberPhotos(query);
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Returns a list of the photos for the authenticated user, with extra usage data.
        /// </para><para>
        /// using the "query" string provided - should be the  "Photos/Details.xml" part of the url.
        /// It shouldn't include "http://api.trademe.co.nz/v1/".
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="query">The query string that will be added to the base url and used to connect to the API.</param>
        /// <returns>MemberPhotos.</returns>
        public MemberPhotos MemberPhotoDetails(string query)
        {
            var getRequest = _connection.AuthenticatedQuery(query);
            var xml = getRequest.ToString();

            return Deserializer<MemberPhotos>.Deserialize(new MemberPhotos(), xml);
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Returns a list of the photos for the authenticated user, with extra usage data.
        /// </para><para>
        /// Creates a query string and performs the request.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <returns>MemberPhotos.</returns>
        public MemberPhotos MemberPhotoDetails()
        {
            var query = String.Format("{0}/{1}{2}", Constants.PHOTOS, Constants.DETAILS, Constants.XML);
            return MemberPhotoDetails(query);
        }

        /// <summary>
        /// <para>
        /// Performs the Photo Method:
        /// Upload a photo.
        /// </para><para>
        /// Serializes the given PhotoUploadRequest into xml and sends the message.
        /// Loads the file and converts it to the appropriate data format for the request - it does not require the "PhotoData" field of the PhotoUploadRequest object to have anything in it.
        ///  </para>
        /// </summary>
        /// <param name="up">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument UploadPhotoFormat(PhotoUploadRequest up)
        {
            var fileName = up.FileName;

            var fs = File.OpenRead(fileName);
            var data = new byte[fs.Length];

            // read in the file to a byte array
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var offset = 0;
                var remaining = data.Length;
                while (remaining > 0)
                {
                    var read = stream.Read(data, offset, remaining);
                    if (read <= 0)
                    {
                        throw new EndOfStreamException(String.Format(Constants.Culture, "End of stream reached with {0} bytes left to read", remaining));
                    }

                    remaining -= read;
                    offset += read;
                }
            }

            // The data in the request is a base64 encoded string of the binary data in the photo.
            var endData = Convert.ToBase64String(data);

            // put the data in the object that will be converted to xml and posted
            up.PhotoData = endData;

            // send the post method
            return _connection.Post(up, "Photos.xml");
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Upload a photo.
        /// </para>
        /// Serializes the given PhotoUploadRequest into xml and sends the message.
        /// </summary>
        /// <param name="up">The object that will be serialized into xml and then sent in a POST message.</param>
        /// <returns>XDocument.</returns>
        public XDocument UploadPhoto(PhotoUploadRequest up)
        {
            // send the post method
            return _connection.Post(up, "Photos.xml");
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Remove a photo.
        /// </para><para>
        /// Creates a query string using the photo id provided.
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="photoId">The id of the photo to be removed.</param>
        /// <returns>RemovePhoto.</returns>
        public string RemovePhoto(string photoId)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}/Remove{2}", Constants.PHOTOS, photoId, Constants.XML);
            return _connection.Post(null, query, true).ToString();
        }

        /// <summary>
        /// <para>Performs the Photo Method:
        /// Adds a photo to an auction. The currently authenticated user must be the seller. POST
        /// </para>
        /// REQUIRES AUTHENTICATION.
        /// </summary>
        /// <param name="photoId">The id of the photo to add.</param>
        /// /// <param name="listingId">The ID of the listing to add the photo to.</param>
        /// <returns>XDocument: PhotoResponse.</returns>
        public XDocument AddPhotoToListing(string photoId, string listingId)
        {
            var query = String.Format(Constants.Culture, "{0}/{1}/Add/{2}{3}", Constants.PHOTOS, photoId, listingId, Constants.XML);
            return _connection.Post(null, query);
        }

    }
}
