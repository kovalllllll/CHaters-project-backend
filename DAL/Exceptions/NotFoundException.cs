namespace DAL.Exceptions;

public class NotFoundException(string message) : Exception(message);