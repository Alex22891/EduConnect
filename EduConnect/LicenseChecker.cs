using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EduConnect;
using Newtonsoft.Json.Linq;

public class LicenseChecker
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<(List<LicensesKeys>, string)> GetLicenseDataFromGitHub(string username)
    {
        List<LicensesKeys> licensesKeysList = new List<LicensesKeys>();
        string userLicenseKey = string.Empty;

        try
        {
            string url = "https://raw.githubusercontent.com/Alex22891/EduConnect/main/licenseData.json";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(responseBody);
            var licenses = json["licenses"];

            foreach (var license in licenses)
            {
                LicensesKeys licensesKeys = new LicensesKeys
                {
                    Id = licensesKeysList.Count + 1,
                    LicenseKey = license["licenseKey"].ToString(),
                    IsActive = (bool)license["isActive"],
                    ExpiryDate = DateTime.Parse(license["expiryDate"].ToString()),
                    Username = license["username"].ToString()
                };
                licensesKeysList.Add(licensesKeys);

                if (license["username"].ToString() == username)
                {
                    userLicenseKey = license["licenseKey"].ToString();
                    Console.WriteLine($"Found license key for user {username}: {userLicenseKey}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении лицензионного ключа: {ex.Message}");
        }

        return (licensesKeysList, userLicenseKey);
    }

    public bool CheckLicenseValidity(List<LicensesKeys> licensesKeys, string userLicenseKey)
    {
        foreach (var license in licensesKeys)
        {
            Console.WriteLine($"Checking license: {license.LicenseKey}, IsActive: {license.IsActive}, ExpiryDate: {license.ExpiryDate}");
            if (license.LicenseKey == userLicenseKey && license.IsActive && license.ExpiryDate > DateTime.Now)
            {
                return true;
            }
        }
        return false;
    }
}