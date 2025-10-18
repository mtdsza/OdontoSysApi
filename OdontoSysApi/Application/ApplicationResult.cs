namespace OdontoSysApi.Application;

public class ApplicationResult<T>
{
    // O dado principal (payload), pode ser nulo em caso de falha.
    public T? Data { get; private set; }
    // Uma mensagem opcional para o cliente (útil para erros).
    public string? Message { get; private set; }
    // Um booleano que indica claramente o sucesso da operação.
    public bool IsSuccess { get; private set; }
    // O código de status HTTP, para dar mais contexto ao cliente.
    public int StatusCode { get; private set; }

    // Construtor privado para forçar o uso dos métodos de fábrica.
    private ApplicationResult(T? data, bool isSuccess,
     int statusCode, string? message = null)
    {
        Data = data;
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
    
    // Método de Fábrica para criar uma resposta de SUCESSO.
    public static ApplicationResult<T> Success(
    T data, int statusCode = 200,
    string? message = null)
    {
        return new ApplicationResult<T>(data, true, statusCode, message);
    }
    // Método de Fábrica para criar uma resposta de FALHA.
    public static ApplicationResult<T>
        Failure(string message, int statusCode)
    {
        return new ApplicationResult<T>(default, false, statusCode, message);
    }  

}