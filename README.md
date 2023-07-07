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
