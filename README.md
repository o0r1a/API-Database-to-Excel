# EB API: Database to Excel Export Tool

This project is a custom `.NET` plugin for **Engineering Base (EB)** designed to extract structured loop metadata from an EB project using API and create an Assistant that exports it to a formatted custom Excel file.
This is intended to support loop diagrams, documentation, and field wiring summaries.

---

## How It Works

- Integrates with EB using the `PlugInWizard` interface.
- Collects EB `ObjectItem`s and inspects their `Kind`, `Aggregations`, and `Associations`.
- Maps relevant data into a C# object model.
- Uses **ClosedXML** to export a clean, styled Excel file.

---

##  Current Issues & Limitations

-  **Performance Bottleneck:** The plugin freezes or hangs EB when run on large projects. This may be due to deep recursive traversal without filters.
-  **Unknown Data Structure:** Exact locations and relationships between components like `Sensor`, `JB`, and `Cable` are unclear without further manual exploration.
-  **UI Improvements Pending:** A basic WinForms UI is used but needs redesign and progress handling.
-  **No Data Input Sample Yet:** Mapping logic can’t be finalized until actual EB structure is fully understood.

---
## Output Example (Planned)

| Sensor | Pin | Sensor Cable | Wire 1 | JB  | JBIB   | JB Cable | Wire 2 |
|--------|-----|--------------|--------|-----|--------|----------|--------|
| 13V09  | 1   | WV           | RT     | JB2B| JB213  | W3       | 17     |

---



