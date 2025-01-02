import string
# Get input from user
userInput = input("Enter a sentence or paragraph: ")

# Remove punctuation
translator = str.maketrans('', '', string.punctuation)
cleanedInput = userInput.translate(translator)

# Split the input text into words and count them, also strip extra spaces
splittedWord = cleanedInput.split()

# Print the total number of words back to the user
print(f"Total word count: {len(splittedWord)}")
