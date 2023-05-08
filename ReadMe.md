### Rose String Decryptor

This Is By No Means A Good Decryptor This Is My Very First Attempt At Writing My Own Tools And Unfortunately Enough It Was On The Rose Obfuscator I Found In An Executor Called Selenite, The Rose Obfuscator Has A Namespace Which Appears As Such
[![](https://cdn.discordapp.com/attachments/1066452941294293044/1104939652714074184/image.png)](https://cdn.discordapp.com/attachments/1066452941294293044/1104939652714074184/image.png)

Now Starting Off I Found Some Code Snippets Which I Recognized As A String Decryptor I've Seen Similar With Eazfuscator, .Net Reactor, ConfuserEx, Etc
I Noticed These 2 Methods Here, The Obscure Names I Opened Into Were Int Values Of 0, 1, 1 Which Can Be Seen In My Rewrite Where I Write This Function As A Method Called Decode
[![](https://cdn.discordapp.com/attachments/1066452941294293044/1104940204864843836/image.png)](https://cdn.discordapp.com/attachments/1066452941294293044/1104940204864843836/image.png)
[![](https://cdn.discordapp.com/attachments/1066452941294293044/1104940307201675414/image.png)](https://cdn.discordapp.com/attachments/1066452941294293044/1104940307201675414/image.png)

For Reference Here Is Both Of My Rewrites
```cs
        public static string Decode(string Input)
        {
            byte[] Bytes = Encoding.ASCII.GetBytes(Input);
            for (int i = 0; i < Bytes.Length; i += 1)
            {
                byte[] Array = Bytes;
                int Number = i;
                Array[Number] = (byte)((int)Array[Number] - 1);
            }
            return Encoding.ASCII.GetString(Bytes);
        }
```
```cs
        public static string Decrypt(string Input)
        {
            short Number = 0;
            do
            {
                if (Number == 0)
                    Number = 1;
            }
            while (Number != 1);

            return Encoding.UTF8.GetString(Convert.FromBase64String(Input));
        }
```

Following Finding Both These Methods I Went And Searched Around Running Upon How It's Called
[![](https://cdn.discordapp.com/attachments/1066452941294293044/1104940705090121790/image.png)](https://cdn.discordapp.com/attachments/1066452941294293044/1104940705090121790/image.png)

So I Began My Rewriting Of Our Their Method Works Which You Can See In The Src Unfortunately My Src Is Not The Best And Partially Functions All Though It Can Get Some Strings As Seen Here
[![](https://cdn.discordapp.com/attachments/1066452941294293044/1104940993570152588/342780860_1179653599443581_4911017361900478569_n.png)](https://cdn.discordapp.com/attachments/1066452941294293044/1104940993570152588/342780860_1179653599443581_4911017361900478569_n.png)

If You Want To Test This Yourself Here Is A Download Of The Selenite Build I Downloaded You Can Find Everything In There To Try And Create You're Own, If You Feel Compelled To Help Me With My Code In My Adventure Into .Net Reversing You Are Fully Welcomed To Make A Pull Request Show The Changes And I'll Gladly Accept Thanks <3
[Selenite Download](https://cdn.discordapp.com/attachments/1066452941294293044/1104941399650078750/SeleniteNew.exe "Selenite Download")
