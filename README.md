# PHDC Scores

An application that calculates a football league table with teams, their ranking, and their points, given an input of match results.

It's for assessment purposes.

# Build

Open the solution with JetBrains Rider or Visual Studio and build it using the Release configuration.

# Usage

### Arguments

The only command-line argument is for providing a path to a file containing the scores.

`--filename`: A path to the file.

### Console Input Mode

Run the console launch configuration within the IDE or alternatively run the built executable:

`PhdcScores.Apps.Console.exe`

Follow the on screen instructions.

### File Input Mode

Run the file launch configuration within the IDE or alternatively run the built executable:

`PhdcScores.Apps.Console.exe --filename C:\path\to\file.txt`

# Examples

### Input

```
Spain 3, Portugal 3
Argentina 1, South Africa 0
Spain 1, South Africa 1
Argentina 3, Portugal 1
Spain 4, India 0
```

### Output

```
PHDC Scores
Processing file: Scores.txt
League:
1. Argentina, 6 pts
2. Spain, 5 pts
3. Portugal, 1 pts
4. South Africa, 1 pts
5. India, 0 pts
```
