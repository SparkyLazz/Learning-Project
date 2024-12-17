## Word Counter

## Description
The **Word Counter** is a terminal-based program that processes a sentence or paragraph provided by the user, removes punctuation, and accurately counts the number of words.

## Features
1. **Punctuation Removal**:
   - Removes all punctuation from the input text to ensure accurate word counting.
2. **Word Splitting**:
   - Splits the cleaned input text into individual words.
3. **Accurate Word Count**:
   - Uses Python's `len()` function to determine the total number of words.
4. **Interactive Input**:
   - Prompts the user to enter text and displays the result.

## Functions Explained
### Removing Punctuation
```python
translator = str.maketrans('', '', string.punctuation)
cleanedInput = userInput.translate(translator)
```
- **`str.maketrans()`** creates a translation table to remove all punctuation.
- **`translate()`** applies the translation table to clean the user input.

### Splitting Text into Words
```python
splittedWord = cleanedInput.split()
```
- **`split()`** divides the cleaned input string into a list of words.
- It automatically handles extra spaces between words.

### Counting Words
```python
len(splittedWord)
```
- Calculates the total number of words in the list.

## How to Run
1. Save the script as `word_counter.py`.
2. Open a terminal and run the script:
   ```bash
   python word_counter.py
   ```
3. Follow the prompt to enter a sentence or paragraph.

## Example Run
```
Enter a sentence or paragraph: Hello, world! This is a simple test.
Total word count: 7
```

## Notes
- The program ensures that punctuation does not affect word counting.
- Handles spaces, special characters, and mixed punctuation effectively.

---
Congratulations on completing the Word Counter project! 🚀
