using RestauranteService.Dtos;

namespace RestauranteService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishRestaurante(RestaurantePublishedDto restaurantePublishedDto);
}
