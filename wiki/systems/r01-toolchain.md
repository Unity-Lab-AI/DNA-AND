---
title: R1 · Toolchain — our own compiler from raw bytes
status: draft
sources: [docs/DESIGN.md, docs/ROADMAP.md#R1, docs/TODO.md#R1]
last-verified: none (plan only)
---

# 🛠️ R1 · Toolchain

> "we make it" · "No dependancies!!!! ... OR COMPOLIERS" · "windows only for now"

Nothing else gets built until this exists. There's no outside tool to lean on, so the chain starts with a tiny assembler script on the PowerShell that ships with Windows, and each step builds the next.

## 🪜 The bootstrap ladder

| Step | Built with | Produces | What it can do |
|---|---|---|---|
| 0️⃣ Mini-assembler | A PowerShell script (PowerShell ships with Windows) | `asm0.ps1` | Turns readable assembly text into a Windows exe |
| 1️⃣ Assembler v1 | Our assembly, built by `asm0.ps1` | `asm1.exe` | The same assembler, now a real exe; it rebuilds itself identically |
| 2️⃣ Assembler | `asm1` | `asm.exe` | Full assembler: labels, macros, data, PE output |
| 3️⃣ Compiler v1 | `asm` | `dnac0.exe` | Compiles our own language |
| 4️⃣ Compiler v2 | `dnac0` (written in Helix) | `dnac.exe` | Self-hosting: the compiler builds itself |
| 5️⃣ Game | `dnac` | `DNA-AND.exe` | The game |

✅ Step 0 is a small assembler written in PowerShell, which comes with Windows ("Start from PowerShell (Suggested)"). After the assembler rebuilds itself, PowerShell is no longer needed. The exe layout follows [the seed spec](r01a-seed-spec.md).

## 🪟 Windows exe format
Windows only runs programs in its own **PE** exe layout. That's a Windows requirement, not a choice. From step 0 on, our assembler writes PE files itself.

## 🧾 Helix, our language
✅ Low-level and C-like: manual memory, no garbage collector. Built for a simulation with billions of things.
✅ The language is called **Helix**.

## ✅ Proof each step works
The rebuilt compiler must compile itself and produce a **byte-identical** copy. Any difference means a bug in the chain.

See also: [How to build](../../docs/HOW-TO-BUILD.md) · [R2 Windows layer](r02-windows-layer.md) · [R16 Packaging](r16-packaging.md)
