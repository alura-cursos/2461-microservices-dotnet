namespace ItemService.EventProcessor
{
    public interface IProcessaEvento
    {
        void Processa(string mensagem);
    }
}
