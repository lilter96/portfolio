"""Check union line coverage across every Cobertura report, without double counting."""
import argparse
import sys
import xml.etree.ElementTree as ET
from pathlib import Path


def merge_line_hits(files):
    lines = {}
    for report in files:
        root = ET.parse(report).getroot()
        sources = [Path(item.text) for item in root.findall("./sources/source") if item.text]
        for cls in root.findall(".//class"):
            filename = Path(cls.attrib["filename"])
            if not filename.is_absolute():
                if len(sources) != 1:
                    raise ValueError(f"Ambiguous source roots in {report}")
                filename = sources[0] / filename
            for line in cls.findall("./lines/line"):
                key = (str(filename), int(line.attrib["number"]))
                lines[key] = lines.get(key, False) or int(line.attrib["hits"]) > 0
    return lines


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("directory", type=Path)
    parser.add_argument("--threshold", type=float, default=30)
    args = parser.parse_args()
    files = sorted(args.directory.rglob("coverage.cobertura.xml"))
    if not files:
        parser.error("No coverage reports found")
    lines = merge_line_hits(files)
    if not lines:
        parser.error("Coverage reports contain no executable source lines")
    covered = sum(lines.values())
    percent = covered / len(lines) * 100
    print(f"Merged coverage: {covered}/{len(lines)} lines, {percent:.1f}% "
          f"across {len(files)} reports (threshold: {args.threshold:g}%)")
    return 0 if percent >= args.threshold else 1


if __name__ == "__main__":
    sys.exit(main())
