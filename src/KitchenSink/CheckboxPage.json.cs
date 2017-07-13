using Starcounter;

namespace KitchenSink
{
    partial class CheckboxPage : Json
    {
        static CheckboxPage()
        {
            DefaultTemplate.DrivingLicenseReaction.Bind = "CalculatedDrivingLicenseReaction";
        }

        public string CalculatedDrivingLicenseReaction
        {
            get
            {
                if (DrivingLicense == true)
                {
                    return "You can drive";
                }
                else
                {
                    return "You can't drive";
                }
            }
        }
    }
}