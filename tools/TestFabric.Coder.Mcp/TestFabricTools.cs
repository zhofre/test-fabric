using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace TestFabric.Coder.Mcp;

[McpServerToolType]
public static class TestFabricTools
{
    private static readonly Lazy<List<IndexRecord>> _lazyIndex = new(LoadIndex);

    private static List<IndexRecord> LoadIndex()
    {
        var indexPath = Path.Combine(AppContext.BaseDirectory, "index.json");
        if (!File.Exists(indexPath))
        {
            return [];
        }

        var json = File.ReadAllText(indexPath);
        return JsonSerializer.Deserialize<List<IndexRecord>>(json) ?? [];
    }

    [McpServerTool]
    [Description("Search TestFabric public APIs and curated examples")]
    public static SearchResult SearchTestfabricDocs(
        [Description("Free-text query or symbol name")]
        string query,
        [Description("Maximum number of results to return (1-20)")]
        int topK = 5,
        [Description("Include member details in results")]
        bool includeMembers = true,
        [Description("Filter by kinds (e.g., Type, Method, Concept)")]
        string[]? kinds = null)
    {
        var index = _lazyIndex.Value;

        // Normalize the query for case-insensitive search
        var queryLower = query.ToLowerInvariant();
        var queryTerms = queryLower.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Search and score results
        var scoredResults = new List<(IndexRecord record, double score)>();

        foreach (var record in index)
        {
            // Filter by kinds if specified
            if (kinds is { Length: > 0 } && !kinds.Contains(record.Kind, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            var score = CalculateScore(record, queryLower, queryTerms);
            if (score > 0)
            {
                scoredResults.Add((record, score));
            }
        }

        // Sort by score descending and take top K
        var topResults = scoredResults
            .OrderByDescending(x => x.score)
            .Take(Math.Clamp(topK, 1, 20))
            .Select(x => new SearchResultItem(
                x.record.Symbol,
                x.record.Kind,
                x.record.Summary,
                x.record.Signature,
                x.record.Examples.Select(e => new UsageExample(
                    e.Title,
                    e.Code,
                    e.Notes
                )).ToArray(),
                includeMembers
                    ? x.record.Members.Select(m => new MemberInfo(
                        m.Name,
                        m.Signature,
                        m.Summary
                    )).ToArray()
                    : null,
                Math.Round(x.score, 2)
            ))
            .ToArray();

        return new SearchResult(topResults);
    }

    private static double CalculateScore(
        IndexRecord record,
        string queryLower,
        string[] queryTerms)
    {
        var score = 0.0;

        var symbolLower = record.Symbol.ToLowerInvariant();
        var summaryLower = record.Summary.ToLowerInvariant();
        var tagsLower = record.Tags.Select(t => t.ToLowerInvariant()).ToArray();

        // Exact symbol match - highest weight
        if (symbolLower == queryLower)
        {
            score += 100.0;
        }
        // Symbol contains full query
        else if (symbolLower.Contains(queryLower))
        {
            score += 50.0;
        }

        // Score based on individual terms
        foreach (var term in queryTerms)
        {
            // Symbol match
            if (symbolLower.Contains(term))
            {
                score += 10.0;
            }

            // Summary match
            if (summaryLower.Contains(term))
            {
                score += 5.0;
            }

            // Tag match
            if (tagsLower.Any(tag => tag.Contains(term)))
            {
                score += 8.0;
            }

            // Member name match
            foreach (var member in record.Members)
            {
                if (member.Name.ToLowerInvariant().Contains(term))
                {
                    score += 3.0;
                }
            }
        }

        return score;
    }
}

public record IndexRecord(
    [property: JsonPropertyName("Symbol")] string Symbol,
    [property: JsonPropertyName("Kind")] string Kind,
    [property: JsonPropertyName("Namespace")]
    string Namespace,
    [property: JsonPropertyName("Assembly")]
    string Assembly,
    [property: JsonPropertyName("Signature")]
    string Signature,
    [property: JsonPropertyName("Summary")]
    string Summary,
    [property: JsonPropertyName("Remarks")]
    string Remarks,
    [property: JsonPropertyName("Members")]
    IndexMember[] Members,
    [property: JsonPropertyName("Examples")]
    IndexExample[] Examples,
    [property: JsonPropertyName("Tags")] string[] Tags
);

public record IndexMember(
    [property: JsonPropertyName("Name")] string Name,
    [property: JsonPropertyName("Signature")]
    string Signature,
    [property: JsonPropertyName("Summary")]
    string Summary
);

public record IndexExample(
    [property: JsonPropertyName("Title")] string Title,
    [property: JsonPropertyName("Code")] string Code,
    [property: JsonPropertyName("Notes")] string Notes
);

public record SearchResult(
    [property: JsonPropertyName("results")]
    SearchResultItem[] Results
);

public record SearchResultItem(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("kind")] string Kind,
    [property: JsonPropertyName("summary")]
    string Summary,
    [property: JsonPropertyName("signature")]
    string Signature,
    [property: JsonPropertyName("usageExamples")]
    UsageExample[] UsageExamples,
    [property: JsonPropertyName("members")]
    MemberInfo[]? Members,
    [property: JsonPropertyName("score")] double Score
);

public record UsageExample(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("code")] string Code,
    [property: JsonPropertyName("notes")] string Notes
);

public record MemberInfo(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("signature")]
    string Signature,
    [property: JsonPropertyName("summary")]
    string Summary
);
