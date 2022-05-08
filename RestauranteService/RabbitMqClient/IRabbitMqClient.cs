using RestauranteService.Dtos;

namespace RestauranteService.RabbitMqClient
{
    public interface IRabbitMqClient
    {
        void PublicaRestaurante(RestauranteReadDto restauranteReadDto);
    }
}
