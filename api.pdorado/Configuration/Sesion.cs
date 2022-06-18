namespace api.pdorado.Configuration
{
    public sealed class Sesion
    {
        #region Singleton
        private static Sesion instance;
        public static Sesion Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Sesion();
                }
                return instance;
            }
        }
        #endregion

        public string ConnectionString { get; set; }
        public string PublicKey { get; set; }
        public List<int> Idiomas { get; set; }
        public static string GetIdiomaTag(int idIdioma)
        {
            switch (idIdioma)
            {
                case 1:
                    return "ES";
                case 2:
                    return "GL";
                case 3:
                    return "ENG";
                default:
                    return "ERROR";
            }
        }
    }
}
