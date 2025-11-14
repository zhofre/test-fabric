# PRD — TestFabric Coder MCP

Purpose: Provide coding agents/IDEs with precise, up-to-date context about the TestFabric library via an MCP server, so
they can generate correct code snippets and patterns (e.g., MockBuilder implementations, TestSuite usage) without
hallucination.

---

## 1) Goals and Non‑Goals

Goals

- Expose TestFabric concepts and public contracts to LLMs through an MCP server as tools and resources.
- Enable search and retrieval of: APIs (types/members), summaries, signatures, usage patterns, and code examples.
- Support common developer intents, e.g.,
    - “Build a MockBuilder for my `IRepository`.”
    - “How do I name test methods?”
    - “What’s the recommended way to inject `ILogger<T>` in tests?”
- Keep the index in sync with the repo and NuGet package releases.

Non‑Goals

- Replace official documentation. MCP serves as a machine‑readable companion.
- Advanced semantic search or RAG quality optimization at v1. Substring/TF‑IDF matching is acceptable for MVP.

Success Metrics

- Agents generate compiling snippets against TestFabric for the top 5 workflows (MockBuilder, Test names, TestSuite,
  Random data, ILogger setup) with >90% accuracy.
- <1s median MCP tool latency with a warm server and local index.

---

## 2) Deliverables

