using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Linq;
using SoapLogging.Database;

namespace SoapLogging.Common
{
    public class SoapBehavior : IEndpointBehavior, IClientMessageInspector
    {
        private readonly ServiceLogRepository _logRepository;
        public SoapBehavior()
        {
            _logRepository = new ServiceLogRepository();
        }
        private int MessageDBID { get; set; }
        private string ModuleName { get; set; }
        private ServiceLog ServiceLogEntity { get; set; }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // adds our inspector to the runtime
            clientRuntime.MessageInspectors.Add(this);
            clientRuntime.MaxFaultSize = 1024000;
        }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }
        public void Validate(ServiceEndpoint endpoint)
        {
            this.ModuleName = endpoint.Contract.ConfigurationName.Split('.')[0];
        }

        object IClientMessageInspector.BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
        {
            try
            {
                this.ServiceLogEntity = new ServiceLog
                {
                    Module = this.ModuleName,
                    ActionName = request.Headers.Action.Split('/').Last(),
                    OutgoingXml = request.ToString(),
                    RequestDate = DateTime.Now
                };

                if (this.ServiceLogEntity.ActionName == "Divide")
                {
                    this.ServiceLogEntity.HintKey = "Divide process started.";
                }
            }
            catch (Exception ex)
            {
                this.ServiceLogEntity = new ServiceLog
                {
                    Module = this.ModuleName,
                    ActionName = request.Headers.Action.Split('/').Last(),
                    OutgoingXml = "Hata" + ex.Message,
                    RequestDate = DateTime.Now
                };
            }

            _logRepository.InsertOrUpdateServiceLog(this.ServiceLogEntity);

            return this.ServiceLogEntity.Id;
        }

        void IClientMessageInspector.AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            int isOk = reply.ToString().IndexOf("search text in incoming xml");
            if (isOk > 0)
                this.ServiceLogEntity.HintKey = "some text";

            this.ServiceLogEntity.IncomingXml = reply.ToString();
            this.ServiceLogEntity.ResponseDate = DateTime.Now;
            _logRepository.InsertOrUpdateServiceLog(this.ServiceLogEntity);
        }
    }
}
