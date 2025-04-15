using GitHubActions_CICD_Pipeline.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class NasaModel : PageModel
{
    private readonly NasaService _nasaService;
    public List<EpicImageViewModel> EpicImages { get; set; }
    public ApodViewModel Apod { get; set; }

    public NasaModel(NasaService nasaService)
    {
        _nasaService = nasaService;
    }
    public async Task OnGetAsync()
    {
        Apod = await _nasaService.GetApodAsync();
        EpicImages = await _nasaService.GetEpicImagesAsync();
    }
}
