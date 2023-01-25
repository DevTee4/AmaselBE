namespace AmaselBE.Configuration
{ 
    public class Setting
    {
      public string ApplicationMode { get; set; }//PROD-DEV
      public string ErrorPath { get; set; }
      public string AppUrl { get; set; }

       public string ConnectionString { get; set; }

       public string DatabaseName { get; set; }
       public string HostName { get; set; }
       public string Username { get; set; }
       public string Password { get; set; }
    }
}
