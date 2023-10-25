using CsvHelper.Configuration.Attributes;

public class Attributes
{
    [Name("First Name")]
    public string? FirstName { get; set; }

    [Name("Last Name")]
    public string? LastName { get; set; }

    [Name("Street Number")]
    public string? StreetNumber { get; set; }

    [Name("Street")]
    public string? Street { get; set; }

    [Name("City")]
    public string? City { get; set; }

    [Name("Province")]
    public string? Province { get; set; }

    [Name("Postal Code")]
    public string? PostalCode { get; set; }

    [Name("Country")]
    public string? Country { get; set; }

    [Name("Phone Number")]
    public string? PhoneNumber { get; set; }

    [Name("email Address")]
    public string? EmailAddress { get; set; }
}
