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
    }
}
