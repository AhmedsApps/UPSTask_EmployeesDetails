using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UPSTask_EmployeesDetails
{
    public static class APICalls
    {
        //Client To access the services
        public static RestClient client = new RestClient(ConfigurationManager.AppSettings["APILink"]);


        public static Root GetAllEmployees(int  page)
        {
            var req = new RestRequest();

            if (page != 0)
            {
                req.AddParameter("page", page);
            }

            var response = client.Execute(req);

            Root returnval = null;



            if (response.StatusCode == HttpStatusCode.OK)
            {
                returnval = JsonConvert.DeserializeObject<Root>(response.Content);
                return returnval;
            }
            else
            {
                return null;
            }
        }

        public static Root SearchForEmployees(string NameFilter,string EmailFilter,int page)
        {
            var req = new RestRequest();
            
            if (page != 0)
            {
                req.AddParameter("page", page);
            }

            if (NameFilter!="")
            {
                req.AddParameter("name",NameFilter);
            }

            if (EmailFilter != "")
            {
                req.AddParameter("email", EmailFilter);
            }

            req.AddHeader("authorization", "Bearer " + ConfigurationManager.AppSettings["Token"]);

            var response = client.Execute(req);

            Root returnval = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                returnval = JsonConvert.DeserializeObject<Root>(response.Content);
                return returnval;
            }
            else
            {
                return null;
            }
        }

        public static bool AddEmployee(EmployeeInfo newEmployee)
        {
            
            var  request = new RestRequest(Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(newEmployee);
            request.AddHeader("authorization", "Bearer " + ConfigurationManager.AppSettings["Token"]);


            var response = client.Post(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.IsSuccessful; 
            }
            else
            {
                return false;
            }

        }


        public static bool DeleteEmployee(int EmployeeID)
        {

            var request = new RestRequest("/{id}", Method.DELETE);
            request.AddUrlSegment("id", "" + EmployeeID);
            request.AddHeader("authorization", "Bearer " + ConfigurationManager.AppSettings["Token"]);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.IsSuccessful;
            }
            else
            {
                return false;
            }

        }

    }
}
