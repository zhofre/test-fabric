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
var records = XmlTools.CreateIndexFromXml(xmlFile);

Console.WriteLine("Enriching index with README.md and samples from tests/samples");
var examples = Enrich.CreateExamples();

// Merge examples into the matching records by Symbol
foreach (var kvp in examples)
{
    var symbol = kvp.Key;
    var newExamples = kvp.Value;

    var idx = Array.FindIndex(records, r => r.Symbol == symbol);
    if (idx < 0)
    {
        continue; // no matching record, skip
    }

    var existing = records[idx].Examples;
    var merged = existing.Concat(newExamples).ToArray();

    records[idx] = records[idx] with { Examples = merged };
}

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
var body = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
File.WriteAllText(targetPath, body);

Console.WriteLine($"index.json written to: {targetPath}");

// -------------- Helpers --------------
