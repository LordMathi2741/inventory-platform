using System.Text;
using System.Text.Json;
using Management.Domain.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Support.Management.Domain.Model.Commands;

namespace Management.Application.RabbitMq.MessageService;
public class RabbitMqService : IRabbitMqService
    {
        private readonly string _rabbitMqUrl = "amqps://wkdcnfnu:OEPZ-wh5y1j2unzvXaay6nR03m7ea4ST@campbell.lmq.cloudamqp.com/wkdcnfnu";
        private readonly string _queueName = "product";

        private IConnection _connection;
        private IModel _channel;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMqUrl),
                DispatchConsumersAsync = true
            };

            try
            {
                _connection = factory.CreateConnection();  
                _channel = _connection.CreateModel();      

                _channel.QueueDeclare(queue: _queueName,
                                      durable: true,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);
                
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"❌ Connection error with RabbitMq: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error while trying to init RabbitMq: {ex.Message}");
            }
        }

        private async Task SendMessageToQueueAsync(object command, string routingKey)
        {
            try
            {
                string message = JsonSerializer.Serialize(command);
                var body = Encoding.UTF8.GetBytes(message);
                
                await Task.Run(() =>
                {
                    var properties = _channel.CreateBasicProperties();
                    properties.Persistent = true;
                    _channel.BasicPublish(exchange: "",
                                          routingKey: routingKey,
                                          basicProperties: properties,
                                          body: body);
                });

                Console.WriteLine($"✅ Message send: {routingKey} - {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌Error while trying to send message: {ex.Message}");
            }
        }

        public async Task SendMessageAsync(CreateProductCommand command)
        {
            await SendMessageToQueueAsync(command, "create_product");
        }

        public async Task SendMessageAsync(UpdateProductCommand command)
        {
            await SendMessageToQueueAsync(command, "update_product");
        }

        public async Task SendMessageAsync(DeleteProductCommand command)
        {
            await SendMessageToQueueAsync(command, "delete_product");
        }
        
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
