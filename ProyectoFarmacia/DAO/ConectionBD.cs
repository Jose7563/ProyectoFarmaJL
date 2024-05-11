namespace ProyectoFarmacia.DAO
{
    public class ConectionBD
    {
        private string cadenaSQL = string.Empty;

        public ConectionBD()
        {


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionStrings:cn").Value;
        }

        public string getConnectionSQL()
        {
            return cadenaSQL;
        }
    }
}
