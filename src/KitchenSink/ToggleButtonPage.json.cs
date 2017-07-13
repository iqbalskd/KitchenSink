using Starcounter;

namespace KitchenSink
{
    partial class ToggleButtonPage : Json
    {
        static ToggleButtonPage()
        {
            DefaultTemplate.AcceptTermsAndConditionsReaction.Bind = "CalculatedAcceptTermsAndConditionsReaction";
        }

        public string CalculatedAcceptTermsAndConditionsReaction =>
            AcceptTermsAndConditions ? "I accept terms and conditions" : "I don't accept terms and conditions";
    }
}