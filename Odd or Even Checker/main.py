while True:
    
    #Let User sent a random number for check Number
    try:
        userInput = float(input("Please put a random number : "))
        print("------------------------------------------------")

        # Ensure the number is a whole number
        if not userInput.is_integer():
            print("Please enter a whole number to check for Odd or Even.")
            print("------------------------------------------------")
            continue
        userInput = int(userInput)

    except ValueError:
        print("------------------------------------------------")
        print("Invalid Number!")
        print("------------------------------------------------")
        continue
    
    #Check if the number can devided by 2
    if not (userInput % 2 == 0):
        #if not then return odd
        print(f"Number of {userInput} is Odd")
        print("------------------------------------------------")
    else:
        #If yes return even
        print(f"Number of {userInput} is Even")
        print("------------------------------------------------")

    #Ask user to check another number
    more = input("Do you want to perform another check? (yes/no): ").lower()
    if more != "yes":
        print("------------------------------------------------")
        print("Thank you for using the Checker!")
        break