namespace SnakeApi.Models
{
    public class SnakeDatabaseSettings
    {
        //La clase se utiliza para almacenar los valores de propiedad appsettings.json
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
