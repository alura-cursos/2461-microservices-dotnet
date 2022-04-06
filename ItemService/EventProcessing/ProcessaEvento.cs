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
        private readonly IItemRepository _itemRepository;

        public ProcessaEvento(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public void Processa(string mensagemParaConsumir)
        {
            AdicionaRestaurante(mensagemParaConsumir);
        }

        private void AdicionaRestaurante(string mensagemParaConsumir)
        {


            var restaurantePublishedDto = JsonSerializer.Deserialize<RestaurantePublishedDto>(mensagemParaConsumir);

            var restaurante = _mapper.Map<Restaurante>(restaurantePublishedDto);
            if (!_itemRepository.ExisteRestauranteExterno(restaurante.IdExterno))
            {
                _itemRepository.CreateRestaurante(restaurante);
                _itemRepository.SaveChanges();
            }
        }
    }
}
