using RestauranteService.Dtos;

namespace RestauranteService.Http;

public interface IItemHttpClient
{
    Task EnviaRestauranteParaItem(RestauranteReadDto readDto);
}
