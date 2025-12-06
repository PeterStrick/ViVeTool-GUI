# ViVeTool-GUI

Note: This repository is a fork of the deprecated ViVeTool-GUI originally maintained by PeterStrick. The original repository is deprecated; this fork continues active maintenance and development.

Windows Feature Control GUI based on ViVe / ViVeTool.

Fork & Origin
----------------

- This repository is a fork of the original ViVeTool-GUI by PeterStrick ([PeterStrick/ViVeTool-GUI](https://github.com/PeterStrick/ViVeTool-GUI)). The original project is marked as deprecated; this fork continues active maintenance and modernization efforts.

Deprecation note
----------------

- The original WinForms-based UI in the upstream repository is deprecated in favor of the WPF rewrite. If you are looking for legacy behavior or old releases, refer to the upstream repository archives.

Contributing & Support
----------------------

- Contributions and issues are welcome. See `CONTRIBUTING.md` for guidelines on patches, PRs, and CI expectations.
- For security or vulnerability reports, follow the guidance in `SECURITY.md`.

Contact
-------

- Primary maintainer: mta1124-1629472
- For release/CI questions open an issue or ping maintainers via the repository's issue tracker.

Status

- This repo is actively maintained — original WinForms is legacy, WPF (.NET 9) is current, Telerik dependency removal in progress.

Maintainers

- Primary maintainer: mta1124-1629472
- Contact: see SECURITY.md for vulnerability reporting

License

- This project is licensed under GPLv3. See LICENSE for details.

Overview

- ViVeTool-GUI provides a graphical frontend for ViVe / ViVeTool feature control.
- The legacy WinForms UI is retained for historical reference; the WPF UI (targeting .NET 9) is the actively developed and supported implementation.

Feed / Publishing workflow (summary)

- Builds and publish artifacts are performed by GitHub Actions workflows.
- Feed/Publishing trigger:
  - Releases (tag push) trigger publish workflow for packaged installers and artifacts.
  - Manually-triggered workflow_dispatch workflows exist for preview feed publishes.
- Scanner and CI:
  - A security/analysis scanner runs on pull requests (see .github/workflows/*).
  - WPF package builds and smoke tests run in CI for PRs targeting the WPF branch.

Example publish workflow snippet

```yaml
# Example: .github/workflows/publish.yml
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
        run: dotnet publish ./src/ViVeTool.WPF/ViVeTool.WPF.csproj -c Release -r win-x64 --self-contained
      - name: Package artifacts
        run: | 
          # package / sign steps here
      - name: Publish to feed
        run: |
          # push to GitHub Releases / private feed
```

Future plans

- Harden WPF tests and add automated UI tests for core flows.
- Provide signed installer artifacts in the release pipeline.

Where to start

- See CONTRIBUTING.md for branch/PR workflow, coding style, and WPF build/test/publish guidelines.
- See .github/ISSUE_TEMPLATE and .github/PULL_REQUEST_TEMPLATE for how to open issues and PRs.
