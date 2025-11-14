using System.Xml.Linq;

namespace TestFabric.Coder.Extractor;

internal static class XmlTools
{
    internal static IndexRecord[] CreateIndexFromXml(
        string xmlPath)
    {
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return [];
        }

        try
        {
            var doc = XDocument.Load(xmlPath);

            var assembly = doc
                .Root?
                .Element("assembly")?
                .Element("name")?
                .Value ?? "";

            var membersParent = doc.Root?.Element("members");
            if (membersParent is null)
            {
                return [];
            }

            // Collect all members grouped by declaring type
            var byType =
                new Dictionary<string, (string Symbol, string Kind, string Namespace, string Assembly, string Signature,
                    string Summary, string Remarks, List<Member> Members)>(StringComparer.Ordinal);

            foreach (var m in membersParent.Elements("member"))
            {
                var full = (string?)m.Attribute("name") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(full) || full.Length < 2)
                {
                    continue;
                }

                var kindPrefix = full[0]; // T/M/P/F/E
                if (full[1] != ':')
                {
                    continue; // unexpected, skip
                }

                var symbol = full.Substring(2);

                switch (kindPrefix)
                {
                    case 'T':
                    {
                        var typeName = symbol;
                        var @namespace = GetNamespace(typeName);
                        var summary = Normalize(GetChildText(m, "summary"));
                        var remarks = Normalize(GetChildText(m, "remarks"));

                        EnsureBucket(byType, typeName);
                        var b = byType[typeName];
                        b.Symbol = typeName;
                        b.Kind = "Type";
                        b.Namespace = @namespace;
                        b.Assembly = assembly;
                        b.Signature = full;
                        b.Summary = summary;
                        b.Remarks = remarks;
                        byType[typeName] = b;
                        break;
                    }
                    case 'M':
                    case 'P':
                    case 'F':
                    case 'E':
                    {
                        var (declaringType, memberName) = SplitDeclaringTypeAndMember(symbol);
                        EnsureBucket(byType, declaringType);
                        var summary = Normalize(GetChildText(m, "summary"));
                        var member = new Member(
                            memberName,
                            full,
                            summary);
                        byType[declaringType].Members.Add(member);
                        // For events/fields/properties we still want type info if not present
                        var b = byType[declaringType];
                        if (string.IsNullOrEmpty(b.Symbol))
                        {
                            b.Symbol = declaringType;
                            b.Kind = "Type";
                            b.Namespace = GetNamespace(declaringType);
                            b.Assembly = assembly;
                            b.Signature = "T:" + declaringType;
                            byType[declaringType] = b;
                        }

                        break;
                    }
                }
            }

            // Project buckets to IndexRecord
            var records = byType.Values
                .OrderBy(b => b.Namespace, StringComparer.Ordinal)
                .ThenBy(b => b.Symbol, StringComparer.Ordinal)
                .Select(b => new IndexRecord(
                    b.Symbol,
                    b.Kind,
                    b.Namespace,
                    b.Assembly,
                    b.Signature,
                    b.Summary,
                    b.Remarks,
                    b.Members.ToArray(),
                    Array.Empty<Example>(),
                    Array.Empty<string>()))
                .ToArray();

            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse XML: {ex.Message}");
            return [];
        }
    }

    private static string GetChildText(
        XElement parent,
        string childName)
    {
        var el = parent.Element(childName);
        if (el is null)
        {
            return string.Empty;
        }

        // Concatenate text including nested <para> etc.
        return string.Concat(el.Nodes().Select(n => n switch
        {
            XText t => t.Value,
            XElement e => e.Value,
            _ => string.Empty
        }));
    }

    private static string Normalize(
        string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        // Collapse whitespace and trim
        return string.Join(" ", text.Split((char[])null!, StringSplitOptions.RemoveEmptyEntries));
    }

    private static string GetNamespace(
        string fullType)
    {
        var lastDot = fullType.LastIndexOf('.');
        return lastDot > 0 ? fullType.Substring(0, lastDot) : string.Empty;
    }

    private static (string declaringType, string memberName) SplitDeclaringTypeAndMember(
        string symbol)
    {
        // symbol is like Namespace.Type.Member or Namespace.Type.#ctor or may include params after ( for methods
        // Remove parameter list for member name extraction
        var baseSymbol = symbol;
        var parenIdx = symbol.IndexOf('(');
        if (parenIdx >= 0)
        {
            baseSymbol = symbol.Substring(0, parenIdx);
        }

        var lastDot = baseSymbol.LastIndexOf('.');
        if (lastDot <= 0)
        {
            return (declaringType: symbol, memberName: symbol);
        }

        var declaring = baseSymbol.Substring(0, lastDot);
        var member = baseSymbol.Substring(lastDot + 1);

        // Normalize constructor name
        if (member == "#ctor")
        {
            var typeShort = declaring.Substring(declaring.LastIndexOf('.') + 1);
            member = typeShort; // display as type name
        }

        return (declaring, member);
    }

    private static void EnsureBucket(
        Dictionary<string, (string Symbol, string Kind, string Namespace, string Assembly, string Signature, string
            Summary,
            string Remarks, List<Member> Members)> map,
        string typeName)
    {
        if (!map.ContainsKey(typeName))
        {
            map[typeName] = (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, new List<Member>());
        }
    }
}
