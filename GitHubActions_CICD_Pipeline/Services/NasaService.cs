using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using GitHubActions_CICD_Pipeline.Models;
using Newtonsoft.Json;

public class NasaService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public NasaService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["NasaApi:ApiKey"];
    }

    public async Task<ApodViewModel> GetApodAsync(string date = null)
    {
        var url = $"https://api.nasa.gov/planetary/apod?api_key={_apiKey}";
        if (!string.IsNullOrEmpty(date))
            url += $"&date={date}";
        var response = await _httpClient.GetStringAsync(url);
        return JsonConvert.DeserializeObject<ApodViewModel>(response);
    }
    public async Task<List<EpicImageViewModel>> GetEpicImagesAsync()
    {
        var url = $"https://api.nasa.gov/EPIC/api/natural?api_key={_apiKey}";
        var response = await _httpClient.GetStringAsync(url);
        var images = JsonConvert.DeserializeObject<List<dynamic>>(response);

        var result = new List<EpicImageViewModel>();
        foreach (var img in images.Take(5)) // Show 5 latest images
        {
            string date = img.date;
            var dt = DateTime.Parse(date);
            string imageName = img.image;
            string imageUrl = $"https://epic.gsfc.nasa.gov/archive/natural/{dt:yyyy}/{dt:MM}/{dt:dd}/jpg/{imageName}.jpg";
            result.Add(new EpicImageViewModel
            {
                Caption = img.caption,
                Date = date,
                ImageName = imageName,
                ImageUrl = imageUrl
            });
        }
        return result;
    }

}

