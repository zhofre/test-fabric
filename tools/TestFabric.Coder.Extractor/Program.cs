using System.Text.Json;
using TestFabric.Coder.Extractor;

Console.WriteLine("TestFabric Coder Extractor");

var baseDir = AppContext.BaseDirectory;

// create the index
var xmlFile = Path.Combine(
    baseDir,
    "..", // -> Debug
    "..", // -> bin
    "..", // -> TestFabric.Coder.Extractor
    "..", // -> tools
    "..", // -> test-fabric
    "src", // -> src
    "TestFabric/bin/Debug",
    "TestFabric.xml");

Console.WriteLine("Reading XML from: " + xmlFile);
IndexRecord[] records = [];


Console.WriteLine("Enriching index with README.md and samples from tests/samples");


Console.WriteLine("Writing index.json");
// Write to tools/TestFabric.Coder.Mcp/index.json (relative to build output dir)
// BaseDirectory typically: .../tools/TestFabric.Coder.Extractor/bin/Debug/<tfm>/
var targetPath = Path.GetFullPath(
    Path.Combine(
        baseDir,
        "..", // -> Debug
        "..", // -> bin
        "..", // -> TestFabric.Coder.Extractor
        "..", // -> tools
        "TestFabric.Coder.Mcp",
        "index.json"));

Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
var body = JsonSerializer.Serialize(records);
File.WriteAllText(targetPath, body);

Console.WriteLine($"index.json written to: {targetPath}");
