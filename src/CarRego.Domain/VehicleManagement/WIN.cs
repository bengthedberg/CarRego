using System.Text.RegularExpressions;
using CarRego.Domain.VehicleManagement.Exceptions;

namespace CarRego.Domain.VehicleManagement;

public sealed record VIN(string Value)
{
    private static Regex VinValidationRegex = new Regex("[A-HJ-NPR-Z0-9]{17}");

    public static VIN Create(string value)
    {
        if (!IsValid(value))
        {
            throw new InvalidVinException();
        }

        return new VIN(value);
    }

    public static VIN Empty => new(string.Empty);
    public static bool IsValid(string value) => VinValidationRegex.IsMatch(value);
    
    public override string ToString()
    {
        return Value;
    }
    public override int GetHashCode() => HashCode.Combine("VIN", Value);
    public bool Equals(VIN? other) => Value == other!.Value;
}