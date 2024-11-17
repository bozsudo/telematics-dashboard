using RabbitMQ.Client;

public class RabbitMQPublisher
{
    private readonly ConnectionFactory _factory;

    public RabbitMQPublisher()
    {
        // Retrieve RabbitMQ URL from environment variable
        string rabbitMqUrl = Environment.GetEnvironmentVariable("RABBITMQ_URL");

        if (string.IsNullOrEmpty(rabbitMqUrl))
        {
            throw new InvalidOperationException("RabbitMQ connection URL is not set.");
        }

        _factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMqUrl)
        };
    }

    public void PublishMessage(string queueName, string message)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var body = System.Text.Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             basicProperties: null,
                             body: body);
    }
}
