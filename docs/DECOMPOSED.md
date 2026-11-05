# DECOMPOSED — Decomposed Task List (Highly Broken-Down Tasks)

**Tier 3 of 3 — the DECOMPOSED task list.** The lowest-level execution-grain task list. Every minor task in `docs/TODO.md` decomposes into one or more decomposed tasks here when YOLO mode picks it up (or when Unity decomposes proactively).

A decomposed task is the **smallest meaningful unit of work** — one file edit, one command, one verification step. If a decomposed task takes more than 15 minutes or touches more than one logical unit, it should be broken down further.

Status markers (same scheme as TODO.md / ROADMAP.md):
- `[ ]` pending
- `[~]` in_progress
- `[x]` complete (move to FINALIZED.md immediately, never leave here)

LAW #0 reminder: every decomposed task preserves the source minor task's verbatim text in its parent reference, AND preserves any user-stated specifics in the decomposition itself.

---

## How the three tiers cascade

```
ROADMAP.md (major)         "<major milestone — multi-session / multi-PR goal>"
       ↓ decomposed into
TODO.md (minor)            "<minor task 1 — day-to-day work grain>"
                           "<minor task 2>"
                           "<minor task 3>"
       ↓ decomposed into
DECOMPOSED.md (this file)  "<smallest unit 1 — single file edit / command / verify step>"
                           "<smallest unit 2>"
                           "<smallest unit 3>"
                           "<smallest unit 4>"
```

YOLO mode picks the next decomposed task in cascade order — current minor's pending decomposed → next decomposed → escalate to next minor when current minor's decomposed list is empty → escalate to next major when current minor list is empty.

---

## In progress

_(none. The R1 seed tasks below come next.)_

---

## Pending

Parent: **R1 — Our own build tools (Helix)**, first minor task. Do these in order.

### [x] Write down the exact Windows exe layout the loader must produce. Done: `wiki/systems/r01a-seed-spec.md`

**Parent minor task (from `docs/TODO.md`, verbatim):**
> "Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code"

**Decomposition rationale:** The loader's output has to be a valid Windows exe, so the layout comes first.

**Files to touch:** `wiki/systems/r01-toolchain.md`

**Verification step:** The layout page lists every required header field and its value, checked against how Windows loads programs.

### [x] Define the hex text input format. Done: `wiki/systems/r01a-seed-spec.md`

**Parent minor task (from `docs/TODO.md`, verbatim):**
> "Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code"

**Decomposition rationale:** Defines what the loader reads: hex digit pairs, with comments and whitespace ignored.

**Files to touch:** `wiki/systems/r01-toolchain.md`, `docs/HOW-TO-BUILD.md`

**Verification step:** A sample input file and its expected output bytes are written side by side.

### [ ] Hand-write the loader as raw bytes, with every byte commented

**Parent minor task (from `docs/TODO.md`, verbatim):**
> "Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code"

**Decomposition rationale:** The seed itself. It's the only program ever typed in by hand.

**Files to touch:** `src/toolchain/seed/` (new)

**Verification step:** Every byte has a comment saying what it does, and the byte count matches the plan.

### [ ] Create the seed exe file from those bytes

**Parent minor task (from `docs/TODO.md`, verbatim):**
> "Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code"

**Decomposition rationale:** Turns the commented bytes into the actual file on disk.

**Files to touch:** `src/toolchain/seed/`

**Verification step:** Windows starts the file without an error.

### [ ] Run the loader on the sample file

**Parent minor task (from `docs/TODO.md`, verbatim):**
> "Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code"

**Decomposition rationale:** Proves that the loader works.

**Files to touch:** `src/toolchain/seed/`

**Verification step:** Its output matches the expected bytes exactly.

### [ ] Make the loader rebuild itself

**Parent minor task (from `docs/TODO.md`, verbatim):**
> "Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code"

**Decomposition rationale:** Proves the seed is trustworthy: written as hex text, run through itself, it must produce a byte-identical copy of itself.

**Files to touch:** `src/toolchain/seed/`

**Verification step:** The new copy and the original are byte-identical.

### [ ] Write the step 0 section of HOW-TO-BUILD

**Parent minor task (from `docs/TODO.md`, verbatim):**
> "Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code"

**Decomposition rationale:** Builders can repeat it.

**Files to touch:** `docs/HOW-TO-BUILD.md`, `wiki/systems/r01-toolchain.md`

**Verification step:** Following the written steps from scratch gives the same seed exe.


---

## TOMBSTONES

_(none)_

---

## Decomposition rules

When a minor task arrives at the top of the YOLO queue:

1. **Read the minor task's verbatim text** from `docs/TODO.md`
2. **Read every file the minor task references** (full file, 800-line chunks per the LAW)
3. **Identify the smallest meaningful units of work** — each becomes a decomposed task
4. **Append decomposed entries here** with this header format:

```markdown
### [ ] <decomposed task title>

**Parent minor task (from `docs/TODO.md`, verbatim):**
> <verbatim minor task text>

**Decomposition rationale:** <one-sentence why this is the right slice>

**Files to touch:** `<path1>` `<path2>` ...

**Verification step:** <how to confirm this slice landed correctly>
```

5. **Flip status** to `[~]` when YOLO picks the task up
6. **Move to FINALIZED.md** when complete (per `§FINALIZED BEFORE DELETE` LAW)

## When NOT to decompose

- **Trivial one-line edits** — a typo fix doesn't need a DECOMPOSED entry; flip the minor task status and ship it
- **Pure-doc updates** that follow a clear template — bundle as a single decomposed task
- **Refactors with no behavior change** that touch <3 files — single decomposed task is fine
- **Tombstoning** — moving an obsolete task doesn't need decomposition

The decomposition tier exists to make multi-step work tractable in YOLO mode, NOT to bureaucratize every edit. Use judgment.
