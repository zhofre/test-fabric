namespace TestFabric.Coder.Extractor;

public record Member(
    string Name,
    string Signature,
    string Summary);

public record Example(
    string Title,
    string Code,
    string Notes);

public record IndexRecord(
    string Symbol,
    string Kind,
    string Namespace,
    string Assembly,
    string Signature,
    string Summary,
    string Remarks,
    Member[] Members,
    Example[] Examples,
    string[] Tags);
