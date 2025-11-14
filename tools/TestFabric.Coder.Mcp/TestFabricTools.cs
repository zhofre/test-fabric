namespace TestFabric.Coder.Mcp;

[McpServerToolType]
public static class TestFabricTools
{
    [McpServerTool]
    [Description("Adds two numbers a and b together and returns the sum of those two numbers.")]
    public static double Add(
        [Description("first number")] double a,
        [Description("second number")] double b)
    {
        return a + b;
    }
}
