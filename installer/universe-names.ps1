# Universe sync: the Universe project's own copy of the code and content carries its own name, never DNA-AND's (the code's
# namespace, its file headers, its content files' labels, its icon maker drawing only its own icon). Run by
# universe-sync.cmd on the Universe folder after copying.
param([string]$Root)
$utf8 = New-Object System.Text.UTF8Encoding($false)
# the icon maker: only Universe's own icon (the spiral galaxy)
$mk = Join-Path $Root 'installer\MakeIcons.cs'
if (Test-Path $mk) {
    $t = [System.IO.File]::ReadAllText($mk)
    $t = $t.Replace("the programs' icons, drawn by our own code: Universe a spiral galaxy, DNA-AND a double helix. Writes", "the program's icon, drawn by our own code: a spiral galaxy. Writes")
    $t = $t.Replace("installer\universe.ico and installer\dna-and.ico (each", "installer\universe.ico (each")
    $t = $t.Replace('// DNA-AND: a double helix', '// a double helix (the other program''s icon, not made here)')
    $t = $t.Replace("the builds put in the programs", "the build puts in the program")
    $t = $t.Replace('        Write(Path.Combine(dir, "dna-and.ico"), Helix);' + "`r`n", '').Replace('        Write(Path.Combine(dir, "dna-and.ico"), Helix);' + "`n", '')
    $t = $t.Replace('Console.WriteLine("Wrote universe.ico and dna-and.ico");', 'Console.WriteLine("Wrote universe.ico");')
    [System.IO.File]::WriteAllText($mk, $t, $utf8)
}
foreach ($dir in @('src', 'content', 'installer')) {
    $path = Join-Path $Root $dir
    if (-not (Test-Path $path)) { continue }
    Get-ChildItem -Path $path -Recurse -File -Include *.cs, *.dnat | ForEach-Object {
        $t = [System.IO.File]::ReadAllText($_.FullName)
        $n = $t.Replace('DnaAnd', 'Universe').Replace('DNA-AND', 'Universe')
        $n = $n.Replace('(each program its own: UniverseWindow, DNAANDWindow)', '(its own: UniverseWindow)').Replace("(the program's own name: universe_zoom.log, dnaand_zoom.log)", "(the program's own name: universe_zoom.log)")
        if ($n -ne $t) { [System.IO.File]::WriteAllText($_.FullName, $n, $utf8) }
    }
}
