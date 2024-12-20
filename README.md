# TaxiTrips ETL Project
## About project
The goal of this task is to implement a simple ETL project in CLI that inserts data from a CSV into a single, flat table.

## Appsetings.json/duplicates.csv
Test-Assessment/ETL/ETL/bin/Debug/net8.0

## Deliverables

### 1. SQL Scripts
SQL scripts for creating the database and tables can be found in the `SqlScripts` folder.

### 2. Number of Rows
Number of rows in the table after running the program: **29041**.

### 3.  Assume your program will be used on much larger data files. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file.

To handle a 10GB CSV file, I would implement chunk-based processing to read and process the file in smaller parts, avoiding loading it entirely into memory. I would also optimize database operations by using batch inserts and validate data in smaller subsets to ensure scalability. Additionally, I’d consider streaming data directly to the database or an intermediate storage system to minimize memory usage during processing.
