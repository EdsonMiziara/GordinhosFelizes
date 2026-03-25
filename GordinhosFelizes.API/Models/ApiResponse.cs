namespace GordinhosFelizes.API.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }

    public static ApiResponse<T> Ok(T data) =>
        new ApiResponse<T> { Success = true, Data = data };

    public static ApiResponse<T> Fail(T data) =>
        new ApiResponse<T> { Success = false, Data = default(T) };
}
