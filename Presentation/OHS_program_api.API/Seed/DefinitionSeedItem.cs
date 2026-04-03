namespace OHS_program_api.API.Seed
{
    public sealed class DefinitionSeedItem
    {
        public string? Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ParentCode { get; set; }
        public string? WorkType { get; set; }
        public string? Description { get; set; }
    }
}
