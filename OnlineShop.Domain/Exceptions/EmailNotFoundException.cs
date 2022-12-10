namespace OnlineShop.Domain.Exceptions;

public class EmailNotFoundException : Exception
{
    public EmailNotFoundException(string email): base(email)
    {
    }
}