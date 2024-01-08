namespace BookStore.Domain.Exceptions;

public class CannotChangeStatusOfADeletedEntityException(string message = "Nõo é possível deletar uma entidade com o status de deletada.") : Exception(message)
{
}
