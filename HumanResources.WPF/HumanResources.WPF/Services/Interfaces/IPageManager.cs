using HumanResources.WPF.Views.Pages;
using System.Windows.Controls;

namespace HumanResources.WPF.Services.Interfaces;

public interface IPageManager
{
	public Page Page { get; set; }
	public Dictionary<string, Page> Pages { get; }
	public CompaniesPageView CompaniesPageView { get; }
	public DepartmentsPageView DepartmentsPageView { get; }
	public VacanciesPageView VacanciesPageView { get; }
	public WorkersPageView WorkersPageView { get; }
	public ProfessionsPageView ProfessionsPageView { get; }

	public void UpdateCompanies();
	public void UpdateDepartments();
	public void UpdateWorkers();
	public void UpdateProfessions();
	public void UpdateVacancies();
}
