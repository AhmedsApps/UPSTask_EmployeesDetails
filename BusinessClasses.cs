using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPSTask_EmployeesDetails
{

    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class EmployeeInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Root
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public List<EmployeeInfo> data { get; set; }
    }

    public enum Gender {Male,Female };
    public enum Status { Active, Inactive };



    public class InsertResponse
    {
        public int code { get; set; }
        public object meta { get; set; }
        public EmployeeInfo data { get; set; }
    }



}