- MCP server (C#) exposing:
    - Tool: `TestFabricTools`
    - Resources for “VIP” concepts (e.g., `IMockBuilder`, `MockBuilder<T>`, `TestSuite`, test naming conventions,
      `Random<>`, `ILogger`/`ILogger<T>` patterns)
    - (Optional) Prompt templates for common authoring tasks
- Doc extraction utility (C# console app) that builds a JSON index from the TestFabric source and XML docs
- JSON index schema and a sample populated index committed under `tools/TestFabric.Coder.Mcp/index.json` (generated
  during CI)
- Integration guide for ChatGPT/Cursor/OpenAI Agents
- Basic test coverage for the extractor and MCP server handlers

---

## 3) High‑Level Architecture

1. Extraction pipeline
    - Build TestFabric with XML docs enabled and parse public APIs + XML summaries.
    - Mine example usages from `tests/` and `samples/` (short curated snippets).
    - Emit normalized JSON records.
2. MCP server
    - Loads `index.json` on startup.
    - Implements `TestFabricTools` with simple ranking (substring/TF‑IDF) and paging.
    - Exposes curated resources for “VIP” items.
3. Agent configuration
    - Register the MCP server as a remote tool.
    - Add brief policy to call `TestFabricTools` whenever TestFabric symbols appear.

---

## 4) Data Model (JSON Index)

File: `tools/TestFabric.Coder.Mcp/index.json`

Record shape (array of objects):

```
{
  "symbol": "TestFabric.Mocking.IMockBuilder",
  "kind": "interface|class|struct|enum|method|property|event|module|concept",
  "namespace": "TestFabric.Mocking",
  "assembly": "TestFabric",
  "signature": "public interface IMockBuilder { ... }",
  "summary": "Abstractions for building fluent mocks for tests.",
  "remarks": "...",
  "members": [
    { "name": "WithX", "signature": "IMockBuilder WithX(...)", "summary": "..." }
  ],
  "usageExamples": [
    {
      "title": "Create MockBuilder for IRepository",
      "code": "var sut = new MockBuilder<IRepository>(); // ...",
      "notes": "Demonstrates fluent WithX pattern and async setup via WithFunctionAsync"
    }
  ],
  "tags": ["mock", "builder", "tests"],
  "links": [
    { "rel": "source", "href": "src/TestFabric/..." },
    { "rel": "tests", "href": "tests/TestFabric.Test/..." }
  ]
}
```

VIP concept records (non‑API) use `kind = "concept"` (e.g., test naming conventions, ILogger injection guidance).

---

## 5) MCP Interface Design

Tool: `search_testfabric_docs`

- Description: Search TestFabric public APIs and curated examples.
- Input JSON schema:

```
{
  "type": "object",
  "properties": {
    "query": { "type": "string", "description": "Free‑text query or symbol name" },
    "topK": { "type": "integer", "default": 5, "minimum": 1, "maximum": 20 },
    "includeMembers": { "type": "boolean", "default": true },
    "kinds": {
      "type": "array",
      "items": { "type": "string" },
      "description": "Filter by kinds (interface,class,method,concept,...)"
    }
  },
  "required": ["query"]
}
```

- Output shape:

```
{
  "results": [
    {
      "symbol": "TestFabric.Mocking.IMockBuilder",
      "kind": "interface",
      "summary": "Abstractions for building fluent mocks for tests.",
      "signature": "public interface IMockBuilder { ... }",
      "usageExamples": [ { "title": "...", "code": "..." } ],
      "score": 0.83
    }
  ]
}
```

Resources (curated)

- URIs, examples:
    - `testfabric://mocking/IMockBuilder`
    - `testfabric://mocking/MockBuilder<T>`
    - `testfabric://testing/TestSuite`
    - `testfabric://testing/TestNamingGuidelines`
    - `testfabric://random/TestFabricRandom`
    - `testfabric://logging/ILoggerPatterns`
- Content: Markdown with overview, full signature(s), 1–2 canonical patterns, gotchas.

Prompts (optional v1)

- `CreateMockBuilderImplementation`
- `AuthorUnitTestWithTestSuite`

---

## 6) Detailed Task List (Actionable)

1. Repository plumbing
    - [ ] Add `tools/TestFabric.Coder.Mcp/` scaffolding (exists) ✓
    - [ ] Ensure `src/TestFabric` builds with XML docs enabled in `TestFabric.csproj` (set
      `<GenerateDocumentationFile>true</GenerateDocumentationFile>` for Release) ✓/verify
    - [ ] Decide MCP server language (TypeScript recommended for SDK maturity) and create project under
      `tools/TestFabric.Coder.Mcp/server/`

2. Extraction utility (C# console app)
    - [ ] Create `tools/TestFabric.Coder.Mcp/Extractor/TestFabric.DocExtractor.csproj`
    - [ ] Implement reflection + XML doc merge:
        - Load `src/TestFabric/bin/Release/.../TestFabric.dll` and its XML
        - Walk public types/members; capture namespace, signatures, summaries
    - [ ] Mine curated examples from:
        - `tests/TestFabric.Test` (e.g., MockBuilder tests, test naming, Random helpers, ILogger mocks)
        - `samples/` projects
    - [ ] Normalize into the JSON schema (see §4) and write `../index.json`
    - [ ] Unit tests for extractor (basic sanity: non‑empty, contains IMockBuilder, TestSuite)

3. Define VIP resources
    - [ ] Draft markdown pages for:
        - IMockBuilder and MockBuilder<T>
        - TestSuite base class and test naming guideline (Given_When_Then)
        - MockBuilder `WithFunctionAsync` and fluent API pattern
        - ILogger/ILogger<T> injection patterns (Loose vs Strict)
        - Random data helpers
    - [ ] Store under `tools/TestFabric.Coder.Mcp/resources/*.md`

4. MCP server implementation
    - [ ] Initialize project via official MCP server template
    - [ ] Load `index.json` at startup; build simple searchable index (substring + basic scoring)
    - [ ] Implement tool `search_testfabric_docs` with input/output schemas from §5
    - [ ] Register resources with URIs from §5 and serve markdown content
    - [ ] (Optional) Add prompt definitions for common tasks
    - [ ] Basic unit tests for tool handler

5. Integration with agents/IDEs
    - [ ] Write `INTEGRATION.md` with steps for ChatGPT/Cursor/OpenAI Agents:
        - Register MCP endpoint
        - Add instruction: “When the user references TestFabric symbols, call `search_testfabric_docs` then incorporate
          the results before proposing code.”
        - Provide example sessions and screenshots

6. CI/CD and release
    - [ ] CI job: build TestFabric Release, run extractor, commit/upload `index.json` artifact
    - [ ] CI job: build and package MCP server docker image (if applicable)
    - [ ] Version and changelog policy for `index.json` and resources

7. Quality and acceptance
    - [ ] Manual validation: run example queries (`IMockBuilder`, `TestSuite`, `ILogger<T>`, `WithFunctionAsync`)
    - [ ] Acceptance tests: ensure the 5 key workflows produce correct code snippets that compile against `TestFabric`
    - [ ] Performance: warm start < 500ms, query p50 < 1s locally

8. Security & governance
    - [ ] Local‑only by default; no PII
    - [ ] License headers in resources; respect TestFabric license
    - [ ] Configurable symbol allow‑list (serve only TestFabric namespace)

Timeline (target)

- Week 1: Extractor + initial index + VIP resource drafts
- Week 2: MCP server MVP + tool handler + integration guide
- Week 3: CI wiring + acceptance validation + polish

---

## 7) Usage Scenarios (Examples)

Example 1 — Create MockBuilder for IRepository

1. Agent detects `IRepository` and calls `search_testfabric_docs` with query "MockBuilder IRepository".
2. Response includes `IMockBuilder`, `MockBuilder<T>`, `WithFunctionAsync` example.
3. Agent generates:

```
public sealed class RepositoryMockBuilder : MockBuilder<IRepository>
{
  public RepositoryMockBuilder WithGetById(Func<Guid, Task<Entity?>> func) =>
    this.WithFunctionAsync(r => r.GetByIdAsync(default), func);
}
```

Example 2 — Test naming

1. Agent asks for naming; reads resource `testfabric://testing/TestNamingGuidelines`.
2. Returns guidance and samples: `Given_<assumptions>_When_<action>_Then_<result>`.

