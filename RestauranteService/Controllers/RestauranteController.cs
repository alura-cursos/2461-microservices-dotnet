using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestauranteService.AsyncDataServices;
using RestauranteService.Data;
using RestauranteService.Dtos;
using RestauranteService.Http;
using RestauranteService.Models;

namespace RestauranteService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestauranteController : ControllerBase
{
    private readonly IRestauranteRepository _repository;
    private readonly IMapper _mapper;
    private readonly IItemHttpClient _itemHttpClient;
    private readonly IMessageBusClient _messageBusClient;

    public RestauranteController(
        IRestauranteRepository repository,
        IMapper mapper,
        IItemHttpClient itemClient,
        IMessageBusClient messageBusClient)
    {
        _repository = repository;
        _mapper = mapper;
        _itemHttpClient = itemClient;
        _messageBusClient = messageBusClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestauranteReadDto>> GetAllRestaurantes()
    {

        var restaurantes = _repository.GetAllRestaurantes();

        return Ok(_mapper.Map<IEnumerable<RestauranteReadDto>>(restaurantes));
    }

    [HttpGet("{id}", Name = "GetRestauranteById")]
    public ActionResult<RestauranteReadDto> GetRestauranteById(int id)
    {
        var restaurante = _repository.GetRestauranteById(id);
        if (restaurante != null)
        {
            return Ok(_mapper.Map<RestauranteReadDto>(restaurante));
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<RestauranteReadDto>> CreateRestaurante(RestauranteCreateDto restauranteCreateDto)
    {
        var restaurante = _mapper.Map<Restaurante>(restauranteCreateDto);
        _repository.CreateRestaurante(restaurante);
        _repository.SaveChanges();

        var restauranteReadDto = _mapper.Map<RestauranteReadDto>(restaurante);


        await _itemHttpClient.EnviaRestauranteParaItem(restauranteReadDto);

        var restaurantePublishedDto = _mapper.Map<RestaurantePublishedDto>(restauranteReadDto);
        restaurantePublishedDto.Evento = "Restaurante_Published";
        _messageBusClient.PublishRestaurante(restaurantePublishedDto);


        return CreatedAtRoute(nameof(GetRestauranteById), new { restauranteReadDto.Id }, restauranteReadDto);
    }
}
