using Application;
using Domain.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructure.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;

        public RabbitMqConsumer(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        public void ConsumeCustomerCreatedEvent()
        {
            // Configura la conexión al servidor RabbitMQ

            var factory = new RabbitMQ.Client.ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            Console.WriteLine(factory.GetType().FullName); 

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("customer_created", durable: true, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var customer = JsonSerializer.Deserialize<CustomerCreatedEvent>(json);
                Console.WriteLine($"[RabbitMQ] Client creado: {customer?.IdentificationNumber}");
            };

            channel.BasicConsume("customer_created", autoAck: true, consumer: consumer);

            Console.WriteLine("[RabbitMQ] Escuchando eventos...");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
