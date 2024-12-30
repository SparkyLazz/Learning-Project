# Simple Password Manager

## Overview
The **Simple Password Manager** is a secure terminal-based application to store, retrieve, and delete encrypted application credentials. It uses AES encryption for data security and JSON for persistence.

---

## Features
1. **Add AppSecure**: Encrypt and save credentials (username and password).
2. **View AppSecure**: Decrypt and view credentials using a user-provided key and IV.
3. **Delete AppSecure**: Decrypt and remove credentials securely with key and IV verification.
4. **Persist Data**: Store all encrypted credentials in a JSON file for long-term access.

---

## How to Use
1. Run the program in your terminal.
2. Use the menu options:
   - Add new credentials.
   - View credentials securely.
   - Delete existing credentials.
3. Provide the correct encryption key and IV whenever required.

---

## Function Details

### Main()
- **Description**: The entry point of the program.
- **Responsibilities**:
  - Displays the main menu.
  - Routes user input to the appropriate function (Add, View, or Delete).
  - Catches and handles invalid input gracefully.

### AddAppSecure()
- **Description**: Prompts the user for application name, username, password, key, and IV.
- **Responsibilities**:
  - Encrypts the username and password using AES encryption.
  - Adds the encrypted credentials to the `_appSecures` list.
  - Saves the updated data to the JSON file.

### ViewAppSecure()
- **Description**: Displays stored application names and allows the user to decrypt specific credentials.
- **Responsibilities**:
  - Renders application names in a formatted table.
  - Prompts the user for ID, key, and IV.
  - Decrypts the selected credentials and displays them.
  - Handles mismatched keys or IVs gracefully.

### DeleteAppSecure()
- **Description**: Deletes a specific credential after verifying with key and IV.
- **Responsibilities**:
  - Renders application names in a formatted table.
  - Prompts the user for ID, key, and IV.
  - Confirms decryption before removing the entry.
  - Updates the JSON file after deletion.

### RenderAppNames()
- **Description**: Displays all stored application names in a grid format.
- **Responsibilities**:
  - Formats the names into rows and columns for readability.

### EncryptStringToBytes()
- **Description**: Encrypts a plaintext string using AES.
- **Responsibilities**:
  - Converts the plaintext to a byte array.
  - Encrypts it with the provided key and IV.
  - Returns the encrypted byte array.

### DecryptStringFromBytes()
- **Description**: Decrypts an encrypted byte array using AES.
- **Responsibilities**:
  - Converts the encrypted byte array to plaintext using the provided key and IV.
  - Returns the decrypted string or an error message if decryption fails.

### Setup()
- **Description**: Prepares the application by loading existing data from the JSON file.
- **Responsibilities**:
  - Searches for a JSON file in the directory.
  - Loads and deserializes data into the `_appSecures` list.

### SaveFile()
- **Description**: Saves the current state of `_appSecures` to the JSON file.
- **Responsibilities**:
  - Serializes the `_appSecures` list to a JSON string.
  - Writes it to the specified file.

### LoadFile()
- **Description**: Loads data from the JSON file into the `_appSecures` list.
- **Responsibilities**:
  - Reads the JSON file.
  - Deserializes its contents into a list of `AppSecure` objects.

---

## Data Model
### AppSecure
- **Fields**:
  - `Id`: Unique identifier for each credential.
  - `ApplicationName`: The name of the application.
  - `Username`: Encrypted username (Base64 encoded).
  - `Password`: Encrypted password (Base64 encoded).

---

## Security Notes
1. **Key and IV**:
   - Users must provide their own key and IV (minimum 16 bytes each).
   - Mismatched keys or IVs will result in incorrect decryption.
2. **Data Persistence**:
   - All encrypted data is saved in a JSON file.
   - Ensure this file is stored securely.

---

## Example Usage
```
Welcome to Password Manager
1. Add AppSecure
2. View AppSecure
3. Delete AppSecure
=========================================================
Your action: 1

Application Name | Empty = Apps: Gmail
Username | Empty = user: myemail@gmail.com
Password | Empty = 123: password123
Key | Minimum 16 bytes: MySecureKey123456
IV | Minimum 16 bytes: MySecureIV123456
Successfully added new Data to database!
```

---

Let me know if you need further details or adjustments!
