---
title: R1 · Step 0 spec — the seed (hex loader)
status: stale
sources: [wiki/systems/r01-toolchain.md, docs/DECOMPOSED.md, Microsoft PE/COFF specification (format facts only)]
last-verified: none (plan only)
---

# 🌱 The seed: exact spec

> 🔴 **Retired (round 20).** DNA-AND is compiled with the C# compiler that ships with Windows. This page is kept for reference only.

The seed is the **only program typed in by hand**. It reads a text file of hex digits and writes the bytes they spell out to a new file. Every later tool is written as hex text and turned into a real program by the seed. After that, each tool builds the next.

> Format facts below come from the Windows exe (PE32+) layout, which is a requirement of Windows itself (allowed: "Windows itself"). No outside code, tool or file is used. Only the layout rules are followed.

---

## 1️⃣ What the seed does

```
seed.exe <input.hex> <output.bin>
```

| Step | Action |
|:-:|---|
| 1 | Read the command line, then split out the two file names |
| 2 | Open `input.hex` and read it all into memory |
| 3 | Walk the text: skip spaces, tabs and line breaks; skip from `#` to the end of the line; turn each pair of hex digits (`0-9`, `a-f`, `A-F`) into one byte |
| 4 | Write the bytes to `output.bin` |
| 5 | Exit with code 0 on success. Any other code means the hex had an odd digit, a bad character, or a file couldn't be opened |

The seed does **nothing else**. It's kept as small as possible so every byte can be checked by eye.

---

## 2️⃣ The hex text format (`.hex`)

| Rule | Example |
|---|---|
| Two hex digits make one byte | `4D 5A` → bytes 0x4D 0x5A |
| Spaces, tabs and line breaks are ignored | `4D5A` = `4D 5A` = `4D\n5A` |
| `#` starts a comment to the end of the line | `90 # one no-op` |
| Upper and lower case are the same | `ff` = `FF` |
| An odd number of digits is an error | `4D 5` fails with exit code 2 |
| Any other character is an error | `4G` fails with exit code 3 |

Every byte in a `.hex` file gets a comment saying what it is. That's how the seed and every early tool are checked.

---

## 3️⃣ The exe layout the seed itself must have

Windows only runs files in its own exe layout (PE32+ for 64-bit). The seed is a single file with **one section** holding both its code and its import list.

| Part | Offset | Holds |
|---|---|---|
| DOS header | 0x000 | `MZ` and, at 0x3C, the offset of the PE header |
| PE signature | 0x040 | `PE\0\0` |
| File header | 0x044 | Machine x86-64 (0x8664), 1 section, optional header size 0xF0, flags 0x0023 (relocations stripped, executable, large-address aware) |
| Optional header | 0x058 | 64-bit magic 0x20B, entry point, image base 0x140000000, section alignment 0x1000, file alignment 0x200, Windows 6.0+, console subsystem, fixed base (no relocation table needed), 16 data directories |
| Section table | 0x148 | One section, `.text`: readable, writable, executable |
| Code + data | 0x200 | The seed's code, its import list for `kernel32.dll`, and its buffers |

**Windows functions it imports**, all from `kernel32.dll`, which is part of Windows:

| Function | Used for |
|---|---|
| `GetCommandLineA` | Reading the two file names |
| `CreateFileA` | Opening the input and creating the output |
| `GetFileSize` | Knowing how much to read |
| `VirtualAlloc` | Getting memory for the text and the bytes |
| `ReadFile` / `WriteFile` | Reading the hex text and writing the bytes |
| `CloseHandle` | Closing files |
| `ExitProcess` | Exiting with the result code |

---

## 4️⃣ How it's proven to work

| Check | Pass when |
|:-:|---|
| 1 | Windows starts `seed.exe` without an error |
| 2 | A sample `.hex` file produces exactly the expected bytes |
| 3 | Bad input (an odd digit or a bad character) gives the right exit code |
| 4 | **Self-rebuild:** the seed's own `.hex` source, run through the seed, gives a byte-identical copy of `seed.exe` |

---

## 5️⃣ Files

| File | What |
|---|---|
| `bootstrap/seed/seed.hex` | The seed, as commented hex: the source of truth |
| `bootstrap/seed/seed.exe` | Made once from `seed.hex` (see below), then it rebuilds itself |
| `bootstrap/seed/test-*.hex` | Sample inputs and their expected outputs |

**Making the very first `seed.exe`:** the bytes in `seed.hex` are written to disk once using only what ships with Windows. After that the seed rebuilds itself (check 4), and nothing outside the project is ever needed again.

🔗 [R1 · Toolchain](r01-toolchain.md) · [How to build](../../docs/HOW-TO-BUILD.md)
