namespace Fina.Api.Commom.Api
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvarioment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.MapSwagger().RequireAuthorization();
        }
    }
}
