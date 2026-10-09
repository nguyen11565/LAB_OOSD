"""Cross-platform project structure checks (NOT a substitute for VS build / SQL integration tests)."""
from pathlib import Path
import re
import xml.etree.ElementTree as ET

root = Path(__file__).resolve().parents[1]
src = root / 'src' / 'QuanLyCongTyDuLich'
proj = src / 'QuanLyCongTyDuLich.csproj'
assert proj.exists(), 'Missing C# project'
assert (root / 'QuanLyCongTyDuLich.sln').exists(), 'Missing solution'
ns = {'m': 'http://schemas.microsoft.com/developer/msbuild/2003'}
p = ET.parse(proj)
compiled = [e.attrib['Include'] for e in p.findall('.//m:Compile', ns)]
assert compiled, 'No source files compiled'
for item in compiled:
    assert (src / item.replace('\\', '/')).exists(), 'Missing compile reference: ' + item
ET.parse(src / 'App.config')
assert '<TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>' in proj.read_text(), 'Framework mismatch'

schema = (root / 'sql' / '01_Schema.sql').read_text()
tables = re.findall(r'CREATE TABLE dbo\.(\w+)', schema, re.I)
assert len(tables) == 16 and len(tables) == len(set(tables)), f'Expected 16 distinct tables, got {tables}'
entities = (src / 'Models' / 'Entities.cs').read_text()
for t in tables:
    assert re.search(r'public sealed class\s+' + re.escape(t) + r'\b', entities), 'Model missing for ' + t
for f in (src / 'Forms').glob('*.cs'):
    assert 'Db.' not in f.read_text(), f'UI bypasses Service: {f.name}'

# C# lightweight scanner: verify paired braces ignoring regular strings, verbatim strings and comments.
def validate_braces(text, name):
    i = 0
    stack = []
    state = 'code'
    while i < len(text):
        c = text[i]
        nxt = text[i + 1] if i + 1 < len(text) else ''
        if state == 'code':
            if c == '/' and nxt == '/': state = 'line'; i += 2; continue
            if c == '/' and nxt == '*': state = 'block'; i += 2; continue
            if c == '@' and nxt == '"': state = 'verbatim'; i += 2; continue
            if c == '"': state = 'string'; i += 1; continue
            if c == "'": state = 'char'; i += 1; continue
            if c in '{([': stack.append(c)
            if c in '})]':
                assert stack, f'Unexpected {c} in {name} at {i}'
                x = stack.pop()
                assert (x, c) in [('(', ')'), ('[', ']'), ('{', '}')], f'Mismatch {x} {c} in {name} at {i}'
        elif state == 'line':
            if c == '\n': state = 'code'
        elif state == 'block':
            if c == '*' and nxt == '/': state = 'code'; i += 2; continue
        elif state == 'verbatim':
            if c == '"' and nxt == '"': i += 2; continue
            if c == '"': state = 'code'
        elif state in ('string', 'char'):
            if c == '\\': i += 2; continue
            if c == ('"' if state == 'string' else "'"): state = 'code'
        i += 1
    assert not stack, f'Unbalanced delimiters in {name}: {stack}'
    assert state in ('code', 'line'), f'Unterminated C# string or comment in {name}: {state}'

for f in src.rglob('*.cs'):
    validate_braces(f.read_text(), str(f.relative_to(root)))

uml = list((root / 'docs' / 'uml').glob('*.puml'))
assert len(uml) == 6, f'Expected six UML files, got {len(uml)}'
assert all('@startuml' in x.read_text() and '@enduml' in x.read_text() for x in uml)
assert (root / 'tests' / '03_SmokeTests.sql').exists()
assert (root / 'tests' / 'KichBanKiemThu.md').exists()
print(f'STATIC CHECK PASS: {len(compiled)} C# compile items; 16 SQL tables and C# entity classes;')
print(f'                    {len(list((src / "Forms").glob("*.cs")))} UI files with no direct Data calls; 6 UML files; XML and C# delimiter checks.')
print('NOTE: Visual Studio build and SQL Server test execution were NOT performed here.')
