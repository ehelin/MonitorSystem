using System;
using System.Collections.Generic;
using BLL.interfaces;
using Shared.misc;
using Shared.data;
using Shared.Helpers;
using System.Net;
using System.IO;

namespace BLL.jobs
{
    public class MonitorWebsite : Job, iSystemJob
    {
        private string appPathSettingName = string.Empty; 

        public MonitorWebsite(string pSystem,
                      string pJobName,
                      string pAppPathSettingName)
        {
            SystemName = pSystem;
            JobName = pJobName;
            appPathSettingName = pAppPathSettingName;
        }

        public override void JobAction()
        {
            try
            {
                MonitorWebsites();
            }
            catch (Exception e)
            {
                dispOut.Events.Add(JobConstants.ERROR + e.Message + JobConstants.STACKTRACE + e.StackTrace.ToString());
            }
        }      

        private void MonitorWebsites()
        {            
            IList<Website> siteWords = GetWebsites();
            string curWebAddress = string.Empty;

            foreach (Website website in siteWords)
            {
                VerifyWebsiteResponse(website.Name.Trim());
                VerifySearchWordPresent(website);
            }
        }        

        private IList<Website> GetWebsites()
        {
            Website curWebsite = null;
            IList<Website> websites = new List<Website>();
            string siteSearchWords = Utility.GetAppSetting(Constants.WEBSITES_TO_CHECK);
            string[] sitesSearchWordsArr = siteSearchWords.Split(';');

            foreach(string entry in sitesSearchWordsArr)
            {
                if (!string.IsNullOrEmpty(entry))
                {
                    string siteWord = entry.Replace(JobConstants.NEW_LINE, string.Empty);
                    siteWord = siteWord.Trim();
                    string[] siteSearchWord = siteWord.Split(',');

                    curWebsite = new Website();
                    curWebsite.Name = siteSearchWord[0];
                    curWebsite.SearchString = siteSearchWord[1];

                    websites.Add(curWebsite);
                }
            }
            
            return websites;
        }
        
        private void VerifySearchWordPresent(Website website)
        {
            string pageContents = string.Empty;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            StreamReader rdr = null;
            int searchWordExists = 0;

            try
            {
                Uri url = new Uri(website.Name);

                dispOut.Events.Add(Messages.TESTING_WEBSITE + website.Name
                        + Messages.SEARCH_TERM + website.SearchString + Messages.END_SINGLE_QUOTE);

                req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = WebRequestMethods.Http.Get;
                req.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                resp = (HttpWebResponse)req.GetResponse();
                rdr = new StreamReader(resp.GetResponseStream());
                pageContents = rdr.ReadToEnd();
                searchWordExists = pageContents.ToLower().IndexOf(website.SearchString);
                
                if (searchWordExists == -1)
                {
                    dispOut.Events.Add(website.Name
                                 + Messages.SEARCH_TERM_NOT_FOUND + website.SearchString + Messages.END_SINGLE_QUOTE);
                }
                else
                    dispOut.Events.Add(Messages.WEBSITE + website.Name
                                        + Messages.SEARCH_TERM_FOUND  + website.SearchString + Messages.END_SINGLE_QUOTE);
            }
            catch (System.Net.WebException ex)
            {
                dispOut.Events.Add(Messages.ERROR_WEBSITE_RESPONSE + website.Name
                       + Messages.ERROR_SEARCH_TERM + website.SearchString + Messages.END_SINGLE_QUOTE_DASH + ex.Message);
            }
            finally
            {
                if (req != null)
                {
                    req = null;
                }

                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }

                if (rdr != null)
                {
                    rdr.Close();
                    rdr.Dispose();
                    rdr = null;
                }
            }
        }

        private void VerifyWebsiteResponse(string curWebAddress)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(curWebAddress);
                hwr.UseDefaultCredentials = true;
                hwr.UserAgent = Constants.APPLICATION_NAME;
                
                dispOut.Events.Add(Messages.TESTING_WEBSITE + JobConstants.DASH + curWebAddress);

                using (HttpWebResponse response = (HttpWebResponse)hwr.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        dispOut.Events.Add(curWebAddress + Messages.NOT_RESPONDING);
                    else
                        dispOut.Events.Add(curWebAddress + Messages.OK_RESPONSE);
                }
            }
            catch (System.Net.WebException ex)
            {
                dispOut.Events.Add(JobConstants.ERROR + curWebAddress + JobConstants.DASH + ex.Message);
            }
        }
    }
}
