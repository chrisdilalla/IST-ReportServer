using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MDLibrary.Utils
{
	public static class BinUtils
	{

		/// <summary>
		/// Reads data into a complete array, throwing an EndOfStreamException
		/// if the stream runs out of data first, or if an IOException
		/// naturally occurs.
		/// </summary>
		/// <param name="stream">The stream to read data from</param>
		/// <param name="data">The array to read bytes into. The array
		/// will be completely filled from the stream, so the data array
		/// must be properly sized to match the known stream before calling.
		/// </param>
		public static void ReadWholeArray(Stream stream, byte[] data)
		{
			int offset = 0;
			int remaining = data.Length;
			while (remaining > 0)
			{
				int read = stream.Read(data, offset, remaining);
				if (read <= 0)
					throw new EndOfStreamException
						(String.Format("End of stream reached with {0} bytes left to read", remaining));
				remaining -= read;
				offset += read;
			}
		}

        public static byte[] GetUtf8ByteArrayFromXDocument(XDocument xdoc)
        {
            xdoc.Declaration = new XDeclaration("1.0", "utf-8", null);
            StringWriter writer = new Utf8StringWriter();
            xdoc.Save(writer, SaveOptions.None);
            return Encoding.UTF8.GetBytes(writer.ToString());
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }


        /// <summary>
        /// Takes the ByteArray version of the XML document
        /// and converts it to XmlDocument format
        /// </summary>
        /// <param name="docData"></param>
        /// <returns></returns>
	    public static XmlDocument GetXmlDocFromByteArray(byte[] docData)
	    {
            MemoryStream ms = new MemoryStream(docData);
            ms.Flush();
            ms.Position = 0;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ms);

	        return xmlDoc;
	    }

	    public static XDocument GetXDocFromByteArray(byte[] docData)
	    {
            MemoryStream ms = new MemoryStream(docData);
            ms.Flush();
            ms.Position = 0;

	        XDocument xDoc = XDocument.Load(ms);

            return xDoc;
        }


	}
}
