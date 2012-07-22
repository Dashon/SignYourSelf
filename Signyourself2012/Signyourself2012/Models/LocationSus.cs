using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Signyourself2012
{
    public class LocationSus
    {

        public string GetCountryByIP(string ipAddress)
        {
            string ipResponse = IPRequestHelper("http://api.ipinfodb.com/v3/ip-city/?key=9a9cfcd09e8a5273319b9787f7f948f635d9288345c7793bcff45c0538653019&ip=", ipAddress);

            XmlDocument ipInfoXML = new XmlDocument();
            ipInfoXML.LoadXml(ipResponse);
            XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("Response");

            NameValueCollection dataXML = new NameValueCollection();

            dataXML.Add(responseXML.Item(0).ChildNodes(2).InnerText, responseXML.Item(0).ChildNodes(2).Value);

            string xmlValue = dataXML.Keys(0);
            
            return xmlValue;
        }

        public string IPRequestHelper(string url, string ipAddress)
        {
            string checkURL = url + ipAddress;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();

            responseStream.Close();
            responseStream.Dispose();

            return responseRead;
        }
    }

}
