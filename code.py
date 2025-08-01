#!/usr/bin/env python3
import sys
import shutil
import os

def resolve_conflicts(path):
    # Backup original
    backup_path = path + ".bak"
    shutil.copy2(path, backup_path)
    print(f"Backed up original to {backup_path}")

    resolved_lines = []
    in_conflict = False
    keep_local = False

    with open(path, 'r', encoding='utf-8') as f:
        for line in f:
            if line.startswith("<<<<<<<"):
                # Enter conflict, start keeping local
                in_conflict = True
                keep_local = True
                continue
            if in_conflict and line.startswith("======="):
                # Switch to skipping remote
                keep_local = False
                continue
            if in_conflict and line.startswith(">>>>>>>"):
                # End of conflict
                in_conflict = False
                continue

            # If not in a conflict, or in the local part, keep the line
            if not in_conflict or keep_local:
                resolved_lines.append(line)

    # Write resolved file
    with open(path, 'w', encoding='utf-8') as f:
        f.writelines(resolved_lines)

    print(f"Resolved conflicts and wrote cleaned file to {path}")

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("Usage: python resolve_scene_conflicts.py <path-to-unity-scene>")
        sys.exit(1)

    scene_path = sys.argv[1]
    if not os.path.isfile(scene_path):
        print(f"Error: file not found: {scene_path}")
        sys.exit(1)

    resolve_conflicts(scene_path)

