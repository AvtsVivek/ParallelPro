using System.Text.Json;

namespace PeopleViewer;

public record Customer(int ID, string GivenName, string FamilyName,
    DateTime StartDate, int Rating, string FormatString)
{
    public override string ToString()
    {
        if (string.IsNullOrEmpty(FormatString))
            return $"{GivenName} {FamilyName}";
        return string.Format(FormatString, GivenName, FamilyName);
    }
}

public static class CustomerReader
{
    private static readonly HttpClient client = new() { BaseAddress = new Uri("http://localhost:9874") };
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public static async Task<List<int>> GetIdsAsync()
    {
        HttpResponseMessage response =
            await client.GetAsync("customers/ids").ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Unable to complete request: status code {response.StatusCode}");

        var stringResult =
            await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<List<int>>(stringResult);
        return result ?? new List<int>();
    }

    public static async Task<Customer> GetCustomerAsync(int id)
    {
        HttpResponseMessage response =
            await client.GetAsync($"customers/{id}").ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Unable to complete request: status code {response.StatusCode}");

        var stringResult =
            await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<Customer>(stringResult, options);
        return result ?? new Customer(0, "", "", DateTime.Now, 0, "");
    }
}
