namespace BE.Services.Api.Configuration
{ 
    public class Setting
    {
      public string ApplicationMode { get; set; }//PROD-DEV
      public string ErrorPath { get; set; }
      public string AppUrl { get; set; }

       public string ConnectionString { get; set; }

       public string DatabaseName { get; set; }
    }
}
