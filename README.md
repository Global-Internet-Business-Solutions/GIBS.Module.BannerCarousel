# GIBS.Module.BannerCarousel

Banner Carousel module for [Oqtane](https://www.oqtane.org/) targeting **.NET 10**.

## Overview

This module displays a Bootstrap carousel from module records and provides an edit/manage UI for administrators.

Recommended banner image ratio: **16:7**.

## Features

- Carousel rendering on the view page (`Index.razor`)
- Slide fields:
  - `Title` (nvarchar(200))
  - `Description` (nvarchar(400))
  - `ImageUrl` (nvarchar(400))
  - `ShowCaption` (bool)
  - `IsActive` (bool)
  - `OrderBy` (int)
- Conditional caption rendering (`Title`/`Description`) when `ShowCaption` is enabled
- Admin edit form with image picker/upload integration
- Move Up / Move Down ordering controls
- Inline listing/management with edit/delete actions

## Project Structure

- `Client/` – Blazor UI (view, edit, settings, client service)
- `Server/` – API/controller, repository, manager, migrations, static assets
- `Shared/` – shared models and service interface
- `Package/` – NuGet packaging scripts and `.nuspec`

## Data / Migrations

- Initial migration: `01.00.00.00` (`InitializeModule`)
- OrderBy migration: `01.00.01.00` (`AddOrderBy`)
- Table: `GIBSBannerCarousel`

## Settings

Module settings currently include:

- `ImageFolder` – folder used by the file manager in Edit UI
- `SettingName` – general text setting

## Build

From the module root:

```powershell
dotnet build
```

## Package

Package metadata/version is defined in:

- `Package/GIBS.Module.BannerCarousel.nuspec`

Helper scripts:

- `Package/debug.cmd <TargetFramework> <ProjectName>`
- `Package/release.cmd <TargetFramework> <ProjectName>`

Example:

```cmd
Package\release.cmd net10.0 GIBS.Module.BannerCarousel
```

## Module Definition

- Name: `BannerCarousel`
- Current Version: `1.0.1`
- Package Name: `GIBS.Module.BannerCarousel`

Defined in `Client/Modules/GIBS.Module.BannerCarousel/ModuleInfo.cs`.
