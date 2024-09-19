using FluentValidation;
using ProjectsManagement.ViewModels.Projects;

namespace ProjectsManagement.Validators
{
    public class ProjectCreateViewModelValidator : AbstractValidator<ProjectCreateViewModel>
    {
        public ProjectCreateViewModelValidator() 
        {
            RuleFor(ProjectCreateViewModel => ProjectCreateViewModel.Title)
                .NotEmpty().WithMessage("Title is required!");
        }
    }
}
