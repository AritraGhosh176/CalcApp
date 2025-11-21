# CalcApp
Simple multi-project calculator solution (Core library, CLI, Web API + minimal UI) with expression evaluation.

## Projects
| Project | Description |
|---------|-------------|
| `CalcCore` | Core library providing `ExpressionEvaluator` (supports + - * / and parentheses). |
| `CalcCli` | Console entry: pass an expression as arguments and get a numeric result. |
| `CalcWeb` | ASP.NET minimal API exposing `/calc?expr=` and serving a simple HTML UI from `wwwroot/index.html`. |
| `CalcTests` | xUnit tests validating precedence, parentheses and error conditions. |

## Quick Usage
Build & test:
```powershell
dotnet build .\CalcCore\CalcCore.csproj
dotnet test .\CalcTests\CalcCore.Tests.csproj
```
CLI:
```powershell
dotnet run --project .\CalcCli\CalcCli.csproj -- "(2+3)*4"
```
Web:
```powershell
dotnet run --project .\CalcWeb\CalcWeb.csproj
# Then browse http://localhost:5000/
```

## Swimm Integration
This repository includes a `.swm` directory with starter documentation to enable developer onboarding using Swimm.

### Connect Repository to Swimm
1. Sign up or log in at https://swimm.io/.
2. Create (or select) a Workspace.
3. Click "Connect Repository" and choose GitHub; install/authorize the Swimm GitHub App if prompted.
4. Grant access to the `CalcApp` repository (adjust permissions if organizational scope required).
5. Wait for Swimm to index the repo (initial indexing may take a short time).
6. In VS Code, install the "Swimm" extension (Extensions panel -> search `Swimm`).
7. Open this repository; the extension will prompt to sign in and link to the Workspace.
8. Open existing docs under `.swm/`, or create new ones (the extension adds code references automatically).
9. Commit new/updated `.swm` docs as normal; they version with your code.
10. Use the extension's verification ("Verify" or status indicators) to keep docs in sync after code changes.

### Existing Docs
- `expression-evaluator-overview.swm`: Design & intended capabilities of `ExpressionEvaluator`.

### Creating More Docs
Recommended additional docs:
- Architecture overview (solution structure).
- API usage guide (web endpoints & sample requests).
- Extensibility roadmap (planned unary minus, functions, etc.).

### CI (Optional)
You can add a GitHub Action to run Swimm verification once a public CLI or API endpoint is available (currently via IDE plugin).

## Roadmap Ideas
- Unary minus & numeric constants like negative numbers.
- Function support (sin, cos, pow) & constants (pi, e).
- Error highlighting in UI.
- GitHub Actions workflow for build & tests.

## License
No license specified yet. Add a LICENSE file if distributing.

