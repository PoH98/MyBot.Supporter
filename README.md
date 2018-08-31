# MyBot.Supporter
This program is used to support a gaming bot program, which called MyBot.Run thaat is available here: https://github.com/MyBotRun/MyBot/

This program is only for learning purpose. It should not be used in commercial purpose but available for all people to use it.

#How to implement Plug-ins dll

1. Download Visual Studio (Is better)
2. Create a new project and select .NET 4.5 dll
3. Reference to MyBot.Supporter.Main.exe
4. Copy the codes below

```
using System;
using MyBot.Supporter.Plugin;

namespace HelloWorld
{
     public class FirstProgram: plugin
     {
	public void function()
        {
            //Your functions
        }

        public bool RunOnce()
        {
            return true; //Is this function run once only or keep looping?
        }

        public string WriteLog()
        {
            return "Hello World!"; //Show log inside Supporter's Custom Plugin log text box
        }
     }
}
```

5. Compile the program and place it into ...\Plugins\*.dll
6. Run Supporter and check the log! ^v^