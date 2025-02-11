using Microsoft.PowerBI.Api.Models;

namespace Telescope.Services;

public interface IPowerBIService
{
    Task<Microsoft.PowerBI.Api.Models.Reports> GetReports();
}