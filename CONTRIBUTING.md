# Contributing

Thank you for contributing to ViVeTool-GUI. This document describes branch/PR flow, coding style, WPF build/test/publish guidance, and how to run the scanner and trigger the publish workflow.

Branch & PR flow

- Work from forks or topic branches in the main repo. Branch naming:
  - `feature/<short-desc>` for new features
  - `fix/<short-desc>` for bug fixes
  - `chore/<short-desc>` for maintenance
- Rebase or merge `master` regularly to avoid long-lived conflicts.
- Open a Pull Request targeting `master` and include:
  - Short summary and motivation
  - Linked issue (if any)
  - Testing instructions and expected results
  - Screenshots for UI changes

Coding style

- This project uses Visual Basic .NET conventions. Keep code readable and consistent with existing files.
- Formatting: use Visual Studio's formatter (`Format Document`) and follow current project style (2–4 space indentation, PascalCase for types and methods, camelCase for private fields with `_` prefix as used in repo).
- Keep method size reasonable; prefer small, testable units.

WPF build / test / publish

- Restore and build locally:

```powershell
dotnet restore "ViVeTool-GUI.Wpf/ViVeTool-GUI.Wpf.vbproj"
dotnet build "ViVeTool-GUI.Wpf/ViVeTool-GUI.Wpf.vbproj" -c Release
```

- Run the WPF app locally (for manual QA):

```powershell
dotnet run --project "ViVeTool-GUI.Wpf/ViVeTool-GUI.Wpf.vbproj" -c Release
```

- Publish self-contained installer artifacts (example):

```powershell
dotnet publish "ViVeTool-GUI.Wpf/ViVeTool-GUI.Wpf.vbproj" -c Release -r win-x64 --self-contained
```

- Tests: run unit tests (if present) with `dotnet test` and confirm all analyzers pass.

Publishing workflow (GitHub Actions)

- Releases are produced by the `publish` workflow; tags `v*.*.*` trigger publish. You can manually trigger a publish via the workflow_dispatch UI in GitHub.
- Example snippet (add to `.github/workflows/publish.yml`):

```yaml
name: Publish
on:
  push:
    tags:
      - 'v*.*.*'
  workflow_dispatch:

jobs:
  build-and-publish:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'
      - name: Build WPF
        run: dotnet publish ./ViVeTool-GUI.Wpf/ViVeTool-GUI.Wpf.vbproj -c Release -r win-x64 --self-contained
      - name: Package artifacts
        run: |
          # package / signing steps
      - name: Publish to feed
        run: |
          # push to GitHub Releases / feed
```

Scanner

- A security/analysis scanner runs on pull requests in CI. To run local static analysis, run any configured analyzers or `dotnet format` and `dotnet build`.
- If a dedicated scanner job exists in `.github/workflows/` you can re-run it via the Actions UI or trigger via `workflow_dispatch` if available.

Contact & support

- See `SECURITY.md` for vulnerability reporting.
- See `CODE_OF_CONDUCT.md` for community expectations.

Thanks for helping keep the project healthy and secure.
