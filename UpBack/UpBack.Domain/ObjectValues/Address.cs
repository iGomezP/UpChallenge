using System.Text.RegularExpressions;

namespace UpBack.Domain.ObjectValues
{
    public partial record Address
    {
        public Address()
        {

        }
        public string Street { get; }
        public string City { get; }
        public string ZipCode { get; }
        public string Country { get; }
        public string State { get; }

        private Address(string street, string city, string zipCode, string country, string state)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
            State = state;
        }

        public static Address Create(string street, string city, string zipCode, string country, string state)
        {
            if (string.IsNullOrWhiteSpace(street) || street.Length < 3 || street.Length > 100)
            {
                throw new ArgumentException("Street must be between 3 and 100 characters and cannot be empty.", nameof(street));
            }

            if (string.IsNullOrWhiteSpace(city) || !CityRegex().IsMatch(city))
            {
                throw new ArgumentException("City must only contain letters and cannot be empty.", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(zipCode) || !ZipRegex().IsMatch(zipCode)) // Ejemplo para EE.UU.
            {
                throw new ArgumentException("Invalid Zip Code format.", nameof(zipCode));
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentException("Country cannot be empty.", nameof(country));
            }

            if (string.IsNullOrWhiteSpace(state) || !StateRegex().IsMatch(state))
            {
                throw new ArgumentException("State must only contain letters and cannot be empty.", nameof(state));
            }

            return new Address(street, city, zipCode, country, state);
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {ZipCode}, {Country}";
        }

        [GeneratedRegex(@"^\d{5}(-\d{4})?$", RegexOptions.Compiled)]
        private static partial Regex ZipRegex();

        [GeneratedRegex(@"^[a-zA-Z\s]+$", RegexOptions.Compiled)]
        private static partial Regex StateRegex();

        [GeneratedRegex(@"^[a-zA-Z\s]+$", RegexOptions.Compiled)]
        private static partial Regex CityRegex();
    }
}
