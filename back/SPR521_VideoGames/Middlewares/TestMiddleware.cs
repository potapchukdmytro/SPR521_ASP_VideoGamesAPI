namespace SPR521_VideoGames.Middlewares
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Request
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Custom middleware invoked");
            Console.ResetColor();
            
            await _next(context);

            // Response
        }
    }
}
