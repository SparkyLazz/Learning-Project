while True:
    #Asking The First and the second Number
    try:
        firstNumber = float(input("Please enter the first Number : "))
        secondNumber = float(input("Please enter the second Number : "))
    except ValueError:
        print("---------------------------------------------------------------------")
        print("Invalid Number | Please provide a valid Number")
        continue

    #Asking which function would be action [add, subtract, devide , multiple]
    print("---------------------------------------------------------------------")
    action = input("| + | - | / | * | Please choose ur action for both number : ")

    #Check userInput based on function
    if action == "+":
        result = firstNumber + secondNumber
        print(f"The result is : {result}")

    elif action == "-":
        result = firstNumber - secondNumber
        print(f"The result is : {result}")

    elif action == "/":
        result = firstNumber / secondNumber
        print(f"The result is : {result}")

    elif action == "/" and secondNumber == 0:
        print("Division by zero is not allowed!")

    elif action == "*":
        result = firstNumber * secondNumber
        print(f"The result is : {result}")
    else:
        print("---------------------------------------------------------------------")
        print("Invalid Action!")

    # Ask if the user wants to perform another calculation
    print("---------------------------------------------------------------------")
    restart = input("Do you want to perform another calculation? (yes/no): ").lower()
    if restart != "yes":
        print("---------------------------------------------------------------------")
        print("Thank you for using the calculator!")
        break
