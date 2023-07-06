# ECDSAToECDH

This project aims to make using ECDSA and ECDH together more simple.
You can create ECDSA keypairs and sign
You can also take that keypair and perform an ECDH shared secret encryption process.

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
