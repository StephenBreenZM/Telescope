using System.Threading.Tasks;
using Microsoft.PowerBI.Api.Models;

namespace Telescope.Services;

public interface IPowerBIService
{
    Task<Reports> GetReports();
}