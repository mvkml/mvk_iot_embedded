---
name: reference-vikiapp
description: VIKIApp reference project location and its Component/ManageComponent entity definitions — use when designing Component control layer in MariVshApp
metadata: 
  node_type: memory
  type: reference
  originSessionId: 9237c530-8105-4b86-a5eb-8be1d5b93014
---

Reference project path: `C:\v\v\learn\lv_python\git\mvk_iot_embedded\work\IIoTUI\documents\ref_projects\VIKIApp\VIKIApp\VIKIApp\VIKIApp`

Key folders: `Entities\`, `Models\`

**Why:** VIKIApp is the production predecessor to MariVshApp. Its entity/model design is authoritative for understanding what the Component control layer should look like.

**How to apply:** When designing ManageComponent (M14) or ComponentState/control features, refer to these entities first.

## VIKIApp Components entity (Entities/Components.cs)
Fields beyond our current MariVshApp Component model:
- `VikiId` (string) — unique ID used for HTTP communication with Hub
- `ImageUrl` (string) — component image
- `CategoryId` (int) — category FK
- `DefaultImageId`, `DisableImageId`, `BackGroundImageId` (int) — image refs for on/off/background states

## VIKIApp ManageComponent entity (Entities/ManageComponent.cs)
This is the CONTROL LAYER — the "home dashboard / favourites" entry for a component.
Key fields:
- Full hierarchy path stored flat: ProjectId/Name, SiteId/Name, SubSiteId/Name, ComponentGroupId/Name, ComponentId
- Enable flags: IsProjectEnable, IsSiteEnable, IsSubSiteEnable, IsComponentGroupEnable, IsComponentEnable
- `SwitchOn` (int) — current on/off state
- `StartTime`, `EndTime` (string) — scheduling
- `ComponentUrl` (string) — HTTP endpoint on Hub to send command to
- `VikiId`, `ChildVikiId` — node identifiers for HTTP
- `IsFavourite` (int) — pinned to favourites
- `DeviceId` (int) — physical device
- `JsonObject` (string) — serialized ManageComponentJson (timer/group/pair/ultrasonic config)
- `NormalityValue`, `USSValue`, `USSNeighborhood`, `USDValue`, `USDNieghborhood` — sensor thresholds
- `ComponentsSetId` (string) — group control
- `TypeId` — component type
- `CategoryId`

ManageComponentJson extends MetadataJson:
- `IsTimerEnable`, `IsGroupEnable`, `IsPairEnable`, `IsUltraSonic`, `IsVideo` (bool)
- `Dependents`: List<DependentComponent> (VikiId, Switch, Url)
