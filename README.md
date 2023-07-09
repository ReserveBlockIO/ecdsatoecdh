# ECDSAToECDH

[![Generic badge](https://img.shields.io/badge/IDE-VS2022-blue.svg)](https://shields.io/)
[![Generic badge](https://img.shields.io/badge/C%23-10%2E0-blue.svg)](https://shields.io/)
[![Generic badge](https://img.shields.io/badge/%2ENet%20Core-6%2E0-blue.svg)](https://shields.io/)

![license](https://img.shields.io/github/license/ReserveBlockIO/ecdsatoecdh)
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/ReserveBlockIO/ecdsatoecdh/dotnet.yml)
![issues](https://img.shields.io/github/issues/ReserveBlockIO/ecdsatoecdh)
![Discord](https://img.shields.io/discord/917499597692211260?label=discord)

![GitHub commit activity](https://img.shields.io/github/commit-activity/m/ReserveBlockIO/ecdsatoecdh)
![GitHub last commit](https://img.shields.io/github/last-commit/ReserveBlockIO/ecdsatoecdh)

![Nuget](https://img.shields.io/nuget/dt/ECDSAToECDH)

Nuget: https://www.nuget.org/packages/ECDSAToECDH/

`> dotnet add package ECDSAToECDH --version 1.0.0`

This project aims to make using ECDSA and ECDH together more simple.
You can create ECDSA keypairs and sign
You can also take that keypair and perform an ECDH shared secret encryption process.
Elliptic Curve forked from Starkbank and modified for the needs of this code base. 

## Diffie-Hellman algorithm:
The Diffie-Hellman algorithm is being used to establish a shared secret that can be used for secret communications while exchanging data over a public network using the elliptic curve to generate points and get the secret key using the parameters.  

* For the sake of simplicity and practical implementation of the algorithm, we will consider only 4 variables, one prime P and G (a primitive root of P) and two private values a and b.

* P and G are both publicly available numbers. Users (say Alice and Bob) pick private values a and b and they generate a key and exchange it publicly. The opposite person receives the key and that generates a secret key, after which they have the same secret key to encrypt.

![image](https://github.com/ReserveBlockIO/ecdsatoecdh/assets/20599614/099805a1-ff6c-4fad-88fa-19ebc83dbfe3)

```
Step 1: Alice and Bob get public numbers P = 23, G = 9

Step 2: Alice selected a private key a = 4 and
        Bob selected a private key b = 3

Step 3: Alice and Bob compute public values
Alice:    x =(9^4 mod 23) = (6561 mod 23) = 6
        Bob:    y = (9^3 mod 23) = (729 mod 23)  = 16

Step 4: Alice and Bob exchange public numbers

Step 5: Alice receives public key y =16 and
        Bob receives public key x = 6

Step 6: Alice and Bob compute symmetric keys
        Alice:  ka = y^a mod p = 65536 mod 23 = 9
        Bob:    kb = x^b mod p = 216 mod 23 = 9

Step 7: 9 is the shared secret.
```
Explaination above sourced from: https://www.geeksforgeeks.org/implementation-diffie-hellman-algorithm/

To Start you need to load the required information:

```csharp
ECDSAToECDH.ECDH ecdh = new ECDSAToECDH.ECDH {
		PrivateKey = "InsertPrivateKey",
		PublicKey = "InsertPublicKey",
		SharedPublicKey = "InsertSharedPublicKey",
		SharedGeneratedKey = "InsertSharedGeneratedKey"		
	};
 ```
From here you can perform encryption:

```csharp
string plainText = @"Hello, world!";
byte[] encryptedData = ecdh.Encrypt(plainText);
string decryptedText = ecdh.Decrypt(encryptedData);
```
This produces:
```
Plain Text: Hello, world!

Encrypted Data (Hex): 49e623757c70c827968f62e2bc0170d2b27373863023db23b1ecf30c308fe98e

Decrypted Text: Hello, world!
```

The most important pieces you will need are the `SharedPublicKey` and the `SharedGeneratedKey`. These are the items you get from the other party. 

To produce a SharedGeneratedKey you can do the following:

```csharp 
var keytoShare = ECDSAToECDH.Generate.GenerateSharedKey("YourPrivateKey", "YourPublicKey", "SecondPartiesPublicKey");`
```

You can also create keys with the inbuilt ECDSA library by:

```csharp
PrivateKey privKey = new PrivateKey();
//Get PrivateKey secret in hex
var privKeySecretHex = privateKey.secret.ToString("x");
//Get Public Key in Hex
var pubKey = privateKey.publicKey();
var pubKeyHex = addPrefix ? ("04" + ByteToHex(pubKey.toString())) : ByteToHex(pubKey.toString());
```

Basic Key Creation Sample:
```csharp
// Generate a new Private Key
PrivateKey privateKey = new PrivateKey();
PublicKey publicKey = privateKey.publicKey();

string message = "Hello, World!";

// Generate a Signature
Signature signature = Ecdsa.sign(message, privateKey);

// Verify signature
Console.WriteLine(Ecdsa.verify(message, signature, publicKey));
```
