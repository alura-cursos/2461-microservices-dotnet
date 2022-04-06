using RabbitMQ.Client;
using RestauranteService.Dtos;
using System.Text;
using System.Text.Json;

namespace RestauranteService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory() { HostName = "localhost", Port = 8002 }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        }

        public void PublishRestaurante(RestaurantePublishedDto restaurantePublishedDto)
        {
            var message = JsonSerializer.Serialize(restaurantePublishedDto);
            EnviaMensagem(message);
        }

        private void EnviaMensagem(string mensagem)
        {
            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}