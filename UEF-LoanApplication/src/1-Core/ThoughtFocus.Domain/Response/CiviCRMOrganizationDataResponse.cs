using System;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class CiviCRMOrganizationDataResponse : BaseResponse 
    {
       public  string Organization_Name { get; set; }
       public  string Street_Address { get; set; }
       public  string City { get; set; }
       public  string State { get; set; }
       public string Zip {get; set;}
       public  string Phone_Number { get; set; }
       public  string Email { get; set; }
       public  string Affiliate { get; set; }
       public  string EIN_Number { get; set; }
       public  string SIC_Code { get; set; }
       public string NAICS {get; set;}
       public  string Business_Type { get; set; }
       public  string Industry { get; set; }
       public  string Url { get; set; }
       public  string Contact_Type { get; set; }
       public  long External_ID_OrdID { get; set; }
       public  string Grant1_Program { get; set; }
       public  decimal Grant1_AmountFunded { get; set; }
       public  string Grant1_ReceivedDate { get; set; }
       public  string Grant2_Program { get; set; }
       public  decimal Grant2_AmountFunded { get; set; }
       public  string Grant2_ReceivedDate { get; set; }
       public  string Grant3_Program { get; set; }
       public  decimal Grant3_AmountFunded { get; set; }
       public  string Grant3_ReceivedDate { get; set; }
    }
    
    public class CiviCRMContactsDataResponse : BaseResponse 
    {
        public  string Prefix { get; set; }
        public  string First_Name { get; set; }
        public  string Middle_Name { get; set; }
        public  string Last_Name { get; set; }
        public  string Affiliate { get; set; }
        public  string Organization_Name { get; set; }
        public  string Role { get; set; }
        public  string Street_Address { get; set; }
        public  string City { get; set; }
        public  string State { get; set; }
        public string Zip {get; set;}
        public  string Phone_Number { get; set; }
        public  string Phone_Type { get; set; }
        public  string Email { get; set; }
        public  string Race { get; set; }
        public  string Ethnicity { get; set; }
        public  string Gender { get; set; }
        public  string Veteran { get; set; }
        public  long ExternalID { get; set; }
        public  long OrganizationID { get; set; }
        public  long ContactID { get; set; }
        public long UniqueID {get; set;}
    }

    public class CiviCRMDataExportLogResponse : BaseResponse 
    {
        public List<CiviCRMDataExportLogDetails> ExportLogs {get; set;}
    }

    public class CiviCRMDataExportLogDetails
    {
        public  string ExportedDate { get; set; }
        public  string ExportedBy { get; set; }
        public long? ExportedUserID {get; set;}
        public  string ExportedType { get; set; }
        public  long RecordCount { get; set; }
    }
}