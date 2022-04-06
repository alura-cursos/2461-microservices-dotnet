namespace ItemService.EventProcessing
{
    public interface IProcessaEvento
    {
        void Processa(string mensagemParaConsumir);
    }
}