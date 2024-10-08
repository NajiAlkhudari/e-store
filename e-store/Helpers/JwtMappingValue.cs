namespace e_store.Helpers
{
    public class JwtMappingValue
{
    public string Key { get; set; }
    public string Issuer { get; set; } // لاحظ اسم Issuer
    public string Audience { get; set; } // لاحظ اسم Audience
    public int DurationInDays { get; set; }
}
}
