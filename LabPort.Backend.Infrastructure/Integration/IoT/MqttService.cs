using LabPort.Backend.Application.Interfaces;
using MQTTnet;
using MQTTnet.Client;


namespace LabPort.Backend.Infrastructure.Integration.IoT
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _options;

        public MqttService()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.emqx.io", 1883)
                .WithClientId($"LabPortBackend-{Guid.NewGuid()}")
                .Build();
        }

        public async Task PublishAsync(string topic, string payload)
        {
            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(_options);
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            await _mqttClient.PublishAsync(message);
        }
    }
}
