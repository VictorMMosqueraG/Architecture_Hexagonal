namespace Api.Middlewares.Validators
{
    public class XBranchIdValidator
    {
        public static void Ensure(HttpContext context)
        {
            var branchId = context.Request.Headers.TryGetValue("X-Branch-ID", out var bid) && !string.IsNullOrWhiteSpace(bid)
                ? bid.ToString()
                : null;

            context.Items["X-Branch-ID"] = branchId;
        }
    }
}