﻿using Microsoft.AspNetCore.Http;


namespace eCommerce.SharedLibrary.Middleware
{
    public class ListenToOnlyApiGateway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //Extract specific header from the request
            var signedHeader = context.Request.Headers["API-Gateway"];

            //Null means, the request is not coming from the API gateway - 503 service unavailable
            if (signedHeader.FirstOrDefault() is null) 
            { 
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry, service is unavailabe");
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
