namespace OHS_program_api.API.Constants
{
    /// <summary>
    /// API versiyon sabiitleri
    /// </summary>
    public static class ApiVersions
    {
        public const string Version1 = "1.0";
        public const string Version2 = "2.0";
        public const string CurrentVersion = Version1;
    }

    /// <summary>
    /// API route sabitleri
    /// </summary>
    public static class ApiRoutes
    {
        private const string BaseRoute = "api/v{version}";

        // Accidents
        public const string GetAllAccidents = BaseRoute + "/accidents";
        public const string GetAccidentById = BaseRoute + "/accidents/{id}";
        public const string CreateAccident = BaseRoute + "/accidents";
        public const string UpdateAccident = BaseRoute + "/accidents/{id}";
        public const string DeleteAccident = BaseRoute + "/accidents/{id}";

        // Personnels
        public const string GetAllPersonnels = BaseRoute + "/personnels";
        public const string GetPersonnelById = BaseRoute + "/personnels/{id}";
        public const string CreatePersonnel = BaseRoute + "/personnels";
        public const string UpdatePersonnel = BaseRoute + "/personnels/{id}";
        public const string DeletePersonnel = BaseRoute + "/personnels/{id}";

        // Users
        public const string GetAllUsers = BaseRoute + "/users";
        public const string GetUserById = BaseRoute + "/users/{id}";

        // Roles
        public const string GetAllRoles = BaseRoute + "/roles";
        public const string CreateRole = BaseRoute + "/roles";
    }
}
