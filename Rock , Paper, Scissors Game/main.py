#Import a random libary
import random
while True:
    # Define the rules using a dictionary
    rules = {
    "rock": {"win": "scissors", "lose": "paper"},
    "paper": {"win": "rock", "lose": "scissors"},
    "scissors": {"win": "paper", "lose": "rock"}
    }

    # Computer randomly chooses an action
    computerChoice = random.choice(list(rules.keys()))

    #Let the user choose
    try:
        userInput = input("Choose your action [rock, paper, scissors] : ").lower()
        print("-------------------------------------------------------------")

        if userInput not in rules:
            print("Invalid action! Please choose one of: rock, paper, scissors.")
            print("-------------------------------------------------------------")
            continue

    except ValueError:
        print("Invalid Character")
        continue

    #Condition if win
    if computerChoice == rules[userInput]["lose"]:
        print("You lose the game!")
        print(f"> Computer choice : {computerChoice} | Your choice : {userInput}")
        print("-------------------------------------------------------------")
    elif userInput == computerChoice:
        print("Game tie!")
        print(f"> Computer choice : {computerChoice} | Your choice : {userInput}")
        print("-------------------------------------------------------------")
    elif computerChoice == rules[userInput]["win"]:
        print("You win the game!")
        print(f"> Computer choice : {computerChoice} | Your choice : {userInput}")
        print("-------------------------------------------------------------")

    #Ask user to check another number
    more = input("Do you want to perform another play? (yes/no): ").lower()
    if more != "yes":
        print("------------------------------------------------")
        print("Thank you for using the game!")
        break