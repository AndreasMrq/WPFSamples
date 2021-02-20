using MVVMWizard.Infrastructure;

namespace MVVMWizard.ViewModels
{
    public class ViewAViewModel : IWizardStep
    {
        public string DisplayName => "ViewA";

        public string AMessage => "This is View A";
    }
}
