#Import random
import random

#Start picking random number between 1 - 10
randomNumber = random.randrange(1, 11)

#let user answer it
change = 5
win = False
while not win:
    #return losing state if the change 0
    if(change == 0):
        print("You dont have any change now! | You lose")
        break

    try:
        userInput = int(input("Guess the number between 1 - 10 : "))
        #Make sure the user input number between 1 - 10
        if not (1 <= userInput <= 10):
            print("Please enter a number between 1 and 10.")
            continue
            
        change -= 1
        #checking if the user input is higher or lower or even correct
        if(userInput > randomNumber):
            print("> The number is too high | Change : " + str(change))
            print("------------------------------------------------")

        if(userInput < randomNumber):
            print("> The number is too low | Change : " + str(change))
            print("------------------------------------------------")

        if(userInput == randomNumber):
            print("> Yeah , The number is : " + str(randomNumber))
            print("> You Won!")
            print("------------------------------------------------")
            #return winning state if the user correct
            win = True

    except ValueError:
        print("Please enter the valid Number")
        
print("Thanks you for playing! ^^")