using System;
using System.ServiceModel.Configuration;

namespace SoapLogging.Common
{
    public class SoapBehaviorExtensionElement : BehaviorExtensionElement
    {
        public SoapBehaviorExtensionElement()
        {
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(SoapBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new SoapBehavior();
        }
    }
}
