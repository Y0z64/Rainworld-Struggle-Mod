using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using RWCustom;
using BepInEx;
using Debug = UnityEngine.Debug;

#pragma warning disable CS0618

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace StruggleMod;

[BepInPlugin("Y0z64.strugglemod", "Struggle Mod", "0.0.1")]
public class StruggleMod : BaseUnityPlugin
{
    private StruggleModOptions Options;

    private int struggleValue = 0;
    private void OnEnable()
    {
        // All hooks go here
        // Atempt to start Options screen
        try
        {
            Options = new StruggleModOptions();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            throw;
        }
        On.RainWorld.OnModsInit += RainWorldOnOnModsInit;
        On.Player.Update += PlayerStruggleHook;
    }
    private void RainWorldOnOnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        // Register Options screen
        orig(self);
        MachineConnector.SetRegisteredOI("Y0z64.strugglemod", Options);
    }

    private void PlayerStruggleHook(On.Player.orig_Update orig, Player self, bool eu)
    {
        orig(self, eu);
        if(Input.anyKeyDown && self.grabbedBy.Count > 0 && !(self.grabbedBy[0].grabber is Player) && !self.dead)
        {
            int releaseValue = 30; // TODO: Add here Option value
            if(struggleValue < releaseValue)
            {
                struggleValue++;
                return;
            }
            // TODO: Add debug Logs here if needed
            self.grabbedBy[0].grabber.Violence(null, Custom.DirVec(self.firstChunk.pos, self.grabbedBy[0].grabber.bodyChunks[0].pos) * 50f, self.grabbedBy[0].grabber.bodyChunks[0], null, Creature.DamageType.Blunt, 0.2f, 130f * Mathf.Lerp(self.grabbedBy[0].grabber.Template.baseStunResistance, 1f, 0.5f));
            self.grabbedBy[0].Release();
        }
    }   
    
}
