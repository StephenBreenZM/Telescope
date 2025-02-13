using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Telescope.Helpers;
using Microsoft.PowerBI.Api.Models;
using Telescope.Services;

namespace Telescope.ViewModels;

public partial class ReportsViewModel : BaseViewModel  
{
    [ObservableProperty]
    bool isReportsListRefreshing;

    [ObservableProperty]
    Report selectedReport;

    public ObservableCollection<Report> VisibleReportsListData { get; } = new();

    IPowerBIService service;

    public ReportsViewModel(IPowerBIService service)
    {
        this.service = service;
        Debug.WriteLine("ReportsViewModel created");
    }

    [RelayCommand]
    async Task RefreshReportsList()
    {
        VisibleReportsListData.Clear();
        Debug.WriteLine("Refresh Reports");

        try
        {
            var reports = await service.GetReports();

            foreach (var report in reports.Value.OrderBy(x => x.Name))
                VisibleReportsListData.Add(report);
        }
        finally
        {
            IsReportsListRefreshing = false;
        }
    }

    [RelayCommand]
    async Task ViewReport()
    {
        if (SelectedReport != null)
        {
            var data = new Dictionary<string, object>()
            {
                {
                    Constants.ReportUrl, SelectedReport.WebUrl
                }
            };

            await Shell.Current.GoToAsync(Constants.DetailsRoute, true, data);
        }
    }
}