using RestauranteService.Models;

namespace RestauranteService.Data;

public interface IRestauranteRepository
{
    void SaveChanges();

    IEnumerable<Restaurante> GetAllRestaurantes();
    Restaurante GetRestauranteById(int id);
    void CreateRestaurante(Restaurante restaurante);
}
