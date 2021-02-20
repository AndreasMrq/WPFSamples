using MVVMWizard.Infrastructure;

namespace MVVMWizard.ViewModels
{
    public class ViewBViewModel : IWizardStep
    {
        public string DisplayName => "ViewB";

        public string BMessage => "This is View B";
    }
}
