namespace Enfermed.Services
{
    public class ValuesService
    {
        // Nombre de la Base de Datos
        public static readonly string DbName = "Enfermed.db";

        // Devuelve la Ruta de la Base de Datos "Centraliza"
        public static string GetDbPath()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(folder, DbName);
        }
    }
}