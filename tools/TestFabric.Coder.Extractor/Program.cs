Console.WriteLine("TestFabric Coder Extractor");

// create the index


// write the index

const string json = "[]";

// Write to tools/TestFabric.Coder.Mcp/index.json (relative to build output dir)
// BaseDirectory typically: .../tools/TestFabric.Coder.Extractor/bin/Debug/<tfm>/
var baseDir = AppContext.BaseDirectory;
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
File.WriteAllText(targetPath, json);

Console.WriteLine($"index.json written to: {targetPath}");
