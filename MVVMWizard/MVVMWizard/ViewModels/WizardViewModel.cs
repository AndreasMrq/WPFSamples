using MVVMWizard.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMWizard.ViewModels
{
    public class WizardViewModel : BindableBase
    {
        private IWizardStep currentStep;

        public WizardViewModel()
        {
            NextCommand = new DelegateCommand(NavigateForward, CanNavigateForward).ObservesProperty(() => CurrentStep);
            BackCommand = new DelegateCommand(NavigateBack, CanNavigateBack).ObservesProperty(() => CurrentStep);

            InitializeSteps();
        }

        public List<IWizardStep> Steps { get; } = new List<IWizardStep>();
        public IWizardStep CurrentStep
        {
            get => currentStep;
            private set => SetProperty(ref currentStep, value);
        }

        public DelegateCommand NextCommand { get; }
        public DelegateCommand BackCommand { get; }

        private void InitializeSteps()
        {
            Steps.AddRange(new IWizardStep[]
            {
                new ViewAViewModel(),
                new ViewBViewModel()
            });
            CurrentStep = Steps.First();
        }

        private void NavigateForward()
        {
            CurrentStep = Steps.GetNextOrDefault(CurrentStep) ?? CurrentStep;
        }

        private bool CanNavigateForward()
        {
            return !CurrentStep.Equals(Steps.Last());
        }

        private void NavigateBack()
        {
            CurrentStep = Steps.GetPreviousOrDefault(CurrentStep) ?? CurrentStep;
        }

        private bool CanNavigateBack()
        {
            return !CurrentStep.Equals(Steps.First());
        }
    }

    public static class ExtendList
    {
        public static T GetPreviousOrDefault<T>(this IList<T> list, T item)
        {
            var index = list?.IndexOf(item) ?? throw new ArgumentNullException(nameof(list));
            return index > 0 ? list[index-1] : default;
        }

        public static T GetNextOrDefault<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item)) 
            { 
                return default; 
            }

            var index = list?.IndexOf(item) ?? throw new ArgumentNullException(nameof(list));
            return index < list.Count-1 ? list[index +1] : default;
        }
    }
}
