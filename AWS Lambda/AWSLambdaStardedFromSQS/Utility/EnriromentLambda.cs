namespace AWSLambdaStardedFromSQS.Utility
{
    internal class EnviromentLambda
    {

        public static string GetByEnviromentVariable()
        {
            var environment = System.Environment.GetEnvironmentVariable("ENVIRONMENT")?.ToLowerInvariant() ?? string.Empty;

            if (string.IsNullOrEmpty(environment))
            {
                System.Environment.SetEnvironmentVariable("ENVIRONMENT", "Development");
                environment = System.Environment.GetEnvironmentVariable("ENVIRONMENT")?.ToLowerInvariant() ?? string.Empty;
            }

            var environmentMap = new Dictionary<string, string>
            {
                ["dev"] = ".Development",
                ["hml"] = ".Homologation",
                ["prd"] = ".Production",
            };

            return environmentMap.TryGetValue(environment, out var suffix) ? suffix : string.Empty;
        }
    }
}
