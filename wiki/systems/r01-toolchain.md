---
title: R1 · Toolchain — Windows' built-in C# compiler
status: verified
sources: [docs/DESIGN.md (rounds 19–20), docs/WORK-LAWS.md]
last-verified: build.cmd and src/ at the first C# commit
---

# 🛠️ R1 · Toolchain

> "Use C# built into Windows"

DNA-AND is written in **C#** and compiled by the **C# compiler that ships with Windows** (`csc.exe`, from the .NET Framework 4.x built into Windows 10/11; C# 5).

## 🧱 What we use, and nothing else

| Allowed | Why |
|---|---|
| `csc.exe` | The compiler that comes with Windows |
| The core .NET runtime (`mscorlib`, `System`) | Ships with Windows; basic types, files, threads, math |
| Windows system files (`kernel32`, `user32`, `gdi32`, `d3d12`, `dxgi`, `winmm` and similar) | Called directly from our code for the window, input, sound, timing and graphics |

**Not used:** NuGet packages, outside libraries, game engines, Windows UI frameworks, or any code we didn't write. Every interop declaration (the code that talks to Windows and Direct3D 12) is our own.

## 🪜 The build

```
build.cmd  →  csc.exe (Windows)  →  DNA-AND.exe
```

One build script calls Windows' `csc.exe` on everything in `src/` and writes `build/DNA-AND.exe`.

## ✅ Proof it works
1. `build.cmd` produces an exe with no errors or warnings.
2. The exe opens its own window and closes cleanly.
3. The same source always builds the same way.

## 🗃️ Retired
Helix, the hex seed and the self-built assembler chain are retired (rounds 19–20). The exe-layout spec stays as a reference only: [r01a-seed-spec.md](r01a-seed-spec.md) (stale).

See also: [How to build](../../docs/HOW-TO-BUILD.md) · [R2 Windows layer](r02-windows-layer.md) · [R16 Packaging](r16-packaging.md)
