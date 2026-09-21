using BoolonxMenu.Mods;
using BoolonxMenu.Classes;
using static BoolonxMenu.Menu.Main;
using static BoolonxMenu.Settings;
using GorillaLocomotion;
using UnityEngine;

namespace BoolonxMenu.Menu
{
    public class Buttons
    {
        /*
         * Here is where all of your buttons are located.
         * To create a button, you may use the following code:
         * 
         * Move to Category:
         *   new ButtonInfo { buttonText = "Settings", method =() => currentCategory = 1, isTogglable = false, toolTip = "Opens the main settings page for the menu."},
         *   new ButtonInfo { buttonText = "Return to Main", method =() => currentCategory = 0, isTogglable = false, toolTip = "Returns to the main page of the menu."},
         * 
         * Togglable Mod:
         *   new ButtonInfo { buttonText = "Platforms", method =() => Movement.Platforms(), toolTip = "Spawns platforms on your hands when pressing grip."},
         *   
         * Making mods enabled by default:
         *  new ButtonInfo { buttonText = "Gunlib Fix", method =() => currentCategory = 0, isTogglable = true, toolTip = "Fixes issue With IItemp Gunlib not disabling", enabled = true },
         */

        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods [0]
                //new ButtonInfo { buttonText = "boolonx.com",  method =() => UnityEngine.Application.OpenURL("https://boolonx.com"), isTogglable = false, toolTip = "Takes you to my website"},
                //new ButtonInfo { buttonText = "boolonx discord",  method =() => UnityEngine.Application.OpenURL("https://discord.gg/zTbB7mVf55"), isTogglable = false, toolTip = "Takes you to my discord"},
                new ButtonInfo { buttonText = "Movement Mods",  method =() => currentCategory = 1, isTogglable = false, toolTip = "Mods page"},
                new ButtonInfo { buttonText = "Rig Mods",  method =() => currentCategory = 2, isTogglable = false, toolTip = "Mods page"},
                new ButtonInfo { buttonText = "Miscellaneous",  method =() => currentCategory = 3, isTogglable = false, toolTip = "Mods page"},
                new ButtonInfo { buttonText = "RPC Stuff",  method =() => currentCategory = 4, isTogglable = false, toolTip = "Mods page"},
            },
            new ButtonInfo[] { // movement mods [1]
                new ButtonInfo { buttonText = "Return to Main", method =() => currentCategory = 0, isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Physics Gorilla", enableMethod =() => PhysicsGorilla.InitializePhysics(), disableMethod =() => PhysicsGorilla.DisablePhysics(), isTogglable = true, toolTip = "Changes the entire movement system to a physics based remake."},
                new ButtonInfo { buttonText = "Physics Climb", enableMethod =() => PhysicsGorilla.PhysicsGrabEverywhere = true, disableMethod =() =>  PhysicsGorilla.PhysicsGrabEverywhere = false, isTogglable = true, toolTip = "Allows you to climb on any surface. Requires Physics Gorilla to be enabled."},
                new ButtonInfo { buttonText = "Punch Mod", method =() => PunchMod.PunchUpdate(), isTogglable = true, toolTip = "Punch mod from Gold's Spooky Terror."},
                new ButtonInfo { buttonText = "Gorilla Tah Fly", enableMethod =() => GorillaTahFly.Enable(), disableMethod =() => GorillaTahFly.Disable(), method =() => GorillaTahFly.Tick(), isTogglable = true, toolTip = "Fly mode from Gorilla Tah's Sandbox gamemode."},
                new ButtonInfo { buttonText = "Bark Fly", enableMethod =() => FlyGuy.Enable(), disableMethod =() => FlyGuy.Disable(), isTogglable = true, toolTip = "Fly mod from the Bark mod menu."},
                new ButtonInfo { buttonText = "Noclip", enableMethod =() => Noclip.Enable(), disableMethod =() => Noclip.Disable(), method =() => Noclip.Tick(), isTogglable = true, toolTip = "Phase through objects by pressing left trigger."},
                new ButtonInfo { buttonText = "Platforms", method =() => Movement.Platforms(), isTogglable = true, toolTip = "plahforms"},
            },
            new ButtonInfo[] { // rig mods [2]
                new ButtonInfo { buttonText = "Return to Main", method =() => currentCategory = 0, isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Body Rotation", enableMethod =() => GSTBodyRotation.Enabled = true, disableMethod =() =>  GSTBodyRotation.Enabled = false, method =() => GSTBodyRotation.Tick(), isTogglable = true, toolTip = "Change the body rotation settings on the computer."},
                new ButtonInfo { buttonText = "Ghost Monkey", enableMethod =() => GhostMonkey.Enable(), disableMethod =() => GhostMonkey.Disable(), isTogglable = true, toolTip = "Freeze your rig by pressing your left trigger button."},
                new ButtonInfo { buttonText = "Player Lookup Gun", enableMethod =() => PlayerGunThingy.Enable(), disableMethod =() => PlayerGunThingy.Disable(), method =() => PlayerGunThingy.Tick(), isTogglable = true, toolTip = "left grip left trigger ykykykkykykykkykky"}
            },
            new ButtonInfo[] { // misc mods [3]
                new ButtonInfo { buttonText = "Return to Main", method =() => currentCategory = 0, isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Fast Throwing",  enableMethod =() => SnowballFast.Enabled = true, disableMethod =() => SnowballFast.Enabled = false, isTogglable = true, toolTip = $"Throw snowballs and other grabbable items at certain speeds. Change the speed in the BOOLONX tab on the computer."},
                new ButtonInfo { buttonText = "No Tap Cooldown", enableMethod =() => GorillaTagger.Instance.tapCoolDown = 0f, disableMethod =() => GorillaTagger.Instance.tapCoolDown = Plugin.initHandTapCooldown, isTogglable = true, toolTip = "Disables cooldown for hand taps."},
                new ButtonInfo { buttonText = "Make Brian Griffin",  method =() => Plugin.MakeBrian(GTPlayer.Instance.headCollider.transform.position + (GTPlayer.Instance.headCollider.transform.forward * 4)), isTogglable = false, toolTip = "Test function from the boolonx.com AssetBundle."},
                new ButtonInfo { buttonText = "Clear Logs",  method =() => Notifications.NotifiLib.ClearAllNotifications(), isTogglable = false, toolTip = "Clears all logs."},
                new ButtonInfo { buttonText = "Disable Logs",  enableMethod =() => Notifications.NotifiLib.IsEnabled = false, disableMethod =() => Notifications.NotifiLib.IsEnabled = true, isTogglable = true, toolTip = "Stops all logs."},
                new ButtonInfo { buttonText = "Disable Auto Sync Transforms",  enableMethod =() => Physics.autoSyncTransforms = false, disableMethod =() => Physics.autoSyncTransforms = true, isTogglable = true, toolTip = "Stops all logs."},
            },
            new ButtonInfo[] { // rpcs [4]
                new ButtonInfo { buttonText = "Return to Main", method =() => currentCategory = 0, isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Set Room Limit to 20 (Master)",  method =() => RoomHax.ApplyMaxPlayers(), isTogglable = false, toolTip = "barely works broooo"},
                new ButtonInfo { buttonText = "ok",  method =() => Okay.TippyTappy(336), isTogglable = false, toolTip = "okaeyyhh"},
                new ButtonInfo { buttonText = "hello aaa",  method =() => Okay.TippyTappy(337), isTogglable = false, toolTip = "hello aaa"},
            }
        };
    }
}
