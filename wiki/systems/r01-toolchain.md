---
title: R1 · Toolchain — our own compiler from raw bytes
status: draft
sources: [docs/DESIGN.md, docs/ROADMAP.md#R1, docs/TODO.md#R1]
last-verified: none (plan only)
---

# 🛠️ R1 · Toolchain

> "we make it" · "No dependancies!!!! ... OR COMPOLIERS" · "windows only for now"

Nothing else gets built until this exists. There's no outside tool to lean on, so the chain starts as **raw machine bytes** and each step builds the next.

## 🪜 The bootstrap ladder

| Step | Built with | Produces | What it can do |
|---|---|---|---|
| 0️⃣ Seed | Typed in by hand as raw bytes | `seed.exe` | Turns a text file of hex digits into bytes |
| 1️⃣ Mini-assembler | Hex, fed through the seed | `asm0.exe` | Turns simple written instructions into machine code |
| 2️⃣ Assembler | `asm0` | `asm.exe` | Full assembler: labels, macros, data, PE output |
| 3️⃣ Compiler v1 | `asm` | `dnac0.exe` | Compiles our own language |
| 4️⃣ Compiler v2 | `dnac0` (written in our language) | `dnac.exe` | Self-hosting: the compiler builds itself |
| 5️⃣ Game | `dnac` | `DNA-AND.exe` | The game |

💬 *Suggestion:* the seed is a tiny hex loader, not the whole assembler in raw bytes. → [S1](../../docs/suggestions/D.md#s1--bootstrap-seed)

## 🪟 Windows exe format
Windows only runs programs in its own **PE** exe layout. That's a Windows requirement, not a choice. From step 2 on, our assembler writes PE files itself.

## 🧾 Our language
💬 *Suggestion:* low-level, C-like, manual memory, no garbage collector. That suits a simulation with billions of things. → [S2](../../docs/suggestions/D.md#s2--language-style)
💬 *Suggestion:* no name yet, called "our language" in docs. → [S3](../../docs/suggestions/D.md#s3--language-name)

## ✅ Proof each step works
The rebuilt compiler must compile itself and produce a **byte-identical** copy. Any difference means a bug in the chain.

See also: [How to build](../../docs/HOW-TO-BUILD.md) · [R2 Windows layer](r02-windows-layer.md) · [R16 Packaging](r16-packaging.md)
