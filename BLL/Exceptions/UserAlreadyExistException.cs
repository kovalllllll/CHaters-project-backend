namespace BLL.Exceptions;

public class UserAlreadyExistException(string message) : Exception(message);