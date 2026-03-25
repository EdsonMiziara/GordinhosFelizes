using GordinhosFelizes.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace GordinhosFelizes.Infrastructure.Middleware;

using System.Text.Json;
using GordinhosFelizes.Domain.Exceptions;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await WriteResponse(context, 404, ex.Message);
        }
        catch (ForbiddenException ex)
        {
            await WriteResponse(context, 403, ex.Message);
        }
        catch (UnauthorizedException ex)
        {
            await WriteResponse(context, 401, ex.Message);
        }
        catch (BusinessException ex)
        {
            await WriteResponse(context, 400, ex.Message);
        }
        catch (HttpRequestErrorException ex)
        {
            await WriteResponse(context, 502, ex.Message);
        }
        catch (TaskCancelException ex)
        {
            await WriteResponse(context, 504, ex.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new
            {
                message = ex.Message,
                inner = ex.InnerException?.Message,
                stackTrace = ex.StackTrace
            });
        }
    }

    private static async Task WriteResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(new
        {
            success = false,
            error = message
        });

        await context.Response.WriteAsync(json);
    }
}