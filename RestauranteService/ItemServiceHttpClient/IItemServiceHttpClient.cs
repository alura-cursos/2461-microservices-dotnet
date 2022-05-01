using RestauranteService.Dtos;

namespace RestauranteService.ItemServiceHttpClient
{
    public interface IItemServiceHttpClient
    {
        public void EnviaRestauranteParaItemService(RestauranteReadDto readDto);
    }
}
