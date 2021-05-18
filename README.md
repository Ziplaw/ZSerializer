# ZSaver
ZSaver is an easy, all-in-one Serialization solution for Unity. It supports serialization for every one of Unity's built in Components, and an easy way of Serializing your own!
## Installation
To get started, simply [right click me and select "Copy this link address"](https://github.com/Ziplaw/ZSave.git), paste it to the Package Manager's **Add Package From git URL** text box and click **Add**.
You're done :)
## Instructions
ZSaver is really simple to use, there's only **two** things to remember:

 - **Persistent Attribute**
 - **Persistent GameObject Component**
The Persistent Attribute will make any class Serializable and Ready to be saved. You just need to add the attribute at the top of your class like this:
```cs
using ZSave;

[Persistent]
public class Testing : MonoBehaviour {
```
Then go to Tools/ZSaver/Persistent Classes Configurator in the Unity Toolbar and select **Remake All**
Then you will be prompted to select a folder, this is where your Serializable class will be stored.
You won't need to be touching this files, so I recommend you save them in Assets/ZSavers.
And you're done! Your class is now serializable and ready to be used.

The Persistent GameObject Component is even easier to use!
Simple add the Persistent GameObject Component to the GameObject you wont to retain and you're good to go.

### Loading and saving
Whenever you want to make a save state of your Persistent GameObjects and Components, just call 
```cs
PersistanceManager.SaveAllObjectsAndComponents();
```
To load it, just call 
```cs
PersistanceManager.LoadAllObjectsAndComponents();
```
That's it :)
