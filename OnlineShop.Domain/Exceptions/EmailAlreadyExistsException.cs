namespace OnlineShop.Domain.Exceptions;

public class EmailAlreadyExistsException : Exception
{
    public string Email;
    public EmailAlreadyExistsException(string message, string email)
        : base(message)
    {
        Email = email;
    }
}