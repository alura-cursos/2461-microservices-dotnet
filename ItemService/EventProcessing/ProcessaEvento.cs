using System.Text.Json;
using AutoMapper;
using ItemService.Models;
using ItemService.Data;
using ItemService.Dtos;

namespace ItemService.EventProcessing
{
    public class ProcessaEvento : IProcessaEvento
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessaEvento(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void Processa(string mensagemParaConsumir)
        {
            using var scope = _scopeFactory.CreateScope();

            var itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();

            var restaurantePublishedDto = JsonSerializer.Deserialize<RestaurantePublishedDto>(mensagemParaConsumir);

            var restaurante = _mapper.Map<Restaurante>(restaurantePublishedDto);

            if (!itemRepository.ExisteRestauranteExterno(restaurante.IdExterno))
            {
                itemRepository.CreateRestaurante(restaurante);
                itemRepository.SaveChanges();
            }
        }
    }
}
