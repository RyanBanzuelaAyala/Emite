using Emite.SignalR.Channel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Emite.Startup
{
    public static class RegisterStartupApp
    {
        public static WebApplication EmiteApp(this WebApplication app, IConfiguration configuration)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<RateLimitingMiddleware>();

            app.UseHttpsRedirection();
            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<EmiteNotificationHub>("/emiteHub");

            return app;
        }
    }
}
