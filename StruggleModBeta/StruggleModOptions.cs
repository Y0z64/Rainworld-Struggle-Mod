using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Menu;
using Menu.Remix;
using Menu.Remix.MixedUI;
using Menu.Remix.MixedUI.ValueTypes;
using UnityEngine;
using RWCustom;

namespace StruggleMod;

public class StruggleModOptions : OptionInterface
{  
    //Initialize instance
    public static StruggleModOptions instance;
    
    //initialize slider and config for struggle value
    private OpSlider struggle_slider;
    private OpSlider knockback_slider;

    public static Configurable<int> struggle_configurable;
    public static Configurable<int> knockback_configurable;

    public StruggleModOptions()
    {
        instance = this;
        struggle_configurable = config.Bind("struggle_slider", 25, new ConfigurableInfo("", null, ""));
        knockback_configurable = config.Bind("knockback_slider", 1, new ConfigurableInfo("", null, ""));
    }

    public override void Initialize()
    {
        Debug.Log("Struggle Options menu initialize");
        base.Initialize();
        Tabs = new OpTab[1]
        {
            new OpTab(this, "Overview")
        };

        // Add banner image
        byte[] img_bytes = File.ReadAllBytes(AssetManager.ResolveFilePath("atlasses\\banner.png"));
        Texture2D texture = new Texture2D(0,0);
        texture.filterMode = FilterMode.Point;
        texture.LoadImage(img_bytes);

        OpImage banner = new OpImage(new Vector2(0f,420f), texture);
        OpLabel mod_info = new OpLabel(new Vector2(300f, 397f), new Vector2(1f, 1f), "StruggleMod  -  By Y0z64  -  Version: " + StruggleMod.version, FLabelAlignment.Center, false);

        OpLabel struggle_label = new OpLabel(300f, 330f, "STRUGGLE INPUTS", true);
        struggle_label.label.alignment = FLabelAlignment.Center;
        OpLabel knockback_label = new OpLabel(300f, 230f, "KNOCKBACK MULTIPLIER", true);
        knockback_label.label.alignment = FLabelAlignment.Center;

        struggle_slider = new OpSlider(struggle_configurable, new Vector2(150f, 300f), 3f){
            description = "Number of inputs before you break free."
        };

        knockback_slider = new OpSlider(knockback_configurable, new Vector2(150f, 200f), 3f){
            description = "Multiplier of how much knockback creatures recieve when you break free"
        };

        struggle_slider.min = 0;
        struggle_slider.max = 100;
        knockback_slider.min = 1;
        knockback_slider.max = 20;

        Tabs[0].AddItems(banner, mod_info, struggle_label, struggle_slider, knockback_label, knockback_slider);
    }

    public override void Update()
    {
        //Directly call the base Update() function, since there where no modifications to it
        base.Update();
    }

}