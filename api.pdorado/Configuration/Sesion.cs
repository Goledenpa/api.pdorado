namespace api.pdorado.Configuration
{
    /// <summary>
    /// Objeto de sesión que maneja varias constantes necesarias en la aplicación
    /// </summary>
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

        /// <summary>
        /// Cadena de conexión encriptada a la base de datos
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Llave pública para las encriptaciones
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Idiomas de la aplicación
        /// </summary>
        public List<int> Idiomas { get; set; }
        
        /// <summary>
        /// Devuelve un tag dependiendo del idioma
        /// </summary>
        /// <param name="idIdioma">Idioma</param>
        /// <returns>El tag</returns>
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
